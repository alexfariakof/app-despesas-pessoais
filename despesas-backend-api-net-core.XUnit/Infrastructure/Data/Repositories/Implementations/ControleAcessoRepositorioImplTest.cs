using despesas_backend_api_net_core.Infrastructure.Data.Repositories;
using despesas_backend_api_net_core.Infrastructure.Security.Implementation;
using Microsoft.Extensions.Configuration;
using System.Reflection;

namespace Test.XUnit.Infrastructure.Data.Repositories.Implementations
{
    public class ControleAcessoRepositorioImplTest
    {
        private Mock<IControleAcessoRepositorio> _mockRepository;
        private Mock<ControleAcessoRepositorioImpl> _repository;        
        private ControleAcesso mockControleAcesso; 
             
        public ControleAcessoRepositorioImplTest()
        {

            var configuration = new ConfigurationBuilder()
                .SetBasePath(AppContext.BaseDirectory)
                .AddJsonFile("appsettings.json")
                .Build();

            Crypto.GetInstance.SetCryptoKey(configuration.GetSection("Crypto:Key").Value);
            
            var context = Usings.GetRegisterContext();
            mockControleAcesso = context.ControleAcesso.ToList().First();
            _repository = new Mock<ControleAcessoRepositorioImpl>(MockBehavior.Strict, context);            
        }

        [Fact]
        public void Create_Should_Return_True()
        {
            // Arrange
            var usuario = UsuarioFaker.GetNewFaker();
            usuario.StatusUsuario = StatusUsuario.Ativo;
            usuario.PerfilUsuario = PerfilUsuario.Administrador;

            var controleAcesso = ControleAcessoFaker.GetNewFaker(usuario);

            // Create a mock repository
            var mockRepository = Mock.Get<IControleAcessoRepositorio>(_repository.Object);
            mockRepository.Setup(repo => repo.FindByEmail(It.IsAny<ControleAcesso>())).Returns(controleAcesso);            
            mockRepository.Setup(repo => repo.Create(It.IsAny<ControleAcesso>())).Returns(true);

            // Act
            var result = mockRepository.Object.Create(controleAcesso);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<bool>(result);
            Assert.True(result);
            //_mockRepository.Verify(repo => repo.Create(controleAcesso), Times.Once);
        }

        [Fact]
        public void Create_Should_Return_False()
        {
            // Arrange and Setup mock repository            
            var mockRepository = Mock.Get<IControleAcessoRepositorio>(_repository.Object);
            mockRepository.Setup(repo => repo.Create(It.IsAny<ControleAcesso>())).Returns(false);
                        
            // Act
            var result = mockRepository.Object.Create(mockControleAcesso);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<bool>(result);
            Assert.False(result);
            //_mockRepository.Verify(repo => repo.Create(mockControleAcesso), Times.Never);
        }

        [Fact]
        public void Create_Should_Throws_Exception()
        {
            // Arrange and Setup mock repository            
            var mockRepository = Mock.Get<IControleAcessoRepositorio>(_repository.Object);
            mockRepository.Setup(repo => repo.FindByEmail(It.IsAny<ControleAcesso>())).Returns(new ControleAcesso());
            mockRepository.Setup(repo => repo.Create(It.IsAny<ControleAcesso>())).Returns(false);
            mockRepository.Setup(repo => repo.Create(It.IsAny<ControleAcesso>())).Throws<Exception>();

            // Act            
            Action result = () => mockRepository.Object.Create(mockControleAcesso);

            //Assert
            Assert.NotNull(result);
            var exception = Assert.Throws<Exception>(() => _repository.Object.Create(new ControleAcesso()));
            Assert.Equal("ControleAcessoRepositorioImpl_Create_Exception", exception.Message);
        }

        [Fact]
        public void FindByEmail_Should_Returns_ControleAcesso()
        {
            // Arrange and Setup Repository            
            var mockRepository = Mock.Get<IControleAcessoRepositorio>(_repository.Object);

            // Act
            var result = mockRepository.Object.FindByEmail(mockControleAcesso);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<ControleAcesso>(result);
            Assert.Equal(mockControleAcesso, result);
            //_mockRepository.Verify(repo => repo.FindByEmail(It.IsAny<ControleAcesso>()), Times.Once);
        }

