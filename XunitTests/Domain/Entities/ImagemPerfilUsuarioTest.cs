namespace Domain.Entities;
public sealed class ImagemPerfilUsuarioTest
{
    [Theory]
    [InlineData(1, "http://localhost/usuario1", "usuario 1", "image/jpg", 1)]
    [InlineData(2, "http://localhost/usuario2", "usuario 2", "image/png", 2)]
    [InlineData(3, "http://localhost/usuario3", "usuario 3", "image/jpeg", 3)]
    public void ImagemPerfilUsuario_Should_Set_Properties_Correctly(int id, string url, string name, string type, int usuarioId)
    {
        // Arrange
        var mockUsuario = Mock.Of<Usuario>();

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