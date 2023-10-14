using despesas_backend_api_net_core.Domain.Entities;
using despesas_backend_api_net_core.Infrastructure.Data.Repositories;
using despesas_backend_api_net_core.Infrastructure.Security.Configuration;
using Microsoft.Extensions.Configuration;
using Moq;

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
    }
}