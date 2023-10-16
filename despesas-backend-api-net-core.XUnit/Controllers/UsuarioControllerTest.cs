using despesas_backend_api_net_core.Business;
using despesas_backend_api_net_core.Controllers;
using despesas_backend_api_net_core.Infrastructure.Data.EntityConfig;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Xunit.Extensions.Ordering;

namespace Test.XUnit.Controllers
{
    [Order(13)]
    public class UsuarioControllerTest
    {
        protected Mock<IUsuarioBusiness> _mockUsuarioBusiness;
        protected UsuarioController _usuarioController;
        protected List<UsuarioVM> _usuarioVMs;
        private UsuarioVM administrador;
        private UsuarioVM usuarioNormal;

        private void SetupBearerToken(int idUsuario)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, idUsuario.ToString())
            };
            var identity = new ClaimsIdentity(claims, "IdUsuario");
            var claimsPrincipal = new ClaimsPrincipal(identity);


            var httpContext = new DefaultHttpContext
            {
                User = claimsPrincipal
            };
            httpContext.Request.Headers["Authorization"] = "Bearer " + Usings.GenerateJwtToken(idUsuario);

            _usuarioController.ControllerContext = new ControllerContext
            {
                HttpContext = httpContext
            };
        }

        public UsuarioControllerTest()
        {
            
            _mockUsuarioBusiness = new Mock<IUsuarioBusiness>();
            _usuarioController = new UsuarioController(_mockUsuarioBusiness.Object);
            var usuarios = UsuarioFaker.Usuarios();
            administrador = new UsuarioMap().Parse(usuarios.FindAll(u => u.PerfilUsuario == PerfilUsuario.Administrador).First());
            usuarioNormal = new UsuarioMap().Parse(usuarios.FindAll(u => u.PerfilUsuario == PerfilUsuario.Usuario).First());
            _usuarioVMs = new UsuarioMap().ParseList(usuarios); 
        }

        [Fact, Order(1)]
        public void Get_Should_Returns_OkResult_When_Usuario_Normal()
        {
            // Arrange
            int idUsuario = usuarioNormal.Id;
            SetupBearerToken(idUsuario);            
            _mockUsuarioBusiness.Setup(business => business.FindAll(idUsuario)).Returns(new List<UsuarioVM>());
            _mockUsuarioBusiness.Setup(business => business.FindById(idUsuario)).Returns(usuarioNormal);

            // Act
            var result = _usuarioController.Get(idUsuario) as ObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.IsType<OkObjectResult>(result);
            Assert.IsType<List<UsuarioVM>>(result.Value);
            Assert.Equal(((List<UsuarioVM>)result.Value).Count, 0);
            _mockUsuarioBusiness.Verify(b => b.FindAll(idUsuario), Times.Never);
        }

        [Fact, Order(2)]
        public void Get_With_InvalidToken_Returns_BadRequest()
        {
            // Arrange
            int idUsuario = _usuarioVMs.Last().Id;
            SetupBearerToken(0);
            _mockUsuarioBusiness.Setup(business => business.FindAll(idUsuario)).Returns(_usuarioVMs.FindAll(u => u.Id == idUsuario));

            // Act
            var result = _usuarioController.Get(idUsuario) as ObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.IsType<BadRequestObjectResult>(result);
            var value = result.Value;
            var message = value?.GetType()?.GetProperty("message")?.GetValue(value, null) as string;
            Assert.Equal("Usuário não permitido a realizar operação!", message);
            _mockUsuarioBusiness.Verify(b => b.FindAll(idUsuario), Times.Never);
        }

        [Fact, Order(3)]
        public void Get_Should_Returns_OkResult_With_Usuarios()
        {
            // Arrange
            int idUsuario = administrador.Id;
            SetupBearerToken(idUsuario);
            _mockUsuarioBusiness.Setup(business => business.FindAll(idUsuario)).Returns(_usuarioVMs.FindAll(u => u.Id == idUsuario));
            _mockUsuarioBusiness.Setup(business => business.FindById(idUsuario)).Returns(administrador);
            // Act
            var result = _usuarioController.Get(idUsuario) as ObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.IsType<OkObjectResult>(result);
            Assert.IsType<List<UsuarioVM>>(result.Value);
            Assert.Equal(_usuarioVMs.FindAll(u => u.Id == idUsuario), result.Value);
            _mockUsuarioBusiness.Verify(b => b.FindAll(idUsuario), Times.Once);
        }

        [Fact, Order(4)]
        public void Post_Should_Return_UsuarioVM()
        {
            // Arrange
            var usuarioVM = _usuarioVMs.First();
            int idUsuario = usuarioVM.Id;
            SetupBearerToken(idUsuario);
            
            _mockUsuarioBusiness.Setup(business => business.Create(usuarioVM)).Returns(usuarioVM);

            // Act
            var result = _usuarioController.Post(usuarioVM) as ObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.IsType<OkObjectResult>(result);
            Assert.IsType<UsuarioVM>(result.Value);
            _mockUsuarioBusiness.Verify(b => b.Create(usuarioVM), Times.Once);
        }

        [Fact, Order(5)]
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
            // Assert
            Assert.NotNull(result);
            Assert.IsType<OkObjectResult>(result);
            Assert.IsType<UsuarioVM>(result.Value);
            _mockUsuarioBusiness.Verify(b => b.Update(usuarioVM), Times.Once);
        }

        [Fact, Order(6)]
        public void Delete_Should_Return_True()
        {
            // Arrange
            var usuarioVM = usuarioNormal;
            int idUsuario = usuarioVM.Id;
            SetupBearerToken(administrador.Id);
            
            _mockUsuarioBusiness.Setup(business => business.Delete(usuarioVM)).Returns(true);
            _mockUsuarioBusiness.Setup(business => business.FindById(administrador.Id)).Returns(administrador);

            // Act
            var result = _usuarioController.Delete(usuarioVM, administrador.Id) as ObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.IsType<OkObjectResult>(result);
            var value = result.Value;
            var message = (bool)value.GetType().GetProperty("message").GetValue(value, null);
            Assert.True(message);
            _mockUsuarioBusiness.Verify(b => b.Delete(usuarioVM), Times.Once);
        }

        [Fact, Order(7)]
        public void GetById_Should_Return_UsuarioVM()
        {
            // Arrange
            var usuarioVM = usuarioNormal;
            var idUsuario = usuarioVM.Id;
            SetupBearerToken(idUsuario);            
            
            _mockUsuarioBusiness.Setup(business => business.FindById(idUsuario)).Returns(usuarioNormal);

            // Act
            var result = _usuarioController.Post(idUsuario) as ObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.IsType<OkObjectResult>(result);
            Assert.IsType<UsuarioVM>(result.Value);
            Assert.Equal(usuarioNormal, result.Value);
            _mockUsuarioBusiness.Verify(b => b.FindById(idUsuario), Times.Once);
        }
        
        [Fact, Order(8)]
        public void GetById_With_InvalidToken_Returns_BadRequest()
        {
            // Arrange
            var usuarioVM = usuarioNormal;
            var idUsuario = usuarioVM.Id;
            SetupBearerToken(0);

            _mockUsuarioBusiness.Setup(business => business.FindById(idUsuario)).Returns(usuarioNormal);

            // Act
            var result = _usuarioController.Post(idUsuario) as ObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.IsType<BadRequestObjectResult>(result);
            var value = result.Value;
            var message = value?.GetType()?.GetProperty("message")?.GetValue(value, null) as string;
            Assert.Equal("Usuário não permitido a realizar operação!", message);
            _mockUsuarioBusiness.Verify(b => b.FindById(idUsuario), Times.Never);
        }

        [Fact, Order(9)]
        public void GetById_Should_Returns_BadRequest_When_Usuario_IsNull()
        {
            // Arrange
            var usuarioVM = usuarioNormal;
            var idUsuario = usuarioVM.Id;
            SetupBearerToken(idUsuario);

            _mockUsuarioBusiness.Setup(business => business.FindById(idUsuario)).Returns((UsuarioVM)null);

            // Act
            var result = _usuarioController.Post(idUsuario) as ObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.IsType<BadRequestObjectResult>(result);
            var value = result.Value;
            var message = value?.GetType()?.GetProperty("message")?.GetValue(value, null) as string;
            Assert.Equal("Usuário não encontrado!", message);
            _mockUsuarioBusiness.Verify(b => b.FindById(idUsuario), Times.Once);
        }

        [Fact, Order(10)]
        public void Post_With_InvalidToken_Returns_BadRequest()
        {
            // Arrange
            var usuarioVM = _usuarioVMs.First();
            int idUsuario = usuarioVM.Id;
            SetupBearerToken(0);

            _mockUsuarioBusiness.Setup(business => business.Create(usuarioVM)).Returns(usuarioVM);

            // Act
            var result = _usuarioController.Post(usuarioVM) as ObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.IsType<BadRequestObjectResult>(result);
            var value = result.Value;
            var message = value?.GetType()?.GetProperty("message")?.GetValue(value, null) as string;
            Assert.Equal("Usuário não permitido a realizar operação!", message);
            _mockUsuarioBusiness.Verify(b => b.Create(usuarioVM), Times.Never);
        }

        [Fact, Order(11)]
        public void Post_Should_Returns_BadRequest_When_Telefone_IsNull()
        {
            // Arrange
            var usuarioVM = _usuarioVMs.First();
            usuarioVM.Telefone = string.Empty;
            int idUsuario = usuarioVM.Id;
            SetupBearerToken(idUsuario);

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

        [Fact, Order(12)]
        public void Post_Should_Returns_BadRequest_When_Email_IsNull()
        {
            // Arrange
            var usuarioVM = _usuarioVMs.First();
            usuarioVM.Email = string.Empty;
            int idUsuario = usuarioVM.Id;
            SetupBearerToken(idUsuario);

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

        [Fact, Order(13)]
        public void Post_Should_Returns_BadRequest_When_Email_IsNullOrWhiteSpace()
        {
            // Arrange
            var usuarioVM = _usuarioVMs.First();
            usuarioVM.Email = " ";
            int idUsuario = usuarioVM.Id;
            SetupBearerToken(idUsuario);

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

        [Fact, Order(14)]
        public void Post_Should_Returns_BadRequest_When_Email_IsInvalid()
        {
            // Arrange
            var usuarioVM = _usuarioVMs.First();
            usuarioVM.Email = "invalidEmail.com";
            int idUsuario = usuarioVM.Id;
            SetupBearerToken(idUsuario);

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

        [Fact, Order(15)]
        public void Put_With_InvalidToken_Returns_BadRequest()
        {
            // Arrange
            var usuarioVM = _usuarioVMs.First();
            int idUsuario = usuarioVM.Id;
            SetupBearerToken(0);

            _mockUsuarioBusiness.Setup(business => business.Update(usuarioVM)).Returns(usuarioVM);

            // Act
            var result = _usuarioController.Put(usuarioVM) as ObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.IsType<BadRequestObjectResult>(result);
            var value = result.Value;
            var message = value?.GetType()?.GetProperty("message")?.GetValue(value, null) as string;
            Assert.Equal("Usuário não permitido a realizar operação!", message);
            _mockUsuarioBusiness.Verify(b => b.Update(usuarioVM), Times.Never);
        }

        [Fact, Order(16)]
        public void Put_Should_Returns_BadRequest_When_Telefone_IsNull()
        {
            // Arrange
            var usuarioVM = _usuarioVMs.First();
            usuarioVM.Telefone = string.Empty;
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
            Assert.Equal("Campo Telefone não pode ser em branco", message);
            _mockUsuarioBusiness.Verify(b => b.Update(usuarioVM), Times.Never);
        }

        [Fact, Order(17)]
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

        [Fact, Order(18)]
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

        [Fact, Order(19)]
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

        [Fact, Order(20)]
        public void Put_Should_Returns_BadRequest_When_Usuario_IsNull()
        {
            // Arrange
            var usuarioVM = _usuarioVMs.First();
            int idUsuario = usuarioVM.Id;
            SetupBearerToken(idUsuario);

            _mockUsuarioBusiness.Setup(business => business.Update(usuarioVM)).Returns((UsuarioVM)null);

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

        [Fact, Order(21)]
        public void Delete_With_InvalidToken_Returns_BadRequest()
        {
            // Arrange
            var usuarioVM = usuarioNormal;
            var idUsuario = usuarioVM.Id;
            SetupBearerToken(0);
            // Act
            var result = _usuarioController.Delete(usuarioNormal, idUsuario) as ObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.IsType<BadRequestObjectResult>(result);
            var value = result.Value;
            var message = value?.GetType()?.GetProperty("message")?.GetValue(value, null) as string;
            Assert.Equal("Usuário não permitido a realizar operação!", message);
        }

        [Fact, Order(22)]
        public void Delete_Should_Returns_BadRequest_When_Usuario_IsNotAdministrador()
        {
            // Arrange
            var usuarioVM = usuarioNormal;
            var idUsuario = usuarioVM.Id;
            SetupBearerToken(idUsuario);

            _mockUsuarioBusiness.Setup(business => business.FindById(idUsuario)).Returns(usuarioNormal);

            // Act
            var result = _usuarioController.Delete(usuarioNormal, idUsuario) as ObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.IsType<BadRequestObjectResult>(result);
            var value = result.Value;
            var message = value?.GetType()?.GetProperty("message")?.GetValue(value, null) as string;
            Assert.Equal("Usuário não possui permissão para exectar deleção!", message);
            _mockUsuarioBusiness.Verify(b => b.FindById(idUsuario), Times.Once);
        }

        [Fact, Order(23)]
        public void Delete_Should_Returns_BadRequest_When_Try_To_Delete_Usuario()
        {
            // Arrange
            var usuarioVM = usuarioNormal;
            var idUsuario = usuarioVM.Id;
            SetupBearerToken(idUsuario);

            _mockUsuarioBusiness.Setup(business => business.FindById(idUsuario)).Returns(administrador);
            _mockUsuarioBusiness.Setup(business => business.Delete(usuarioNormal)).Returns(false);

            // Act
            var result = _usuarioController.Delete(usuarioNormal, idUsuario) as ObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.IsType<BadRequestObjectResult>(result);
            var value = result.Value;
            var message = value?.GetType()?.GetProperty("message")?.GetValue(value, null) as string;
            Assert.Equal("Erro ao excluir Usuário!", message);
            _mockUsuarioBusiness.Verify(b => b.FindById(idUsuario), Times.Once);
            _mockUsuarioBusiness.Verify(b => b.Delete(usuarioVM), Times.Once);
        }
    }
}