namespace Domain.Entities;
public sealed class ControleAcessoTest
{
    [Theory]
    [InlineData("Teste@teste.com", "Teste password 1 ")]
    [InlineData("Teste2@teste.com", "Teste password 2 ")]
    [InlineData("Teste3@teste.com", "Teste password 3 ")]
    public void ControleAcesso_Should_Set_Properties_Correctly(string login, string senha)
    {
        var mockUsuario = Mock.Of<Usuario>();
        var id = Guid.NewGuid();
        var usuarioId = Guid.NewGuid(); 

        // Arrange and Act
        var controleAcesso = new ControleAcesso
        { 
            Id = id,
            Login = login, 
            Senha = senha,
            UsuarioId = usuarioId,
            Usuario  = mockUsuario              
            
        };
        
        // Assert
        Assert.Equal(id, controleAcesso.Id);
        Assert.Equal(login, controleAcesso.Login);
        Assert.NotNull(controleAcesso.Senha);
        Assert.Equal(usuarioId, controleAcesso.UsuarioId);
        Assert.Equal(mockUsuario, controleAcesso.Usuario);
    }
}