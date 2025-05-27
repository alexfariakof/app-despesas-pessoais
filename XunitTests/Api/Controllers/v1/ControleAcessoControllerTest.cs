using Business.Abstractions;
using Business.Dtos.v1;
using Despesas.WebApi.Controllers.v1;
using Microsoft.AspNetCore.Mvc;
using Business.Dtos.Core;
using __mock__.v1;

namespace Api.Controllers.v1;

public sealed class ControleAcessoControllerTest
{
    private readonly Mock<IControleAcessoBusiness<ControleAcessoDto, LoginDto>> _mockControleAcessoBusiness;
    private readonly ControleAcessoController _controleAcessoController;

    public ControleAcessoControllerTest()
    {
        _mockControleAcessoBusiness = new Mock<IControleAcessoBusiness<ControleAcessoDto, LoginDto>>();
        _controleAcessoController = new ControleAcessoController(_mockControleAcessoBusiness.Object);
    }

    [Fact]
    public void Post_With_ValidData_Returns_OkResult()
    {
        // Arrange
        var controleAcessoDto = ControleAcessoFaker.Instance.GetNewFakerVM();
        _mockControleAcessoBusiness.Setup(b => b.Create(It.IsAny<ControleAcessoDto>()));

        // Act
        var result = _controleAcessoController.Post(controleAcessoDto) as ObjectResult;

        // Assert
        Assert.NotNull(result);
        Assert.IsType<OkObjectResult>(result);
        var value = result.Value;
        var message = (bool?)value?.GetType()?.GetProperty("message")?.GetValue(value, null);
        Assert.True(message);
    }

    [Fact]
    public void Post_With_ValidData_Returns_BadRequest()
    {
        // Arrange
        var controleAcessoDto = ControleAcessoFaker.Instance.GetNewFakerVM();
        _mockControleAcessoBusiness.Setup(b => b.Create(It.IsAny<ControleAcessoDto>())).Throws<Exception>();

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
        controleAcessoDto.Telefone = String.Empty;

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
        controleAcessoDto.Email = String.Empty;

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
        controleAcessoDto.Senha = String.Empty;

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
        controleAcessoDto.ConfirmaSenha = String.Empty;

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
        var loginDto = new LoginDto { Email = "teste@teste.com", Senha = "password" };
        _mockControleAcessoBusiness.Setup(b => b.ValidateCredentials(It.IsAny<LoginDto>())).Returns(new AuthenticationDto());

        // Act
        var result = _controleAcessoController.SignIn(loginDto) as ObjectResult;

        // Assert
        Assert.NotNull(result);
        Assert.IsType<OkObjectResult>(result);
    }

    [Fact]
    public void SignIn_With_NUll_Login_Returns_BadRequest()
    {
        // Arrange
        var loginDto = new LoginDto { Email = "", Senha = "password" };
        _mockControleAcessoBusiness.Setup(b => b.ValidateCredentials(It.IsAny<LoginDto>())).Returns(new AuthenticationDto());

        // Act
        var result = _controleAcessoController.SignIn(loginDto) as ObjectResult;

        // Assert
        Assert.NotNull(result);
        Assert.IsType<BadRequestObjectResult>(result);
        var value = result.Value;
        var message = value?.GetType()?.GetProperty("message")?.GetValue(value, null) as string;
        Assert.Equal("Campo Login não pode ser em branco ou nulo!", message);
    }

    [Fact]
    public void SignIn_With_NUll_Password_Returns_BadRequest()
    {
        // Arrange
        var loginDto = new LoginDto { Email = "teste@teste.com", Senha = " " };
        _mockControleAcessoBusiness.Setup(b => b.ValidateCredentials(It.IsAny<LoginDto>())).Returns(new AuthenticationDto());

        // Act
        var result = _controleAcessoController.SignIn(loginDto) as ObjectResult;

        // Assert
        Assert.NotNull(result);
        Assert.IsType<BadRequestObjectResult>(result);
        var value = result.Value;
        var message = value?.GetType()?.GetProperty("message")?.GetValue(value, null) as string;
        Assert.Equal("Campo Senha não pode ser em branco ou nulo!", message);
    }

