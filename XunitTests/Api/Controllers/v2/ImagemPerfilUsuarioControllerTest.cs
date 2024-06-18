using Business.Abstractions;
using Business.Dtos.v2;
using Despesas.WebApi.Controllers.v2;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text;
using __mock__.v2;
using AutoMapper;
using Business.Dtos.Core.Profile;

namespace Api.Controllers.v2;

public sealed class ImagemPerfilUsuarioControllerTest
{
    private Mock<IUsuarioBusiness<UsuarioDto>> _mockUsuarioBusiness;
    private Mock<IImagemPerfilUsuarioBusiness<ImagemPerfilDto, UsuarioDto>> _mockImagemPerfilBusiness;
    private UsuarioController _usuarioController;
    private Mapper _mapperImagemPerfil;
    private Mapper _mapperUsuario;

    public ImagemPerfilUsuarioControllerTest()
    {
        _mockUsuarioBusiness = new Mock<IUsuarioBusiness<UsuarioDto>>();
        _mockImagemPerfilBusiness = new Mock<IImagemPerfilUsuarioBusiness<ImagemPerfilDto, UsuarioDto>>();
        _usuarioController = new UsuarioController(_mockUsuarioBusiness.Object, _mockImagemPerfilBusiness.Object);
        _mapperImagemPerfil = new Mapper(new MapperConfiguration(cfg => { cfg.AddProfile<ImagemPerfilUsuarioProfile>(); }));
        _mapperUsuario = new Mapper(new MapperConfiguration(cfg => { cfg.AddProfile<UsuarioProfile>(); }));
    }

    [Fact]
    public void Get_ImagemPerfilUsuario_Should_Returns_OkResults_With_ImagemPerfilUsuario()
    {
        // Arrange
        var _imagemPerfilUsuarios = ImagemPerfilUsuarioFaker.Instance.ImagensPerfilUsuarios();
        var _imagemPerfilUsuarioDtos = _mapperImagemPerfil.Map<List<ImagemPerfilDto>>(_imagemPerfilUsuarios);
        var usuarioDto = _mapperUsuario.Map<UsuarioDto>(_imagemPerfilUsuarios.First().Usuario);
        int idUsuario = usuarioDto.Id;
        Usings.SetupBearerToken(idUsuario, _usuarioController);
        _mockImagemPerfilBusiness.Setup(business => business.FindAll(idUsuario)).Returns(_imagemPerfilUsuarioDtos);

        // Act
        var result = _usuarioController.GetImagemPerfil() as ObjectResult;
        // Assert
        Assert.NotNull(result);
        Assert.IsType<OkObjectResult>(result);
        var _imagemPerfilUsuario = result.Value as ImagemPerfilDto;
        Assert.NotNull(_imagemPerfilUsuario);
        Assert.IsType<ImagemPerfilDto>(_imagemPerfilUsuario);
        _mockImagemPerfilBusiness.Verify(b => b.FindAll(idUsuario), Times.Once);
    }

    [Fact]
    public void Get_ImagemPerfilUsuario_Should_Returns_BadRequest_When_ImagemPerfilUsuario_NotFound()
    {
        // Arrange
        var _imagemPerfilUsuarios = ImagemPerfilUsuarioFaker.Instance.ImagensPerfilUsuarios();
        var _imagemPerfilUsuarioDtos = _mapperImagemPerfil.Map<List<ImagemPerfilDto>>(_imagemPerfilUsuarios);
        var usuarioDto = _mapperUsuario.Map<UsuarioDto>(_imagemPerfilUsuarios.First().Usuario);
        int idUsuario = 987654;
        Usings.SetupBearerToken(idUsuario, _usuarioController);
        _mockImagemPerfilBusiness.Setup(business => business.FindAll(idUsuario)).Returns(_imagemPerfilUsuarioDtos);

        // Act
        var result = _usuarioController.GetImagemPerfil() as ObjectResult;

        // Assert
        Assert.NotNull(result);
        Assert.IsType<BadRequestObjectResult>(result);
        var message = result.Value;        
        Assert.Equal("Usuário não possui nenhuma imagem de perfil cadastrada!", message);
        _mockImagemPerfilBusiness.Verify(b => b.FindAll(idUsuario), Times.Once);
    }

