using Business.Abstractions;
using Business.Dtos.v2;
using despesas_backend_api_net_core.Controllers.v2;
using Microsoft.AspNetCore.Mvc;
using Fakers.v2;
using Domain.Entities.ValueObjects;
using AutoMapper;
using Business.Dtos.Core.Profile;

namespace Api.Controllers.v2;
public sealed class UsuarioControllerTest
{
    private Mock<IUsuarioBusiness<UsuarioDto>> _mockUsuarioBusiness;
    private Mock<IImagemPerfilUsuarioBusiness<ImagemPerfilDto, UsuarioDto>> _mockImagemPerfilBusiness;
    private UsuarioController _usuarioController;
    private List<UsuarioDto> _usuarioDtos;
    private UsuarioDto administrador;
    private UsuarioDto usuarioNormal;
    private Mapper _mapper;

    public UsuarioControllerTest()
    {
        _mockUsuarioBusiness = new Mock<IUsuarioBusiness<UsuarioDto>>();
        _mockImagemPerfilBusiness = new Mock<IImagemPerfilUsuarioBusiness<ImagemPerfilDto, UsuarioDto>>();
        _usuarioController = new UsuarioController(_mockUsuarioBusiness.Object, _mockImagemPerfilBusiness.Object);
        var usuarios = UsuarioFaker.Instance.GetNewFakersUsuarios(20);
        _mapper = new Mapper(new MapperConfiguration(cfg => {  cfg.AddProfile<UsuarioProfile>(); }));
        administrador = _mapper.Map<UsuarioDto>(usuarios.FindAll(u => u.PerfilUsuario == PerfilUsuario.PerfilType.Administrador).First());
        usuarioNormal = _mapper.Map<UsuarioDto>(usuarios.FindAll(u => u.PerfilUsuario == PerfilUsuario.PerfilType.Usuario).First());
        _usuarioDtos = _mapper.Map<List<UsuarioDto>>(usuarios);
    }

    [Fact]
    public void Get_Returns__OkObjectResult_With_Usuario()
    {
        // Arrange
        var usaurios = UsuarioFaker.Instance.GetNewFakersUsuarios(10);
        var usauriosDtos = _mapper.Map<List<UsuarioDto>>(usaurios);
        int idUsuario = usaurios.FindAll(u => u.PerfilUsuario == PerfilUsuario.PerfilType.Usuario).Last().Id;
        Usings.SetupBearerToken(idUsuario, _usuarioController);
        _mockUsuarioBusiness.Setup(business => business.FindById(It.IsAny<int>())).Returns(usauriosDtos.Find(u => u.Id == idUsuario) ?? new());

                // Act
        var result = _usuarioController.GetUsuarioById() as ObjectResult;

        // Assert
        Assert.NotNull(result);
        Assert.IsType<OkObjectResult>(result);
        var usuarioDto = Assert.IsType<UsuarioDto>(result.Value);
        Assert.Equal(idUsuario, usuarioDto.Id);
        _mockUsuarioBusiness.Verify(bussines => bussines.FindById(It.IsAny<int>()), Times.Once);
    }
        
    [Fact]
    public void Put_Should_Update_UsuarioDto()
    {
        // Arrange
        var usuarioDto = _usuarioDtos[4];
        int idUsuario = usuarioDto.Id;
        Usings.SetupBearerToken(idUsuario, _usuarioController);
        _mockUsuarioBusiness.Setup(business => business.Update(It.IsAny<UsuarioDto>())).Returns(usuarioDto);

        // Act
        var result = _usuarioController.Put(usuarioDto) as ObjectResult;

        // Assert
        Assert.NotNull(result);
        Assert.IsType<OkObjectResult>(result);
        Assert.IsType<UsuarioDto>(result.Value);
        _mockUsuarioBusiness.Verify(b => b.Update(It.IsAny<UsuarioDto>()), Times.Once);
    }

