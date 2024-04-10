namespace Domain.ViewModel;
public class ImagemPerfilUsuarioVMTest
{
    [Theory]
    [InlineData(1, "http://localhost:user1", "User 1 ", "jpg", "image/jpg", 1)]
    [InlineData(2, "http://localhost:user2", "User 2 ", "jpeg", "image/jpeg", 2)]
    [InlineData(3, "http://localhost:user3", "User 3 ", "png", "image/png", 3)]
    public void ImagemPerfil_UsuarioVM_Should_Set_Properties_Correctly(int id, string url, string name, string type,string contentType, int idUsuario)
    {
        // Arrange and Act
        var data = DateTime.Now;
        var dataVencimento = DateTime.Now;
        byte[] arquivoData = new byte[] { 1, 2, 3, 4, 5 };
        var imagemPerfilUsuarioVM = new ImagemPerfilVM
        {
            Id = id,
            Url = url,
            Name = name,
            Type = type,
            ContentType = contentType,
            IdUsuario = idUsuario,                
            Arquivo = arquivoData
        };
        
        // Assert
        Assert.Equal(id, imagemPerfilUsuarioVM.Id);
        Assert.Equal(url, imagemPerfilUsuarioVM.Url);
        Assert.Equal(name, imagemPerfilUsuarioVM.Name);
        Assert.Equal(type, imagemPerfilUsuarioVM.Type);
        Assert.Equal(contentType, imagemPerfilUsuarioVM.ContentType);
        Assert.Equal(idUsuario, imagemPerfilUsuarioVM.IdUsuario);
        Assert.Equal(arquivoData, imagemPerfilUsuarioVM.Arquivo);
    }
}