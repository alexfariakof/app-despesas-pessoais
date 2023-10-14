namespace Test.XUnit.Domain.VM
{
    public class LoginVMTest
    {
        [Theory]
        [InlineData(1, "user1@user1.com", "userTeste1", "userTeste1")]
        [InlineData(2, "user2@user2.com", "userTeste2", "userTeste2")]
        [InlineData(3, "user3@user3.com", "userTeste3", "userTeste3")]
        public void LoginVM_Should_Set_Properties_Correctly(int idUsuario, string email, string senha, string confirmaSenha)
        {
            // Arrange and Act
            var loginVM = new LoginVM
            {
                IdUsuario = idUsuario,
                Email = email,
                Senha = senha,
                ConfirmaSenha = confirmaSenha
            };

            // Assert
            Assert.Equal(idUsuario, loginVM.IdUsuario);
            Assert.Equal(email, loginVM.Email);
            Assert.Equal(senha, loginVM.Senha);
            Assert.Equal(confirmaSenha, loginVM.ConfirmaSenha);
        }
    }
}