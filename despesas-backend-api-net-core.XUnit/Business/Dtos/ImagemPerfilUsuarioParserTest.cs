namespace Business.Dtos.Parser;
public class ImagemPerfilUsuarioParserTest
{
    [Fact]
    public void Should_Parse_ImagemPerfilUsuarioVM_To_ImagemPerfilUsuario()
    {
        // Arrange
        var parser = new ImagemPerfilUsuarioParser();
        var origin = new ImagemPerfilDto
        {
            Id = 1,
            Name = "example.jpg",
            Type = "jpg",
            Url = "https://example.com/image.jpg",
            IdUsuario = 42
        };

        // Act
        var result = parser.Parse(origin);

        // Assert
        Assert.Equal(origin.Id, result.Id);
        Assert.Equal(origin.Name, result.Name);
        Assert.Equal(origin.Type, result.Type);
        Assert.Equal(origin.Url, result.Url);
        Assert.Equal(origin.IdUsuario, result.UsuarioId);
    }

    [Fact]
    public void Should_Parse_ImagemPerfilUsuario_To_ImagemPerfilUsuarioVM()
    {
        // Arrange
        var parser = new ImagemPerfilUsuarioParser();
        var origin = new ImagemPerfilUsuario
        {
            Id = 1,
            Name = "example.jpg",
            Type = "jpg",
            Url = "https://example.com/image.jpg",
            UsuarioId = 42
        };

        // Act
        var result = parser.Parse(origin);

        // Assert
        Assert.Equal(origin.Id, result.Id);
        Assert.Equal(origin.Name, result.Name);
        Assert.Equal(origin.Type, result.Type);
        Assert.Equal(origin.Url, result.Url);
        Assert.Equal(origin.UsuarioId, result.IdUsuario);
    }

    [Fact]
    public void Should_Parse_List_ImagemPerfilUsuarioVM_To_ImagemPerfilUsuario_List()
    {
        // Arrange
        var parser = new ImagemPerfilUsuarioParser();
        var usuarioVM = UsuarioFaker.Instance.GetNewFakerVM();
        var originList = ImagemPerfilUsuarioFaker.ImagensPerfilUsuarioVMs(
            usuarioVM,
            usuarioVM.Id
        );
        // Act
        var resultList = parser.ParseList(originList);

        // Assert
        Assert.Equal(originList.Count, resultList.Count);
        for (int i = 0; i < originList.Count; i++)
        {
            Assert.Equal(originList[i].Id, resultList[i].Id);
            Assert.Equal(originList[i].Name, resultList[i].Name);
            Assert.Equal(originList[i].Type, resultList[i].Type);
            Assert.Equal(originList[i].Url, resultList[i].Url);
            Assert.Equal(originList[i].IdUsuario, resultList[i].UsuarioId);
        }
    }

    [Fact]
    public void Shhould_Parse_List_ImagemPerfilUsuario_To_ImagemPerfilUsuarioVM_List()
    {
        // Arrange
        var parser = new ImagemPerfilUsuarioParser();
        var usuario = UsuarioFaker.Instance.GetNewFaker();
        var originList = ImagemPerfilUsuarioFaker.ImagensPerfilUsuarios(usuario, usuario.Id);

        // Act
        var resultList = parser.ParseList(originList);

        // Assert
        Assert.Equal(originList.Count, resultList.Count);
        for (int i = 0; i < originList.Count; i++)
        {
            Assert.Equal(originList[i].Id, resultList[i].Id);
            Assert.Equal(originList[i].Name, resultList[i].Name);
            Assert.Equal(originList[i].Type, resultList[i].Type);
            Assert.Equal(originList[i].Url, resultList[i].Url);
            Assert.Equal(originList[i].UsuarioId, resultList[i].IdUsuario);
        }
    }
}
