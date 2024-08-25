using Business.Dtos.v1;
using __mock__.v1;

namespace Business.Dtos.Parser;
public sealed class ImagemPerfilUsuarioParserTest
{
    [Fact]
    public void Should_Parse_ImagemPerfilUsuarioDto_To_ImagemPerfilUsuario()
    {
        // Arrange
        var parser = new ImagemPerfilUsuarioParser();
        var origin = new ImagemPerfilDto
        {
            Id = Guid.NewGuid(),
            Name = "example.jpg",
            ContentType = "image/jpg",
            Url = "https://example.com/image.jpg",
            UsuarioId = Guid.NewGuid()
        };

        // Act
        var result = parser.Parse(origin);

        // Assert
        Assert.Equal(origin.Id, result.Id);
        Assert.Equal(origin.Name, result.Name);
        Assert.Equal(origin.ContentType, result.ContentType);
        Assert.Equal(origin.Url, result.Url);
        Assert.Equal(origin.UsuarioId, result.UsuarioId);
    }

    [Fact]
    public void Should_Parse_ImagemPerfilUsuario_To_ImagemPerfilUsuarioDto()
    {
        // Arrange
        var parser = new ImagemPerfilUsuarioParser();
        var origin = new ImagemPerfilUsuario
        {
            Id = Guid.NewGuid(),
            Name = "example.jpg",
            ContentType = "image/jpg",
            Url = "https://example.com/image.jpg",
            UsuarioId = Guid.NewGuid()
        };

        // Act
        var result = parser.Parse(origin);

        // Assert
        Assert.Equal(origin.Id, result.Id);
        Assert.Equal(origin.Name, result.Name);
        Assert.Equal(origin.ContentType, result.ContentType);
        Assert.Equal(origin.Url, result.Url);
        Assert.Equal(origin.UsuarioId, result.UsuarioId);
    }

    [Fact]
    public void Should_Parse_List_ImagemPerfilUsuarioDto_To_ImagemPerfilUsuario_List()
    {
        // Arrange
        var parser = new ImagemPerfilUsuarioParser();
        var usuarioDto = UsuarioFaker.Instance.GetNewFakerVM();
        var originList = ImagemPerfilUsuarioFaker.Instance.ImagensPerfilUsuarioDtos(
            usuarioDto,
            usuarioDto.Id
        );
        // Act
        var resultList = parser.ParseList(originList);

        // Assert
        Assert.Equal(originList.Count, resultList.Count);
        for (int i = 0; i < originList.Count; i++)
        {
            Assert.Equal(originList[i].Id, resultList[i].Id);
            Assert.Equal(originList[i].Name, resultList[i].Name);
            Assert.Equal(originList[i].ContentType, resultList[i].ContentType);
            Assert.Equal(originList[i].Url, resultList[i].Url);
            Assert.Equal(originList[i].UsuarioId, resultList[i].UsuarioId);
        }
    }

    [Fact]
    public void Shhould_Parse_List_ImagemPerfilUsuario_To_ImagemPerfilUsuarioDto_List()
    {
        // Arrange
        var parser = new ImagemPerfilUsuarioParser();
        var usuario = UsuarioFaker.Instance.GetNewFaker();
        var originList = ImagemPerfilUsuarioFaker.Instance.ImagensPerfilUsuarios(usuario, usuario.Id);

        // Act
        var resultList = parser.ParseList(originList);

        // Assert
        Assert.Equal(originList.Count, resultList.Count);
        for (int i = 0; i < originList.Count; i++)
        {
            Assert.Equal(originList[i].Id, resultList[i].Id);
            Assert.Equal(originList[i].Name, resultList[i].Name);
            Assert.Equal(originList[i].ContentType, resultList[i].ContentType);
            Assert.Equal(originList[i].Url, resultList[i].Url);
            Assert.Equal(originList[i].UsuarioId, resultList[i].UsuarioId);
        }
    }
}
