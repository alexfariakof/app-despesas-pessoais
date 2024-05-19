using Business.Dtos.v1;

namespace Business.Dtos;
public class ControleAcessoDtoTest
{

    [Theory]
    [InlineData("Usuario 1 ", "Teste Usuario 1" , "(21) 99999-9999", "user1@user.com", "senhaUser1", "senhaUser1")]
    [InlineData("Usuario 2 ", "Teste Usuario 2", "(21) 99999-9999", "user2@user.com", "senhaUser2", "senhaUser2")]
    [InlineData("Usuario 3 ", "Teste Usuario 3", "(21) 99999-9999", "user3@user.com", "senhaUser3", "senhaUser3")]
    public void ControleAcessoDto_Should_Set_Properties_Correctly(string nome, string sobreNome, string telefone, string email, string senha, string confirmaSenha)
    {
        // Arrange and Act
        var id = new Random().Next();

        var controleAcessoDto = new ControleAcessoDto
        {
            Id = id,
            Nome = nome,
            SobreNome = sobreNome,
            Telefone = telefone,
            Email = email,
            Senha = senha,
            ConfirmaSenha = confirmaSenha
        };

        // Assert
        Assert.Equal(id, controleAcessoDto.Id);
        Assert.Equal(nome, controleAcessoDto.Nome);
        Assert.Equal(sobreNome, controleAcessoDto.SobreNome);
        Assert.Equal(telefone, controleAcessoDto.Telefone);
        Assert.Equal(email, controleAcessoDto.Email);
        Assert.Equal(senha, controleAcessoDto.Senha);
        Assert.Equal(confirmaSenha, controleAcessoDto.ConfirmaSenha);
    }
}