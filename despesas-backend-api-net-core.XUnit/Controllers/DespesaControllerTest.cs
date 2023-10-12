using despesas_backend_api_net_core.Business.Generic;
using despesas_backend_api_net_core.Controllers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Xunit.Extensions.Ordering;

namespace Test.XUnit.Controllers
{
    [Order(11)]
    public class DespesaControllerTest
    {
        protected Mock<IBusiness<DespesaVM>> _mockDespesaBusiness;
        protected DespesaController _despesaController;
        protected List<DespesaVM> _despesaVMs;
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

            _despesaController.ControllerContext = new ControllerContext
            {
                HttpContext = httpContext
            };
        }

        public DespesaControllerTest()
        {
            
            _mockDespesaBusiness = new Mock<IBusiness<DespesaVM>>();
            _despesaController = new DespesaController(_mockDespesaBusiness.Object);
            _despesaVMs = DespesaFaker.DespesasVMs();    
        }

        [Fact, Order(1)]
        public void Get_Should_Return_All_Despesas_From_Usuario()
        {
            // Arrange
            int idUsuario = _despesaVMs.First().IdUsuario;
            SetupBearerToken(idUsuario);
            _mockDespesaBusiness.Setup(business => business.FindAll(idUsuario)).Returns(_despesaVMs);

            // Act
            var result = _despesaController.Get() as ObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.IsType<OkObjectResult>(result);
            Assert.Equal(_despesaVMs, result.Value);
            _mockDespesaBusiness.Verify(b => b.FindAll(idUsuario), Times.Once);
        }

        [Fact, Order(2)]
        public void GetById_Should_Returns_BadRequest_When_Despesa_NULL()
        {
            // Arrange
            var despesaVM = _despesaVMs.First();
            var idUsuario = despesaVM.IdUsuario;
            SetupBearerToken(idUsuario);
            _mockDespesaBusiness.Setup(business => business.FindById(despesaVM.Id, idUsuario)).Returns((DespesaVM)null);

            // Act
            var result = _despesaController.Get(despesaVM.Id) as ObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.IsType<BadRequestObjectResult>(result);
            var value = result.Value;
            var message = (string)value.GetType().GetProperty("message").GetValue(value, null);
            Assert.Equal("Nenhuma despesa foi encontrada.", message);
            _mockDespesaBusiness.Verify(b => b.FindById(despesaVM.Id, idUsuario), Times.Once);
        }

        [Fact, Order(3)]
        public void GetById_Should_Return_Despesa_With_Valid_Id()
        {
            // Arrange
            var despesa = _despesaVMs.Last();
            int idUsuario = despesa.IdUsuario;
            int despesaId = despesa.Id;
            SetupBearerToken(idUsuario);
            
            _mockDespesaBusiness.Setup(business => business.FindById(despesaId, idUsuario)).Returns(despesa);

            // Act
            var result = _despesaController.Get(despesaId) as ObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.IsType<OkObjectResult>(result);
            var value = result.Value;
            var message = (bool)value.GetType().GetProperty("message").GetValue(value, null);
            Assert.True(message);
            var _despesa = (DespesaVM)value.GetType().GetProperty("despesa").GetValue(value, null);
            Assert.NotNull(_despesa);
            Assert.IsType<DespesaVM>(_despesa);
            _mockDespesaBusiness.Verify(b => b.FindById(despesaId, idUsuario), Times.Once);
        }

        [Fact, Order(4)]
        public void GetById_Should_Returns_BadRequest_When_Throws_Error()
        {
            // Arrange
            var despesaVM = _despesaVMs.First();
            var idUsuario = despesaVM.IdUsuario;
            SetupBearerToken(idUsuario);
            _mockDespesaBusiness.Setup(business => business.FindById(despesaVM.Id, idUsuario)).Throws(new Exception());

            // Act
            var result = _despesaController.Get(despesaVM.Id) as ObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.IsType<BadRequestObjectResult>(result);
            var value = result.Value;
            var message = (string)value.GetType().GetProperty("message").GetValue(value, null);
            Assert.Equal("Não foi possível realizar a consulta da despesa.", message);
            _mockDespesaBusiness.Verify(b => b.FindById(despesaVM.Id, idUsuario), Times.Once);
        }

        [Fact, Order(5)]
        public void GetByIdUsuario_Should_Return_Despesas_For_Valid_UserId()
        {
            // Arrange
            int idUsuario = _despesaVMs.Last().Id;
            SetupBearerToken(idUsuario);            
            _mockDespesaBusiness.Setup(business => business.FindAll(idUsuario)).Returns(_despesaVMs.FindAll( d => d.IdUsuario == idUsuario));

            // Act
            var result = _despesaController.Post(idUsuario) as ObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.IsType<OkObjectResult>(result);
            Assert.Equal(_despesaVMs.FindAll(d => d.IdUsuario == idUsuario), result.Value);
            _mockDespesaBusiness.Verify(b => b.FindAll(idUsuario), Times.Once);
        }

