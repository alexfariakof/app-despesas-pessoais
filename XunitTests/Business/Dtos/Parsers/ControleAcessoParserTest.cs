using Business.Dtos.v1;
using Domain.Entities.ValueObjects;

namespace Business.Dtos.Parser;
public sealed class ControleAcessoParserTest
{
    [Fact]
    public void Should_Parse_ControleAcessoDto_To_ControleAcesso()
    {
        // Arrange
        var controleAcessoParser = new ControleAcessoParser();
        var controleAcessoDto = new ControleAcessoDto
        {
            Id = Guid.NewGuid(),
            Email = "test@example.com",
            Senha = "password",
            Nome = "Test",
            Telefone = "123456789",
            SobreNome = "User",
        };

        // Act
        var controleAcesso = controleAcessoParser.Parse(controleAcessoDto);

        // Assert
        Assert.NotNull(controleAcesso);
        Assert.Equal(controleAcessoDto.Id, controleAcesso.Id);
        Assert.Equal(controleAcessoDto.Email, controleAcesso.Login);
        Assert.Equal(controleAcessoDto.Senha, controleAcesso.Senha);
        Assert.NotNull(controleAcesso.Usuario);
        Assert.Equal(controleAcessoDto.Nome, controleAcesso.Usuario.Nome);
        Assert.Equal(controleAcessoDto.Telefone, controleAcesso.Usuario.Telefone);
        Assert.Equal(controleAcessoDto.SobreNome, controleAcesso.Usuario.SobreNome);
    }

    [Fact]
    public void Should_Parse_ControleAcesso_To_ControleAcessoDto()
    {
        // Arrange
        var controleAcessoParser = new ControleAcessoParser();
        var idUsuario = Guid.NewGuid();
        var usuario = new Usuario
        {
            Id = idUsuario,
            Nome = "Test",
            PerfilUsuario = new PerfilUsuario(PerfilUsuario.Perfil.Admin),
            Telefone = "123456789",
            SobreNome = "User",
            Email = "test@example.com"
        };
        var controleAcesso = new ControleAcesso
        {
            Id = Guid.NewGuid(),
            Login = "test@example.com",
            UsuarioId = idUsuario,
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
        Assert.Equal(controleAcesso.UsuarioId, controleAcessoDto.UsuarioId);
        Assert.Equal(controleAcesso.Senha, controleAcessoDto.Senha);
        Assert.Equal(usuario.Id, controleAcessoDto.UsuarioId);
        Assert.Equal(usuario.Nome, controleAcessoDto.Nome);
        Assert.Equal(usuario.Telefone, controleAcessoDto.Telefone);
        Assert.Equal(usuario.SobreNome, controleAcessoDto.SobreNome);
    }

}
