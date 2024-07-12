using Business.Abstractions;
using Business.Dtos.Parser;
using Despesas.WebApi.Controllers.v1;
using Business.Dtos.v1;
using __mock__.v1;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text;

namespace Api.Controllers.v1;

public sealed class ImagemPerfilUsuarioControllerTest
{
    private Mock<IUsuarioBusiness<UsuarioDto>> _mockUsuarioBusiness;
    private Mock<IImagemPerfilUsuarioBusiness<ImagemPerfilDto, UsuarioDto>> _mockImagemPerfilBusiness;
    private UsuarioController _usuarioController;

    public ImagemPerfilUsuarioControllerTest()
    {
        _mockUsuarioBusiness = new Mock<IUsuarioBusiness<UsuarioDto>>();
        _mockImagemPerfilBusiness = new Mock<IImagemPerfilUsuarioBusiness<ImagemPerfilDto, UsuarioDto>>();
        _usuarioController = new UsuarioController(_mockUsuarioBusiness.Object, _mockImagemPerfilBusiness.Object);
    }

    [Fact]
    public void GetImage_Should_Returns_OkResults_With_ImagemPerfilUsuario()
    {
        // Arrange
        var _imagemPerfilUsuarios = ImagemPerfilUsuarioFaker.Instance.ImagensPerfilUsuarios();
        var _imagemPerfilUsuarioDtos = new ImagemPerfilUsuarioParser().ParseList(
            _imagemPerfilUsuarios
        );
        var usuarioDto = new UsuarioParser().Parse(_imagemPerfilUsuarios.First().Usuario);
        Guid idUsuario = usuarioDto.Id;
        Usings.SetupBearerToken(idUsuario, _usuarioController);
        
        _mockImagemPerfilBusiness.Setup(business => business.FindAll(idUsuario)).Returns(_imagemPerfilUsuarioDtos);

        // Act
        var result = _usuarioController.GetImage() as ObjectResult;

        // Assert
        Assert.NotNull(result);
        Assert.IsType<OkObjectResult>(result);
        var value = result.Value;

        var message = (bool?)value?.GetType()?.GetProperty("message")?.GetValue(value, null);

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
        var _imagemPerfilUsuarios = ImagemPerfilUsuarioFaker.Instance.ImagensPerfilUsuarios();
        var _imagemPerfilUsuarioDtos = new ImagemPerfilUsuarioParser().ParseList(_imagemPerfilUsuarios);
        var usuarioDto = new UsuarioParser().Parse(_imagemPerfilUsuarios.First().Usuario);
        Guid idUsuario = Guid.NewGuid();
        Usings.SetupBearerToken(idUsuario, _usuarioController);
        _mockImagemPerfilBusiness.Setup(business => business.FindAll(idUsuario)).Returns(_imagemPerfilUsuarioDtos);

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
    public async Task Post_Should_Create_And_Returns_OkResult_For_ImagesTypes_JPG_PNG_JPEG()
    {
        // Arrange
        // Arrange
        var _imagemPerfilUsuarios = ImagemPerfilUsuarioFaker.Instance.ImagensPerfilUsuarios();
        var _imagemPerfilUsuarioDtos = new ImagemPerfilUsuarioParser().ParseList(_imagemPerfilUsuarios);
        var imagemPerfilUsuarioDto = _imagemPerfilUsuarioDtos.First();
        Guid idUsuario = imagemPerfilUsuarioDto.UsuarioId;

        Usings.SetupBearerToken(idUsuario, _usuarioController);
        _mockImagemPerfilBusiness.Setup(business => business.Create(It.IsAny<ImagemPerfilDto>())).Returns(imagemPerfilUsuarioDto);

        var formFile = new FormFile(
            new MemoryStream(Encoding.UTF8.GetBytes("Test file content")),
            0,
            Encoding.UTF8.GetBytes("Test file content").Length,
            "test",
            "test.jpg"
        );
        formFile.Headers = new HeaderDictionary { { "Content-Type", "image/jpg" } };

        // Act
        var result = await _usuarioController.PostImagemPerfil(formFile) as ObjectResult;

        // Assert
        Assert.NotNull(result);
        Assert.IsType<OkObjectResult>(result);
        var value = result.Value;
        var message = (bool?)value?.GetType()?.GetProperty("message")?.GetValue(value, null);

        Assert.True(message);
        var imagemPerfilUsuario = value?.GetType()?.GetProperty("imagemPerfilUsuario")?.GetValue(value, null) as ImagemPerfilDto;
        Assert.NotNull(imagemPerfilUsuario);
        Assert.IsType<ImagemPerfilDto>(imagemPerfilUsuario);
        _mockImagemPerfilBusiness.Verify(b => b.Create(It.IsAny<ImagemPerfilDto>()), Times.Once);

        // Arrage file type PNG
        formFile = new FormFile(
            new MemoryStream(Encoding.UTF8.GetBytes("Test file content png")),
            0,
            Encoding.UTF8.GetBytes("Test file content png").Length,
            "Test File PNG",
            "test.png"
        );
        formFile.Headers = new HeaderDictionary { { "Content-Type", "image/png" } };

        // Act file type PNG
        result = await _usuarioController.PostImagemPerfil(formFile) as ObjectResult;

        // Assert file Type PNG
        Assert.NotNull(result);
        Assert.IsType<OkObjectResult>(result);
        value = result.Value;

        message = (bool?)value?.GetType()?.GetProperty("message")?.GetValue(value, null);

        Assert.True(message);
        imagemPerfilUsuario = value?.GetType()?.GetProperty("imagemPerfilUsuario")?.GetValue(value, null) as ImagemPerfilDto;
        Assert.NotNull(imagemPerfilUsuario);
        Assert.IsType<ImagemPerfilDto>(imagemPerfilUsuario);
        _mockImagemPerfilBusiness.Verify(b => b.Create(It.IsAny<ImagemPerfilDto>()), Times.Exactly(2));

        // Arrage file type JPEG
        formFile = new FormFile(
            new MemoryStream(Encoding.UTF8.GetBytes("Test file contentjpeg")),
            0,
            Encoding.UTF8.GetBytes("Test file content jpeg").Length,
            "Test File JPEG",
            "test.jpeg"
        );
        formFile.Headers = new HeaderDictionary { { "Content-Type", "image/jpeg" } };

        // Act file type JPEG
        result = await _usuarioController.PostImagemPerfil(formFile) as ObjectResult;

        // Assert file Type JPEG
        Assert.NotNull(result);
        Assert.IsType<OkObjectResult>(result);
        value = result.Value;
        message = (bool?)value?.GetType()?.GetProperty("message")?.GetValue(value, null);
        Assert.True(message);
        imagemPerfilUsuario = value?.GetType()?.GetProperty("imagemPerfilUsuario")?.GetValue(value, null) as ImagemPerfilDto;
        Assert.NotNull(imagemPerfilUsuario);
        Assert.IsType<ImagemPerfilDto>(imagemPerfilUsuario);
        _mockImagemPerfilBusiness.Verify(b => b.Create(It.IsAny<ImagemPerfilDto>()), Times.Exactly(3));
    }

    [Fact]
    public async Task Post_Should_Returns_BadRequest_For_Invalid_Images_Type()
    {
        // Arrange
        var imagemPerfilUsuarioDto = ImagemPerfilUsuarioFaker.Instance.ImagensPerfilUsuarioDtos().First();
        Guid idUsuario = imagemPerfilUsuarioDto.UsuarioId;
        Usings.SetupBearerToken(idUsuario, _usuarioController);
        _mockImagemPerfilBusiness.Setup(business => business.Create(It.IsAny<ImagemPerfilDto>())).Returns(imagemPerfilUsuarioDto);

        var formFile = new FormFile(
            new MemoryStream(Encoding.UTF8.GetBytes("Test file not Image type content")),
            0,
            Encoding.UTF8.GetBytes("Test file not Image type content").Length,
            "DATA File Erro",
            "test.txt"
        );
        formFile.Headers = new HeaderDictionary { { "Content-Type", "image/txt" } };

        // Act
        var result = await _usuarioController.PostImagemPerfil(formFile) as ObjectResult;

        // Assert
        Assert.NotNull(result);
        Assert.IsType<BadRequestObjectResult>(result);
        var value = result.Value;
        var message = value?.GetType()?.GetProperty("message")?.GetValue(value, null) as string;
        Assert.Equal("Apenas arquivos do tipo jpg, jpeg ou png são aceitos.", message);
        _mockImagemPerfilBusiness.Verify(b => b.Create(It.IsAny<ImagemPerfilDto>()),Times.Never);
    }

    [Fact]
    public async Task Post_Should_Try_Create_And_Returns_BadRequest()
    {
        // Arrange
        var imagemPerfilUsuarioDto = ImagemPerfilUsuarioFaker.Instance.ImagensPerfilUsuarioDtos().First();
        Guid idUsuario = imagemPerfilUsuarioDto.UsuarioId;
        Usings.SetupBearerToken(idUsuario, _usuarioController);
        _mockImagemPerfilBusiness.Setup(business => business.Create(It.IsAny<ImagemPerfilDto>())).Returns(() => null);

        var formFile = new FormFile(
            new MemoryStream(Encoding.UTF8.GetBytes("Test file content")),
            0,
            Encoding.UTF8.GetBytes("Test file content").Length,
            "test",
            "test.jpg"
        );
        formFile.Headers = new HeaderDictionary { { "Content-Type", "image/jpg" } };

        // Act
        var result = await _usuarioController.PostImagemPerfil(formFile) as ObjectResult;

        // Assert
        Assert.NotNull(result);
        Assert.IsType<BadRequestObjectResult>(result);
        var value = result.Value;
        value = result.Value;
        var message = (bool?)value?.GetType()?.GetProperty("message")?.GetValue(value, null);
        Assert.False(message);
        var imagemPerfilUsuario = value?.GetType()?.GetProperty("imagemPerfilUsuario")?.GetValue(value, null) as ImagemPerfilDto;
        Assert.Null(imagemPerfilUsuario);
        _mockImagemPerfilBusiness.Verify(b => b.Create(It.IsAny<ImagemPerfilDto>()), Times.Once);
    }

    [Fact]
    public async Task Post_Throws_Erro_And_Returns_BadRequest()
    {
        // Arrange
        Usings.SetupBearerToken(Guid.NewGuid(), _usuarioController);
        _mockImagemPerfilBusiness.Setup(business => business.Create(It.IsAny<ImagemPerfilDto>())).Returns(() => null);

        var formFile = new FormFile(
            new MemoryStream(Encoding.UTF8.GetBytes("Test file content")),
            0,
            Encoding.UTF8.GetBytes("Test file content").Length,
            "test",
            "test.jpg"
        );

        // Act
        var result = await _usuarioController.PostImagemPerfil(formFile) as ObjectResult;

        // Assert
        Assert.NotNull(result);
        Assert.IsType<BadRequestObjectResult>(result);
        var value = result.Value;
        value = result.Value;
        //var message = value?.GetType()?.GetProperty("message")?.GetValue(value, null) as string;
        //Assert.Equal("Erro ao incluir nova imagem de peefil!", message);
        _mockImagemPerfilBusiness.Verify(b => b.Create(It.IsAny<ImagemPerfilDto>()),Times.Never);
    }

    [Fact]
    public async Task Put_Should_Returns_OkResult_For_ImagesTypes_JPG_PNG_JPEG()
    {
        // Arrange
        var imagemPerfilUsuarioDto = ImagemPerfilUsuarioFaker.Instance.ImagensPerfilUsuarioDtos().First();
        Guid idUsuario = imagemPerfilUsuarioDto.UsuarioId;

        Usings.SetupBearerToken(idUsuario, _usuarioController);
        _mockImagemPerfilBusiness.Setup(business => business.Update(It.IsAny<ImagemPerfilDto>())).Returns(imagemPerfilUsuarioDto);

        var formFile = new FormFile(
            new MemoryStream(Encoding.UTF8.GetBytes("Test file content")),
            0,
            Encoding.UTF8.GetBytes("Test file content").Length,
            "test",
            "test.jpg"
        );
        formFile.Headers = new HeaderDictionary { { "Content-Type", "image/jpg" } };

        // Act
        var result = await _usuarioController.PutImagemPerfil(formFile) as ObjectResult;

        // Assert
        Assert.NotNull(result);
        Assert.IsType<OkObjectResult>(result);
        var value = result.Value;
        var message = (bool?)value?.GetType()?.GetProperty("message")?.GetValue(value, null);

        Assert.True(message);
        var imagemPerfilUsuario = value?.GetType()?.GetProperty("imagemPerfilUsuario")?.GetValue(value, null) as ImagemPerfilDto;
        Assert.NotNull(imagemPerfilUsuario);
        Assert.IsType<ImagemPerfilDto>(imagemPerfilUsuario);
        _mockImagemPerfilBusiness.Verify(b => b.Update(It.IsAny<ImagemPerfilDto>()), Times.Once);

        // Arrage file type PNG
        formFile = new FormFile(
            new MemoryStream(Encoding.UTF8.GetBytes("Test file content png")),
            0,
            Encoding.UTF8.GetBytes("Test file content png").Length,
            "Test File PNG",
            "test.png"
        );
        formFile.Headers = new HeaderDictionary { { "Content-Type", "image/png" } };

        // Act file type PNG
        result = await _usuarioController.PutImagemPerfil(formFile) as ObjectResult;

        // Assert file Type PNG
        Assert.NotNull(result);
        Assert.IsType<OkObjectResult>(result);
        value = result.Value;

        message = (bool?)value?.GetType()?.GetProperty("message")?.GetValue(value, null);

        Assert.True(message);
        imagemPerfilUsuario = value?.GetType()?.GetProperty("imagemPerfilUsuario")?.GetValue(value, null) as ImagemPerfilDto;
        Assert.NotNull(imagemPerfilUsuario);
        Assert.IsType<ImagemPerfilDto>(imagemPerfilUsuario);
        _mockImagemPerfilBusiness.Verify(b => b.Update(It.IsAny<ImagemPerfilDto>()),Times.Exactly(2));

        // Arrage file type JPEG
        formFile = new FormFile(
            new MemoryStream(Encoding.UTF8.GetBytes("Test file contentjpeg")),
            0,
            Encoding.UTF8.GetBytes("Test file content jpeg").Length,
            "Test File JPEG",
            "test.jpeg"
        );
        formFile.Headers = new HeaderDictionary { { "Content-Type", "image/jpeg" } };

        // Act file type JPEG
        result = await _usuarioController.PutImagemPerfil(formFile) as ObjectResult;

        // Assert file Type JPEG
        Assert.NotNull(result);
        Assert.IsType<OkObjectResult>(result);
        value = result.Value;

        message = (bool?)value?.GetType()?.GetProperty("message")?.GetValue(value, null);

        Assert.True(message);
        imagemPerfilUsuario = value?.GetType()?.GetProperty("imagemPerfilUsuario")?.GetValue(value, null) as ImagemPerfilDto;
        Assert.NotNull(imagemPerfilUsuario);
        Assert.IsType<ImagemPerfilDto>(imagemPerfilUsuario);
        _mockImagemPerfilBusiness.Verify(b => b.Update(It.IsAny<ImagemPerfilDto>()),Times.Exactly(3));
    }

    [Fact]
    public async Task Put_Throws_Erro_And_Returns_BadRequest()
    {
        // Arrange        
        Usings.SetupBearerToken(Guid.NewGuid(), _usuarioController);
        _mockImagemPerfilBusiness.Setup(business => business.Update(It.IsAny<ImagemPerfilDto>())).Returns(() => null);

        var formFile = new FormFile(
            new MemoryStream(Encoding.UTF8.GetBytes("Test file content")),
            0,
            Encoding.UTF8.GetBytes("Test file content").Length,
            "test",
            "test.jpg"
        );

        // Act
        var result = await _usuarioController.PutImagemPerfil(formFile) as ObjectResult;

        // Assert
        Assert.NotNull(result);
        Assert.IsType<BadRequestObjectResult>(result);
        var value = result.Value;
        value = result.Value;
        //var message = value?.GetType()?.GetProperty("message")?.GetValue(value, null) as string;
        //Assert.Equal("Erro ao Atualizar imagem do perfil!", message);
        _mockImagemPerfilBusiness.Verify(b => b.Update(It.IsAny<ImagemPerfilDto>()),Times.Never);
    }

    [Fact]
    public async Task Put_Should_Returns_BadRequest_For_Invalid_Images_Type()
    {
        // Arrange
        var imagemPerfilUsuarioDto = ImagemPerfilUsuarioFaker.Instance.ImagensPerfilUsuarioDtos().First();
        Guid idUsuario = imagemPerfilUsuarioDto.UsuarioId;

        Usings.SetupBearerToken(idUsuario, _usuarioController);
        _mockImagemPerfilBusiness.Setup(business => business.Update(It.IsAny<ImagemPerfilDto>())).Returns(imagemPerfilUsuarioDto);

        var formFile = new FormFile(
            new MemoryStream(Encoding.UTF8.GetBytes("Test file not Image type content")),
            0,
            Encoding.UTF8.GetBytes("Test file not Image type content").Length,
            "DATA File Erro",
            "test.txt"
        );
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
    public async Task Put_Should_Returns_BadRequest_When_ImagemPerfil_IsNull()
    {
        // Arrange
        var imagemPerfilUsuarioDto = ImagemPerfilUsuarioFaker.Instance.ImagensPerfilUsuarioDtos().First();
        Guid idUsuario = imagemPerfilUsuarioDto.UsuarioId;
        Usings.SetupBearerToken(idUsuario, _usuarioController);
        _mockImagemPerfilBusiness.Setup(business => business.Update(It.IsAny<ImagemPerfilDto>())).Returns(() => null);

        var formFile = new FormFile(
            new MemoryStream(Encoding.UTF8.GetBytes("Test file content")),
            0,
            Encoding.UTF8.GetBytes("Test file content").Length,
            "test",
            "test.jpg"
        );
        formFile.Headers = new HeaderDictionary { { "Content-Type", "image/jpg" } };

        // Act
        var result = await _usuarioController.PutImagemPerfil(formFile) as ObjectResult;

        // Assert
        Assert.NotNull(result);
        Assert.IsType<BadRequestObjectResult>(result);
        var value = result.Value;
        value = result.Value;
        var message = (bool?)value?.GetType()?.GetProperty("message")?.GetValue(value, null);

        Assert.False(message);
        var imagemPerfilUsuario = value?.GetType()?.GetProperty("imagemPerfilUsuario")?.GetValue(value, null) as ImagemPerfilDto;
        Assert.Null(imagemPerfilUsuario);
        _mockImagemPerfilBusiness.Verify(b => b.Update(It.IsAny<ImagemPerfilDto>()), Times.Once);
    }

    [Fact]
    public void Delete_Should_Returns_OkResults()
    {
        // Arrange
        var idUsuario = Guid.NewGuid();
        Usings.SetupBearerToken(idUsuario, _usuarioController);
        _mockImagemPerfilBusiness.Setup(business => business.Delete(It.IsAny<Guid>())).Returns(true);

        // Act
        var result = _usuarioController.DeleteImagemPerfil() as ObjectResult;

        // Assert
        Assert.NotNull(result);
        Assert.IsType<OkObjectResult>(result);
        var value = result.Value;
        var message = value?.GetType()?.GetProperty("message")?.GetValue(value, null);
        Assert.IsType<bool>(message);
        Assert.True((bool)message);
        _mockImagemPerfilBusiness.Verify(b => b.Delete(It.IsAny<Guid>()), Times.Once);
    }

    [Fact]
    public void Delete_Should_Returns_BadRequest_When_Try_To_Delete()
    {
        // Arrange
        var idUsuario = Guid.NewGuid();
        Usings.SetupBearerToken(idUsuario, _usuarioController);
        _mockImagemPerfilBusiness.Setup(business => business.Delete(It.IsAny<Guid>())).Returns(false);

        // Act
        var result = _usuarioController.DeleteImagemPerfil() as ObjectResult;

        // Assert
        // Assert
        Assert.NotNull(result);
        Assert.IsType<BadRequestObjectResult>(result);
        var value = result.Value;
        value = result.Value;
        var message = value?.GetType()?.GetProperty("message")?.GetValue(value, null);
        Assert.IsType<bool>(message);
        Assert.False((bool)message);
        _mockImagemPerfilBusiness.Verify(b => b.Delete(It.IsAny<Guid>()), Times.Once);
    }

    [Fact]
    public void Delete_Throws_Erro_And_Retuns_BadRequestResult()
    {
        // Arrange
        var idUsuario = Guid.NewGuid();
        Usings.SetupBearerToken(idUsuario, _usuarioController);
        _mockImagemPerfilBusiness.Setup(business => business.Delete(It.IsAny<Guid>())).Throws<Exception>();

        // Act
        var result = _usuarioController.DeleteImagemPerfil() as ObjectResult;

        // Assert
        // Assert
        Assert.NotNull(result);
        Assert.IsType<BadRequestObjectResult>(result);
        var value = result.Value;
        value = result.Value;
        var message = value?.GetType()?.GetProperty("message")?.GetValue(value, null) as string;
        Assert.Equal("Erro ao excluir imagem do perfil!", message);
        _mockImagemPerfilBusiness.Verify(b => b.Delete(It.IsAny<Guid>()), Times.Once);
    }
}