    [Fact]
    public async Task Post_ImagemPerfilUsuario_Should_Create_And_Returns_OkResult_For_ImagesTypes_JPG_PNG_JPEG()
    {
        // Arrange
        var _imagemPerfilUsuarios = ImagemPerfilUsuarioFaker.Instance.ImagensPerfilUsuarios();
        var _imagemPerfilUsuarioDtos = _mapperImagemPerfil.Map<List<ImagemPerfilDto>>(_imagemPerfilUsuarios);
        var imagemPerfilUsuarioDto = _imagemPerfilUsuarioDtos.First();
        int idUsuario = imagemPerfilUsuarioDto.UsuarioId;

        Usings.SetupBearerToken(idUsuario, _usuarioController);
        _mockImagemPerfilBusiness.Setup(business => business.Create(It.IsAny<ImagemPerfilDto>())).Returns(imagemPerfilUsuarioDto);

        var formFile = new FormFile(new MemoryStream(Encoding.UTF8.GetBytes("Test file content")), 0, Encoding.UTF8.GetBytes("Test file content").Length, "test", "test.jpg");
        formFile.Headers = new HeaderDictionary { { "Content-Type", "image/jpg" } };

        // Act
        var result = await _usuarioController.PostImagemPerfil(formFile) as ObjectResult;

        // Assert
        Assert.NotNull(result);
        Assert.IsType<OkObjectResult>(result);
        var imagemPerfilUsuario = result.Value as ImagemPerfilDto;
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
        imagemPerfilUsuario = result.Value as ImagemPerfilDto;
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
        imagemPerfilUsuario = result.Value as ImagemPerfilDto;
        Assert.NotNull(imagemPerfilUsuario);
        Assert.IsType<ImagemPerfilDto>(imagemPerfilUsuario);
        _mockImagemPerfilBusiness.Verify(b => b.Create(It.IsAny<ImagemPerfilDto>()), Times.Exactly(3));
    }

    [Fact]
    public async Task Post_ImagemPerfilUsuario_Should_Returns_BadRequest_For_Invalid_Images_Type()
    {
        // Arrange
        var imagemPerfilUsuarioDto = ImagemPerfilUsuarioFaker.Instance.ImagensPerfilUsuarioDtos().First();
        int idUsuario = imagemPerfilUsuarioDto.UsuarioId;
        Usings.SetupBearerToken(idUsuario, _usuarioController);
        _mockImagemPerfilBusiness.Setup(business => business.Create(It.IsAny<ImagemPerfilDto>())).Returns(imagemPerfilUsuarioDto);

        var formFile = new FormFile(new MemoryStream(Encoding.UTF8.GetBytes("Test file not Image type content")), 0, Encoding.UTF8.GetBytes("Test file not Image type content").Length, "DATA File Erro", "test.txt");
        formFile.Headers = new HeaderDictionary { { "Content-Type", "image/txt" } };

        // Act
        var result = await _usuarioController.PostImagemPerfil(formFile) as ObjectResult;

        // Assert
        Assert.NotNull(result);
        Assert.IsType<BadRequestObjectResult>(result);
        var message = result.Value;        
        Assert.Equal("Apenas arquivos do tipo jpg, jpeg ou png são aceitos.", message);
        _mockImagemPerfilBusiness.Verify(b => b.Create(It.IsAny<ImagemPerfilDto>()), Times.Never);
    }

    [Fact]
    public async Task Post_ImagemPerfilUsuario_Should_Try_Create_And_Returns_BadRequest()
    {
        // Arrange
        var imagemPerfilUsuarioDto = ImagemPerfilUsuarioFaker.Instance.ImagensPerfilUsuarioDtos().First();
        int idUsuario = imagemPerfilUsuarioDto.UsuarioId;
        Usings.SetupBearerToken(idUsuario, _usuarioController);
        _mockImagemPerfilBusiness.Setup(business => business.Create(It.IsAny<ImagemPerfilDto>())).Returns((ImagemPerfilDto?)null);

        var formFile = new FormFile(new MemoryStream(Encoding.UTF8.GetBytes("Test file content")), 0, Encoding.UTF8.GetBytes("Test file content").Length, "test", "test.jpg");
        formFile.Headers = new HeaderDictionary { { "Content-Type", "image/jpg" } };

        // Act
        var result = await _usuarioController.PostImagemPerfil(formFile) as ObjectResult;

        // Assert
        Assert.NotNull(result);
        Assert.IsType<BadRequestObjectResult>(result);
        var imagemPerfilUsuario = result.Value as ImagemPerfilDto;
        Assert.Null(imagemPerfilUsuario);
        _mockImagemPerfilBusiness.Verify(b => b.Create(It.IsAny<ImagemPerfilDto>()), Times.Once);
    }