    [Fact]
    public void Put_Should_Returns_BadRequest_When_Telefone_IsNull()
    {
        var usaurios = UsuarioFaker.Instance.GetNewFakersUsuarios(10);
        var usuarioDto = _mapper.Map<UsuarioDto>(usaurios.FindAll(u => u.PerfilUsuario == PerfilUsuario.PerfilType.Administrador).First());
        usuarioDto.Telefone = null;
        var usauriosDtos = _mapper.Map<List<UsuarioDto>>(usaurios);
        int idUsuario = usuarioDto.Id;
        //_mockUsuarioBusiness.Setup(business => business.Update(It.IsAny<UsuarioDto>())).Throws(new ArgumentException("Campo Telefone não pode ser em branco"));

        // Act
        var result = _usuarioController.Put(usuarioDto) as ObjectResult;

        // Assert
        Assert.NotNull(result);
        Assert.IsType<BadRequestObjectResult>(result);
        var message = result.Value;
        Assert.Equal("Erro ao atualizar Usuário!", message);
        _mockUsuarioBusiness.Verify(b => b.Update(It.IsAny<UsuarioDto>()), Times.Never);
    }

    [Fact]
    public void Put_Should_Returns_BadRequest_When_Email_IsNull()
    {
        // Arrange
        var usuarioDto = _usuarioDtos.First();
        usuarioDto.Email = string.Empty;
        int idUsuario = usuarioDto.Id;
        Usings.SetupBearerToken(idUsuario, _usuarioController);
        _mockUsuarioBusiness.Setup(business => business.Update(It.IsAny<UsuarioDto>())).Throws(new ArgumentException("Campo Login não pode ser em branco"));

        // Act
        var result = _usuarioController.Put(usuarioDto) as ObjectResult;

        // Assert
        Assert.NotNull(result);
        Assert.IsType<BadRequestObjectResult>(result);
        var message = result.Value;
        Assert.Equal("Campo Login não pode ser em branco", message);
        _mockUsuarioBusiness.Verify(b => b.Update(It.IsAny<UsuarioDto>()), Times.Once);
    }

    [Fact]
    public void Put_Should_Returns_BadRequest_When_Email_IsNullOrWhiteSpace()
    {
        // Arrange
        var usuarioDto = _usuarioDtos.First();
        usuarioDto.Email = " ";
        int idUsuario = usuarioDto.Id;
        Usings.SetupBearerToken(idUsuario, _usuarioController);
        _mockUsuarioBusiness.Setup(business => business.Update(It.IsAny<UsuarioDto>())).Throws(new ArgumentException("Campo Login não pode ser em branco"));

        // Act
        var result = _usuarioController.Put(usuarioDto) as ObjectResult;

        // Assert
        Assert.NotNull(result);
        Assert.IsType<BadRequestObjectResult>(result);
        var message = result.Value;
        Assert.Equal("Campo Login não pode ser em branco", message);
        _mockUsuarioBusiness.Verify(b => b.Update(It.IsAny<UsuarioDto>()), Times.Once);
    }

    [Fact]
    public void Put_Should_Returns_BadRequest_When_Email_IsInvalid()
    {
        // Arrange
        var usuarioDto = _usuarioDtos.First();
        usuarioDto.Email = "invalidEmail.com";
        int idUsuario = usuarioDto.Id;
        Usings.SetupBearerToken(idUsuario, _usuarioController);
        _mockUsuarioBusiness.Setup(business => business.Update(It.IsAny<UsuarioDto>())).Throws(new ArgumentException("Email inválido!"));

        // Act
        var result = _usuarioController.Put(usuarioDto) as ObjectResult;

        // Assert
        Assert.NotNull(result);
        Assert.IsType<BadRequestObjectResult>(result);
        var message = result.Value;
        Assert.Equal("Email inválido!", message);
        _mockUsuarioBusiness.Verify(b => b.Update(It.IsAny<UsuarioDto>()), Times.Once);
    }

    [Fact]
    public void Put_Should_Returns_OkObjectResult_with_Empty_Result_When_Usuario_IsNull()
    {
        // Arrange
        var usuarioDto = usuarioNormal;
        int idUsuario = usuarioDto.Id;
        Usings.SetupBearerToken(idUsuario, _usuarioController);
        _mockUsuarioBusiness.Setup(business => business.Update(It.IsAny<UsuarioDto>())).Throws(new ArgumentException("Usuário não encontrado!"));

        // Act
        var result = _usuarioController.Put(usuarioDto) as ObjectResult;

        // Assert
        Assert.NotNull(result);
        Assert.IsType<BadRequestObjectResult>(result);
        var message = result.Value;
        Assert.Equal("Usuário não encontrado!", message);
        _mockUsuarioBusiness.Verify(b => b.Update(It.IsAny<UsuarioDto>()), Times.Once);
    }    
}