    [Fact]
    public void SignIn_With_InvalidEmail_Returns_BadRequest_EmailInvalido()
    {
        // Arrange
        var loginDto = new LoginDto { Email = "email invalido", Senha = "password" };

        // Act
        var result = _controleAcessoController.SignIn(loginDto) as ObjectResult;

        // Assert
        Assert.NotNull(result);
        Assert.IsType<BadRequestObjectResult>(result);
        var value = result.Value;
        var message = value?.GetType()?.GetProperty("message")?.GetValue(value, null) as string;
        Assert.Equal("Email inválido!", message);
    }

    [Fact]
    public void SignIn_With_InvalidEmail_Returns_BadRequest_Login_Erro()
    {
        // Arrange
        var loginDto = new LoginDto { Email = "email@invalido.com", Senha = "password" };

        // Act
        var result = _controleAcessoController.SignIn(loginDto) as ObjectResult;

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
        var changePasswordDto = new ChangePasswordDto { Senha = "!12345", ConfirmaSenha = "!12345" };
        Usings.SetupBearerToken(Guid.NewGuid(), _controleAcessoController);
        _mockControleAcessoBusiness.Setup(b => b.ChangePassword(It.IsAny<Guid>(), "!12345"));

        // Act
        var result = _controleAcessoController.ChangePassword(changePasswordDto) as ObjectResult;

        // Assert
        Assert.NotNull(result);
        Assert.IsType<OkObjectResult>(result);
        var value = result.Value;
        var message = (bool?)value?.GetType()?.GetProperty("message")?.GetValue(value, null);
        Assert.True(message);
    }


    [Fact]
    public void ChangePassword_With_NULL_Password_Returns_BadRequest()
    {
        // Arrange
        var changePasswordDto = new ChangePasswordDto { Senha = "", ConfirmaSenha = "!12345" };
        Usings.SetupBearerToken(Guid.NewGuid(), _controleAcessoController);

        // Act
        var result = _controleAcessoController.ChangePassword(changePasswordDto) as ObjectResult;

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
        var changePasswordDto = new ChangePasswordDto { Senha = "!12345", ConfirmaSenha = "" };
        Usings.SetupBearerToken(Guid.NewGuid(), _controleAcessoController);

        // Act
        var result = _controleAcessoController.ChangePassword(changePasswordDto) as ObjectResult;

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
        var changePasswordDto = new ChangePasswordDto { Senha = "!12345", ConfirmaSenha = "!12345" };
        Usings.SetupBearerToken(Guid.NewGuid(), _controleAcessoController);
        _mockControleAcessoBusiness.Setup(b => b.ChangePassword(It.IsAny<Guid>(), It.IsAny<string>())).Throws<Exception>();

        // Act
        var result = _controleAcessoController.ChangePassword(changePasswordDto) as ObjectResult;

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

        var message = (bool?)value?.GetType()?.GetProperty("message")?.GetValue(value, null);

        Assert.True(message);
    }

    [Fact]
    public void RecoveryPassword_With_NUll_Email_Returns_BadRequest()
    {
        // Arrange
        var email = String.Empty;

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
        _mockControleAcessoBusiness.Setup(b => b.RecoveryPassword(It.IsAny<string>())).Throws<Exception>();
        Usings.SetupBearerToken(Guid.NewGuid(), _controleAcessoController);

        // Act
        var result = _controleAcessoController.RecoveryPassword(email) as ObjectResult;

        // Assert
        Assert.NotNull(result);
        Assert.IsType<BadRequestObjectResult>(result);
        var value = result.Value;
        var message = value?.GetType()?.GetProperty("message")?.GetValue(value, null) as string;
        Assert.Equal("Email não pode ser enviado, tente novamente mais tarde.", message);
    }
}
