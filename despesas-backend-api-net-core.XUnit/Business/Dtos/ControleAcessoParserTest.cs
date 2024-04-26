using System.ComponentModel.DataAnnotations;

namespace Business.Dtos.Parser;
public class ControleAcessoParserTest
{
    [Fact]
    public void Should_Parse_ControleAcessoDto_To_ControleAcesso()
    {
        // Arrange
        var controleAcessoParser = new ControleAcessoParser();
        var controleAcessoDto = new ControleAcessoDto
        {
            Id = 1,
            Email = "test@example.com",
            Senha = "password",
            IdUsuario = 1,
            Nome = "Test",
            PerfilUsuario = PerfilUsuario.Administrador,
            Telefone = "123456789",
            SobreNome = "User",
            RefreshToken = "fakeRefreshToken"
        };

        // Act
        var controleAcesso = controleAcessoParser.Parse(controleAcessoDto);

        // Assert
        Assert.NotNull(controleAcesso);
        Assert.Equal(controleAcessoDto.Id, controleAcesso.Id);
        Assert.Equal(controleAcessoDto.Email, controleAcesso.Login);
        Assert.Equal(controleAcessoDto.IdUsuario, controleAcesso.UsuarioId);
        Assert.NotEqual(controleAcessoDto.Senha, controleAcesso.Senha);
        Assert.Equal(controleAcessoDto.RefreshToken, controleAcesso.RefreshToken);
        Assert.NotNull(controleAcesso.Usuario);
        Assert.Equal(controleAcessoDto.IdUsuario, controleAcesso.Usuario.Id);
        Assert.Equal(controleAcessoDto.Nome, controleAcesso.Usuario.Nome);
        Assert.Equal(controleAcessoDto.PerfilUsuario, controleAcesso.Usuario.PerfilUsuario);
        Assert.Equal(controleAcessoDto.Telefone, controleAcesso.Usuario.Telefone);
        Assert.Equal(controleAcessoDto.SobreNome, controleAcesso.Usuario.SobreNome);
    }

    [Fact]
    public void Should_Parse_ControleAcesso_To_ControleAcessoDto()
    {
        // Arrange
        var controleAcessoParser = new ControleAcessoParser();
        var usuario = new Usuario
        {
            Id = 1,
            Nome = "Test",
            PerfilUsuario = PerfilUsuario.Administrador,
            Telefone = "123456789",
            SobreNome = "User",
            Email = "test@example.com"
        };
        var controleAcesso = new ControleAcesso
        {
            Id = 1,
            Login = "test@example.com",
            UsuarioId = 1,
            Senha = "password",
            RefreshToken = "fakeRefreshToken",
            Usuario = usuario
        };

        // Act
        var controleAcessoDto = controleAcessoParser.Parse(controleAcesso);

        // Assert
        Assert.NotNull(controleAcessoDto);
        Assert.Equal(controleAcesso.Id, controleAcessoDto.Id);
        Assert.Equal(controleAcesso.Login, controleAcessoDto.Email);
        Assert.Equal(controleAcesso.UsuarioId, controleAcessoDto.IdUsuario);
        Assert.NotEqual(controleAcesso.Senha, controleAcessoDto.Senha);
        Assert.Equal(controleAcesso.RefreshToken, controleAcessoDto.RefreshToken);
        Assert.Equal(usuario.Id, controleAcessoDto.IdUsuario);
        Assert.Equal(usuario.Nome, controleAcessoDto.Nome);
        Assert.Equal(usuario.PerfilUsuario, controleAcessoDto.PerfilUsuario);
        Assert.Equal(usuario.Telefone, controleAcessoDto.Telefone);
        Assert.Equal(usuario.SobreNome, controleAcessoDto.SobreNome);
    }

    [Fact]
    public void Should_Validate_ControleAcessoDto()
    {
        // Arrange
        var controleAcessoDto = new ControleAcessoDto
        {
            Senha = "password",
            ConfirmaSenha = "password"
        };

        // Act
        var validationResults = controleAcessoDto.Validate(new ValidationContext(controleAcessoDto));

        // Assert
        Assert.Empty(validationResults);
    }


    [Fact]
    public void Should_Validate_ControleAcessoDto_With_Empty_ConfirmaSenha()
    {
        // Arrange
        var controleAcessoDto = new ControleAcessoDto
        {
            Senha = "password",
            ConfirmaSenha = ""
        };

        // Act
        var validationResults = controleAcessoDto.Validate(new ValidationContext(controleAcessoDto));

        // Assert
        Assert.NotEmpty(validationResults);
    }

    [Fact]
    public void Should_Validate_ControleAcessoDto_With_Diferent_Password()
    {
        // Arrange
        var controleAcessoDto = new ControleAcessoDto
        {
            Senha = "password",
            ConfirmaSenha = "diferent"
        };

        // Act
        var validationResults = controleAcessoDto.Validate(new ValidationContext(controleAcessoDto));

        // Assert
        Assert.NotEmpty(validationResults);
    }

}