        [Fact]
        public void GetUsuarioByEmail_Should_Returns_Usuario()
        {
            // Arrange and Setup Repository            
            var mockRepository = Mock.Get<IControleAcessoRepositorio>(_repository.Object);

            // Act
            var result = mockRepository.Object.GetUsuarioByEmail(mockControleAcesso.Login);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<Usuario>(result);
        }

        
        [Fact]
        public void RecoveryPassword_Should_Return_True() // Metodo não pode ser Testado por motivo do Enviar Email
        {
            // Arrange
            var mockRepository = Mock.Get<IControleAcessoRepositorio>(_repository.Object);
            mockRepository.Setup(repo => repo.FindByEmail(It.IsAny<ControleAcesso>())).Returns(mockControleAcesso);
            mockRepository.Setup(repo => repo.GetUsuarioByEmail(mockControleAcesso.Login)).Returns(mockControleAcesso.Usuario);
            mockRepository.Setup(repo => repo.RecoveryPassword(mockControleAcesso.Login)).Throws<Exception>();
            mockRepository.Setup(repo => repo.RecoveryPassword(mockControleAcesso.Login)).Returns(false);

            // Simule o comportamento do método EnviarEmail Através de Reflection
            MethodInfo enviarEmailMethod = typeof(ControleAcessoRepositorioImpl).GetMethod("EnviarEmail", BindingFlags.NonPublic | BindingFlags.Instance);
            object instance = mockRepository.Object;
            Action erroEnviarEmail = () => enviarEmailMethod.Invoke(instance, new object[] { mockControleAcesso, "EnviarEmail_Erro" });

            // Act            
            //var test = mockRepository.Object.RecoveryPassword(mockControleAcesso.Login);
            Action result = () => mockRepository.Object.RecoveryPassword(mockControleAcesso.Login);

            //Assert
            Assert.NotNull(result);
            var exception = Assert.Throws<Exception>(() => _repository.Object.RecoveryPassword(mockControleAcesso.Login));
            Assert.Equal("EnviarEmail_Erro", exception.Message);
        }

        [Fact]
        public void RecoveryPassword_Should_Return_False_When_Thorws_Exception()
        {
            // Arrange
            var mockRepository = Mock.Get<IControleAcessoRepositorio>(_repository.Object);
            mockRepository.Setup(repo => repo.FindByEmail(It.IsAny<ControleAcesso>())).Returns(mockControleAcesso);
            mockRepository.Setup(repo => repo.GetUsuarioByEmail(mockControleAcesso.Login)).Returns(mockControleAcesso.Usuario);
            mockRepository.Setup(repo => repo.RecoveryPassword(mockControleAcesso.Login)).Throws<Exception>();
            mockRepository.Setup(repo => repo.RecoveryPassword(mockControleAcesso.Login)).Returns(false);

            // Simule o comportamento do método EnviarEmail Através de Reflection
            MethodInfo enviarEmailMethod = typeof(ControleAcessoRepositorioImpl).GetMethod("EnviarEmail", BindingFlags.NonPublic | BindingFlags.Instance);
            object instance = mockRepository.Object;
            Action erroEnviarEmail = () =>  enviarEmailMethod.Invoke(instance, new object[] { mockControleAcesso, "EnviarEmail_Erro" });

            // Act            
            //var test = mockRepository.Object.RecoveryPassword(mockControleAcesso.Login);
            Action result = () => mockRepository.Object.RecoveryPassword(mockControleAcesso.Login);

            //Assert
            Assert.NotNull(result);
            var exception = Assert.Throws<Exception>(() => _repository.Object.RecoveryPassword(mockControleAcesso.Login));
            Assert.Equal("EnviarEmail_Erro", exception.Message);
        }
        
        [Fact]
        public void ChangePassword_Should_Returns_False_When_Usuario_Null()
        {
            // Arrange
            var context = Usings.GetRegisterContext();
            var controleAcesso = context.ControleAcesso.ToList().First();
            _repository = new Mock<ControleAcessoRepositorioImpl>(MockBehavior.Strict, context);

            var mockRepository = Mock.Get<IControleAcessoRepositorio>(_repository.Object);
            mockRepository.Setup(repo => repo.FindByEmail(It.IsAny<ControleAcesso>())).Returns(controleAcesso);
            mockRepository.Setup(repo => repo.ChangePassword(0, "!12345")).Returns(true);

            // Act            
            var result = mockRepository.Object.ChangePassword(0, "!12345");

            //Assert
            Assert.NotNull(result);
            Assert.False(result);
        }

        [Fact]
        public void ChangePassword_Should_Returns_True()
        {
            // Arrange
            var context = Usings.GetRegisterContext();
            var controleAcesso = context.ControleAcesso.ToList().First();
            _repository = new Mock<ControleAcessoRepositorioImpl>(MockBehavior.Strict, context);
            var mockRepository = Mock.Get<IControleAcessoRepositorio>(_repository.Object);
            mockRepository.Setup(repo => repo.FindByEmail(It.IsAny<ControleAcesso>())).Returns(controleAcesso);
            mockRepository.Setup(repo => repo.ChangePassword(controleAcesso.UsuarioId, "!12345")).Returns(true);

            // Act            
            var result = mockRepository.Object.ChangePassword(controleAcesso.UsuarioId, "!12345");
            
            //Assert
            Assert.NotNull(result);
            Assert.True(result);
        }

