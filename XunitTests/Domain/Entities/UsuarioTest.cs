using Domain.Entities.ValueObjects;

namespace Domain.Entities;
public sealed class UsuarioTest
{
    [Theory]
    [InlineData("Teste Usuario Administrador ", "Teste", "219999-9999", "adm@adm.com", StatusUsuario.Ativo, PerfilUsuario.Perfil.Admin)]
    [InlineData("Teste Usuario Ativo ", "Teste", "219999-9999", "teste1@ativo.com", StatusUsuario.Ativo, PerfilUsuario.Perfil.User)]
    [InlineData("Teste Usuario Inativo", "Teste", "219999-9999", "teste1@teste.com", StatusUsuario.Inativo, PerfilUsuario.Perfil.User)]

    public void Usuario_Should_Set_Properties_Correctly(string nome, string sobreNome, string telefone, string email, StatusUsuario statusUsuario, PerfilUsuario.Perfil perfilUsuario)
    {
        // Arrange and Act
        var id = Guid.NewGuid();
        var usuario = new Usuario
        {
            Id = id,
            Nome = nome,
            SobreNome = sobreNome,
            Telefone = telefone,
            Email = email,
            StatusUsuario = statusUsuario,
            PerfilUsuario = new PerfilUsuario(perfilUsuario)
        };

        // Assert
        Assert.Equal(id, usuario.Id);
        Assert.Equal(nome, usuario.Nome);
        Assert.Equal(sobreNome, usuario.SobreNome);
        Assert.Equal(telefone, usuario.Telefone);
        Assert.Equal(email, usuario.Email);
        Assert.Equal(statusUsuario, usuario.StatusUsuario);
        Assert.Equal(perfilUsuario, usuario.PerfilUsuario);
    }
}