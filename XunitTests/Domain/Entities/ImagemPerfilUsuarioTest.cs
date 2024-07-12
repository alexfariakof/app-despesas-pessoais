namespace Domain.Entities;
public sealed class ImagemPerfilUsuarioTest
{
    [Theory]
    [InlineData("http://localhost/usuario1", "usuario 1", "image/jpg")]
    [InlineData("http://localhost/usuario2", "usuario 2", "image/png")]
    [InlineData("http://localhost/usuario3", "usuario 3", "image/jpeg")]
    public void ImagemPerfilUsuario_Should_Set_Properties_Correctly(string url, string name, string type)
    {
        // Arrange
        var mockUsuario = Mock.Of<Usuario>();
        var id = Guid.NewGuid();
        var usuarioId = Guid.NewGuid();

        // Act
        var imagemPerfilUsuario = new ImagemPerfilUsuario
        { 
            Id = id,
            Url = url,
            Name = name,
            ContentType = type,
            UsuarioId = usuarioId,                
            Usuario  = mockUsuario                
        };
        
        // Assert
        Assert.Equal(id, imagemPerfilUsuario.Id);
        Assert.Equal(url, imagemPerfilUsuario.Url);
        Assert.Equal(name, imagemPerfilUsuario.Name);
        Assert.Equal(type, imagemPerfilUsuario.ContentType);
        Assert.Equal(usuarioId, imagemPerfilUsuario.UsuarioId);
        Assert.Equal(mockUsuario, imagemPerfilUsuario.Usuario);
    }
}