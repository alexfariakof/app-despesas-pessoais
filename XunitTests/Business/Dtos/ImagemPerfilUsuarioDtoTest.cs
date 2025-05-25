using Business.Dtos.v1;

namespace Business.Dtos;
public sealed class ImagemPerfilUsuarioDtoTest
{
    [Theory]
    [InlineData("http://localhost:user1", "User 1 ", "jpg", "image/jpg")]
    [InlineData("http://localhost:user2", "User 2 ", "jpeg", "image/jpeg")]
    [InlineData("http://localhost:user3", "User 3 ", "png", "image/png")]
    public void ImagemPerfil_UsuarioDto_Should_Set_Properties_Correctly(string url, string name, string type, string contentType)
    {
        // Arrange and Act
        var id = Guid.NewGuid();
        var idUsuario = Guid.NewGuid();
        var data = DateTime.Now;
        var dataVencimento = DateTime.Now;
        byte[] arquivoData = new byte[] { 1, 2, 3, 4, 5 };
        var imagemPerfilUsuarioDto = new ImagemPerfilDto
        {
            Id = id,
            Url = url,
            Name = name,
            Type = type,
            ContentType = contentType,
            UsuarioId = idUsuario,
            Arquivo = arquivoData
        };

        // Assert
        Assert.Equal(id, imagemPerfilUsuarioDto.Id);
        Assert.Equal(url, imagemPerfilUsuarioDto.Url);
        Assert.Equal(name, imagemPerfilUsuarioDto.Name);
        Assert.Equal(type, imagemPerfilUsuarioDto.Type);
        Assert.Equal(contentType, imagemPerfilUsuarioDto.ContentType);
        Assert.Equal(idUsuario, imagemPerfilUsuarioDto.UsuarioId);
        Assert.Equal(arquivoData, imagemPerfilUsuarioDto.Arquivo);
    }
}