using despesas_backend_api_net_core.Business;
using despesas_backend_api_net_core.Controllers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Xunit.Extensions.Ordering;

namespace Controllers
{
    [Order(3)]
    public class ControleAcessoControllerTest
    {
        protected readonly Mock<IControleAcessoBusiness> _mockControleAcessoBusiness;
        protected readonly ControleAcessoController _controleAcessoController;

        private ControleAcessoVM CreateValidControleAcessoVM()
        {
            return new ControleAcessoVM
            {
                Nome = "Teste ",
                SobreNome = "Controle Acesso",
                Email = "teste@teste.com",
                Telefone = "(21) 9999-9999",
                Senha = "!12345",
                ConfirmaSenha = "!12345"
            };
        }

        private void SetupBearerToken(int userId)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, userId.ToString())
            };
            var identity = new ClaimsIdentity(claims, "IdUsuario");
            var claimsPrincipal = new ClaimsPrincipal(identity);

            var httpContext = new DefaultHttpContext { User = claimsPrincipal };
            httpContext.Request.Headers["Authorization"] =
                "Bearer " + Usings.GenerateJwtToken(userId);

            _controleAcessoController.ControllerContext = new ControllerContext
            {
                HttpContext = httpContext
            };
        }

        public ControleAcessoControllerTest()
        {
            _mockControleAcessoBusiness = new Mock<IControleAcessoBusiness>();
            _controleAcessoController = new ControleAcessoController(
                _mockControleAcessoBusiness.Object
            );
        }

        [Fact]
        public void Post_With_ValidData_Returns_OkResult()
        {
            // Arrange
            var controleAcessoVM = CreateValidControleAcessoVM();
            _mockControleAcessoBusiness
                .Setup(b => b.Create(It.IsAny<ControleAcesso>()))
                .Returns(true);

            // Act
            var result = _controleAcessoController.Post(controleAcessoVM) as ObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.IsType<OkObjectResult>(result);
            var value = result.Value;

            var message = (bool)value.GetType().GetProperty("message").GetValue(value, null);

            Assert.True(message);
        }

        [Fact]
        public void Post_With_ValidData_Returns_BadRequest()
        {
            // Arrange
            var controleAcessoVM = CreateValidControleAcessoVM();
            _mockControleAcessoBusiness
                .Setup(b => b.Create(It.IsAny<ControleAcesso>()))
                .Returns(null);

            // Act
            var result = _controleAcessoController.Post(controleAcessoVM) as ObjectResult;

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
            var controleAcessoVM = CreateValidControleAcessoVM();
            controleAcessoVM.Telefone = String.Empty;

            // Act
            var result = _controleAcessoController.Post(controleAcessoVM) as ObjectResult;

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
            var controleAcessoVM = CreateValidControleAcessoVM();
            controleAcessoVM.Email = String.Empty;

            // Act
            var result = _controleAcessoController.Post(controleAcessoVM) as ObjectResult;

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
            var controleAcessoVM = CreateValidControleAcessoVM();
            controleAcessoVM.Email = "email Inválido";

            // Act
            var result = _controleAcessoController.Post(controleAcessoVM) as ObjectResult;

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
            var controleAcessoVM = CreateValidControleAcessoVM();
            controleAcessoVM.Senha = String.Empty;

            // Act
            var result = _controleAcessoController.Post(controleAcessoVM) as ObjectResult;

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
            var controleAcessoVM = CreateValidControleAcessoVM();
            controleAcessoVM.ConfirmaSenha = String.Empty;

            // Act
            var result = _controleAcessoController.Post(controleAcessoVM) as ObjectResult;

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
            var controleAcessoVM = CreateValidControleAcessoVM();
            controleAcessoVM.ConfirmaSenha = "senha Errada";

            // Act
            var result = _controleAcessoController.Post(controleAcessoVM) as ObjectResult;

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
            var loginVM = new LoginVM { Email = "teste@teste.com", Senha = "password" };
            _mockControleAcessoBusiness
                .Setup(b => b.FindByLogin(It.IsAny<ControleAcesso>()))
                .Returns(new Authentication());

            // Act
            var result = _controleAcessoController.SignIn(loginVM) as ObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public void SignIn_With_NUll_Login_Returns_BadRequest()
        {
            // Arrange
            var loginVM = new LoginVM { Email = "", Senha = "password" };
            _mockControleAcessoBusiness
                .Setup(b => b.FindByLogin(It.IsAny<ControleAcesso>()))
                .Returns(new Authentication());

            // Act
            var result = _controleAcessoController.SignIn(loginVM) as ObjectResult;

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
            var loginVM = new LoginVM { Email = "teste@teste.com", Senha = " " };
            _mockControleAcessoBusiness
                .Setup(b => b.FindByLogin(It.IsAny<ControleAcesso>()))
                .Returns(new Authentication());

            // Act
            var result = _controleAcessoController.SignIn(loginVM) as ObjectResult;

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
            var loginVM = new LoginVM { Email = "email invalido", Senha = "password" };

            // Act
            var result = _controleAcessoController.SignIn(loginVM) as ObjectResult;

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
            var loginVM = new LoginVM { Email = "email@invalido.com", Senha = "password" };

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
            var loginVM = new LoginVM { Senha = "!12345", ConfirmaSenha = "!12345" };
            SetupBearerToken(1);
            _mockControleAcessoBusiness.Setup(b => b.ChangePassword(1, "!12345")).Returns(true);

            // Act
            var result = _controleAcessoController.ChangePassword(loginVM) as ObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.IsType<OkObjectResult>(result);
            var value = result.Value;

            var message = (bool)value.GetType().GetProperty("message").GetValue(value, null);

            Assert.True(message);
        }

        [Fact]
        public void ChangePassword_With_Usuario_Teste_Returns_BadRequest()
        {
            // Arrange
            var loginVM = new LoginVM { Senha = "!12345", ConfirmaSenha = "!12345" };
            SetupBearerToken(2);

            // Act
            var result = _controleAcessoController.ChangePassword(loginVM) as ObjectResult;

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
            var loginVM = new LoginVM { Senha = "", ConfirmaSenha = "!12345" };
            SetupBearerToken(1);

            // Act
            var result = _controleAcessoController.ChangePassword(loginVM) as ObjectResult;

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
            var loginVM = new LoginVM { Senha = "!12345", ConfirmaSenha = "" };
            SetupBearerToken(1);

            // Act
            var result = _controleAcessoController.ChangePassword(loginVM) as ObjectResult;

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
            var loginVM = new LoginVM { Senha = "!12345", ConfirmaSenha = "!12345" };
            SetupBearerToken(1);
            _mockControleAcessoBusiness.Setup(b => b.ChangePassword(1, "!12345")).Returns(false);

            // Act
            var result = _controleAcessoController.ChangePassword(loginVM) as ObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.IsType<BadRequestObjectResult>(result);
            var value = result.Value;
            var message = value?.GetType()?.GetProperty("message")?.GetValue(value, null) as string;
            Assert.Equal(
                "Erro ao trocar senha tente novamente mais tarde ou entre em contato com nosso suporte.",
                message
            );
        }

        [Fact]
        public void RecoveryPassword_WithValidEmail_ReturnsOkResult()
        {
            // Arrange
            var email = "teste@teste.com";
            _mockControleAcessoBusiness.Setup(b => b.RecoveryPassword(email)).Returns(true);

            // Act
            var result = _controleAcessoController.RecoveryPassword(email) as ObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.IsType<OkObjectResult>(result);
            var value = result.Value;

            var message = (bool)value.GetType().GetProperty("message").GetValue(value, null);

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
}
