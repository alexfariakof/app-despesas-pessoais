namespace Business.Dtos.Parser;
using Fakers.v1;
public sealed class UsuarioParserTest
{
    [Fact]
    public void Should_Parse_UsuarioDto_To_Usuario()
    {
        // Arrange
        var parser = new UsuarioParser();
        var origin = UsuarioFaker.Instance.GetNewFakerVM();

        // Act
        var result = parser.Parse(origin);

        // Assert
        Assert.Equal(origin.Id, result.Id);
        Assert.Equal(origin.Email, result.Email);
        Assert.Equal(origin.Nome, result.Nome);
        Assert.Equal(origin.SobreNome, result.SobreNome);
        Assert.Equal(origin.Telefone, result.Telefone);
        Assert.Equal(origin.PerfilUsuario, result.PerfilUsuario);
    }

    [Fact]
    public void Should_Parse_Usuario_To_UsuarioDto()
    {
        // Arrange
        var parser = new UsuarioParser();
        var origin = UsuarioFaker.Instance.GetNewFaker();

        // Act
        var result = parser.Parse(origin);

        // Assert
        Assert.Equal(origin.Id, result.Id);
        Assert.Equal(origin.Email, result.Email);
        Assert.Equal(origin.Nome, result.Nome);
        Assert.Equal(origin.SobreNome, result.SobreNome);
        Assert.Equal(origin.Telefone, result.Telefone);
    }

    [Fact]
    public void Should_ParseList_UsuarioDto_List_To_Usuario_List()
    {
        // Arrange
        var parser = new UsuarioParser();

        var originList = UsuarioFaker.Instance.GetNewFakersUsuarios();

        // Act
        var resultList = parser.ParseList(originList);

        // Assert
        Assert.Equal(originList.Count, resultList.Count);
        for (int i = 0; i < originList.Count; i++)
        {
            Assert.Equal(originList[i].Id, resultList[i].Id);
            Assert.Equal(originList[i].Email, resultList[i].Email);
            Assert.Equal(originList[i].Nome, resultList[i].Nome);
            Assert.Equal(originList[i].SobreNome, resultList[i].SobreNome);
            Assert.Equal(originList[i].Telefone, resultList[i].Telefone);
        }
    }

    [Fact]
    public void Should_ParseList_Usuario_List_To_UsuarioDto_List()
    {
        // Arrange
        var parser = new UsuarioParser();
        var originList = UsuarioFaker.Instance.GetNewFakersUsuariosVMs();

        // Act
        var resultList = parser.ParseList(originList);

        // Assert
        Assert.Equal(originList.Count, resultList.Count);
        for (int i = 0; i < originList.Count; i++)
        {
            Assert.Equal(originList[i].Id, resultList[i].Id);
            Assert.Equal(originList[i].Email, resultList[i].Email);
            Assert.Equal(originList[i].Nome, resultList[i].Nome);
            Assert.Equal(originList[i].SobreNome, resultList[i].SobreNome);
            Assert.Equal(originList[i].Telefone, resultList[i].Telefone);
        }
    }
}
