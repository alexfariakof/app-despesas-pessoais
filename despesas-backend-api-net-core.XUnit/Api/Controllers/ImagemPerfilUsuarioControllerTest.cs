using Business.Abstractions;
using Business.Dtos.Parser;
using despesas_backend_api_net_core.Controllers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Text;

namespace Api.Controllers;

public class ImagemPerfilUsuarioControllerTest
{
    protected Mock<IUsuarioBusiness> _mockUsuarioBusiness;
    protected Mock<IImagemPerfilUsuarioBusiness> _mockImagemPerfilBusiness;
    protected UsuarioController _usuarioController;
    protected List<UsuarioDto>? _usuarioVMs;

    private void SetupBearerToken(int idUsuario)
    {
        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, idUsuario.ToString())
        };
        var identity = new ClaimsIdentity(claims, "IdUsuario");
        var claimsPrincipal = new ClaimsPrincipal(identity);
        var httpContext = new DefaultHttpContext { User = claimsPrincipal };
        httpContext.Request.Headers["Authorization"] = "Bearer " + Usings.GenerateJwtToken(idUsuario);
        _usuarioController.ControllerContext = new ControllerContext { HttpContext = httpContext };
    }

    public ImagemPerfilUsuarioControllerTest()
    {
        _mockUsuarioBusiness = new Mock<IUsuarioBusiness>();
        _mockImagemPerfilBusiness = new Mock<IImagemPerfilUsuarioBusiness>();
        _usuarioController = new UsuarioController(_mockUsuarioBusiness.Object, _mockImagemPerfilBusiness.Object);
    }

    [Fact]
    public void GetImage_Should_Returns_OkResults_With_ImagemPerfilUsuario()
    {
        // Arrange
        var _imagemPerfilUsuarios = ImagemPerfilUsuarioFaker.ImagensPerfilUsuarios();
        var _imagemPerfilUsuarioVMs = new ImagemPerfilUsuarioParser().ParseList(_imagemPerfilUsuarios);
        var usuarioVM = new UsuarioParser().Parse(_imagemPerfilUsuarios.First().Usuario);
        int idUsuario = usuarioVM.Id;
        SetupBearerToken(idUsuario);
        _mockImagemPerfilBusiness.Setup(business => business.FindAll(idUsuario)).Returns(_imagemPerfilUsuarioVMs);

        // Act
        var result = _usuarioController.GetImage() as ObjectResult;
        // Assert
        Assert.NotNull(result);
        Assert.IsType<OkObjectResult>(result);
        var value = result.Value;
        var message = (bool)(value?.GetType()?.GetProperty("message")?.GetValue(value, null) ?? false);
        Assert.True(message);
        var _imagemPerfilUsuario = value?.GetType()?.GetProperty("imagemPerfilUsuario")?.GetValue(value, null) as ImagemPerfilDto;
        Assert.NotNull(_imagemPerfilUsuario);
        Assert.IsType<ImagemPerfilDto>(_imagemPerfilUsuario);
        _mockImagemPerfilBusiness.Verify(b => b.FindAll(idUsuario), Times.Once);
    }

    [Fact]
    public void Get_Should_Returns_BadRequest_When_ImagemPerfilUsuario_NotFound()
    {
        // Arrange
        var _imagemPerfilUsuarios = ImagemPerfilUsuarioFaker.ImagensPerfilUsuarios();
        var _imagemPerfilUsuarioVMs = new ImagemPerfilUsuarioParser().ParseList(_imagemPerfilUsuarios);
        var usuarioVM = new UsuarioParser().Parse(_imagemPerfilUsuarios.First().Usuario);
        int idUsuario = 987654;
        SetupBearerToken(idUsuario);
        _mockImagemPerfilBusiness.Setup(business => business.FindAll(idUsuario)).Returns(_imagemPerfilUsuarioVMs);

        // Act
        var result = _usuarioController.GetImage() as ObjectResult;

        // Assert
        Assert.NotNull(result);
        Assert.IsType<BadRequestObjectResult>(result);
        var value = result.Value;
        var message = value?.GetType()?.GetProperty("message")?.GetValue(value, null) as string;
        Assert.Equal("Usuário não possui nenhuma imagem de perfil cadastrada!", message);
        _mockImagemPerfilBusiness.Verify(b => b.FindAll(idUsuario), Times.Once);
    }

    [Fact]
    public async void Post_Should_Create_And_Returns_OkResult_For_ImagesTypes_JPG_PNG_JPEG()
    {
        // Arrange
        var _imagemPerfilUsuarios = ImagemPerfilUsuarioFaker.ImagensPerfilUsuarios();
        var _imagemPerfilUsuarioVMs = new ImagemPerfilUsuarioParser().ParseList(_imagemPerfilUsuarios);
        var imagemPerfilUsuarioVM = _imagemPerfilUsuarioVMs.First();
        int idUsuario = imagemPerfilUsuarioVM.IdUsuario;

        SetupBearerToken(idUsuario);
        _mockImagemPerfilBusiness.Setup(business => business.Create(It.IsAny<ImagemPerfilDto>())).Returns(imagemPerfilUsuarioVM);

        var formFile = new FormFile(new MemoryStream(Encoding.UTF8.GetBytes("Test file content")), 0, Encoding.UTF8.GetBytes("Test file content").Length, "test", "test.jpg");
        formFile.Headers = new HeaderDictionary { { "Content-Type", "image/jpg" } };

        // Act
        var result = await _usuarioController.PostImagemPerfil(formFile) as ObjectResult;

        // Assert
        Assert.NotNull(result);
        Assert.IsType<OkObjectResult>(result);
        var value = result.Value;
        var message = (bool)(value?.GetType()?.GetProperty("message")?.GetValue(value, null) ?? false);
        Assert.True(message);
        var imagemPerfilUsuario = value?.GetType()?.GetProperty("imagemPerfilUsuario")?.GetValue(value, null) as ImagemPerfilDto;
        Assert.NotNull(imagemPerfilUsuario);
        Assert.IsType<ImagemPerfilDto>(imagemPerfilUsuario);
        _mockImagemPerfilBusiness.Verify(b => b.Create(It.IsAny<ImagemPerfilDto>()), Times.Once);

        // Arrage file type PNG
        formFile = new FormFile(new MemoryStream(Encoding.UTF8.GetBytes("Test file content png")), 0, Encoding.UTF8.GetBytes("Test file content png").Length, "Test File PNG", "test.png" );
        formFile.Headers = new HeaderDictionary { { "Content-Type", "image/png" } };

        // Act file type PNG
        result = await _usuarioController.PostImagemPerfil(formFile) as ObjectResult;

        // Assert file Type PNG
        Assert.NotNull(result);
        Assert.IsType<OkObjectResult>(result);
        value = result.Value;
        message = (bool)(value?.GetType()?.GetProperty("message")?.GetValue(value, null) ?? false);
        Assert.True(message);
        imagemPerfilUsuario = value?.GetType()?.GetProperty("imagemPerfilUsuario")?.GetValue(value, null) as ImagemPerfilDto;
        Assert.NotNull(imagemPerfilUsuario);
        Assert.IsType<ImagemPerfilDto>(imagemPerfilUsuario);
        _mockImagemPerfilBusiness.Verify( b => b.Create(It.IsAny<ImagemPerfilDto>()), Times.Exactly(2));

        // Arrage file type JPEG
        formFile = new FormFile(new MemoryStream(Encoding.UTF8.GetBytes("Test file contentjpeg")), 0, Encoding.UTF8.GetBytes("Test file content jpeg").Length, "Test File JPEG", "test.jpeg");
        formFile.Headers = new HeaderDictionary { { "Content-Type", "image/jpeg" } };

        // Act file type JPEG
        result = await _usuarioController.PostImagemPerfil(formFile) as ObjectResult;

        // Assert file Type JPEG
        Assert.NotNull(result);
        Assert.IsType<OkObjectResult>(result);
        value = result.Value;

        message = (bool)(value?.GetType()?.GetProperty("message")?.GetValue(value, null) ?? false);
        Assert.True(message);
        imagemPerfilUsuario = value?.GetType()?.GetProperty("imagemPerfilUsuario")?.GetValue(value, null) as ImagemPerfilDto;
        Assert.NotNull(imagemPerfilUsuario);
        Assert.IsType<ImagemPerfilDto>(imagemPerfilUsuario);
        _mockImagemPerfilBusiness.Verify(b => b.Create(It.IsAny<ImagemPerfilDto>()), Times.Exactly(3));
    }

    [Fact]
    public async void Post_Should_Returns_BadRequest_For_Invalid_Images_Type()
    {
        // Arrange
        var imagemPerfilUsuarioVM = ImagemPerfilUsuarioFaker.ImagensPerfilUsuarioVMs().First();
        int idUsuario = imagemPerfilUsuarioVM.IdUsuario;
        SetupBearerToken(idUsuario);
        _mockImagemPerfilBusiness.Setup(business => business.Create(It.IsAny<ImagemPerfilDto>())).Returns(imagemPerfilUsuarioVM);

        var formFile = new FormFile(new MemoryStream(Encoding.UTF8.GetBytes("Test file not Image type content")), 0, Encoding.UTF8.GetBytes("Test file not Image type content").Length, "DATA File Erro", "test.txt");
        formFile.Headers = new HeaderDictionary { { "Content-Type", "image/txt" } };

        // Act
        var result = await _usuarioController.PostImagemPerfil(formFile) as ObjectResult;

        // Assert
        Assert.NotNull(result);
        Assert.IsType<BadRequestObjectResult>(result);
        var value = result.Value;
        var message = value?.GetType()?.GetProperty("message")?.GetValue(value, null) as string;
        Assert.Equal("Apenas arquivos do tipo jpg, jpeg ou png são aceitos.", message);
        _mockImagemPerfilBusiness.Verify(b => b.Create(It.IsAny<ImagemPerfilDto>()), Times.Never);
    }

    [Fact]
    public async void Post_Should_Try_Create_And_Returns_BadRequest()
    {
        // Arrange
        var imagemPerfilUsuarioVM = ImagemPerfilUsuarioFaker.ImagensPerfilUsuarioVMs().First();
        int idUsuario = imagemPerfilUsuarioVM.IdUsuario;
        SetupBearerToken(idUsuario);

        _mockImagemPerfilBusiness.Setup(business => business.Create(It.IsAny<ImagemPerfilDto>())).Returns((ImagemPerfilDto)null);

        var formFile = new FormFile(new MemoryStream(Encoding.UTF8.GetBytes("Test file content")), 0, Encoding.UTF8.GetBytes("Test file content").Length, "test", "test.jpg");
        formFile.Headers = new HeaderDictionary { { "Content-Type", "image/jpg" } };

        // Act
        var result = await _usuarioController.PostImagemPerfil(formFile) as ObjectResult;

        // Assert
        Assert.NotNull(result);
        Assert.IsType<BadRequestObjectResult>(result);
        var value = result.Value;
        value = result.Value;
        var message = (bool)(value?.GetType()?.GetProperty("message")?.GetValue(value, null) ?? false);
        Assert.False(message);
        var imagemPerfilUsuario = value?.GetType()?.GetProperty("imagemPerfilUsuario")?.GetValue(value, null) as ImagemPerfilDto;
        Assert.Null(imagemPerfilUsuario);
        _mockImagemPerfilBusiness.Verify(b => b.Create(It.IsAny<ImagemPerfilDto>()), Times.Once);
    }

    [Fact]
    public async void Post_Throws_Erro_And_Returns_BadRequest()
    {
        // Arrange
        var idUsuario = 1;
        SetupBearerToken(idUsuario);
        _mockImagemPerfilBusiness.Setup(business => business.Create(It.IsAny<ImagemPerfilDto>())).Returns((ImagemPerfilDto)null);

        var formFile = new FormFile(new MemoryStream(Encoding.UTF8.GetBytes("Test file content")), 0, Encoding.UTF8.GetBytes("Test file content").Length, "test", "test.jpg");

        // Act
        var result = await _usuarioController.PostImagemPerfil(formFile) as ObjectResult;

        // Assert
        Assert.NotNull(result);
        Assert.IsType<BadRequestObjectResult>(result);
        var value = result.Value;
        value = result.Value;
        //var message = value?.GetType()?.GetProperty("message")?.GetValue(value, null) as String;
        //Assert.Equal("Erro ao incluir nova imagem de peefil!", message);
        _mockImagemPerfilBusiness.Verify(b => b.Create(It.IsAny<ImagemPerfilDto>()), Times.Never);
    }

    [Fact]
    public async void Put_Should_Returns_OkResult_For_ImagesTypes_JPG_PNG_JPEG()
    {
        // Arrange
        var imagemPerfilUsuarioVM = ImagemPerfilUsuarioFaker.ImagensPerfilUsuarioVMs().First();
        int idUsuario = imagemPerfilUsuarioVM.IdUsuario;
        SetupBearerToken(idUsuario);
        _mockImagemPerfilBusiness.Setup(business => business.Update(It.IsAny<ImagemPerfilDto>())).Returns(imagemPerfilUsuarioVM);

        var formFile = new FormFile(new MemoryStream(Encoding.UTF8.GetBytes("Test file content")), 0, Encoding.UTF8.GetBytes("Test file content").Length, "test", "test.jpg");
        formFile.Headers = new HeaderDictionary { { "Content-Type", "image/jpg" } };

        // Act
        var result = await _usuarioController.PutImagemPerfil(formFile) as ObjectResult;

        // Assert
        Assert.NotNull(result);
        Assert.IsType<OkObjectResult>(result);
        var value = result.Value;
        var message = (bool)(value?.GetType()?.GetProperty("message")?.GetValue(value, null) ?? false);

        Assert.True(message);
        var imagemPerfilUsuario = value?.GetType()?.GetProperty("imagemPerfilUsuario")?.GetValue(value, null) as ImagemPerfilDto;
        Assert.NotNull(imagemPerfilUsuario);
        Assert.IsType<ImagemPerfilDto>(imagemPerfilUsuario);
        _mockImagemPerfilBusiness.Verify(b => b.Update(It.IsAny<ImagemPerfilDto>()), Times.Once);

        // Arrage file type PNG
        formFile = new FormFile(new MemoryStream(Encoding.UTF8.GetBytes("Test file content png")), 0, Encoding.UTF8.GetBytes("Test file content png").Length, "Test File PNG", "test.png");
        formFile.Headers = new HeaderDictionary { { "Content-Type", "image/png" } };

        // Act file type PNG
        result = await _usuarioController.PutImagemPerfil(formFile) as ObjectResult;

        // Assert file Type PNG
        Assert.NotNull(result);
        Assert.IsType<OkObjectResult>(result);
        value = result.Value;
        message = (bool)(value?.GetType()?.GetProperty("message")?.GetValue(value, null) ?? false);
        Assert.True(message);
        imagemPerfilUsuario = value?.GetType()?.GetProperty("imagemPerfilUsuario")?.GetValue(value, null) as ImagemPerfilDto;
        Assert.NotNull(imagemPerfilUsuario);
        Assert.IsType<ImagemPerfilDto>(imagemPerfilUsuario);
        _mockImagemPerfilBusiness.Verify(b => b.Update(It.IsAny<ImagemPerfilDto>()),Times.Exactly(2));

        // Arrage file type JPEG
        formFile = new FormFile(new MemoryStream(Encoding.UTF8.GetBytes("Test file contentjpeg")), 0, Encoding.UTF8.GetBytes("Test file content jpeg").Length, "Test File JPEG", "test.jpeg");
        formFile.Headers = new HeaderDictionary { { "Content-Type", "image/jpeg" } };

        // Act file type JPEG
        result = await _usuarioController.PutImagemPerfil(formFile) as ObjectResult;

        // Assert file Type JPEG
        Assert.NotNull(result);
        Assert.IsType<OkObjectResult>(result);
        value = result.Value;
        message = (bool)(value?.GetType()?.GetProperty("message")?.GetValue(value, null) ?? false);
        Assert.True(message);
        imagemPerfilUsuario = value?.GetType()?.GetProperty("imagemPerfilUsuario")?.GetValue(value, null) as ImagemPerfilDto;
        Assert.NotNull(imagemPerfilUsuario);
        Assert.IsType<ImagemPerfilDto>(imagemPerfilUsuario);
        _mockImagemPerfilBusiness.Verify(b => b.Update(It.IsAny<ImagemPerfilDto>()), Times.Exactly(3));
    }

    [Fact]
    public async void Put_Throws_Erro_And_Returns_BadRequest()
    {
        // Arrange
        int idUsuario = 1;
        SetupBearerToken(idUsuario);
        _mockImagemPerfilBusiness.Setup(business => business.Update(It.IsAny<ImagemPerfilDto>())).Returns((ImagemPerfilDto)null);

        var formFile = new FormFile(new MemoryStream(Encoding.UTF8.GetBytes("Test file content")), 0, Encoding.UTF8.GetBytes("Test file content").Length, "test", "test.jpg");

        // Act
        var result = await _usuarioController.PutImagemPerfil(formFile) as ObjectResult;

        // Assert
        Assert.NotNull(result);
        Assert.IsType<BadRequestObjectResult>(result);
        var value = result.Value;
        value = result.Value;
        //var message = value?.GetType()?.GetProperty("message")?.GetValue(value, null) as String;
        //Assert.Equal("Erro ao Atualizar imagem do perfil!", message);
        _mockImagemPerfilBusiness.Verify(b => b.Update(It.IsAny<ImagemPerfilDto>()), Times.Never);
    }

    [Fact]
    public async void Put_Should_Returns_BadRequest_For_Invalid_Images_Type()
    {
        // Arrange
        var imagemPerfilUsuarioVM = ImagemPerfilUsuarioFaker.ImagensPerfilUsuarioVMs().First();
        int idUsuario = imagemPerfilUsuarioVM.IdUsuario;

        SetupBearerToken(idUsuario);
        _mockImagemPerfilBusiness.Setup(business => business.Update(It.IsAny<ImagemPerfilDto>())).Returns(imagemPerfilUsuarioVM);

        var formFile = new FormFile(new MemoryStream(Encoding.UTF8.GetBytes("Test file not Image type content")), 0, Encoding.UTF8.GetBytes("Test file not Image type content").Length, "DATA File Erro", "test.txt");
        formFile.Headers = new HeaderDictionary { { "Content-Type", "image/txt" } };

        // Act
        var result = await _usuarioController.PutImagemPerfil(formFile) as ObjectResult;

        // Assert
        Assert.NotNull(result);
        Assert.IsType<BadRequestObjectResult>(result);
        var value = result.Value;
        var message = value?.GetType()?.GetProperty("message")?.GetValue(value, null) as string;
        Assert.Equal("Apenas arquivos do tipo jpg, jpeg ou png são aceitos.", message);
        _mockImagemPerfilBusiness.Verify(b => b.Update(It.IsAny<ImagemPerfilDto>()),Times.Never);
    }

    [Fact]
    public async void Put_Should_Returns_BadRequest_When_ImagemPerfil_IsNull()
    {
        // Arrange
        var imagemPerfilUsuarioVM = ImagemPerfilUsuarioFaker.ImagensPerfilUsuarioVMs().First();
        int idUsuario = imagemPerfilUsuarioVM.IdUsuario;
        SetupBearerToken(idUsuario);
        _mockImagemPerfilBusiness.Setup(business => business.Update(It.IsAny<ImagemPerfilDto>())).Returns((ImagemPerfilDto)null);

        var formFile = new FormFile(new MemoryStream(Encoding.UTF8.GetBytes("Test file content")), 0, Encoding.UTF8.GetBytes("Test file content").Length, "test", "test.jpg");
        formFile.Headers = new HeaderDictionary { { "Content-Type", "image/jpg" } };

        // Act
        var result = await _usuarioController.PutImagemPerfil(formFile) as ObjectResult;

        // Assert
        Assert.NotNull(result);
        Assert.IsType<BadRequestObjectResult>(result);
        var value = result.Value;
        value = result.Value;
        var message = (bool)(value?.GetType()?.GetProperty("message")?.GetValue(value, null) ?? false);
        Assert.False(message);
        var imagemPerfilUsuario = value?.GetType()?.GetProperty("imagemPerfilUsuario")?.GetValue(value, null) as ImagemPerfilDto;
        Assert.Null(imagemPerfilUsuario);
        _mockImagemPerfilBusiness.Verify(b => b.Update(It.IsAny<ImagemPerfilDto>()), Times.Once);
    }

    [Fact]
    public void Delete_Should_Returns_OkResults()
    {
        // Arrange
        int idUsuario = 1;
        SetupBearerToken(idUsuario);
        _mockImagemPerfilBusiness.Setup(business => business.Delete(It.IsAny<int>())).Returns(true);

        // Act
        var result = _usuarioController.DeleteImagemPerfil() as ObjectResult;

        // Assert
        Assert.NotNull(result);
        Assert.IsType<OkObjectResult>(result);
        var value = result.Value;
        var message = value?.GetType()?.GetProperty("message")?.GetValue(value, null);
        Assert.IsType<bool>(message);
        Assert.True((bool)message);
        _mockImagemPerfilBusiness.Verify(b => b.Delete(It.IsAny<int>()), Times.Once);
    }

    [Fact]
    public void Delete_Should_Returns_BadRequest_When_Try_To_Delete()
    {
        // Arrange
        int idUsuario = 1;
        SetupBearerToken(idUsuario);
        _mockImagemPerfilBusiness.Setup(business => business.Delete(It.IsAny<int>())).Returns(false);

        // Act
        var result = _usuarioController.DeleteImagemPerfil() as ObjectResult;

        // Assert
        Assert.NotNull(result);
        Assert.IsType<BadRequestObjectResult>(result);
        var value = result.Value;
        value = result.Value;
        var message = value?.GetType()?.GetProperty("message")?.GetValue(value, null);
        Assert.IsType<bool>(message);
        Assert.False((bool)message);
        _mockImagemPerfilBusiness.Verify(b => b.Delete(It.IsAny<int>()), Times.Once);
    }

    [Fact]
    public void Delete_Throws_Erro_And_Retuns_BadRequestResult()
    {
        // Arrange
        int idUsuario = 1;
        SetupBearerToken(idUsuario);
        _mockImagemPerfilBusiness.Setup(business => business.Delete(It.IsAny<int>())).Throws<Exception>();

        // Act
        var result = _usuarioController.DeleteImagemPerfil() as ObjectResult;

        // Assert
        Assert.NotNull(result);
        Assert.IsType<BadRequestObjectResult>(result);
        var value = result.Value;
        value = result.Value;
        var message = value?.GetType()?.GetProperty("message")?.GetValue(value, null) as string;
        Assert.Equal("Erro ao excluir imagem do perfil!", message);
        _mockImagemPerfilBusiness.Verify(b => b.Delete(It.IsAny<int>()), Times.Once);
    }
}