        [Fact, Order(6)]
        public void GetByIdUsuario_Should_Returns_BadRequest_For_Invalid_UserId()
        {
            // Arrange
            int idUsuario = _despesaVMs.Last().Id;
            SetupBearerToken(2);
            _mockDespesaBusiness.Setup(business => business.FindAll(idUsuario)).Returns(_despesaVMs.FindAll(d => d.IdUsuario == idUsuario));

            // Act
            var result = _despesaController.Post(idUsuario) as ObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.IsType<BadRequestObjectResult>(result);
            var value = result.Value;
            var message = (string)value.GetType().GetProperty("message").GetValue(value, null);
            Assert.Equal("Usuário não permitido a realizar operação!", message);
            _mockDespesaBusiness.Verify(b => b.FindAll(idUsuario), Times.Never);
        }

        [Fact, Order(7)]
        public void GetByIdUsuario_Should_Returns_BadRequest_For_UserId_0()
        {
            // Arrange
            var despesa = _despesaVMs.Last();
            despesa.IdUsuario = 0;
            int idUsuario = despesa.IdUsuario;
            SetupBearerToken(idUsuario);
            _mockDespesaBusiness.Setup(business => business.FindAll(idUsuario)).Returns(_despesaVMs.FindAll(d => d.IdUsuario == idUsuario));

            // Act
            var result = _despesaController.Post(idUsuario) as ObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.IsType<BadRequestObjectResult>(result);
            var value = result.Value;
            var message = (string)value.GetType().GetProperty("message").GetValue(value, null);
            Assert.Equal("Usuário inexistente!", message);
            _mockDespesaBusiness.Verify(b => b.FindAll(idUsuario), Times.Never);
        }

        [Fact, Order(8)]
        public void Post_Should_Create_Despesa()
        {
            // Arrange
            var despesaVM = _despesaVMs[3];
            int idUsuario = despesaVM.IdUsuario ;
            SetupBearerToken(idUsuario);
            _mockDespesaBusiness.Setup(business => business.Create(despesaVM)).Returns(despesaVM);

            // Act
            var result = _despesaController.Post(despesaVM) as ObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.IsType<OkObjectResult>(result);
            var value = result.Value;
            var message = (bool)value.GetType().GetProperty("message").GetValue(value, null);
            Assert.True(message);
            var _despesa = (DespesaVM)value.GetType().GetProperty("despesa").GetValue(value, null);
            Assert.NotNull(_despesa);
            Assert.IsType<DespesaVM>(_despesa);
            _mockDespesaBusiness.Verify(b => b.Create(despesaVM), Times.Once());
        }

        [Fact, Order(9)]
        public void Post_Should_Returns_BadRequest_When_Despesa_With_InvalidUsuario()
        {
            // Arrange
            var despesaVM = _despesaVMs[3];
            int idUsuario = despesaVM.IdUsuario;
            SetupBearerToken(0);
            _mockDespesaBusiness.Setup(business => business.Create(despesaVM)).Returns(despesaVM);

            // Act
            var result = _despesaController.Post(despesaVM) as ObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.IsType<BadRequestObjectResult>(result);
            var value = result.Value;
            var message = (string)value.GetType().GetProperty("message").GetValue(value, null);
            Assert.Equal("Usuário não permitido a realizar operação!", message);
            _mockDespesaBusiness.Verify(b => b.Create(despesaVM), Times.Never);
        }

        [Fact, Order(10)]
        public void Post_Should_Returns_BadRequest_When_Throws_Error()
        {
            // Arrange
            var despesaVM = _despesaVMs[3];
            int idUsuario = despesaVM.IdUsuario;
            SetupBearerToken(idUsuario);
            _mockDespesaBusiness.Setup(business => business.Create(despesaVM)).Throws(new Exception());

            // Act
            var result = _despesaController.Post(despesaVM) as ObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.IsType<BadRequestObjectResult>(result);
            var value = result.Value;
            var message = (string)value.GetType().GetProperty("message").GetValue(value, null);
            Assert.Equal("Não foi possível realizar o cadastro da despesa.", message);
            _mockDespesaBusiness.Verify(b => b.Create(despesaVM), Times.Once);
        }

