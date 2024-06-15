using Microsoft.AspNetCore.Mvc;
using Business.Abstractions;
using Despesas.WebApi.Controllers.v2;
using Business.Dtos.v2;
using __mock__.v2;
using Business.Dtos.Core;

namespace Api.Controllers.v2;
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
        var controleAcesso = ControleAcessoFaker.Instance.GetNewFaker();
        var controleAcessoDto = ControleAcessoFaker.Instance.GetNewFakerVM(controleAcesso.Usuario);
        _mockControleAcessoBusiness.Setup(b => b.Create(It.IsAny<ControleAcessoDto>()));

        // Act
        var result = _controleAcessoController.Post(controleAcessoDto) as ObjectResult;

        // Assert
        Assert.NotNull(result);
        Assert.IsType<OkObjectResult>(result);
        var message = (bool?)result.Value;
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
        var message = result.Value;
        Assert.Equal("Não foi possível realizar o cadastro.", message);
    }

    [Fact]
    public void Post_With_Null_Telefone_Returns_BadRequest()
    {
        // Arrange
        var controleAcessoDto = ControleAcessoFaker.Instance.GetNewFakerVM();        
        controleAcessoDto.Telefone = string.Empty;
        _mockControleAcessoBusiness.Setup(b => b.Create(It.IsAny<ControleAcessoDto>())).Throws(new ArgumentException("Campo Telefone não pode ser em branco"));

        // Act
        var result = _controleAcessoController.Post(controleAcessoDto) as ObjectResult;

        // Assert
        Assert.NotNull(result);
        Assert.IsType<BadRequestObjectResult>(result);
        var message = result.Value;
        Assert.Equal("Campo Telefone não pode ser em branco", message);
    }

    [Fact]
    public void Post_With_NUll_Email_Returns_BadRequest()
    {
        // Arrange
        var controleAcessoDto = ControleAcessoFaker.Instance.GetNewFakerVM();
        _mockControleAcessoBusiness.Setup(b => b.Create(It.IsAny<ControleAcessoDto>())).Throws(new ArgumentException("Campo Login não pode ser em branco"));
        controleAcessoDto.Email = string.Empty;

        // Act
        var result = _controleAcessoController.Post(controleAcessoDto) as ObjectResult;

        // Assert
        Assert.NotNull(result);
        Assert.IsType<BadRequestObjectResult>(result);
        var message = result.Value;
        Assert.Equal("Campo Login não pode ser em branco", message);
    }

    [Fact]
    public void Post_With_InvalidEmail_Returns_BadRequest()
    {
        // Arrange
        var controleAcessoDto = ControleAcessoFaker.Instance.GetNewFakerVM();
        _mockControleAcessoBusiness.Setup(b => b.Create(It.IsAny<ControleAcessoDto>())).Throws(new ArgumentException("Email inválido!"));
        controleAcessoDto.Email = "email";

        // Act
        var result = _controleAcessoController.Post(controleAcessoDto) as ObjectResult;

        // Assert
        Assert.NotNull(result);
        Assert.IsType<BadRequestObjectResult>(result);
        var message = result.Value;
        Assert.Equal("Email inválido!", message);
    }

    [Fact]
    public void Post_With_NUll_Password_Returns_BadRequest()
    {
        // Arrange
        var controleAcessoDto = ControleAcessoFaker.Instance.GetNewFakerVM();
        controleAcessoDto.Senha = string.Empty;
        _mockControleAcessoBusiness.Setup(b => b.Create(It.IsAny<ControleAcessoDto>())).Throws(new ArgumentException("Campo Senha não pode ser em branco ou nulo"));
        // Act
        var result = _controleAcessoController.Post(controleAcessoDto) as ObjectResult;

        // Assert
        Assert.NotNull(result);
        Assert.IsType<BadRequestObjectResult>(result);
        var message = result.Value;
        Assert.Equal("Campo Senha não pode ser em branco ou nulo", message);
    }

    [Fact]
    public void Post_With_NUll_ConfirmedPassword_Returns_BadRequest()
    {
        // Arrange
        var controleAcessoDto = ControleAcessoFaker.Instance.GetNewFakerVM();
        _mockControleAcessoBusiness.Setup(b => b.Create(It.IsAny<ControleAcessoDto>())).Throws(new ArgumentException("Campo Confirma Senha não pode ser em branco ou nulo"));
        controleAcessoDto.ConfirmaSenha = string.Empty;

        // Act
        var result = _controleAcessoController.Post(controleAcessoDto) as ObjectResult;

        // Assert
        Assert.NotNull(result);
        Assert.IsType<BadRequestObjectResult>(result);
        var message = result.Value;
        Assert.Equal("Campo Confirma Senha não pode ser em branco ou nulo", message);
    }

    [Fact]
    public void Post_With_Password_Mismatch_Returns_BadRequest()
    {
        // Arrange
        var controleAcessoDto = ControleAcessoFaker.Instance.GetNewFakerVM();
        controleAcessoDto.ConfirmaSenha = "senha Errada";
        _mockControleAcessoBusiness.Setup(b => b.Create(It.IsAny<ControleAcessoDto>())).Throws(new ArgumentException("Senha e Confirma Senha são diferentes!"));

        // Act
        var result = _controleAcessoController.Post(controleAcessoDto) as ObjectResult;

        // Assert
        Assert.NotNull(result);
        Assert.IsType<BadRequestObjectResult>(result);
        var message = result.Value;
        Assert.Equal("Senha e Confirma Senha são diferentes!", message);
    }

    [Fact]
    public void SignIn_BadRequest_When_TryCatch_Throws_ArgumentException()
    {
        // Arrange
        var loginVM = new LoginDto { Email = "teste@teste.com", Senha = "password" };
        _mockControleAcessoBusiness.Setup(b => b.ValidateCredentials(It.IsAny<LoginDto>())).Throws<ArgumentException>();

        // Act
        var result = _controleAcessoController.SignIn(loginVM) as ObjectResult;

        // Assert
        Assert.NotNull(result);
        Assert.IsType<BadRequestObjectResult>(result);
    }

    [Fact]
    public void SignIn_With_ValidData_Returns_ObjectResult()
    {
        // Arrange
        var loginVM = new LoginDto { Email = "teste@teste.com", Senha = "password" };
        _mockControleAcessoBusiness.Setup(b => b.ValidateCredentials(It.IsAny<LoginDto>())).Returns(new AuthenticationDto());

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
    }

    [Fact]
    public void SignIn_With_InvalidEmail_Returns_BadRequest_Login_Erro()
    {
        // Arrange
        var loginVM = new LoginDto { Email = "email", Senha = "password" };

        // Act
        var result = _controleAcessoController.SignIn(loginVM) as ObjectResult;

        // Assert
        Assert.NotNull(result);
        Assert.IsType<BadRequestObjectResult>(result);
        var message = result.Value;
        Assert.Equal("Não foi possível realizar o login do usuário.", message);
    }

    [Fact]
    public void ChangePassword_With_ValidData_Returns_OkResult()
    {
        // Arrange
        var changePasswordVM = new ChangePasswordDto { Senha = "!12345", ConfirmaSenha = "!12345" };
        Usings.SetupBearerToken(1, _controleAcessoController);
        _mockControleAcessoBusiness.Setup(b => b.ChangePassword(1, "!12345"));

        // Act
        var result = _controleAcessoController.ChangePassword(changePasswordVM) as ObjectResult;

        // Assert
        Assert.NotNull(result);
        Assert.IsType<OkObjectResult>(result);
        var message = (bool?)result.Value;
        Assert.True(message);
    }

    [Fact]
    public void ChangePassword_With_Usuario_Teste_Returns_BadRequest()
    {
        // Arrange
        var changePasswordVM = new ChangePasswordDto { Senha = "!12345", ConfirmaSenha = "!12345" };
        Usings.SetupBearerToken(2, _controleAcessoController);

        // Act
        var result = _controleAcessoController.ChangePassword(changePasswordVM) as ObjectResult;

        // Assert
        Assert.NotNull(result);
        Assert.IsType<BadRequestObjectResult>(result);
        var message = result.Value;
        Assert.Equal("A senha deste usuário não pode ser atualizada!", message);
    }

    [Fact]
    public void ChangePassword_With_NULL_Password_Returns_BadRequest()
    {
        // Arrange
        var changePasswordVM = new ChangePasswordDto { Senha = null, ConfirmaSenha = "!12345" };
        Usings.SetupBearerToken(1, _controleAcessoController);

        // Act
        var result = _controleAcessoController.ChangePassword(null) as ObjectResult;

        // Assert
        Assert.NotNull(result);
        Assert.IsType<BadRequestObjectResult>(result);
        var message = result.Value;
        Assert.Equal("Erro ao trocar senha tente novamente mais tarde ou entre em contato com nosso suporte.", message);
    }

    [Fact]
    public void ChangePassword_With_NULL_ConfirmedPassword_Returns_BadRequest()
    {
        // Arrange
        var changePasswordVM = new ChangePasswordDto { Senha = "!12345", ConfirmaSenha = null };
        Usings.SetupBearerToken(1, _controleAcessoController);

        // Act
        var result = _controleAcessoController.ChangePassword(null) as ObjectResult;

        // Assert
        Assert.NotNull(result);
        Assert.IsType<BadRequestObjectResult>(result);
        var message = result.Value;
        Assert.Equal("Erro ao trocar senha tente novamente mais tarde ou entre em contato com nosso suporte.", message);
    }

    [Fact]
    public void ChangePassword_With_ValidData_Returns_BadRequest()
    {
        // Arrange
        var changePasswordVM = new ChangePasswordDto { Senha = "!12345", ConfirmaSenha = "!12345" };
        Usings.SetupBearerToken(1, _controleAcessoController);
        _mockControleAcessoBusiness.Setup(b => b.ChangePassword(1, "!12345")).Throws(new Exception());

        // Act
        var result = _controleAcessoController.ChangePassword(changePasswordVM) as ObjectResult;

        // Assert
        Assert.NotNull(result);
        Assert.IsType<BadRequestObjectResult>(result);
        var message = result.Value;
        Assert.Equal("Erro ao trocar senha tente novamente mais tarde ou entre em contato com nosso suporte.", message);
    }

    [Fact]
    public void RecoveryPassword_WithValidEmail_ReturnsOkResult()
    {
        // Arrange
        var email = "teste@teste.com";
        _mockControleAcessoBusiness.Setup(b => b.RecoveryPassword(It.IsAny<string>())).Callback(() => { });
        _mockControleAcessoBusiness.Setup(b => b.RecoveryPassword(It.IsAny<string>()));
        Usings.SetupBearerToken(1, _controleAcessoController);

        // Act
        var result = _controleAcessoController.RecoveryPassword(email) as ObjectResult;

        // Assert
        Assert.NotNull(result);
        Assert.IsType<OkObjectResult>(result);
        var message = (bool?)result.Value;
        Assert.True(message);
    }

    [Fact]
    public void RecoveryPassword_With_NUll_Email_Returns_BadRequest()
    {
        // Arrange
        var email = string.Empty;
        _mockControleAcessoBusiness.Setup(b => b.RecoveryPassword(It.IsAny<string>())).Throws<Exception>();
        // Act
        var result = _controleAcessoController.RecoveryPassword(email) as NoContentResult;

        // Assert
        Assert.NotNull(result);
        Assert.IsType<NoContentResult>(result);
    }

    [Fact]
    public void RecoveryPassword_Lenght_Bigger_Than_256_Email_Returns_BadRequest()
    {
        // Arrange
        var email = new string('A', 257);
        _mockControleAcessoBusiness.Setup(b => b.RecoveryPassword(It.IsAny<string>())).Throws<Exception>();
        
        // Act
        var result = _controleAcessoController.RecoveryPassword(email) as NoContentResult;

        // Assert
        Assert.NotNull(result);
        Assert.IsType<NoContentResult>(result);
    }

    [Fact]
    public void RecoveryPassword_With_Invalid_Email_Returns_BadRequest()
    {
        // Arrange
        var email = "email Invalido";
        _mockControleAcessoBusiness.Setup(b => b.RecoveryPassword(It.IsAny<string>())).Throws<Exception>();
        // Act
        var result = _controleAcessoController.RecoveryPassword(email) as NoContentResult;

        // Assert
        Assert.NotNull(result);
        Assert.IsType<NoContentResult>(result);
    }

    [Fact]
    public void RecoveryPassword_WithInvalid_Email_Returns_BadRequest_Email_Nao_Enviado()
    {
        // Arrange
        var email = "email@invalido.com";
        _mockControleAcessoBusiness.Setup(b => b.RecoveryPassword(It.IsAny<string>())).Throws<Exception>();

        // Act
        var result = _controleAcessoController.RecoveryPassword(email) as NoContentResult;

        // Assert
        Assert.NotNull(result);
        Assert.IsType<NoContentResult>(result);
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
        _mockControleAcessoBusiness.Setup(b => b.ValidateCredentials(It.IsAny<string>())).Returns((AuthenticationDto)null);
        // Act
        var result = _controleAcessoController.Refresh("fakeRefreshToken") as NoContentResult;

        // Assert
        Assert.NotNull(result);
        Assert.Equal(204, result.StatusCode);
    }

    [Fact]
    public void Refresh_With_Null_Result_Returns_BadRequest()
    {
        // Arrange
        var authenticationDto = new AuthenticationDto();
        _mockControleAcessoBusiness.Setup(b => b.ValidateCredentials(It.IsAny<string>())).Returns<AuthenticationDto>(null);

        // Act
        var result = _controleAcessoController.Refresh("fakeRefreshToken") as NoContentResult;

        // Assert
        Assert.NotNull(result);
        Assert.Equal(204, result.StatusCode);
    }
}