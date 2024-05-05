using Business.Abstractions;
using Business.Dtos.Parser;
using despesas_backend_api_net_core.Controllers.v1;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Api.Controllers.v1;

public class UsuarioControllerTest
{
    protected Mock<IUsuarioBusiness> _mockUsuarioBusiness;
    protected Mock<IImagemPerfilUsuarioBusiness> _mockImagemPerfilBusiness;
    protected UsuarioController _usuarioController;
    protected List<UsuarioDto> _usuarioDtos;
    private UsuarioDto administrador;
    private UsuarioDto usuarioNormal;

    private void SetupBearerToken(int idUsuario)
    {
        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, idUsuario.ToString())
        };
        var identity = new ClaimsIdentity(claims, "IdUsuario");
        var claimsPrincipal = new ClaimsPrincipal(identity);

        var httpContext = new DefaultHttpContext { User = claimsPrincipal };
        httpContext.Request.Headers["Authorization"] =
            "Bearer " + Usings.GenerateJwtToken(idUsuario);

        _usuarioController.ControllerContext = new ControllerContext
        {
            HttpContext = httpContext
        };
    }

    public UsuarioControllerTest()
    {
        _mockUsuarioBusiness = new Mock<IUsuarioBusiness>();
        _mockImagemPerfilBusiness = new Mock<IImagemPerfilUsuarioBusiness>();
        _usuarioController = new UsuarioController(_mockUsuarioBusiness.Object, _mockImagemPerfilBusiness.Object);
        var usuarios = UsuarioFaker.Instance.GetNewFakersUsuarios(20);
        administrador = new UsuarioParser().Parse(usuarios.FindAll(u => u.PerfilUsuario == PerfilUsuario.Administrador).First());
        usuarioNormal = new UsuarioParser().Parse(usuarios.FindAll(u => u.PerfilUsuario == PerfilUsuario.Usuario).First());
        _usuarioDtos = new UsuarioParser().ParseList(usuarios);
    }

    [Fact]
    public void Get_With_Usuario_Normal_Returns_BadRequest()
    {
        // Arrange
        var usaurios = UsuarioFaker.Instance.GetNewFakersUsuarios(10);
        var usauriosVMs = new UsuarioParser().ParseList(usaurios);
        int idUsuario = usaurios.FindAll(u => u.PerfilUsuario == PerfilUsuario.Usuario).Last().Id;
        SetupBearerToken(idUsuario);
        _mockUsuarioBusiness.Setup(business => business.FindAll(idUsuario)).Returns(usauriosVMs.FindAll(u => u.Id == idUsuario));
        _mockUsuarioBusiness.Setup(business => business.FindById(idUsuario)).Returns(usauriosVMs.Find(u => u.Id == idUsuario));

        // Act
        var result = _usuarioController.Get() as ObjectResult;

        // Assert
        Assert.NotNull(result);
        Assert.IsType<BadRequestObjectResult>(result);
        var value = result.Value;
        var message = value?.GetType()?.GetProperty("message")?.GetValue(value, null) as string;
        Assert.Equal("Usuário não permitido a realizar operação!", message);
        _mockUsuarioBusiness.Verify(b => b.FindAll(idUsuario), Times.Never);
    }

    [Fact]
    public void Get_Should_Returns_OkResult_With_Usuarios()
    {
        // Arrange
        int idUsuario = administrador.Id;
        SetupBearerToken(idUsuario);
        _mockUsuarioBusiness.Setup(business => business.FindAll(idUsuario)).Returns(_usuarioDtos.FindAll(u => u.Id == idUsuario));
        _mockUsuarioBusiness.Setup(business => business.FindById(idUsuario)).Returns(administrador);

        // Act
        var result = _usuarioController.Get() as ObjectResult;

        // Assert
        Assert.NotNull(result);
        Assert.IsType<OkObjectResult>(result);
        Assert.IsType<List<UsuarioDto>>(result.Value);
        Assert.Equal(_usuarioDtos.FindAll(u => u.Id == idUsuario), result.Value);
        _mockUsuarioBusiness.Verify(b => b.FindAll(idUsuario), Times.Once);
    }

    [Fact]
    public void GetUsuario_Should_Returns_BadRequest()
    {
        // Arrange
        int idUsuario = usuarioNormal.Id;
        SetupBearerToken(idUsuario);
        _mockUsuarioBusiness.Setup(business => business.FindById(idUsuario)).Returns((UsuarioDto)null);

        // Act
        var result = _usuarioController.GetUsuario() as ObjectResult;

        // Assert
        Assert.NotNull(result);
        Assert.IsType<BadRequestObjectResult>(result);
        var value = result.Value;
        var message = value?.GetType()?.GetProperty("message")?.GetValue(value, null) as string;
        Assert.Equal("Usuário não encontrado!", message);
        _mockUsuarioBusiness.Verify(b => b.FindById(idUsuario), Times.Once);
    }

    [Fact]
    public void GetUsuario_Should_Returns_OkResult_When_Usuario_Normal()
    {
        // Arrange
        int idUsuario = usuarioNormal.Id;
        SetupBearerToken(idUsuario);
        _mockUsuarioBusiness.Setup(business => business.FindById(idUsuario)).Returns(usuarioNormal);

        // Act
        var result = _usuarioController.GetUsuario() as ObjectResult;

        // Assert
        Assert.NotNull(result);
        Assert.IsType<OkObjectResult>(result);
        Assert.IsType<UsuarioDto>(result.Value);
        _mockUsuarioBusiness.Verify(b => b.FindById(idUsuario), Times.Once);
    }

    [Fact]
    public void Post_Should_Returns_BadRequest_When_Usuario_Is_Not_Administrador()
    {
        // Arrange
        var usaurios = UsuarioFaker.Instance.GetNewFakersUsuarios(10);
        var usuarioDto = new UsuarioParser().Parse(usaurios.FindAll(u => u.PerfilUsuario == PerfilUsuario.Usuario).Last());
        var usauriosVMs = new UsuarioParser().ParseList(usaurios);
        int idUsuario = usuarioDto.Id;
        SetupBearerToken(idUsuario);
        _mockUsuarioBusiness.Setup(business => business.FindById(idUsuario)).Returns(usauriosVMs.Find(u => u.Id == idUsuario));
        _mockUsuarioBusiness.Setup(business => business.Create(usuarioDto)).Returns(usuarioDto);

        // Act
        var result = _usuarioController.Post(usuarioDto) as ObjectResult;

        // Assert
        Assert.NotNull(result);
        Assert.IsType<BadRequestObjectResult>(result);
        var value = result.Value;
        var message = value?.GetType()?.GetProperty("message")?.GetValue(value, null) as string;
        Assert.Equal("Usuário não permitido a realizar operação!", message);
        _mockUsuarioBusiness.Verify(b => b.FindById(idUsuario), Times.Once);
        _mockUsuarioBusiness.Verify(b => b.Create(usuarioDto), Times.Never);
    }

    [Fact]
    public void Post_Should_Returns_OkResult_When_Usuario_Is_Administrador()
    {
        // Arrange
        var usaurios = UsuarioFaker.Instance.GetNewFakersUsuarios(10);
        var usuarioDto = new UsuarioParser().Parse(usaurios.FindAll(u => u.PerfilUsuario == PerfilUsuario.Administrador).Last());
        var usauriosVMs = new UsuarioParser().ParseList(usaurios);
        int idUsuario = usuarioDto.Id;
        SetupBearerToken(idUsuario);
        _mockUsuarioBusiness.Setup(business => business.FindById(idUsuario)).Returns(usauriosVMs.Find(u => u.Id == idUsuario));
        _mockUsuarioBusiness.Setup(business => business.Create(usuarioDto)).Returns(usuarioDto);

        // Act
        var result = _usuarioController.Post(usuarioDto) as ObjectResult;

        // Assert
        Assert.NotNull(result);
        Assert.IsType<OkObjectResult>(result);
        Assert.IsType<UsuarioDto>(result.Value);
        _mockUsuarioBusiness.Verify(b => b.Create(usuarioDto), Times.Once);
    }

    [Fact]
    public void Put_Should_Update_UsuarioDto()
    {
        // Arrange
        var usuarioDto = _usuarioDtos[1];
        int idUsuario = usuarioDto.Id;
        SetupBearerToken(idUsuario);
        _mockUsuarioBusiness.Setup(business => business.Update(usuarioDto)).Returns(usuarioDto);

        // Act
        var result = _usuarioController.Put(usuarioDto) as ObjectResult;

        // Assert
        Assert.NotNull(result);
        Assert.IsType<OkObjectResult>(result);
        Assert.IsType<UsuarioDto>(result.Value);
        _mockUsuarioBusiness.Verify(b => b.Update(usuarioDto), Times.Once);
    }

    [Fact]
    public void Delete_Should_Return_True()
    {
        // Arrange
        var usuarioDto = usuarioNormal;
        int idUsuario = usuarioDto.Id;
        SetupBearerToken(administrador.Id);
        _mockUsuarioBusiness.Setup(business => business.Delete(usuarioDto)).Returns(true);
        _mockUsuarioBusiness.Setup(business => business.FindById(administrador.Id)).Returns(administrador);

        // Act
        var result = _usuarioController.Delete(usuarioDto) as ObjectResult;

        // Assert
        Assert.NotNull(result);
        Assert.IsType<OkObjectResult>(result);
        var value = result.Value;

        var message = (bool)value.GetType().GetProperty("message").GetValue(value, null);

        Assert.True(message);
        _mockUsuarioBusiness.Verify(b => b.Delete(usuarioDto), Times.Once);
    }

    [Fact]
    public void Post_Should_Returns_BadRequest_When_Telefone_IsNull()
    {
        // Arrange
        var usaurios = UsuarioFaker.Instance.GetNewFakersUsuarios(10);
        var usuarioDto = new UsuarioParser().Parse(usaurios.FindAll(u => u.PerfilUsuario == PerfilUsuario.Administrador).Last());
        usuarioDto.Telefone = null;
        var usauriosVMs = new UsuarioParser().ParseList(usaurios);
        int idUsuario = usuarioDto.Id;
        SetupBearerToken(idUsuario);
        _mockUsuarioBusiness.Setup(business => business.FindById(idUsuario)).Returns(usauriosVMs.Find(u => u.Id == idUsuario));
        _mockUsuarioBusiness.Setup(business => business.Create(usuarioDto)).Returns(usuarioDto);

        // Act
        var result = _usuarioController.Post(usuarioDto) as ObjectResult;

        // Assert
        Assert.NotNull(result);
        Assert.IsType<BadRequestObjectResult>(result);
        var value = result.Value;
        var message = value?.GetType()?.GetProperty("message")?.GetValue(value, null) as string;
        Assert.Equal("Campo Telefone não pode ser em branco", message);
        _mockUsuarioBusiness.Verify(b => b.Create(usuarioDto), Times.Never);
    }

    [Fact]
    public void Post_Should_Returns_BadRequest_When_Email_IsNull()
    {
        // Arrange
        var usaurios = UsuarioFaker.Instance.GetNewFakersUsuarios(10);
        var usuarioDto = new UsuarioParser().Parse(usaurios.FindAll(u => u.PerfilUsuario == PerfilUsuario.Administrador).Last());
        usuarioDto.Email = null;
        var usauriosVMs = new UsuarioParser().ParseList(usaurios);
        int idUsuario = usuarioDto.Id;
        SetupBearerToken(idUsuario);
        _mockUsuarioBusiness.Setup(business => business.FindById(idUsuario)).Returns(usauriosVMs.Find(u => u.Id == idUsuario));
        _mockUsuarioBusiness.Setup(business => business.Create(usuarioDto)).Returns(usuarioDto);

        // Act
        var result = _usuarioController.Post(usuarioDto) as ObjectResult;

        // Assert
        Assert.NotNull(result);
        Assert.IsType<BadRequestObjectResult>(result);
        var value = result.Value;
        var message = value?.GetType()?.GetProperty("message")?.GetValue(value, null) as string;
        Assert.Equal("Campo Login não pode ser em branco", message);
        _mockUsuarioBusiness.Verify(b => b.Create(usuarioDto), Times.Never);
    }

    [Fact]
    public void Post_Should_Returns_BadRequest_When_Email_IsNullOrWhiteSpace()
    {
        // Arrange
        var usaurios = UsuarioFaker.Instance.GetNewFakersUsuarios(10);
        var usuarioDto = new UsuarioParser().Parse(usaurios.FindAll(u => u.PerfilUsuario == PerfilUsuario.Administrador).Last());
        usuarioDto.Email = "  ";
        var usauriosVMs = new UsuarioParser().ParseList(usaurios);
        int idUsuario = usuarioDto.Id;
        SetupBearerToken(idUsuario);
        _mockUsuarioBusiness.Setup(business => business.FindById(idUsuario)).Returns(usauriosVMs.Find(u => u.Id == idUsuario));
        _mockUsuarioBusiness.Setup(business => business.Create(usuarioDto)).Returns(usuarioDto);

        // Act
        var result = _usuarioController.Post(usuarioDto) as ObjectResult;

        // Assert
        Assert.NotNull(result);
        Assert.IsType<BadRequestObjectResult>(result);
        var value = result.Value;
        var message = value?.GetType()?.GetProperty("message")?.GetValue(value, null) as string;
        Assert.Equal("Campo Login não pode ser em branco", message);
        _mockUsuarioBusiness.Verify(b => b.Create(usuarioDto), Times.Never);
    }

    [Fact]
    public void Post_Should_Returns_BadRequest_When_Email_IsInvalid()
    {
        // Arrange
        var usaurios = UsuarioFaker.Instance.GetNewFakersUsuarios(10);
        var usuarioDto = new UsuarioParser().Parse(usaurios.FindAll(u => u.PerfilUsuario == PerfilUsuario.Administrador).Last());
        usuarioDto.Email = "TestINvalidemail";
        var usauriosVMs = new UsuarioParser().ParseList(usaurios);
        int idUsuario = usuarioDto.Id;
        SetupBearerToken(idUsuario);
        _mockUsuarioBusiness.Setup(business => business.FindById(idUsuario)).Returns(usauriosVMs.Find(u => u.Id == idUsuario));
        _mockUsuarioBusiness.Setup(business => business.Create(usuarioDto)).Returns(usuarioDto);

        // Act
        var result = _usuarioController.Post(usuarioDto) as ObjectResult;

        // Assert
        Assert.NotNull(result);
        Assert.IsType<BadRequestObjectResult>(result);
        var value = result.Value;
        var message = value?.GetType()?.GetProperty("message")?.GetValue(value, null) as string;
        Assert.Equal("Email inválido!", message);
        _mockUsuarioBusiness.Verify(b => b.Create(usuarioDto), Times.Never);
    }

    [Fact]
    public void Put_Should_Returns_BadRequest_When_Telefone_IsNull()
    {
        var usaurios = UsuarioFaker.Instance.GetNewFakersUsuarios(10);
        var usuarioDto = new UsuarioParser().Parse(usaurios.FindAll(u => u.PerfilUsuario == PerfilUsuario.Administrador).First());
        usuarioDto.Telefone = null;
        var usauriosVMs = new UsuarioParser().ParseList(usaurios);
        int idUsuario = usuarioDto.Id;
        _mockUsuarioBusiness.Setup(business => business.Update(usuarioDto)).Returns(usuarioDto);

        // Act
        var result = _usuarioController.Put(usuarioDto) as ObjectResult;

        // Assert
        Assert.NotNull(result);
        Assert.IsType<BadRequestObjectResult>(result);
        var value = result.Value;
        var message = value?.GetType()?.GetProperty("message")?.GetValue(value, null) as string;
        Assert.Equal("Campo Telefone não pode ser em branco", message);
        _mockUsuarioBusiness.Verify(b => b.Update(usuarioDto), Times.Never);
    }

    [Fact]
    public void Put_Should_Returns_BadRequest_When_Email_IsNull()
    {
        // Arrange
        var usuarioDto = _usuarioDtos.First();
        usuarioDto.Email = string.Empty;
        int idUsuario = usuarioDto.Id;
        SetupBearerToken(idUsuario);
        _mockUsuarioBusiness.Setup(business => business.Update(usuarioDto)).Returns(usuarioDto);

        // Act
        var result = _usuarioController.Put(usuarioDto) as ObjectResult;

        // Assert
        Assert.NotNull(result);
        Assert.IsType<BadRequestObjectResult>(result);
        var value = result.Value;
        var message = value?.GetType()?.GetProperty("message")?.GetValue(value, null) as string;
        Assert.Equal("Campo Login não pode ser em branco", message);
        _mockUsuarioBusiness.Verify(b => b.Update(usuarioDto), Times.Never);
    }

    [Fact]
    public void Put_Should_Returns_BadRequest_When_Email_IsNullOrWhiteSpace()
    {
        // Arrange
        var usuarioDto = _usuarioDtos.First();
        usuarioDto.Email = " ";
        int idUsuario = usuarioDto.Id;
        SetupBearerToken(idUsuario);
        _mockUsuarioBusiness.Setup(business => business.Update(usuarioDto)).Returns(usuarioDto);

        // Act
        var result = _usuarioController.Put(usuarioDto) as ObjectResult;

        // Assert
        Assert.NotNull(result);
        Assert.IsType<BadRequestObjectResult>(result);
        var value = result.Value;
        var message = value?.GetType()?.GetProperty("message")?.GetValue(value, null) as string;
        Assert.Equal("Campo Login não pode ser em branco", message);
        _mockUsuarioBusiness.Verify(b => b.Update(usuarioDto), Times.Never);
    }

    [Fact]
    public void Put_Should_Returns_BadRequest_When_Email_IsInvalid()
    {
        // Arrange
        var usuarioDto = _usuarioDtos.First();
        usuarioDto.Email = "invalidEmail.com";
        int idUsuario = usuarioDto.Id;
        SetupBearerToken(idUsuario);
        _mockUsuarioBusiness.Setup(business => business.Update(usuarioDto)).Returns(usuarioDto);

        // Act
        var result = _usuarioController.Put(usuarioDto) as ObjectResult;

        // Assert
        Assert.NotNull(result);
        Assert.IsType<BadRequestObjectResult>(result);
        var value = result.Value;
        var message = value?.GetType()?.GetProperty("message")?.GetValue(value, null) as string;
        Assert.Equal("Email inválido!", message);
        _mockUsuarioBusiness.Verify(b => b.Update(usuarioDto), Times.Never);
    }

    [Fact]
    public void Put_Should_Returns_BadRequest_When_Usuario_IsNull()
    {
        // Arrange
        var usuarioDto = _usuarioDtos.First();
        int idUsuario = usuarioDto.Id;
        SetupBearerToken(idUsuario);
        _mockUsuarioBusiness.Setup(business => business.Update(usuarioDto)).Returns((UsuarioDto)null);

        // Act
        var result = _usuarioController.Put(usuarioDto) as ObjectResult;

        // Assert
        Assert.NotNull(result);
        Assert.IsType<BadRequestObjectResult>(result);
        var value = result.Value;
        var message = value?.GetType()?.GetProperty("message")?.GetValue(value, null) as string;
        Assert.Equal("Usuário não encontrado!", message);
        _mockUsuarioBusiness.Verify(b => b.Update(usuarioDto), Times.Once);
    }

    [Fact]
    public void Delete_Should_Returns_BadRequest_When_Usuario_IsNotAdministrador()
    {
        // Arrange
        var usaurios = UsuarioFaker.Instance.GetNewFakersUsuarios(10);
        var usuarioDto = new UsuarioParser().Parse(usaurios.FindAll(u => u.PerfilUsuario == PerfilUsuario.Usuario).Last());
        var usauriosVMs = new UsuarioParser().ParseList(usaurios);
        int idUsuario = usuarioDto.Id;
        SetupBearerToken(idUsuario);
        _mockUsuarioBusiness.Setup(business => business.FindById(idUsuario)).Returns(usauriosVMs.Find(u => u.Id == idUsuario));
        _mockUsuarioBusiness.Setup(business => business.Delete(usuarioNormal)).Returns(false);

        // Act
        var result = _usuarioController.Delete(usuarioNormal) as ObjectResult;

        // Assert
        Assert.NotNull(result);
        Assert.IsType<BadRequestObjectResult>(result);
        var value = result.Value;
        var message = value?.GetType()?.GetProperty("message")?.GetValue(value, null) as string;
        Assert.Equal("Usuário não permitido a realizar operação!", message);
        _mockUsuarioBusiness.Verify(b => b.FindById(idUsuario), Times.Once);
        _mockUsuarioBusiness.Verify(b => b.Delete(usuarioNormal), Times.Never);
    }

    [Fact]
    public void Delete_Should_Returns_BadRequest_When_Try_To_Delete_Usuario()
    {
        // Arrange
        var usaurios = UsuarioFaker.Instance.GetNewFakersUsuarios(10);
        var usuarioDto = new UsuarioParser().Parse(usaurios.FindAll(u => u.PerfilUsuario == PerfilUsuario.Administrador).Last());
        var usauriosVMs = new UsuarioParser().ParseList(usaurios);
        int idUsuario = usuarioDto.Id;
        SetupBearerToken(idUsuario);
        _mockUsuarioBusiness.Setup(business => business.FindById(idUsuario)).Returns(usauriosVMs.Find(u => u.Id == idUsuario));
        _mockUsuarioBusiness.Setup(business => business.Delete(usuarioNormal)).Returns(false);

        // Act
        var result = _usuarioController.Delete(usuarioNormal) as ObjectResult;

        // Assert
        Assert.NotNull(result);
        Assert.IsType<BadRequestObjectResult>(result);
        var value = result.Value;
        var message = value?.GetType()?.GetProperty("message")?.GetValue(value, null) as string;
        Assert.Equal("Erro ao excluir Usuário!", message);
        _mockUsuarioBusiness.Verify(b => b.FindById(idUsuario), Times.Once);
        _mockUsuarioBusiness.Verify(b => b.Delete(usuarioNormal), Times.Once);
    }

    [Fact]
    public void PutAdministrador_Should_Update_UsuarioDto()
    {
        // Arrange
        // Arrange
        var usaurios = UsuarioFaker.Instance.GetNewFakersUsuarios(10);
        var usuarioDto = new UsuarioParser().Parse(usaurios.FindAll(u => u.PerfilUsuario == PerfilUsuario.Administrador).Last());
        var usauriosVMs = new UsuarioParser().ParseList(usaurios);
        int idUsuario = usuarioDto.Id;
        SetupBearerToken(idUsuario);
        _mockUsuarioBusiness.Setup(business => business.FindById(idUsuario)).Returns(usauriosVMs.Find(u => u.Id == idUsuario));
        SetupBearerToken(idUsuario);
        _mockUsuarioBusiness.Setup(business => business.Update(usuarioDto)).Returns(usuarioDto);

        // Act
        var result = _usuarioController.PutAdministrador(usuarioDto) as ObjectResult;

        // Assert
        Assert.NotNull(result);
        Assert.IsType<OkObjectResult>(result);
        Assert.IsType<UsuarioDto>(result.Value);
        _mockUsuarioBusiness.Verify(b => b.Update(usuarioDto), Times.Once);
    }

    [Fact]
    public void PutAdministrador_Should_Returns_BadRequest_When_Email_IsInvalid()
    {
        // Arrange
        var usaurios = UsuarioFaker.Instance.GetNewFakersUsuarios(10);
        var usuarioDto = new UsuarioParser().Parse(usaurios.FindAll(u => u.PerfilUsuario == PerfilUsuario.Administrador).Last());
        usuarioDto.Email = "TestINvalidemail";
        var usauriosVMs = new UsuarioParser().ParseList(usaurios);
        int idUsuario = usuarioDto.Id;
        SetupBearerToken(idUsuario);
        _mockUsuarioBusiness.Setup(business => business.FindById(idUsuario)).Returns(usauriosVMs.Find(u => u.Id == idUsuario));
        _mockUsuarioBusiness.Setup(business => business.Update(usuarioDto)).Returns(usuarioDto);

        // Act
        var result = _usuarioController.PutAdministrador(usuarioDto) as ObjectResult;

        // Assert
        Assert.NotNull(result);
        Assert.IsType<BadRequestObjectResult>(result);
        var value = result.Value;
        var message = value?.GetType()?.GetProperty("message")?.GetValue(value, null) as string;
        Assert.Equal("Email inválido!", message);
        _mockUsuarioBusiness.Verify(b => b.Update(usuarioDto), Times.Never);
    }

    [Fact]
    public void PutAdministrador_Should_Returns_BadRequest_When_Telefone_IsNull()
    {
        // Arrange
        var usaurios = UsuarioFaker.Instance.GetNewFakersUsuarios(10);
        var usuarioDto = new UsuarioParser().Parse(usaurios.FindAll(u => u.PerfilUsuario == PerfilUsuario.Administrador).Last());
        usuarioDto.Telefone = null;
        var usauriosVMs = new UsuarioParser().ParseList(usaurios);
        int idUsuario = usuarioDto.Id;
        SetupBearerToken(idUsuario);
        _mockUsuarioBusiness.Setup(business => business.FindById(idUsuario)).Returns(usauriosVMs.Find(u => u.Id == idUsuario));
        _mockUsuarioBusiness.Setup(business => business.Update(usuarioDto)).Returns(usuarioDto);

        // Act
        var result = _usuarioController.PutAdministrador(usuarioDto) as ObjectResult;

        // Assert
        Assert.NotNull(result);
        Assert.IsType<BadRequestObjectResult>(result);
        var value = result.Value;
        var message = value?.GetType()?.GetProperty("message")?.GetValue(value, null) as string;
        Assert.Equal("Campo Telefone não pode ser em branco", message);
        _mockUsuarioBusiness.Verify(b => b.Update(usuarioDto), Times.Never);
    }

    [Fact]
    public void PutAdministrador_Should_Returns_BadRequest_When_Email_IsNull()
    {
        // Arrange
        var usaurios = UsuarioFaker.Instance.GetNewFakersUsuarios(10);
        var usuarioDto = new UsuarioParser().Parse(usaurios.FindAll(u => u.PerfilUsuario == PerfilUsuario.Administrador).Last());
        usuarioDto.Email = null;
        var usauriosVMs = new UsuarioParser().ParseList(usaurios);
        int idUsuario = usuarioDto.Id;
        SetupBearerToken(idUsuario);
        _mockUsuarioBusiness.Setup(business => business.FindById(idUsuario)).Returns(usauriosVMs.Find(u => u.Id == idUsuario));
        _mockUsuarioBusiness.Setup(business => business.Update(usuarioDto)).Returns(usuarioDto);

        // Act
        var result = _usuarioController.PutAdministrador(usuarioDto) as ObjectResult;

        // Assert
        Assert.NotNull(result);
        Assert.IsType<BadRequestObjectResult>(result);
        var value = result.Value;
        var message = value?.GetType()?.GetProperty("message")?.GetValue(value, null) as string;
        Assert.Equal("Campo Login não pode ser em branco", message);
        _mockUsuarioBusiness.Verify(b => b.Update(usuarioDto), Times.Never);
    }

    [Fact]
    public void PutAdministrador_Should_Returns_BadRequest_When_Email_IsNullOrWhiteSpace()
    {
        // Arrange
        var usaurios = UsuarioFaker.Instance.GetNewFakersUsuarios(10);
        var usuarioDto = new UsuarioParser().Parse(usaurios.FindAll(u => u.PerfilUsuario == PerfilUsuario.Administrador).Last());
        usuarioDto.Email = " ";
        var usauriosVMs = new UsuarioParser().ParseList(usaurios);
        int idUsuario = usuarioDto.Id;
        SetupBearerToken(idUsuario);
        _mockUsuarioBusiness.Setup(business => business.FindById(idUsuario)).Returns(usauriosVMs.Find(u => u.Id == idUsuario));
        _mockUsuarioBusiness.Setup(business => business.Update(usuarioDto)).Returns(usuarioDto);

        // Act
        var result = _usuarioController.PutAdministrador(usuarioDto) as ObjectResult;

        // Assert
        Assert.NotNull(result);
        Assert.IsType<BadRequestObjectResult>(result);
        var value = result.Value;
        var message = value?.GetType()?.GetProperty("message")?.GetValue(value, null) as string;
        Assert.Equal("Campo Login não pode ser em branco", message);
        _mockUsuarioBusiness.Verify(b => b.Update(usuarioDto), Times.Never);
    }

    [Fact]
    public void PutAdministrador_Should_Returns_BadRequest_When_Usuario_IsNull()
    {
        // Arrange
        var usaurios = UsuarioFaker.Instance.GetNewFakersUsuarios(10);
        var usuarioDto = new UsuarioParser().Parse(usaurios.FindAll(u => u.PerfilUsuario == PerfilUsuario.Administrador).Last());
        var usauriosVMs = new UsuarioParser().ParseList(usaurios);
        int idUsuario = usuarioDto.Id;
        SetupBearerToken(idUsuario);
        _mockUsuarioBusiness.Setup(business => business.FindById(idUsuario)).Returns(usauriosVMs.Find(u => u.Id == idUsuario));
        _mockUsuarioBusiness.Setup(business => business.Update(usuarioDto)).Returns((UsuarioDto)null);

        // Act
        var result = _usuarioController.PutAdministrador(usuarioDto) as ObjectResult;

        // Assert
        Assert.NotNull(result);
        Assert.IsType<BadRequestObjectResult>(result);
        var value = result.Value;
        var message = value?.GetType()?.GetProperty("message")?.GetValue(value, null) as string;
        Assert.Equal("Usuário não encontrado!", message);

        _mockUsuarioBusiness.Verify(b => b.Update(null), Times.Never);
    }

    [Fact]
    public void PutAdministrador_Should_Returns_BadRequest_When_Usuario_Is_Not_Administrador()
    {
        // Arrange
        var usaurios = UsuarioFaker.Instance.GetNewFakersUsuarios(10);
        var usuarioDto = new UsuarioParser().Parse(usaurios.FindAll(u => u.PerfilUsuario == PerfilUsuario.Usuario).First());
        var usauriosVMs = new UsuarioParser().ParseList(usaurios);
        int idUsuario = usuarioDto.Id;
        SetupBearerToken(idUsuario);
        _mockUsuarioBusiness.Setup(business => business.FindById(idUsuario)).Returns(usauriosVMs.Find(u => u.Id == idUsuario));
        _mockUsuarioBusiness.Setup(business => business.Update(usuarioDto)).Returns((UsuarioDto)null);

        // Act
        var result = _usuarioController.PutAdministrador(usuarioDto) as ObjectResult;

        // Assert
        Assert.NotNull(result);
        Assert.IsType<BadRequestObjectResult>(result);
        var value = result.Value;
        var message = value?.GetType()?.GetProperty("message")?.GetValue(value, null) as string;
        Assert.Equal("Usuário não permitido a realizar operação!", message);
        _mockUsuarioBusiness.Verify(b => b.Update(usuarioDto), Times.Never);
    }
}
