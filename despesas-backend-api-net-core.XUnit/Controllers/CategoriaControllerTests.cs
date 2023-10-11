using despesas_backend_api_net_core.Business.Generic;
using despesas_backend_api_net_core.Controllers;
using despesas_backend_api_net_core.Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Test.XUnit.Controllers
{
    public class CategoriaControllerTests : BaseCategoriaTest
    {
        private void SetupBearerToken(int userId)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, userId.ToString())
            };
            var identity = new ClaimsIdentity(claims, "IdUsuario");
            var claimsPrincipal = new ClaimsPrincipal(identity);


            var httpContext = new DefaultHttpContext
            {
                User = claimsPrincipal
            };
            httpContext.Request.Headers["Authorization"] = "Bearer " + Usings.GenerateJwtToken(userId);

            _categoriaController.ControllerContext = new ControllerContext
            {
                HttpContext = httpContext
            };
        }

        public CategoriaControllerTests()
        {
        }


        [Fact]
        public void Get_Returns_Ok_Result()
        {
            // Arrange
            _mockCategoriaBusiness = new Mock<IBusiness<CategoriaVM>>();
            _categoriaController = new CategoriaController(_mockCategoriaBusiness.Object);
            var categoriaVM = _categoriaVMs.First();
            SetupBearerToken(categoriaVM.IdUsuario);
            _mockCategoriaBusiness.Setup(b => b.FindAll(categoriaVM.IdUsuario)).Returns(_categoriaVMs);

            // Act
            var result = _categoriaController.Get() as OkObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.IsType<List<CategoriaVM>>(result.Value);
            var returnedResults = result.Value as List<CategoriaVM>;
            Assert.Equal(_categoriaVMs.Count, returnedResults.Count);
        }

        [Fact]
        public void GetById_Returns_Ok_Result()
        {
            // Arrange            
            _mockCategoriaBusiness = new Mock<IBusiness<CategoriaVM>>();
            _categoriaController = new CategoriaController(_mockCategoriaBusiness.Object);
            var categoriaVM = _categoriaVMs.First();
            var idCategoria = categoriaVM.IdUsuario;
            SetupBearerToken(idCategoria);

            _mockCategoriaBusiness.Setup(b => b.FindById(idCategoria, categoriaVM.IdUsuario)).Returns(categoriaVM);

            // Act
            var result = _categoriaController.GetById(idCategoria) as OkObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.IsType<CategoriaVM>(result.Value);
            Assert.Equal(categoriaVM, result.Value);
        }

        [Fact]
        public void GetById_Returns_NotFound()
        {
            // Arrange
            _mockCategoriaBusiness = new Mock<IBusiness<CategoriaVM>>();
            _categoriaController = new CategoriaController(_mockCategoriaBusiness.Object);
            var idCategoria = 0;
            var categoriaVM = new CategoriaVM();
            SetupBearerToken(idCategoria);
            _mockCategoriaBusiness.Setup(b => b.FindById(idCategoria, categoriaVM.IdUsuario)).Returns((CategoriaVM)null);

            // Act
            var result = _categoriaController.GetById(idCategoria) as NotFoundResult;

            // Assert
            Assert.NotNull(result);
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public void GetByIdUsuario_Returns_Ok_Result()
        {
            // Arrange
            _mockCategoriaBusiness = new Mock<IBusiness<CategoriaVM>>();
            _categoriaController = new CategoriaController(_mockCategoriaBusiness.Object);
            var idUsuario = _categoriaVMs.First().Id;
            SetupBearerToken(idUsuario);
            var dataSet = _categoriaVMs.FindAll(c => c.IdUsuario == idUsuario);
            _mockCategoriaBusiness.Setup(b => b.FindAll(idUsuario)).Returns(dataSet);


            // Act
            var result = _categoriaController.GetByIdUsuario(idUsuario) as OkObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.IsType<List<CategoriaVM>>(result.Value);
            var returnedResults = result.Value as List<CategoriaVM>;
            Assert.Equal(dataSet.Count, returnedResults.Count);
        }

        [Fact]
        public void GetByIdUsuario_Returns_BadRequest()
        {
            // Arrange
            _mockCategoriaBusiness = new Mock<IBusiness<CategoriaVM>>();
            _categoriaController = new CategoriaController(_mockCategoriaBusiness.Object);
            var categoriaVM = _categoriaVMs.First();
            var idUsuario = categoriaVM.IdUsuario;
            SetupBearerToken(10); 
            _mockCategoriaBusiness.Setup(b => b.FindAll(idUsuario)).Returns(new List<CategoriaVM>());

            // Act
            var result = _categoriaController.GetByIdUsuario(idUsuario) as ObjectResult;

            // Assert
            Assert.NotNull(result);            
            Assert.IsType<BadRequestObjectResult>(result);
            var value = result.Value;
            var message = (string)value.GetType().GetProperty("message").GetValue(value, null);
            Assert.Equal(message, "Usuário não permitido a realizar operação!");
        }

        [Fact]
        public void GetByTipoCategoria_Returns_Ok_Result()
        {
            // Arrange
            _mockCategoriaBusiness = new Mock<IBusiness<CategoriaVM>>();
            _categoriaController = new CategoriaController(_mockCategoriaBusiness.Object);
            var categoriaVM = _categoriaVMs.First();
            var idUsuario = categoriaVM.IdUsuario;            
            SetupBearerToken(categoriaVM.Id);
            var tipoCategoria = TipoCategoria.Todas;
            var dataSet = _categoriaVMs.FindAll(c => c.Id == idUsuario && c.IdTipoCategoria == (int)tipoCategoria);
            _mockCategoriaBusiness.Setup(b => b.FindAll(idUsuario)).Returns(dataSet);

            // Act
            var result = _categoriaController.GetByTipoCategoria(idUsuario, tipoCategoria) as ObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.IsType<OkObjectResult>(result);            
            Assert.IsType<List<CategoriaVM>>(result.Value);
            var returnedResults = result.Value as List<CategoriaVM>;
            Assert.Equal(dataSet.Count, returnedResults.Count);
        }

        [Fact]
        public void Post_Returns_Ok_Result()
        {
            // Arrange
            _mockCategoriaBusiness = new Mock<IBusiness<CategoriaVM>>();
            _categoriaController = new CategoriaController(_mockCategoriaBusiness.Object);
            var categoriaVM = _categoriaVMs.First();
            SetupBearerToken(categoriaVM.Id);

            _mockCategoriaBusiness.Setup(b => b.Create(categoriaVM)).Returns(categoriaVM);

            // Act
            var result = _categoriaController.Post(categoriaVM) as ObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.IsType<CategoriaVM>(result.Value);
            var value = result.Value;
            var message = (bool)value.GetType().GetProperty("message").GetValue(value, null);
            Assert.True(message);

        }

        [Fact]
        public void Post_Returns_Bad_Request_When_BearerToken_Diferenet_Usuario()
        {
            // Arrange
            _mockCategoriaBusiness = new Mock<IBusiness<CategoriaVM>>();
            _categoriaController = new CategoriaController(_mockCategoriaBusiness.Object);
            var categoriaVM = _categoriaVMs.First();
            SetupBearerToken(0);

            // Assume a mismatch in the user ID
            _mockCategoriaBusiness.Setup(b => b.Create(categoriaVM)).Returns(categoriaVM);

            // Act
            var result = _categoriaController.Post(categoriaVM) as ObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.IsType<BadRequestObjectResult>(result);
            var value = result.Value;
            var message = (string)value.GetType().GetProperty("message").GetValue(value, null);
            Assert.Equal(message, "Usuário não permitido a realizar operação!");
        }

        [Fact]
        public void Post_Returns_Bad_Request_When_TipoCategoria_Todas()
        {
            // Arrange
            _mockCategoriaBusiness = new Mock<IBusiness<CategoriaVM>>();
            _categoriaController = new CategoriaController(_mockCategoriaBusiness.Object);
            var obj = _categoriaVMs.First();
            var categoriaVM = new CategoriaVM
            {
                Id = obj.Id,
                Descricao = obj.Descricao,
                IdUsuario = obj.IdUsuario,
                IdTipoCategoria = (int)TipoCategoria.Todas
            };
            SetupBearerToken(categoriaVM.Id);

            // Assume a mismatch in the user ID
            _mockCategoriaBusiness.Setup(b => b.Create(categoriaVM)).Returns(categoriaVM);

            // Act
            var result = _categoriaController.Post(categoriaVM) as ObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.IsType<BadRequestObjectResult>(result);
            var value = result.Value;
            var message = (string)value.GetType().GetProperty("message").GetValue(value, null);
            Assert.Equal(message, "Nenhum tipo de Categoria foi selecionado!");
        }

        [Fact]
        public void Post_Returns_Bad_Request_When_TryCatch_ThrowError()
        {
            // Arrange
            _mockCategoriaBusiness = new Mock<IBusiness<CategoriaVM>>();
            _categoriaController = new CategoriaController(_mockCategoriaBusiness.Object);
            var categoriaVM = _categoriaVMs.First();
            SetupBearerToken(categoriaVM.Id);

            // Assume a mismatch in the user ID
            _mockCategoriaBusiness.Setup(b => b.Create(categoriaVM)).Throws(new Exception());

            // Act
            var result = _categoriaController.Post(categoriaVM) as ObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.IsType<BadRequestObjectResult>(result);
            var value = result.Value;
            var message = (string)value.GetType().GetProperty("message").GetValue(value, null);
            Assert.Equal(message, "Não foi possível realizar o cadastro de uma nova categoria, tente mais tarde ou entre em contato com o suporte.");
        }

        [Fact]
        public void Put_Returns_Ok_Result()
        {
            // Arrange
            _mockCategoriaBusiness = new Mock<IBusiness<CategoriaVM>>();
            _categoriaController = new CategoriaController(_mockCategoriaBusiness.Object);
            var categoriaVM = _categoriaVMs.First();
            SetupBearerToken(categoriaVM.Id);

            _mockCategoriaBusiness.Setup(b => b.Update(categoriaVM)).Returns(categoriaVM);

            // Act
            var result = _categoriaController.Put(categoriaVM) as ObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.IsType<CategoriaVM>(result.Value);
            var value = result.Value;
            var message = (bool)value.GetType().GetProperty("message").GetValue(value, null);
            Assert.True(message);
        }

        [Fact]
        public void Put_Returns_Bad_Request()
        {
            // Arrange
            _mockCategoriaBusiness = new Mock<IBusiness<CategoriaVM>>();
            _categoriaController = new CategoriaController(_mockCategoriaBusiness.Object);
            var categoriaVM = _categoriaVMs.First();
            SetupBearerToken(categoriaVM.Id);
            
            // Assume a mismatch in the user ID
            _mockCategoriaBusiness.Setup(b => b.Update(categoriaVM)).Returns((CategoriaVM)null);

            // Act
            var result = _categoriaController.Put(categoriaVM) as BadRequestObjectResult;

            // Assert
            Assert.NotNull(result);
        }

        [Fact]
        public void Delete_Returns_Ok_Result()
        {
            // Arrange
            _mockCategoriaBusiness = new Mock<IBusiness<CategoriaVM>>();
            _categoriaController = new CategoriaController(_mockCategoriaBusiness.Object);
            var categoriaVM = _categoriaVMs.Last();
            SetupBearerToken(categoriaVM.Id);
            
            _mockCategoriaBusiness.Setup(b => b.Delete(It.IsAny<CategoriaVM>())).Returns(true);

            // Act
            var result = _categoriaController.Delete(categoriaVM) as ObjectResult;

            // Assert
            Assert.NotNull(result);
            var value = result.Value;
            var message = (bool)value.GetType().GetProperty("message").GetValue(value, null);
            Assert.True(message);
        }

        [Fact]
        public void Delete_Returns_OK_Result_Message_False()
        {
            // Arrange
            _mockCategoriaBusiness = new Mock<IBusiness<CategoriaVM>>();
            _categoriaController = new CategoriaController(_mockCategoriaBusiness.Object);
            var categoriaVM = _categoriaVMs.First();
            SetupBearerToken(categoriaVM.Id);
            _mockCategoriaBusiness.Setup(b => b.Delete(categoriaVM)).Returns(false);

            // Act
            var result = _categoriaController.Delete(categoriaVM) as ObjectResult;

            // Assert
            Assert.NotNull(result);
            var value = result.Value;
            var message = (bool)value.GetType().GetProperty("message").GetValue(value, null);
            Assert.False(message);
        }

        [Fact]
        public void Delete_Returns_BadRequest()
        {
            // Arrange
            _mockCategoriaBusiness = new Mock<IBusiness<CategoriaVM>>();
            _categoriaController = new CategoriaController(_mockCategoriaBusiness.Object);
            var categoriaVM = _categoriaVMs.First();
            SetupBearerToken(12);            
            _mockCategoriaBusiness.Setup(b => b.Delete(categoriaVM)).Returns(false);

            // Act
            var result = _categoriaController.Delete(categoriaVM) as BadRequestObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.IsType<BadRequestObjectResult>(result);
        }
    }

    public class BaseCategoriaTest
    {
        protected Mock<IBusiness<CategoriaVM>> _mockCategoriaBusiness;
        protected CategoriaController _categoriaController;
        protected List<CategoriaVM> _categoriaVMs;

        public BaseCategoriaTest()
        {
            _mockCategoriaBusiness = new Mock<IBusiness<CategoriaVM>>();
            _categoriaController = new CategoriaController(_mockCategoriaBusiness.Object);
            _categoriaVMs = CategoriaFaker.CategoriasVMs();
        }
    }
}

