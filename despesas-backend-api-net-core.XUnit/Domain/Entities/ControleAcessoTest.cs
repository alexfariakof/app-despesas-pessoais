namespace Test.XUnit.Domain.Entities
{
    public class ControleAcessoTest
    {
        [Theory]
        [InlineData(1, "Teste 1", "Teste password 1 ", 1 , typeof(Usuario))]
        [InlineData(1, "Teste 2", "Teste password 2 ", 2, typeof(Usuario))]
        [InlineData(3, "Teste 3", "Teste password 3 ", 3, typeof(Usuario))]
        public void ControleAcesso_Should_Set_Properties_Correctly(int id, string login, string senha, int usuarioId, Type type)
        {
            var mockUsuario = Mock.Of<Usuario>();

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
            Assert.Equal(senha, controleAcesso.Senha);
            Assert.Equal(usuarioId, controleAcesso.UsuarioId);
            Assert.Equal(mockUsuario, controleAcesso.Usuario);
        }
    }
}