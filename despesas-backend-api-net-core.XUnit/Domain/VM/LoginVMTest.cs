namespace Domain.ViewModel
{
    public class LoginVMTest
    {
        [Theory]
        [InlineData("user1@user1.com", "userTeste1", "userTeste1")]
        [InlineData("user2@user2.com", "userTeste2", "userTeste2")]
        [InlineData("user3@user3.com", "userTeste3", "userTeste3")]
        public void LoginVM_Should_Set_Properties_Correctly(string email, string senha, string confirmaSenha)
        {
            // Arrange and Act
            var loginVM = new LoginVM
            {
                Email = email,
                Senha = senha,
                ConfirmaSenha = confirmaSenha
            };

            // Assert
            Assert.Equal(email, loginVM.Email);
            Assert.Equal(senha, loginVM.Senha);
            Assert.Equal(confirmaSenha, loginVM.ConfirmaSenha);
        }
    }
}