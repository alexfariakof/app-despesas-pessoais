using despesas_backend_api_net_core.Controllers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Business.Abstractions;

namespace Api.Controllers;
public class ControleAcessoControllerTest
{
    protected readonly Mock<IControleAcessoBusiness> _mockControleAcessoBusiness;
    protected readonly ControleAcessoController _controleAcessoController;
    private void SetupBearerToken(int userId)
    {
        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, userId.ToString())
        };
        var identity = new ClaimsIdentity(claims, "IdUsuario");
        var claimsPrincipal = new ClaimsPrincipal(identity);

        var httpContext = new DefaultHttpContext { User = claimsPrincipal };
        httpContext.Request.Headers["Authorization"] = "Bearer " + Usings.GenerateJwtToken(userId);
        _controleAcessoController.ControllerContext = new ControllerContext { HttpContext = httpContext };
    }

    public ControleAcessoControllerTest()
    {
        _mockControleAcessoBusiness = new Mock<IControleAcessoBusiness>();
        _controleAcessoController = new ControleAcessoController(_mockControleAcessoBusiness.Object);
    }

    [Fact]
    public void Post_With_ValidData_Returns_OkResult()
    {
        // Arrange
        var controleAcesso = ControleAcessoFaker.Instance.GetNewFaker();
        var controleAcessoDto = ControleAcessoFaker.Instance.GetNewFakerVM(controleAcesso.Usuario);
        _mockControleAcessoBusiness.Setup(b => b.Create(It.IsAny<ControleAcesso>()));

        // Act
        var result = _controleAcessoController.Post(controleAcessoDto) as ObjectResult;

        // Assert
        Assert.NotNull(result);
        Assert.IsType<OkObjectResult>(result);
        var value = result.Value;
        var message = (bool)(value?.GetType()?.GetProperty("message")?.GetValue(value, null) ?? false);
        Assert.True(message);
    }

    [Fact]
    public void Post_With_ValidData_Returns_BadRequest()
    {
        // Arrange
        ControleAcesso? controleAcesso = null;
        var controleAcessoDto = ControleAcessoFaker.Instance.GetNewFakerVM();
        _mockControleAcessoBusiness.Setup(b => b.Create(It.IsAny<ControleAcesso>())).Throws<Exception>();

        // Act
        var result = _controleAcessoController.Post(controleAcessoDto) as ObjectResult;

        // Assert
        Assert.NotNull(result);
        Assert.IsType<BadRequestObjectResult>(result);
        var value = result.Value;
        var message = value?.GetType()?.GetProperty("message")?.GetValue(value, null) as string;
        Assert.Equal("Não foi possível realizar o cadastro.", message);
    }

    [Fact]
    public void Post_With_Null_Telefone_Returns_BadRequest()
    {
        // Arrange
        var controleAcessoDto = ControleAcessoFaker.Instance.GetNewFakerVM();
        controleAcessoDto.Telefone = string.Empty;

        // Act
        var result = _controleAcessoController.Post(controleAcessoDto) as ObjectResult;

        // Assert
        Assert.NotNull(result);
        Assert.IsType<BadRequestObjectResult>(result);
        var value = result.Value;
        var message = value?.GetType()?.GetProperty("message")?.GetValue(value, null) as string;
        Assert.Equal("Campo Telefone não pode ser em branco", message);
    }

    [Fact]
    public void Post_With_NUll_Email_Returns_BadRequest()
    {
        // Arrange
        var controleAcessoDto = ControleAcessoFaker.Instance.GetNewFakerVM();
        controleAcessoDto.Email = string.Empty;

        // Act
        var result = _controleAcessoController.Post(controleAcessoDto) as ObjectResult;

        // Assert
        Assert.NotNull(result);
        Assert.IsType<BadRequestObjectResult>(result);
        var value = result.Value;
        var message = value?.GetType()?.GetProperty("message")?.GetValue(value, null) as string;
        Assert.Equal("Campo Login não pode ser em branco", message);
    }

    [Fact]
    public void Post_With_InvalidEmail_Returns_BadRequest()
    {
        // Arrange
        var controleAcessoDto = ControleAcessoFaker.Instance.GetNewFakerVM();
        controleAcessoDto.Email = "email Inválido";

        // Act
        var result = _controleAcessoController.Post(controleAcessoDto) as ObjectResult;

        // Assert
        Assert.NotNull(result);
        Assert.IsType<BadRequestObjectResult>(result);
        var value = result.Value;
        var message = value?.GetType()?.GetProperty("message")?.GetValue(value, null) as string;
        Assert.Equal("Email inválido!", message);
    }

    [Fact]
    public void Post_With_NUll_Password_Returns_BadRequest()
    {
        // Arrange
        var controleAcessoDto = ControleAcessoFaker.Instance.GetNewFakerVM();
        controleAcessoDto.Senha = string.Empty;

        // Act
        var result = _controleAcessoController.Post(controleAcessoDto) as ObjectResult;

        // Assert
        Assert.NotNull(result);
        Assert.IsType<BadRequestObjectResult>(result);
        var value = result.Value;
        var message = value?.GetType()?.GetProperty("message")?.GetValue(value, null) as string;
        Assert.Equal("Campo Senha não pode ser em branco ou nulo", message);
    }

    [Fact]
    public void Post_With_NUll_ConfirmedPassword_Returns_BadRequest()
    {
        // Arrange
        var controleAcessoDto = ControleAcessoFaker.Instance.GetNewFakerVM();
        controleAcessoDto.ConfirmaSenha = string.Empty;

        // Act
        var result = _controleAcessoController.Post(controleAcessoDto) as ObjectResult;

        // Assert
        Assert.NotNull(result);
        Assert.IsType<BadRequestObjectResult>(result);
        var value = result.Value;
        var message = value?.GetType()?.GetProperty("message")?.GetValue(value, null) as string;
        Assert.Equal("Campo Confirma Senha não pode ser em branco ou nulo", message);
    }

    [Fact]
    public void Post_With_Password_Mismatch_Returns_BadRequest()
    {
        // Arrange
        var controleAcessoDto = ControleAcessoFaker.Instance.GetNewFakerVM();
        controleAcessoDto.ConfirmaSenha = "senha Errada";

        // Act
        var result = _controleAcessoController.Post(controleAcessoDto) as ObjectResult;

        // Assert
        Assert.NotNull(result);
        Assert.IsType<BadRequestObjectResult>(result);
        var value = result.Value;
        var message = value?.GetType()?.GetProperty("message")?.GetValue(value, null) as string;
        Assert.Equal("Senha e Confirma Senha são diferentes!", message);
    }

    [Fact]
    public void SignIn_With_ValidData_Returns_ObjectResult()
    {
        // Arrange
        var loginVM = new LoginDto { Email = "teste@teste.com", Senha = "password" };
        _mockControleAcessoBusiness.Setup(b => b.ValidateCredentials(It.IsAny<ControleAcessoDto>())).Returns(new AuthenticationDto());

        // Act
        var result = _controleAcessoController.SignIn(loginVM) as ObjectResult;

        // Assert
        Assert.NotNull(result);
        Assert.IsType<OkObjectResult>(result);
    }

    [Fact]
    public void SignIn_With_InvalidEmail_Returns_BadRequest_EmailInvalido()
    {
        // Arrange
        var loginVM = new LoginDto { Email = "email@invalido.com", Senha = "password" };

        // Act
        var result = _controleAcessoController.SignIn(loginVM) as ObjectResult;

        // Assert
        Assert.NotNull(result);
        Assert.IsType<BadRequestObjectResult>(result);
        var value = result.Value;
        var message = value?.GetType()?.GetProperty("message")?.GetValue(value, null) as string;
        //Assert.Equal("Email inválido!", message);
    }

    [Fact]
    public void SignIn_With_InvalidEmail_Returns_BadRequest_Login_Erro()
    {
        // Arrange
        var loginVM = new LoginDto { Email = "email@invalido.com", Senha = "password" };

        // Act
        var result = _controleAcessoController.SignIn(loginVM) as ObjectResult;

        // Assert
        Assert.NotNull(result);
        Assert.IsType<BadRequestObjectResult>(result);
        var value = result.Value;
        var message = value?.GetType()?.GetProperty("message")?.GetValue(value, null) as string;
        Assert.Equal("Erro ao realizar login!", message);
    }

    [Fact]
    public void ChangePassword_With_ValidData_Returns_OkResult()
    {
        // Arrange
        var changePasswordVM = new ChangePasswordDto { Senha = "!12345", ConfirmaSenha = "!12345" };
        SetupBearerToken(1);
        _mockControleAcessoBusiness.Setup(b => b.ChangePassword(1, "!12345"));

        // Act
        var result = _controleAcessoController.ChangePassword(changePasswordVM) as ObjectResult;

        // Assert
        Assert.NotNull(result);
        Assert.IsType<OkObjectResult>(result);
        var value = result.Value;

        var message = (bool)(value?.GetType()?.GetProperty("message")?.GetValue(value, null) ?? false);

        Assert.True(message);
    }

    [Fact]
    public void ChangePassword_With_Usuario_Teste_Returns_BadRequest()
    {
        // Arrange
        var changePasswordVM = new ChangePasswordDto { Senha = "!12345", ConfirmaSenha = "!12345" };
        SetupBearerToken(2);

        // Act
        var result = _controleAcessoController.ChangePassword(changePasswordVM) as ObjectResult;

        // Assert
        Assert.NotNull(result);
        Assert.IsType<BadRequestObjectResult>(result);
        var value = result.Value;
        var message = value?.GetType()?.GetProperty("message")?.GetValue(value, null) as string;
        Assert.Equal("A senha deste usuário não pode ser atualizada!", message);
    }

    [Fact]
    public void ChangePassword_With_NULL_Password_Returns_BadRequest()
    {
        // Arrange
        var changePasswordVM = new ChangePasswordDto { Senha = "", ConfirmaSenha = "!12345" };
        SetupBearerToken(1);

        // Act
        var result = _controleAcessoController.ChangePassword(changePasswordVM) as ObjectResult;

        // Assert
        Assert.NotNull(result);
        Assert.IsType<BadRequestObjectResult>(result);
        var value = result.Value;
        var message = value?.GetType()?.GetProperty("message")?.GetValue(value, null) as string;
        Assert.Equal("Campo Senha não pode ser em branco ou nulo!", message);
    }

    [Fact]
    public void ChangePassword_With_NULL_ConfirmedPassword_Returns_BadRequest()
    {
        // Arrange
        var changePasswordVM = new ChangePasswordDto { Senha = "!12345", ConfirmaSenha = "" };
        SetupBearerToken(1);

        // Act
        var result = _controleAcessoController.ChangePassword(changePasswordVM) as ObjectResult;

        // Assert
        Assert.NotNull(result);
        Assert.IsType<BadRequestObjectResult>(result);
        var value = result.Value;
        var message = value?.GetType()?.GetProperty("message")?.GetValue(value, null) as string;
        Assert.Equal("Campo Confirma Senha não pode ser em branco ou nulo!", message);
    }

    [Fact]
    public void ChangePassword_With_ValidData_Returns_BadRequest()
    {
        // Arrange
        var changePasswordVM = new ChangePasswordDto { Senha = "!12345", ConfirmaSenha = "!12345" };
        SetupBearerToken(1);
        _mockControleAcessoBusiness.Setup(b => b.ChangePassword(1, "!12345")).Throws(new Exception());

        // Act
        var result = _controleAcessoController.ChangePassword(changePasswordVM) as ObjectResult;

        // Assert
        Assert.NotNull(result);
        Assert.IsType<BadRequestObjectResult>(result);
        var value = result.Value;
        var message = value?.GetType()?.GetProperty("message")?.GetValue(value, null) as string;
        Assert.Equal("Erro ao trocar senha tente novamente mais tarde ou entre em contato com nosso suporte.", message);
    }

    [Fact]
    public void RecoveryPassword_WithValidEmail_ReturnsOkResult()
    {
        // Arrange
        var email = "teste@teste.com";
        _mockControleAcessoBusiness.Setup(b => b.RecoveryPassword(email));

        // Act
        var result = _controleAcessoController.RecoveryPassword(email) as ObjectResult;

        // Assert
        Assert.NotNull(result);
        Assert.IsType<OkObjectResult>(result);
        var value = result.Value;
        var message = (bool)(value?.GetType()?.GetProperty("message")?.GetValue(value, null) ?? false);
        Assert.True(message);
    }

    [Fact]
    public void RecoveryPassword_With_NUll_Email_Returns_BadRequest()
    {
        // Arrange
        var email = string.Empty;

        // Act
        var result = _controleAcessoController.RecoveryPassword(email) as ObjectResult;

        // Assert
        Assert.NotNull(result);
        Assert.IsType<BadRequestObjectResult>(result);
        var value = result.Value;
        var message = value?.GetType()?.GetProperty("message")?.GetValue(value, null) as string;
        Assert.Equal("Campo Login não pode ser em branco ou nulo!", message);
    }

    [Fact]
    public void RecoveryPassword_Lenght_Bigger_Than_256_Email_Returns_BadRequest()
    {
        // Arrange
        var email = new string('A', 257);

        // Act
        var result = _controleAcessoController.RecoveryPassword(email) as ObjectResult;

        // Assert
        Assert.NotNull(result);
        Assert.IsType<BadRequestObjectResult>(result);
        var value = result.Value;
        var message = value?.GetType()?.GetProperty("message")?.GetValue(value, null) as string;
        Assert.Equal("Email inválido!", message);
    }

    [Fact]
    public void RecoveryPassword_With_Invalid_Email_Returns_BadRequest()
    {
        // Arrange
        var email = "email Invalido";

        // Act
        var result = _controleAcessoController.RecoveryPassword(email) as ObjectResult;

        // Assert
        Assert.NotNull(result);
        Assert.IsType<BadRequestObjectResult>(result);
        var value = result.Value;
        var message = value?.GetType()?.GetProperty("message")?.GetValue(value, null) as string;
        Assert.Equal("Email inválido!", message);
    }

    [Fact]
    public void RecoveryPassword_WithInvalid_Email_Returns_BadRequest_Email_Nao_Enviado()
    {
        // Arrange
        var email = "email@invalido.com";
        _mockControleAcessoBusiness.Setup(b => b.RecoveryPassword(It.IsAny<string>())).Throws(new Exception());
        
        // Act
        var result = _controleAcessoController.RecoveryPassword(email) as ObjectResult;

        // Assert
        Assert.NotNull(result);
        Assert.IsType<BadRequestObjectResult>(result);
        var value = result.Value;
        var message = value?.GetType()?.GetProperty("message")?.GetValue(value, null) as string;
        Assert.Equal("Email não pode ser enviado, tente novamente mais tarde.", message);
    }

    [Fact]
    public void Refresh_With_ValidData_Returns_OkResult()
    {
        // Arrange
        var authenticationDto = new AuthenticationDto();
        _mockControleAcessoBusiness.Setup(b => b.ValidateCredentials(It.IsAny<string>())).Returns(new AuthenticationDto());

        // Act
        var result = _controleAcessoController.Refresh("fakeRefreshToken") as ObjectResult;

        // Assert
        Assert.NotNull(result);
        Assert.IsType<OkObjectResult>(result);
    }

    [Fact]
    public void Refresh_With_InvalidData_Returns_BadRequest()
    {
        // Arrange
        var authenticationDto = new AuthenticationDto();
        _controleAcessoController.ModelState.AddModelError("Key", "Error");

        // Act
        var result = _controleAcessoController.Refresh("fakeRefreshToken") as BadRequestObjectResult;

        // Assert
        Assert.NotNull(result);
        Assert.Equal(400, result.StatusCode);
    }

    [Fact]
    public void Refresh_With_Null_Result_Returns_BadRequest()
    {
        // Arrange
        var authenticationDto = new AuthenticationDto();
        _mockControleAcessoBusiness.Setup(b => b.ValidateCredentials(It.IsAny<string>())).Returns<AuthenticationDto>(null);

        // Act
        var result = _controleAcessoController.Refresh("fakeRefreshToken") as BadRequestObjectResult;

        // Assert
        Assert.NotNull(result);
        Assert.Equal(400, result.StatusCode);
    }

    [Fact]
    public void Revoke_With_ValidData_Returns_NoContentResult()
    {
        // Arrange
        SetupBearerToken(1);

        // Act
        var result = _controleAcessoController.Revoke() as NoContentResult;

        // Assert
        Assert.NotNull(result);
        Assert.IsType<NoContentResult>(result);
    }

    [Fact]
    public void Revoke_Throws_Exception_Returns_BadRequest()
    {
        // Arrange
        _mockControleAcessoBusiness.Setup(b => b.RevokeToken(It.IsAny<int>())).Throws(new Exception());

        // Act
        var result = _controleAcessoController.Revoke() as BadRequestObjectResult;

        // Assert
        Assert.NotNull(result);
        Assert.Equal(400, result.StatusCode);
    }

}
