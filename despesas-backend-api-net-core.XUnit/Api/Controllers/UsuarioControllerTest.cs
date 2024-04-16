using Business.Abstractions;
using Business.Dtos.Parser;
using despesas_backend_api_net_core.Controllers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Api.Controllers;
public class UsuarioControllerTest
{
    protected Mock<IUsuarioBusiness> _mockUsuarioBusiness;
    protected Mock<IImagemPerfilUsuarioBusiness> _mockImagemPerfilBusiness;
    protected UsuarioController _usuarioController;
    protected List<UsuarioDto> _usuarioVMs;
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
        httpContext.Request.Headers["Authorization"] = "Bearer " + Usings.GenerateJwtToken(idUsuario);
        _usuarioController.ControllerContext = new ControllerContext { HttpContext = httpContext };
    }

    public UsuarioControllerTest()
    {
        _mockUsuarioBusiness = new Mock<IUsuarioBusiness>();
        _mockImagemPerfilBusiness = new Mock<IImagemPerfilUsuarioBusiness>();
        _usuarioController = new UsuarioController(_mockUsuarioBusiness.Object, _mockImagemPerfilBusiness.Object);
        var usuarios = UsuarioFaker.Instance.GetNewFakersUsuarios(20);
        administrador = new UsuarioParser().Parse(usuarios.FindAll(u => u.PerfilUsuario == PerfilUsuario.Administrador).First());
        usuarioNormal = new UsuarioParser().Parse(usuarios.FindAll(u => u.PerfilUsuario == PerfilUsuario.Usuario).First());
        _usuarioVMs = new UsuarioParser().ParseList(usuarios);
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
        _mockUsuarioBusiness.Setup(business => business.FindById(idUsuario)).Returns(usauriosVMs.Find(u => u.Id == idUsuario) ?? new());

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
        _mockUsuarioBusiness.Setup(business => business.FindAll(idUsuario)).Returns(_usuarioVMs.FindAll(u => u.Id == idUsuario));
        _mockUsuarioBusiness.Setup(business => business.FindById(idUsuario)).Returns(administrador);

        // Act
        var result = _usuarioController.Get() as ObjectResult;

        // Assert
        Assert.NotNull(result);
        Assert.IsType<OkObjectResult>(result);
        Assert.IsType<List<UsuarioDto>>(result.Value);
        Assert.Equal(_usuarioVMs.FindAll(u => u.Id == idUsuario), result.Value);
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
        var usuarioVM = new UsuarioParser().Parse(usaurios.FindAll(u => u.PerfilUsuario == PerfilUsuario.Usuario).Last());
        var usauriosVMs = new UsuarioParser().ParseList(usaurios);
        int idUsuario = usuarioVM.Id;
        SetupBearerToken(idUsuario);
        _mockUsuarioBusiness.Setup(business => business.FindById(idUsuario)).Returns(usauriosVMs.Find(u => u.Id == idUsuario) ?? new());

        _mockUsuarioBusiness.Setup(business => business.Create(usuarioVM)).Returns(usuarioVM);

        // Act
        var result = _usuarioController.Post(usuarioVM) as ObjectResult;

        // Assert
        Assert.NotNull(result);
        Assert.IsType<BadRequestObjectResult>(result);
        var value = result.Value;
        var message = value?.GetType()?.GetProperty("message")?.GetValue(value, null) as string;
        Assert.Equal("Usuário não permitido a realizar operação!", message);
        _mockUsuarioBusiness.Verify(b => b.FindById(idUsuario), Times.Once);
        _mockUsuarioBusiness.Verify(b => b.Create(usuarioVM), Times.Never);
    }

    [Fact]
    public void Post_Should_Returns_OkResult_When_Usuario_Is_Administrador()
    {
        // Arrange
        var usaurios = UsuarioFaker.Instance.GetNewFakersUsuarios(10);
        var usuarioVM = new UsuarioParser().Parse(usaurios.FindAll(u => u.PerfilUsuario == PerfilUsuario.Administrador).Last());
        var usauriosVMs = new UsuarioParser().ParseList(usaurios);
        int idUsuario = usuarioVM.Id;
        SetupBearerToken(idUsuario);
        _mockUsuarioBusiness.Setup(business => business.FindById(idUsuario)).Returns(usauriosVMs.Find(u => u.Id == idUsuario) ?? new());
        _mockUsuarioBusiness.Setup(business => business.Create(usuarioVM)).Returns(usuarioVM);

        // Act
        var result = _usuarioController.Post(usuarioVM) as ObjectResult;

        // Assert
        Assert.NotNull(result);
        Assert.IsType<OkObjectResult>(result);
        Assert.IsType<UsuarioDto>(result.Value);
        _mockUsuarioBusiness.Verify(b => b.Create(usuarioVM), Times.Once);
    }

    [Fact]
    public void Put_Should_Update_UsuarioVM()
    {
        // Arrange
        var usuarioVM = _usuarioVMs[4];
        int idUsuario = usuarioVM.Id;
        SetupBearerToken(idUsuario);
        _mockUsuarioBusiness.Setup(business => business.Update(usuarioVM)).Returns(usuarioVM);

        // Act
        var result = _usuarioController.Put(usuarioVM) as ObjectResult;

        // Assert
        Assert.NotNull(result);
        Assert.IsType<OkObjectResult>(result);
        Assert.IsType<UsuarioDto>(result.Value);
        _mockUsuarioBusiness.Verify(b => b.Update(usuarioVM), Times.Once);
    }

    [Fact]
    public void Delete_Should_Return_True()
    {
        // Arrange
        var usuarioVM = usuarioNormal;
        int idUsuario = usuarioVM.Id;
        SetupBearerToken(administrador.Id);
        _mockUsuarioBusiness.Setup(business => business.Delete(usuarioVM)).Returns(true);
        _mockUsuarioBusiness.Setup(business => business.FindById(administrador.Id)).Returns(administrador);

        // Act
        var result = _usuarioController.Delete(usuarioVM) as ObjectResult;

        // Assert
        Assert.NotNull(result);
        Assert.IsType<OkObjectResult>(result);
        var value = result.Value;
        var message = (bool)(value?.GetType()?.GetProperty("message")?.GetValue(value, null) ?? false);
        Assert.True(message);
        _mockUsuarioBusiness.Verify(b => b.Delete(usuarioVM), Times.Once);
    }

    [Fact]
    public void Post_Should_Returns_BadRequest_When_Telefone_IsNull()
    {
        // Arrange
        var usaurios = UsuarioFaker.Instance.GetNewFakersUsuarios(10);
        var usuarioVM = new UsuarioParser().Parse(usaurios.FindAll(u => u.PerfilUsuario == PerfilUsuario.Administrador).Last());
        usuarioVM.Telefone = null;
        var usauriosVMs = new UsuarioParser().ParseList(usaurios);
        int idUsuario = usuarioVM.Id;
        SetupBearerToken(idUsuario);
        _mockUsuarioBusiness.Setup(business => business.FindById(idUsuario)).Returns(usauriosVMs.Find(u => u.Id == idUsuario) ?? new());
        _mockUsuarioBusiness.Setup(business => business.Create(usuarioVM)).Returns(usuarioVM);

        // Act
        var result = _usuarioController.Post(usuarioVM) as ObjectResult;

        // Assert
        Assert.NotNull(result);
        Assert.IsType<BadRequestObjectResult>(result);
        var value = result.Value;
        var message = value?.GetType()?.GetProperty("message")?.GetValue(value, null) as string;
        Assert.Equal("Campo Telefone não pode ser em branco", message);
        _mockUsuarioBusiness.Verify(b => b.Create(usuarioVM), Times.Never);
    }

    [Fact]
    public void Post_Should_Returns_BadRequest_When_Email_IsNull()
    {
        // Arrange
        var usaurios = UsuarioFaker.Instance.GetNewFakersUsuarios(10);
        var usuarioVM = new UsuarioParser().Parse(usaurios.FindAll(u => u.PerfilUsuario == PerfilUsuario.Administrador).Last());
        usuarioVM.Email = null;
        var usauriosVMs = new UsuarioParser().ParseList(usaurios);
        int idUsuario = usuarioVM.Id;
        SetupBearerToken(idUsuario);
        _mockUsuarioBusiness.Setup(business => business.FindById(idUsuario)).Returns(usauriosVMs.Find(u => u.Id == idUsuario) ?? new());
        _mockUsuarioBusiness.Setup(business => business.Create(usuarioVM)).Returns(usuarioVM);

        // Act
        var result = _usuarioController.Post(usuarioVM) as ObjectResult;

        // Assert
        Assert.NotNull(result);
        Assert.IsType<BadRequestObjectResult>(result);
        var value = result.Value;
        var message = value?.GetType()?.GetProperty("message")?.GetValue(value, null) as string;
        Assert.Equal("Campo Login não pode ser em branco", message);
        _mockUsuarioBusiness.Verify(b => b.Create(usuarioVM), Times.Never);
    }

    [Fact]
    public void Post_Should_Returns_BadRequest_When_Email_IsNullOrWhiteSpace()
    {
        // Arrange
        var usaurios = UsuarioFaker.Instance.GetNewFakersUsuarios(10);
        var usuarioVM = new UsuarioParser().Parse(usaurios.FindAll(u => u.PerfilUsuario == PerfilUsuario.Administrador).Last());
        usuarioVM.Email = "  ";
        var usauriosVMs = new UsuarioParser().ParseList(usaurios);
        int idUsuario = usuarioVM.Id;
        SetupBearerToken(idUsuario);
        _mockUsuarioBusiness.Setup(business => business.FindById(idUsuario)).Returns(usauriosVMs.Find(u => u.Id == idUsuario) ?? new());
        _mockUsuarioBusiness.Setup(business => business.Create(usuarioVM)).Returns(usuarioVM);

        // Act
        var result = _usuarioController.Post(usuarioVM) as ObjectResult;

        // Assert
        Assert.NotNull(result);
        Assert.IsType<BadRequestObjectResult>(result);
        var value = result.Value;
        var message = value?.GetType()?.GetProperty("message")?.GetValue(value, null) as string;
        Assert.Equal("Campo Login não pode ser em branco", message);
        _mockUsuarioBusiness.Verify(b => b.Create(usuarioVM), Times.Never);
    }

    [Fact]
    public void Post_Should_Returns_BadRequest_When_Email_IsInvalid()
    {
        // Arrange
        var usaurios = UsuarioFaker.Instance.GetNewFakersUsuarios(10);
        var usuarioVM = new UsuarioParser().Parse(usaurios.FindAll(u => u.PerfilUsuario == PerfilUsuario.Administrador).Last());
        usuarioVM.Email = "TestINvalidemail";
        var usauriosVMs = new UsuarioParser().ParseList(usaurios);
        int idUsuario = usuarioVM.Id;
        SetupBearerToken(idUsuario);
        _mockUsuarioBusiness.Setup(business => business.FindById(idUsuario)).Returns(usauriosVMs.Find(u => u.Id == idUsuario) ?? new());
        _mockUsuarioBusiness.Setup(business => business.Create(usuarioVM)).Returns(usuarioVM);

        // Act
        var result = _usuarioController.Post(usuarioVM) as ObjectResult;

        // Assert
        Assert.NotNull(result);
        Assert.IsType<BadRequestObjectResult>(result);
        var value = result.Value;
        var message = value?.GetType()?.GetProperty("message")?.GetValue(value, null) as string;
        Assert.Equal("Email inválido!", message);
        _mockUsuarioBusiness.Verify(b => b.Create(usuarioVM), Times.Never);
    }

    [Fact]
    public void Put_Should_Returns_BadRequest_When_Telefone_IsNull()
    {
        var usaurios = UsuarioFaker.Instance.GetNewFakersUsuarios(10);
        var usuarioVM = new UsuarioParser().Parse(usaurios.FindAll(u => u.PerfilUsuario == PerfilUsuario.Administrador).First());
        usuarioVM.Telefone = null;
        var usauriosVMs = new UsuarioParser().ParseList(usaurios);
        int idUsuario = usuarioVM.Id;
        _mockUsuarioBusiness.Setup(business => business.Update(usuarioVM)).Returns(usuarioVM);

        // Act
        var result = _usuarioController.Put(usuarioVM) as ObjectResult;

        // Assert
        Assert.NotNull(result);
        Assert.IsType<BadRequestObjectResult>(result);
        var value = result.Value;
        var message = value?.GetType()?.GetProperty("message")?.GetValue(value, null) as string;
        Assert.Equal("Campo Telefone não pode ser em branco", message);
        _mockUsuarioBusiness.Verify(b => b.Update(usuarioVM), Times.Never);
    }

    [Fact]
    public void Put_Should_Returns_BadRequest_When_Email_IsNull()
    {
        // Arrange
        var usuarioVM = _usuarioVMs.First();
        usuarioVM.Email = string.Empty;
        int idUsuario = usuarioVM.Id;
        SetupBearerToken(idUsuario);
        _mockUsuarioBusiness.Setup(business => business.Update(usuarioVM)).Returns(usuarioVM);

        // Act
        var result = _usuarioController.Put(usuarioVM) as ObjectResult;

        // Assert
        Assert.NotNull(result);
        Assert.IsType<BadRequestObjectResult>(result);
        var value = result.Value;
        var message = value?.GetType()?.GetProperty("message")?.GetValue(value, null) as string;
        Assert.Equal("Campo Login não pode ser em branco", message);
        _mockUsuarioBusiness.Verify(b => b.Update(usuarioVM), Times.Never);
    }

    [Fact]
    public void Put_Should_Returns_BadRequest_When_Email_IsNullOrWhiteSpace()
    {
        // Arrange
        var usuarioVM = _usuarioVMs.First();
        usuarioVM.Email = " ";
        int idUsuario = usuarioVM.Id;
        SetupBearerToken(idUsuario);
        _mockUsuarioBusiness.Setup(business => business.Update(usuarioVM)).Returns(usuarioVM);

        // Act
        var result = _usuarioController.Put(usuarioVM) as ObjectResult;

        // Assert
        Assert.NotNull(result);
        Assert.IsType<BadRequestObjectResult>(result);
        var value = result.Value;
        var message = value?.GetType()?.GetProperty("message")?.GetValue(value, null) as string;
        Assert.Equal("Campo Login não pode ser em branco", message);
        _mockUsuarioBusiness.Verify(b => b.Update(usuarioVM), Times.Never);
    }

    [Fact]
    public void Put_Should_Returns_BadRequest_When_Email_IsInvalid()
    {
        // Arrange
        var usuarioVM = _usuarioVMs.First();
        usuarioVM.Email = "invalidEmail.com";
        int idUsuario = usuarioVM.Id;
        SetupBearerToken(idUsuario);
        _mockUsuarioBusiness.Setup(business => business.Update(usuarioVM)).Returns(usuarioVM);

        // Act
        var result = _usuarioController.Put(usuarioVM) as ObjectResult;

        // Assert
        Assert.NotNull(result);
        Assert.IsType<BadRequestObjectResult>(result);
        var value = result.Value;
        var message = value?.GetType()?.GetProperty("message")?.GetValue(value, null) as string;
        Assert.Equal("Email inválido!", message);
        _mockUsuarioBusiness.Verify(b => b.Update(usuarioVM), Times.Never);
    }

    [Fact]
    public void Put_Should_Returns_BadRequest_When_Usuario_IsNull()
    {
        // Arrange
        var usuarioVM = _usuarioVMs.First();
        int idUsuario = usuarioVM.Id;
        SetupBearerToken(idUsuario);
        _mockUsuarioBusiness.Setup(business => business.Update(usuarioVM)).Returns((UsuarioDto)null);

        // Act
        var result = _usuarioController.Put(usuarioVM) as ObjectResult;

        // Assert
        Assert.NotNull(result);
        Assert.IsType<BadRequestObjectResult>(result);
        var value = result.Value;
        var message = value?.GetType()?.GetProperty("message")?.GetValue(value, null) as string;
        Assert.Equal("Usuário não encontrado!", message);
        _mockUsuarioBusiness.Verify(b => b.Update(usuarioVM), Times.Once);
    }

    [Fact]
    public void Delete_Should_Returns_BadRequest_When_Usuario_IsNotAdministrador()
    {
        // Arrange
        var usaurios = UsuarioFaker.Instance.GetNewFakersUsuarios(10);
        var usuarioVM = new UsuarioParser().Parse(usaurios.FindAll(u => u.PerfilUsuario == PerfilUsuario.Usuario).Last());
        var usauriosVMs = new UsuarioParser().ParseList(usaurios);
        int idUsuario = usuarioVM.Id;
        SetupBearerToken(idUsuario);
        _mockUsuarioBusiness.Setup(business => business.FindById(idUsuario)).Returns(usauriosVMs.Find(u => u.Id == idUsuario) ?? new());
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
        var usuarioVM = new UsuarioParser().Parse(usaurios.FindAll(u => u.PerfilUsuario == PerfilUsuario.Administrador).Last());
        var usauriosVMs = new UsuarioParser().ParseList(usaurios);
        int idUsuario = usuarioVM.Id;
        SetupBearerToken(idUsuario);
        _mockUsuarioBusiness.Setup(business => business.FindById(idUsuario)).Returns(usauriosVMs.Find(u => u.Id == idUsuario) ?? new());
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
    public void PutAdministrador_Should_Update_UsuarioVM()
    {
        // Arrange
        var usaurios = UsuarioFaker.Instance.GetNewFakersUsuarios(10);
        var usuarioVM = new UsuarioParser().Parse(usaurios.FindAll(u => u.PerfilUsuario == PerfilUsuario.Administrador).Last());
        var usauriosVMs = new UsuarioParser().ParseList(usaurios);
        int idUsuario = usuarioVM.Id;
        SetupBearerToken(idUsuario);
        _mockUsuarioBusiness.Setup(business => business.FindById(idUsuario)).Returns(usauriosVMs.Find(u => u.Id == idUsuario) ?? new());
        SetupBearerToken(idUsuario);
        _mockUsuarioBusiness.Setup(business => business.Update(usuarioVM)).Returns(usuarioVM);

        // Act
        var result = _usuarioController.PutAdministrador(usuarioVM) as ObjectResult;

        // Assert
        Assert.NotNull(result);
        Assert.IsType<OkObjectResult>(result);
        Assert.IsType<UsuarioDto>(result.Value);
        _mockUsuarioBusiness.Verify(b => b.Update(usuarioVM), Times.Once);
    }

    [Fact]
    public void PutAdministrador_Should_Returns_BadRequest_When_Email_IsInvalid()
    {
        // Arrange
        var usaurios = UsuarioFaker.Instance.GetNewFakersUsuarios(10);
        var usuarioVM = new UsuarioParser().Parse(usaurios.FindAll(u => u.PerfilUsuario == PerfilUsuario.Administrador).Last());
        usuarioVM.Email = "TestINvalidemail";
        var usauriosVMs = new UsuarioParser().ParseList(usaurios);
        int idUsuario = usuarioVM.Id;
        SetupBearerToken(idUsuario);
        _mockUsuarioBusiness.Setup(business => business.FindById(idUsuario)).Returns(usauriosVMs.Find(u => u.Id == idUsuario) ?? new());
        _mockUsuarioBusiness.Setup(business => business.Update(usuarioVM)).Returns(usuarioVM);

        // Act
        var result = _usuarioController.PutAdministrador(usuarioVM) as ObjectResult;

        // Assert
        Assert.NotNull(result);
        Assert.IsType<BadRequestObjectResult>(result);
        var value = result.Value;
        var message = value?.GetType()?.GetProperty("message")?.GetValue(value, null) as string;
        Assert.Equal("Email inválido!", message);
        _mockUsuarioBusiness.Verify(b => b.Update(usuarioVM), Times.Never);
    }

    [Fact]
    public void PutAdministrador_Should_Returns_BadRequest_When_Telefone_IsNull()
    {
        // Arrange
        var usaurios = UsuarioFaker.Instance.GetNewFakersUsuarios(10);
        var usuarioVM = new UsuarioParser().Parse(usaurios.FindAll(u => u.PerfilUsuario == PerfilUsuario.Administrador).Last());
        usuarioVM.Telefone = null;
        var usauriosVMs = new UsuarioParser().ParseList(usaurios);
        int idUsuario = usuarioVM.Id;
        SetupBearerToken(idUsuario);
        _mockUsuarioBusiness.Setup(business => business.FindById(idUsuario)).Returns(usauriosVMs.Find(u => u.Id == idUsuario) ?? new());
        _mockUsuarioBusiness.Setup(business => business.Update(usuarioVM)).Returns(usuarioVM);

        // Act
        var result = _usuarioController.PutAdministrador(usuarioVM) as ObjectResult;

        // Assert
        Assert.NotNull(result);
        Assert.IsType<BadRequestObjectResult>(result);
        var value = result.Value;
        var message = value?.GetType()?.GetProperty("message")?.GetValue(value, null) as string;
        Assert.Equal("Campo Telefone não pode ser em branco", message);
        _mockUsuarioBusiness.Verify(b => b.Update(usuarioVM), Times.Never);
    }

    [Fact]
    public void PutAdministrador_Should_Returns_BadRequest_When_Email_IsNull()
    {
        // Arrange
        var usaurios = UsuarioFaker.Instance.GetNewFakersUsuarios(10);
        var usuarioVM = new UsuarioParser().Parse(usaurios.FindAll(u => u.PerfilUsuario == PerfilUsuario.Administrador).Last());
        usuarioVM.Email = null;
        var usauriosVMs = new UsuarioParser().ParseList(usaurios);
        int idUsuario = usuarioVM.Id;
        SetupBearerToken(idUsuario);
        _mockUsuarioBusiness.Setup(business => business.FindById(idUsuario)).Returns(usauriosVMs.Find(u => u.Id == idUsuario) ?? new());

        _mockUsuarioBusiness.Setup(business => business.Update(usuarioVM)).Returns(usuarioVM);

        // Act
        var result = _usuarioController.PutAdministrador(usuarioVM) as ObjectResult;

        // Assert
        Assert.NotNull(result);
        Assert.IsType<BadRequestObjectResult>(result);
        var value = result.Value;
        var message = value?.GetType()?.GetProperty("message")?.GetValue(value, null) as string;
        Assert.Equal("Campo Login não pode ser em branco", message);
        _mockUsuarioBusiness.Verify(b => b.Update(usuarioVM), Times.Never);
    }

    [Fact]
    public void PutAdministrador_Should_Returns_BadRequest_When_Email_IsNullOrWhiteSpace()
    {
        // Arrange
        var usaurios = UsuarioFaker.Instance.GetNewFakersUsuarios(20);
        var usuarioVM = new UsuarioParser().Parse(usaurios.FindAll(u => u.PerfilUsuario == PerfilUsuario.Administrador).Last());
        usuarioVM.Email = " ";
        var usauriosVMs = new UsuarioParser().ParseList(usaurios);
        int idUsuario = usuarioVM.Id;
        SetupBearerToken(idUsuario);
        _mockUsuarioBusiness.Setup(business => business.FindById(idUsuario)).Returns(usauriosVMs.Find(u => u.Id == idUsuario) ?? new());
        _mockUsuarioBusiness.Setup(business => business.Update(usuarioVM)).Returns(usuarioVM);

        // Act
        var result = _usuarioController.PutAdministrador(usuarioVM) as ObjectResult;

        // Assert
        Assert.NotNull(result);
        Assert.IsType<BadRequestObjectResult>(result);
        var value = result.Value;
        var message = value?.GetType()?.GetProperty("message")?.GetValue(value, null) as string;
        Assert.Equal("Campo Login não pode ser em branco", message);
        _mockUsuarioBusiness.Verify(b => b.Update(usuarioVM), Times.Never);
    }

    [Fact]
    public void PutAdministrador_Should_Returns_BadRequest_When_Usuario_IsNull()
    {
        // Arrange
        var usaurios = UsuarioFaker.Instance.GetNewFakersUsuarios(10);
        var usuarioVM = new UsuarioParser().Parse(usaurios.FindAll(u => u.PerfilUsuario == PerfilUsuario.Administrador).Last());
        var usauriosVMs = new UsuarioParser().ParseList(usaurios);
        int idUsuario = usuarioVM.Id;
        SetupBearerToken(idUsuario);
        _mockUsuarioBusiness.Setup(business => business.FindById(idUsuario)).Returns(usauriosVMs.Find(u => u.Id == idUsuario) ?? new());
        _mockUsuarioBusiness.Setup(business => business.Update(usuarioVM)).Returns((UsuarioDto)null);

        // Act
        var result = _usuarioController.PutAdministrador(usuarioVM) as ObjectResult;

        // Assert
        Assert.NotNull(result);
        Assert.IsType<BadRequestObjectResult>(result);
        var value = result.Value;
        var message = value?.GetType()?.GetProperty("message")?.GetValue(value, null) as string;
        Assert.Equal("Usuário não encontrado!", message);
        _mockUsuarioBusiness.Verify(b => b.Update(It.IsAny<UsuarioDto>()), Times.Once);
    }

    [Fact]
    public void PutAdministrador_Should_Returns_BadRequest_When_Usuario_Is_Not_Administrador()
    {
        // Arrange
        var usaurios = UsuarioFaker.Instance.GetNewFakersUsuarios(10);
        var usuarioVM = new UsuarioParser().Parse(usaurios.FindAll(u => u.PerfilUsuario == PerfilUsuario.Usuario).First());
        var usauriosVMs = new UsuarioParser().ParseList(usaurios);
        int idUsuario = usuarioVM.Id;
        SetupBearerToken(idUsuario);
        _mockUsuarioBusiness.Setup(business => business.FindById(idUsuario)).Returns(usauriosVMs.Find(u => u.Id == idUsuario) ?? new());
        _mockUsuarioBusiness.Setup(business => business.Update(usuarioVM)).Returns((UsuarioDto)null);

        // Act
        var result = _usuarioController.PutAdministrador(usuarioVM) as ObjectResult;

        // Assert
        Assert.NotNull(result);
        Assert.IsType<BadRequestObjectResult>(result);
        var value = result.Value;
        var message = value?.GetType()?.GetProperty("message")?.GetValue(value, null) as string;
        Assert.Equal("Usuário não permitido a realizar operação!", message);
        _mockUsuarioBusiness.Verify(b => b.Update(usuarioVM), Times.Never);
    }
}
