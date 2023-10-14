namespace Test.XUnit.Domain.VM
{
    public class ControleAcessoVMTest
    {

        [Theory]
        [InlineData("Usuario 1 ", "Teste Usuario 1" , "(21) 99999-9999", "user1@user.com", "senhaUser1", "senhaUser1")]
        [InlineData("Usuario 2 ", "Teste Usuario 2", "(21) 99999-9999", "user2@user.com", "senhaUser2", "senhaUser2")]
        [InlineData("Usuario 3 ", "Teste Usuario 3", "(21) 99999-9999", "user3@user.com", "senhaUser3", "senhaUser3")]
        public void ControleAcessoVM_Should_Set_Properties_Correctly(string nome, string sobreNome, string telefone, string email, string senha, string confirmaSenha)
        {
            // Arrange and Act

            var controleAcessoVM = new ControleAcessoVM
            {
                Nome = nome,
                SobreNome = sobreNome,
                Telefone = telefone,
                Email = email,
                Senha = senha,
                ConfirmaSenha = confirmaSenha
            };        

            // Assert
            Assert.Equal(nome, controleAcessoVM.Nome);
            Assert.Equal(sobreNome, controleAcessoVM.SobreNome);
            Assert.Equal(telefone, controleAcessoVM.Telefone);
            Assert.Equal(email, controleAcessoVM.Email);
            Assert.Equal(senha, controleAcessoVM.Senha);
            Assert.Equal(confirmaSenha, controleAcessoVM.ConfirmaSenha);
        }
    }
}