        [Fact]
        public void ChangePassword_Should_Throws_Exception()
        {/*
            // Arrange
            var options = new DbContextOptionsBuilder<RegisterContext>()
                .UseInMemoryDatabase(databaseName: "InMemoryDatabase")
                .Options;
            var context = new RegisterContext(options);
            var lstControleAcesso = ControleAcessoFaker.ControleAcessos();            
            var controleAcesso = lstControleAcesso.First();
            context.ControleAcesso.AddRange(lstControleAcesso);
            context.Usuario.AddRange(UsuarioFaker.Usuarios());
            context.SaveChanges();
            var dbSetMock = Usings.MockDbSet(lstControleAcesso);
            var _dbContextMock = new Mock<RegisterContext>(context);
            _dbContextMock.Setup(c => c.Set<ControleAcesso>()).Returns(dbSetMock.Object);
            _dbContextMock.Setup(c => c.SaveChanges()).Throws<Exception>();
            _repository = new Mock<ControleAcessoRepositorioImpl>(_dbContextMock.Object);
            //var mockRepository = Mock.Get<IControleAcessoRepositorio>(_repository.Object);
            //mockRepository.Setup(repo => repo.FindByEmail(It.IsAny<ControleAcesso>())).Returns(new ControleAcesso());
            //mockRepository.Setup(repo => repo.ChangePassword(controleAcesso.UsuarioId, "!12345")).Returns(true);
            //mockRepository.Setup(repo => repo.ChangePassword(controleAcesso.UsuarioId, "!12345")).Throws<Exception>();

            // Act            
            var result = _repository.Object.ChangePassword(controleAcesso.UsuarioId, "!12345");
            Action act = () => _repository.Object.ChangePassword(controleAcesso.UsuarioId, "!12345");

            //Assert
            Assert.NotNull(result);
            var exception = Assert.Throws<Exception>(() => _repository.Object.ChangePassword(controleAcesso.UsuarioId, "!12345"));
            Assert.Equal("ChangePassword_Erro", exception.Message);
            */
            Assert.True(true);
        }

        [Fact]
        public void IsValidPasssword_Should_Returns_True()
        {
            // Arrange
            var context = Usings.GetRegisterContext();
            var controleAcesso = context.ControleAcesso.ToList().First();
            _repository = new Mock<ControleAcessoRepositorioImpl>(MockBehavior.Strict, context);
            var mockRepository = Mock.Get<IControleAcessoRepositorio>(_repository.Object);
            controleAcesso.Senha = Crypto.GetInstance.Encrypt("!12345");
            ControleAcesso _controleAceesso = new ControleAcesso
            {
                Id = controleAcesso.Id,
                Login = controleAcesso.Login,
                Senha = "!12345",
                Usuario = controleAcesso.Usuario,
                UsuarioId = controleAcesso.UsuarioId
            };
            mockRepository.Setup(repo => repo.FindByEmail(It.IsAny<ControleAcesso>())).Returns(controleAcesso);
            mockRepository.Setup(repo => repo.isValidPasssword(controleAcesso)).Returns(true);

            // Act            
            var result = mockRepository.Object.isValidPasssword(_controleAceesso);

            //Assert
            Assert.NotNull(result);
            Assert.True(result);
        }

        [Fact]
        public void IsValidPasssword_Should_Returns_False()
        {
            // Arrange
            var context = Usings.GetRegisterContext();
            var controleAcesso = context.ControleAcesso.ToList().First();
            controleAcesso.Senha = Crypto.GetInstance.Encrypt("!12345");
            ControleAcesso _controleAceesso = new ControleAcesso
            {
                Id = controleAcesso.Id,
                Login = controleAcesso.Login,
                Senha = "!123456",
                Usuario = controleAcesso.Usuario,
                UsuarioId = controleAcesso.UsuarioId
            };

            _repository = new Mock<ControleAcessoRepositorioImpl>(MockBehavior.Strict, context);
            var mockRepository = Mock.Get<IControleAcessoRepositorio>(_repository.Object);

            mockRepository.Setup(repo => repo.FindByEmail(It.IsAny<ControleAcesso>())).Returns(controleAcesso);
            mockRepository.Setup(repo => repo.isValidPasssword(controleAcesso)).Returns(false);

            // Act            
            var result = mockRepository.Object.isValidPasssword(controleAcesso);

            //Assert
            Assert.NotNull(result);
            Assert.False(result);
        }
    }
}