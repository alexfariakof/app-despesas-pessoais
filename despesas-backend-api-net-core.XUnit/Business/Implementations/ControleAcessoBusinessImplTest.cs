using despesas_backend_api_net_core.Business.Implementations;
using despesas_backend_api_net_core.Infrastructure.Data.Repositories;
using despesas_backend_api_net_core.Infrastructure.Security.Configuration;
using Microsoft.Extensions.Configuration;

namespace Test.XUnit.Business.Implementations
{
    public class ControleAcessoBusinessImplTest
    {
        private readonly Mock<IControleAcessoRepositorio> _repositorioMock;
        private readonly ControleAcessoBusinessImpl _controleAcessoBusiness;

        public ControleAcessoBusinessImplTest()
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(AppContext.BaseDirectory)
                .AddJsonFile("appsettings.json")
                .Build();

            var signingConfigurations = new SigningConfigurations();
            configuration.GetSection("TokenConfigurations").Bind(signingConfigurations);

            var tokenConfigurations = new TokenConfiguration();
            configuration.GetSection("TokenConfigurations").Bind(tokenConfigurations);


            _repositorioMock = new Mock<IControleAcessoRepositorio>();
            _controleAcessoBusiness = new ControleAcessoBusinessImpl(_repositorioMock.Object, signingConfigurations, tokenConfigurations);
        }

        [Fact]
        public void Create_ControleAcesso_Returns_True()
        {
            // Arrange
            var controleAcesso= new ControleAcesso
            {
                Login = "teste@teste.com",
                Senha = "teste",
                Usuario = new Usuario
                {
                    Email = "teste@teste.com",
                    Nome = "Teste Usuário 1",
                    SobreNome = "Teste",
                    Telefone = "(21) 9999-9999",
                    PerfilUsuario = PerfilUsuario.Usuario                    
                }
            };

            _repositorioMock.Setup(repo => repo.Create(It.IsAny<ControleAcesso>())).Returns(true);

            // Act
            var result = _controleAcessoBusiness.Create(controleAcesso);

            // Assert
            Assert.True(result);
            _repositorioMock.Verify(repo => repo.Create(controleAcesso), Times.Once);
        }

        [Fact]
        public void FindByLogin_Should_Return_ValidCredentials_And_AccessToken()
        {
            // Arrange
            var controleAcesso = new ControleAcesso
            {
                Login = "teste@teste.com",
                Senha = "teste",
                
            };

            var usuario = new Usuario
            {
                Id = 1,
                Email = "teste@teste.com",
                StatusUsuario = StatusUsuario.Ativo
            };


            _repositorioMock.Setup(repo => repo.GetUsuarioByEmail(controleAcesso.Login)).Returns(usuario);
            _repositorioMock.Setup(repo => repo.isValidPasssword(controleAcesso)).Returns(true);
            _repositorioMock.Setup(repo => repo.FindByEmail(controleAcesso)).Returns(controleAcesso);            
            
            // Act
            var result = _controleAcessoBusiness.FindByLogin(controleAcesso);

            // Assert
            Assert.True(result.Authenticated);
            Assert.NotNull(result.AccessToken);
        }

        [Fact]
        public void FindByLogin_Should_Returns_UsaurioInexistente()
        {
            // Arrange
            var controleAcesso = new ControleAcesso { Login = "teste@teste.com" };
            _repositorioMock.Setup(repo => repo.GetUsuarioByEmail(controleAcesso.Login)).Returns((Usuario)null);

            // Act
            var result = _controleAcessoBusiness.FindByLogin(controleAcesso);

            // Assert
            Assert.False(result.Authenticated);
            Assert.Contains("Usuário inexistente!", result.Message);
        }

        [Fact]
        public void FindByLogin_ShouldReturnsUsuarioInativo()
        {
            // Arrange
            var controleAcesso = new ControleAcesso { Login = "teste@teste.com" };
            var usuarioInativo = new Usuario { StatusUsuario = StatusUsuario.Inativo };

            _repositorioMock.Setup(repo => repo.GetUsuarioByEmail(controleAcesso.Login)).Returns(usuarioInativo);

            // Act
            var result = _controleAcessoBusiness.FindByLogin(controleAcesso);

            // Assert
            Assert.False(result.Authenticated);
            Assert.Contains("Usuário Inativo!", result.Message);
        }