    [Fact]
    public async Task Post_Throws_Erro_And_Returns_BadRequest()
    {
        // Arrange
        var idUsuario = 1;
        Usings.SetupBearerToken(idUsuario, _usuarioController);
        _mockImagemPerfilBusiness.Setup(business => business.Create(It.IsAny<ImagemPerfilDto>())).Returns((ImagemPerfilDto?)null);
        var formFile = new FormFile(new MemoryStream(Encoding.UTF8.GetBytes("Test file content")), 0, Encoding.UTF8.GetBytes("Test file content").Length, "test", "test.jpg");

        // Act
        var result = await _usuarioController.PostImagemPerfil(formFile) as ObjectResult;

        // Assert
        Assert.NotNull(result);
        Assert.IsType<BadRequestObjectResult>(result);
        var value = result.Value;
        value = result.Value;
        //Assert.Equal("Erro ao incluir nova imagem de peefil!", message);
        _mockImagemPerfilBusiness.Verify(b => b.Create(It.IsAny<ImagemPerfilDto>()), Times.Never);
    }

    [Fact]
    public async Task Put_Should_Returns_OkResult_For_ImagesTypes_JPG_PNG_JPEG()
    {
        // Arrange
        var imagemPerfilUsuarioDto = ImagemPerfilUsuarioFaker.Instance.ImagensPerfilUsuarioDtos().First();
        int idUsuario = imagemPerfilUsuarioDto.UsuarioId;
        Usings.SetupBearerToken(idUsuario, _usuarioController);
        _mockImagemPerfilBusiness.Setup(business => business.Update(It.IsAny<ImagemPerfilDto>())).Returns(imagemPerfilUsuarioDto);

        var formFile = new FormFile(new MemoryStream(Encoding.UTF8.GetBytes("Test file content")), 0, Encoding.UTF8.GetBytes("Test file content").Length, "test", "test.jpg");
        formFile.Headers = new HeaderDictionary { { "Content-Type", "image/jpg" } };

        // Act
        var result = await _usuarioController.PutImagemPerfil(formFile) as ObjectResult;

        // Assert
        Assert.NotNull(result);
        Assert.IsType<OkObjectResult>(result);
        var imagemPerfilUsuario = result.Value as ImagemPerfilDto;
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
        imagemPerfilUsuario = result.Value as ImagemPerfilDto;
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
        imagemPerfilUsuario = result.Value as ImagemPerfilDto;
        Assert.NotNull(imagemPerfilUsuario);
        Assert.IsType<ImagemPerfilDto>(imagemPerfilUsuario);
        _mockImagemPerfilBusiness.Verify(b => b.Update(It.IsAny<ImagemPerfilDto>()), Times.Exactly(3));
    }

    [Fact]
    public async Task Put_Throws_Erro_And_Returns_BadRequest()
    {
        // Arrange
        int idUsuario = 1;
        Usings.SetupBearerToken(idUsuario, _usuarioController);
        _mockImagemPerfilBusiness.Setup(business => business.Update(It.IsAny<ImagemPerfilDto>())).Returns((ImagemPerfilDto?)null);
        var formFile = new FormFile(new MemoryStream(Encoding.UTF8.GetBytes("Test file content")), 0, Encoding.UTF8.GetBytes("Test file content").Length, "test", "test.jpg");

        // Act
        var result = await _usuarioController.PutImagemPerfil(formFile) as ObjectResult;

        // Assert
        Assert.NotNull(result);
        Assert.IsType<BadRequestObjectResult>(result);
        var value = result.Value;
        value = result.Value;

        //Assert.Equal("Erro ao Atualizar imagem do perfil!", message);
        _mockImagemPerfilBusiness.Verify(b => b.Update(It.IsAny<ImagemPerfilDto>()), Times.Never);
    }