        [Fact, Order(11)]
        public void Put_Should_Returns_BadRequest_When_Despesa_With_InvalidUsuario()
        {
            // Arrange
            var despesaVM = _despesaVMs[3];
            int idUsuario = despesaVM.IdUsuario;
            SetupBearerToken(0);
            _mockDespesaBusiness.Setup(business => business.Update(despesaVM)).Returns(despesaVM);

            // Act
            var result = _despesaController.Put(despesaVM) as ObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.IsType<BadRequestObjectResult>(result);
            var value = result.Value;
            var message = (string)value.GetType().GetProperty("message").GetValue(value, null);
            Assert.Equal("Usuário não permitido a realizar operação!", message);
            _mockDespesaBusiness.Verify(b => b.Update(despesaVM), Times.Never);
        }

        [Fact, Order(12)]
        public void Put_Should_Update_Despesa()
        {
            // Arrange
            var despesaVM = _despesaVMs[4];
            int idUsuario = despesaVM.IdUsuario;
            SetupBearerToken(idUsuario);         
            _mockDespesaBusiness.Setup(business => business.Update(despesaVM)).Returns(despesaVM);

            // Act
            var result = _despesaController.Put(despesaVM) as ObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.IsType<OkObjectResult>(result);
            var value = result.Value;
            var message = (bool)value.GetType().GetProperty("message").GetValue(value, null);
            Assert.True(message);
            var _despesa = (DespesaVM)value.GetType().GetProperty("despesa").GetValue(value, null);
            Assert.NotNull(_despesa);
            Assert.IsType<DespesaVM>(_despesa);
            _mockDespesaBusiness.Verify(b => b.Update(despesaVM), Times.Once);   
        }

        [Fact, Order(13)]
        public void Put_Should_Returns_BadRequest_When_Despesa_Return_Null()
        {
            // Arrange
            var despesaVM = _despesaVMs[3];
            int idUsuario = despesaVM.IdUsuario;
            SetupBearerToken(idUsuario);
            _mockDespesaBusiness.Setup(business => business.Update(despesaVM)).Returns((DespesaVM)null);

            // Act
            var result = _despesaController.Put(despesaVM) as ObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.IsType<BadRequestObjectResult>(result);
            var value = result.Value;
            var message = (string)value.GetType().GetProperty("message").GetValue(value, null);
            Assert.Equal("Não foi possível atualizar o cadastro da despesa.", message);
            _mockDespesaBusiness.Verify(b => b.Update(despesaVM), Times.Once);
        }

        [Fact, Order(14)]
        public void Delete_Should_Returns_OkResult()
        {
            // Arrange
            var despesaVM = _despesaVMs[2];
            int idUsuario = despesaVM.IdUsuario;
            SetupBearerToken(idUsuario);
            
            _mockDespesaBusiness.Setup(business => business.Delete(despesaVM)).Returns(true);

            // Act
            var result = _despesaController.Delete(despesaVM) as ObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.IsType<OkObjectResult>(result);
            var value = result.Value;
            var message = (bool)value.GetType().GetProperty("message").GetValue(value, null);
            Assert.True(message);
            _mockDespesaBusiness.Verify(b => b.Delete(despesaVM), Times.Once);
        }

        [Fact, Order(15)]
        public void Delete_Should_Returns_BadRequest_With_InvalidUsuario()
        {
            // Arrange
            var despesaVM = _despesaVMs[2];
            int idUsuario = despesaVM.IdUsuario;
            SetupBearerToken(0);

            _mockDespesaBusiness.Setup(business => business.Delete(despesaVM)).Returns(true);

            // Act
            var result = _despesaController.Delete(despesaVM) as ObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.IsType<BadRequestObjectResult>(result);
            var value = result.Value;
            var message = (string)value.GetType().GetProperty("message").GetValue(value, null);
            Assert.Equal("Usuário não permitido a realizar operação!", message);
            _mockDespesaBusiness.Verify(b => b.Delete(despesaVM), Times.Never);
        }

        [Fact, Order(16)]
        public void Delete_Should_Returns_BadResquest_When_Despesa_Not_Deleted()
        {
            // Arrange
            var despesaVM = _despesaVMs[2];
            int idUsuario = despesaVM.IdUsuario;
            SetupBearerToken(idUsuario);

            _mockDespesaBusiness.Setup(business => business.Delete(despesaVM)).Returns(false);

            // Act
            var result = _despesaController.Delete(despesaVM) as ObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.IsType<BadRequestObjectResult>(result);
            var value = result.Value;
            var message = (string)value.GetType().GetProperty("message").GetValue(value, null);
            Assert.Equal("Erro ao excluir Despesa!", message);
            _mockDespesaBusiness.Verify(b => b.Delete(despesaVM), Times.Once);
        }
    }
}
