using despesas_backend_api_net_core.Business;
using despesas_backend_api_net_core.Controllers;
using despesas_backend_api_net_core.Domain.Entities;
using despesas_backend_api_net_core.Domain.VM;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace Test.XUnit.Controllers
{
    public class TestControleAcessoController
    {
        private readonly ControleAcessoController _controller;
        private readonly Mock<IControleAcessoBusiness> _controleAcessoBusinessMock;

        public TestControleAcessoController()
        {
            _controleAcessoBusinessMock = new Mock<IControleAcessoBusiness>();
            _controller = new ControleAcessoController(_controleAcessoBusinessMock.Object);
        }

        [Fact]
        public void Post_ValidControleAcesso_ReturnsOkResult()
        {
            // Arrange
            var controleAcessoVO = new ControleAcessoVM
            {
                Email = "example@example.com",
                Senha = "password",
                Nome = "John",
                SobreNome = "Doe",
                Telefone = "123456789",
            };

            _controleAcessoBusinessMock
                .Setup(business => business.Create(It.IsAny<ControleAcesso>()))
                .Returns(true);

            // Act
            var result = _controller.Post(controleAcessoVO);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var response = Assert.IsType<OkObjectResult>(okResult.Value);
            Assert.True((bool)response.Value);
        }

        [Fact]
        public void Post_InvalidControleAcesso_ReturnsBadRequestResult()
        {
            // Arrange
            var controleAcessoVO = new ControleAcessoVM();

            // Act
            var result = _controller.Post(controleAcessoVO);

            // Assert
            Assert.IsType<BadRequestResult>(result);
        }

        [Fact]
        public void SignIn_ValidLogin_ReturnsObjectResult()
        {
            // Arrange
            var login = new LoginVM
            {
                Email = "example@example.com",
                Senha = "password"
            };

            _controleAcessoBusinessMock
                .Setup(business => business.FindByLogin(It.IsAny<ControleAcesso>()))
                .Returns(new ControleAcesso());

            // Act
            var result = _controller.SignIn(login);

            // Assert
            Assert.IsType<ObjectResult>(result);
        }

        [Fact]
        public void ChangePassword_ValidLogin_ReturnsOkResult()
        {
            // Arrange
            var login = new LoginVM
            {
                IdUsuario = 123,
                Senha = "newpassword"
            };

            _controleAcessoBusinessMock
                .Setup(business => business.ChangePassword(It.IsAny<int>(), It.IsAny<string>()))
                .Returns(true);

            // Act
            var result = _controller.ChangePassword(login);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var response = Assert.IsType<OkObjectResult>(okResult.Value);
            Assert.True((bool)response.Value);
        }

        [Fact]
        public void ChangePassword_InvalidLogin_ReturnsBadRequestResult()
        {
            // Arrange
            var login = new LoginVM();

            // Act
            var result = _controller.ChangePassword(login);

            // Assert
            Assert.IsType<BadRequestResult>(result);
        }

        [Fact]
        public void RecoveryPassword_ValidEmail_ReturnsOkResult()
        {
            // Arrange
            var email = "example@example.com";

            _controleAcessoBusinessMock
                .Setup(business => business.RecoveryPassword(It.IsAny<string>()))
                .Returns(true);

            // Act
            var result = _controller.RecoveryPassword(email);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var response = Assert.IsType<OkObjectResult>(okResult.Value);
            Assert.True((bool)response.Value);
        }

        [Fact]
        public void RecoveryPassword_InvalidEmail_ReturnsBadRequestResult()
        {
            // Arrange
            string email = null;

            // Act
            var result = _controller.RecoveryPassword(email);

            // Assert
            Assert.IsType<BadRequestResult>(result);
        }

        // Add more test cases for other methods if needed
    }

}

