namespace Domain.ViewModel;
public class UsuarioVMTest
{

    [Theory]
    [InlineData(1, "Usuario 1", "Teste Usuario 1" ,"(21) 99999-9999", "user1@user.com")]
    [InlineData(2, "Usuario 2", "Teste Usuario 2", "(21) 99999-9999", "user2@user.com")]
    [InlineData(3, "Usuario 3", "Teste Usuario 3", "(21) 99999-9999", "user3@user.com")]
    public void UsuarioVM_Should_Set_Properties_Correctly(int id, string nome, string sobreNome, string telefone, string email)
    {
        // Arrange and Act

        var usuarioVM = new UsuarioDto
        {
            Id = id,
            Nome = nome,
            SobreNome = sobreNome,
            Telefone = telefone,
            Email = email
        };

        // Assert
        Assert.Equal(id, usuarioVM.Id);
        Assert.Equal(nome, usuarioVM.Nome);
        Assert.Equal(sobreNome, usuarioVM.SobreNome);
        Assert.Equal(telefone, usuarioVM.Telefone);
        Assert.Equal(email, usuarioVM.Email);
    }
}