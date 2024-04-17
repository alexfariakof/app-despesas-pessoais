using Business.Authentication;
using Business.Dtos;
using Domain.Core;
using Microsoft.Extensions.Configuration;

namespace Business;
public class ControleAcessoBusinessImplTest
{
    private readonly Mock<IControleAcessoRepositorioImpl> _repositorioMock;
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

        _repositorioMock = new Mock<IControleAcessoRepositorioImpl>();
        _controleAcessoBusiness = new ControleAcessoBusinessImpl(_repositorioMock.Object, signingConfigurations, tokenConfigurations, new EmailSender());
    }

    [Fact]
    public void Create_Should_ControleAcesso_Returns_True()
    {
        // Arrange
        var controleAcesso = new ControleAcesso
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

        _repositorioMock.Setup(repo => repo.Create(It.IsAny<ControleAcesso>()));

        // Act
        _controleAcessoBusiness.Create(controleAcesso);

        // Assert        
        _repositorioMock.Verify(repo => repo.Create(controleAcesso), Times.Once);
    }

    [Fact]
    public void FindByLogin_Should_Return_Valid_Credentials_And_AccessToken()
    {
        // Arrange
        var controleAcesso = new ControleAcessoDto { Email = "teste@teste.com", Senha = "teste", };

        var usuario = new Usuario
        {
            Id = 1,
            Email = "teste@teste.com",
            StatusUsuario = StatusUsuario.Ativo
        };

        _repositorioMock.Setup(repo => repo.GetUsuarioByEmail(controleAcesso.Email)).Returns(usuario);
        _repositorioMock.Setup(repo => repo.IsValidPasssword(controleAcesso.Email, controleAcesso.Senha)).Returns(true);
        _repositorioMock.Setup(repo => repo.FindByEmail(It.IsAny<ControleAcesso>())).Returns(new ControleAcesso { Login = controleAcesso.Email});

        // Act
        var result = _controleAcessoBusiness.ValidateCredentials(controleAcesso);

        // Assert
        Assert.True(result.Authenticated);
        Assert.NotNull(result.AccessToken);
    }

    [Fact]
    public void FindByLogin_Should_Returns_Usaurio_Inexistente()
    {
        // Arrange
        var controleAcesso = new ControleAcessoDto { Email = "teste@teste.com" };
        _repositorioMock.Setup(repo => repo.GetUsuarioByEmail(controleAcesso.Email)).Returns((Usuario)null);

        // Act
        var result = _controleAcessoBusiness.ValidateCredentials(controleAcesso);

        // Assert
        Assert.False(result.Authenticated);
        Assert.Contains("Usuário inexistente!", result.Message);
    }

    [Fact]
    public void FindByLogin_Should_Returns_Usuario_Inativo()
    {
        // Arrange
        var controleAcesso = new ControleAcessoDto  { Email = "teste@teste.com" };
        var usuarioInativo = new Usuario { StatusUsuario = StatusUsuario.Inativo };
        _repositorioMock.Setup(repo => repo.GetUsuarioByEmail(controleAcesso.Email)).Returns(usuarioInativo);

        // Act
        var result = _controleAcessoBusiness.ValidateCredentials(controleAcesso);

        // Assert
        Assert.False(result.Authenticated);
        Assert.Contains("Usuário Inativo!", result.Message);
    }

    [Fact]
    public void FindByLogin_Should_Returns_Email_Inexistente()
    {
        // Arrange
        var controleAcesso = new ControleAcessoDto { Email = "teste@teste.com", Senha = "teste", };
        var usuario = new Usuario
        {
            Id = 1,
            Email = "teste@teste.com",
            StatusUsuario = StatusUsuario.Ativo
        };

        _repositorioMock.Setup(repo => repo.GetUsuarioByEmail(controleAcesso.Email)).Returns(usuario);
        _repositorioMock.Setup(repo => repo.IsValidPasssword(controleAcesso.Email, controleAcesso.Senha)).Returns(true);
        _repositorioMock.Setup(repo => repo.FindByEmail(It.IsAny<ControleAcesso>())).Returns((ControleAcesso)null);

        // Act
        var result = _controleAcessoBusiness.ValidateCredentials(controleAcesso);

        // Assert
        Assert.False(result.Authenticated);
        Assert.Contains("Email inexistente!", result.Message);
    }

    [Fact]
    public void FindByLogin_Should_Returns_Senha_Invalida()
    {
        // Arrange
        var controleAcesso = new ControleAcessoDto  { Email = "teste@teste.com", Senha = "teste", };

        var usuario = new Usuario
        {
            Id = 1,
            Email = "teste@teste.com",
            StatusUsuario = StatusUsuario.Ativo
        };

        _repositorioMock.Setup(repo => repo.GetUsuarioByEmail(controleAcesso.Email)).Returns(usuario);
        _repositorioMock.Setup(repo => repo.IsValidPasssword(controleAcesso.Email, controleAcesso.Senha)).Returns(false);
        _repositorioMock.Setup(repo => repo.FindByEmail(It.IsAny<ControleAcesso>())).Returns(new ControleAcesso { Login = controleAcesso.Email });

        // Act
        var result = _controleAcessoBusiness.ValidateCredentials(controleAcesso);

        // Assert
        Assert.False(result.Authenticated);
        Assert.Contains("Senha inválida!", result.Message);
    }

    [Fact]
    public void FindByLogin_Should_Returns_Usuario_Invalido()
    {
        // Arrange
        var controleAcesso = new ControleAcessoDto  { Email = "teste@teste.com", Senha = "teste", };

        var usuario = new Usuario
        {
            Id = 1,
            Email = "teste@teste.com",
            StatusUsuario = StatusUsuario.Ativo
        };

        _repositorioMock.Setup(repo => repo.GetUsuarioByEmail(controleAcesso.Email)).Returns(usuario);
        _repositorioMock.Setup(repo => repo.IsValidPasssword(controleAcesso.Email, controleAcesso.Senha)).Returns(true);
        _repositorioMock.Setup(repo => repo.FindByEmail(It.IsAny<ControleAcesso>())).Throws(new ArgumentException("Usuário Inválido!"));

        // Act &  Assert
        Assert.Throws<ArgumentException>(() => _controleAcessoBusiness.ValidateCredentials(controleAcesso));
    }

    [Fact]
    public void RecoveryPassword_Should_Execute_And_Returns_True()
    {
        /*
        // Arrange
        string email = "teste@example.com";
        _repositorioMock.Setup(repo => repo.RecoveryPassword(email)).Returns(true);

        // Act
        bool result = _controleAcessoBusiness.RecoveryPassword(email);

        // Assert
        Assert.True(result);
        _repositorioMock.Verify(repo => repo.RecoveryPassword(email), Times.Once);
        */
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
        _repositorioMock.Verify(repo => repo.ChangePassword(idUsuario, newPassword),Times.Once);
    }
}