        [Fact]
        public void FindByLogin_Should_Returns_EmailInexistente()
        {
            // Arrange
            var controleAcesso = new ControleAcesso
            {
                Login = "teste@teste.com",
                Senha = "teste",

            };

            var usuario = new Usuario
            {
                Id = 1,
                Email = "teste@teste.com",
                StatusUsuario = StatusUsuario.Ativo
            };


            _repositorioMock.Setup(repo => repo.GetUsuarioByEmail(controleAcesso.Login)).Returns(usuario);
            _repositorioMock.Setup(repo => repo.isValidPasssword(controleAcesso)).Returns(true);
            _repositorioMock.Setup(repo => repo.FindByEmail(controleAcesso)).Returns((ControleAcesso)null);

            // Act
            var result = _controleAcessoBusiness.FindByLogin(controleAcesso);

            // Assert
            Assert.False(result.Authenticated);
            Assert.Contains("Email inexistente!", result.Message);
        }
        
        [Fact]
        public void FindByLogin_Should_Returns_SenahInvalida()
        {
            // Arrange
            var controleAcesso = new ControleAcesso
            {
                Login = "teste@teste.com",
                Senha = "teste",

            };

            var usuario = new Usuario
            {
                Id = 1,
                Email = "teste@teste.com",
                StatusUsuario = StatusUsuario.Ativo
            };


            _repositorioMock.Setup(repo => repo.GetUsuarioByEmail(controleAcesso.Login)).Returns(usuario);
            _repositorioMock.Setup(repo => repo.isValidPasssword(controleAcesso)).Returns(false);
            _repositorioMock.Setup(repo => repo.FindByEmail(controleAcesso)).Returns(controleAcesso);

            // Act
            var result = _controleAcessoBusiness.FindByLogin(controleAcesso);

            // Assert
            Assert.False(result.Authenticated);
            Assert.Contains("Senha inválida!", result.Message);
        }

        [Fact]
        public void FindByLogin_Should_Returns_UsuarioInvalido()
        {
            // Arrange
            var controleAcesso = new ControleAcesso
            {
                Login = "",
                Senha = "teste",

            };

            var usuario = new Usuario
            {
                Id = 1,
                Email = "teste@teste.com",
                StatusUsuario = StatusUsuario.Ativo
            };


            _repositorioMock.Setup(repo => repo.GetUsuarioByEmail(controleAcesso.Login)).Returns(usuario);
            _repositorioMock.Setup(repo => repo.isValidPasssword(controleAcesso)).Returns(true);
            _repositorioMock.Setup(repo => repo.FindByEmail(controleAcesso)).Returns(controleAcesso);

            // Act
            var result = _controleAcessoBusiness.FindByLogin(controleAcesso);

            // Assert
            Assert.False(result.Authenticated);
            Assert.Contains("Usuário Inválido!", result.Message);
        }

        [Fact]
        public void RecoveryPassword_Should_Execute_And_Returns_True()
        {
            // Arrange
            string email = "teste@example.com";

            _repositorioMock.Setup(repo => repo.RecoveryPassword(email)).Returns(true);

            // Act
            bool result = _controleAcessoBusiness.RecoveryPassword(email);

            // Assert
            Assert.True(result);
            _repositorioMock.Verify(repo => repo.RecoveryPassword(email), Times.Once);
        }

        [Fact]
        public void ChangePassword_Should_Execute_And_Returns_True()
        {
            // Arrange
            int idUsuario = 1;
            string newPassword = "123456789";

            _repositorioMock.Setup(repo => repo.ChangePassword(idUsuario, newPassword)).Returns(true);

            // Act
            bool result = _controleAcessoBusiness.ChangePassword(idUsuario, newPassword);

            // Assert
            Assert.True(result);
            _repositorioMock.Verify(repo => repo.ChangePassword(idUsuario, newPassword), Times.Once);
        }
    }
}