    [Fact]
    public async Task Put_Should_Returns_BadRequest_For_Invalid_Images_Type()
    {
        // Arrange
        var imagemPerfilUsuarioDto = ImagemPerfilUsuarioFaker.Instance.ImagensPerfilUsuarioDtos().First();
        int idUsuario = imagemPerfilUsuarioDto.UsuarioId;
        Usings.SetupBearerToken(idUsuario, _usuarioController);
        _mockImagemPerfilBusiness.Setup(business => business.Update(It.IsAny<ImagemPerfilDto>())).Returns(imagemPerfilUsuarioDto);
        var formFile = new FormFile(new MemoryStream(Encoding.UTF8.GetBytes("Test file not Image type content")), 0, Encoding.UTF8.GetBytes("Test file not Image type content").Length, "DATA File Erro", "test.txt");
        formFile.Headers = new HeaderDictionary { { "Content-Type", "image/txt" } };

        // Act
        var result = await _usuarioController.PutImagemPerfil(formFile) as ObjectResult;

        // Assert
        Assert.NotNull(result);
        Assert.IsType<BadRequestObjectResult>(result);
        var message = result.Value;        
        Assert.Equal("Apenas arquivos do tipo jpg, jpeg ou png são aceitos.", message);
        _mockImagemPerfilBusiness.Verify(b => b.Update(It.IsAny<ImagemPerfilDto>()),Times.Never);
    }

    [Fact]
    public async Task Put_Should_Returns_BadRequest_When_ImagemPerfil_IsNull()
    {
        // Arrange
        var imagemPerfilUsuarioDto = ImagemPerfilUsuarioFaker.Instance.ImagensPerfilUsuarioDtos().First();
        int idUsuario = imagemPerfilUsuarioDto.UsuarioId;
        Usings.SetupBearerToken(idUsuario, _usuarioController);
        _mockImagemPerfilBusiness.Setup(business => business.Update(It.IsAny<ImagemPerfilDto>())).Returns((ImagemPerfilDto?)null);
        var formFile = new FormFile(new MemoryStream(Encoding.UTF8.GetBytes("Test file content")), 0, Encoding.UTF8.GetBytes("Test file content").Length, "test", "test.jpg");
        formFile.Headers = new HeaderDictionary { { "Content-Type", "image/jpg" } };

        // Act
        var result = await _usuarioController.PutImagemPerfil(formFile) as ObjectResult;

        // Assert
        Assert.NotNull(result);
        Assert.IsType<BadRequestObjectResult>(result);
        var imagemPerfilUsuario = result.Value as ImagemPerfilDto;
        Assert.Null(imagemPerfilUsuario);
        _mockImagemPerfilBusiness.Verify(b => b.Update(It.IsAny<ImagemPerfilDto>()), Times.Once);
    }

    [Fact]
    public void Delete_Should_Returns_OkResults()
    {
        // Arrange
        int idUsuario = 1;
        Usings.SetupBearerToken(idUsuario, _usuarioController);
        _mockImagemPerfilBusiness.Setup(business => business.Delete(It.IsAny<int>())).Returns(true);

        // Act
        var result = _usuarioController.DeleteImagemPerfil() as ObjectResult;

        // Assert
        Assert.NotNull(result);
        Assert.IsType<OkObjectResult>(result);
        var message  = (bool?)result.Value;
        Assert.IsType<bool>(message);
        Assert.True((bool)message);
        _mockImagemPerfilBusiness.Verify(b => b.Delete(It.IsAny<int>()), Times.Once);
    }

    [Fact]
    public void Delete_Should_Returns_BadRequest_When_Try_To_Delete()
    {
        // Arrange
        int idUsuario = 1;
        Usings.SetupBearerToken(idUsuario, _usuarioController);
        _mockImagemPerfilBusiness.Setup(business => business.Delete(It.IsAny<int>())).Returns(false);

        // Act
        var result = _usuarioController.DeleteImagemPerfil() as ObjectResult;

        // Assert
        Assert.NotNull(result);
        Assert.IsType<BadRequestObjectResult>(result);
        var message = result.Value;
        Assert.Equal("Erro ao excluir imagem do perfil!", message);
        _mockImagemPerfilBusiness.Verify(b => b.Delete(It.IsAny<int>()), Times.Once);
    }

    [Fact]
    public void Delete_Throws_Erro_And_Retuns_BadRequestResult()
    {
        // Arrange
        int idUsuario = 1;
        Usings.SetupBearerToken(idUsuario, _usuarioController);
        _mockImagemPerfilBusiness.Setup(business => business.Delete(It.IsAny<int>())).Throws<Exception>();

        // Act
        var result = _usuarioController.DeleteImagemPerfil() as ObjectResult;

        // Assert
        Assert.NotNull(result);
        Assert.IsType<BadRequestObjectResult>(result);
        var message = result.Value;               
        Assert.Equal("Erro ao excluir imagem do perfil!", message);
        _mockImagemPerfilBusiness.Verify(b => b.Delete(It.IsAny<int>()), Times.Once);
    }
}
