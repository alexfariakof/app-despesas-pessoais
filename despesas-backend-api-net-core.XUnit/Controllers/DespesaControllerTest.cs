using despesas_backend_api_net_core.Controllers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Xunit.Extensions.Ordering;

namespace Controllers
{
    [Order(4)]
    public class DespesaControllerTest
    {
        protected Mock<IBusiness<DespesaVM>> _mockDespesaBusiness;
        protected DespesaController _despesaController;

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

            _despesaController.ControllerContext = new ControllerContext
            {
                HttpContext = httpContext
            };
        }

        public DespesaControllerTest()
        {
            _mockDespesaBusiness = new Mock<IBusiness<DespesaVM>>();
            _despesaController = new DespesaController(_mockDespesaBusiness.Object);
        }

        [Fact, Order(1)]
        public void Get_Should_Return_All_Despesas_From_Usuario()
        {
            // Arrange
            var _despesaVMs = DespesaFaker.DespesasVMs();

            int idUsuario = _despesaVMs.First().IdUsuario;

            SetupBearerToken(idUsuario);
            _mockDespesaBusiness
                .Setup(business => business.FindAll(idUsuario))
                .Returns(_despesaVMs);

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
            var despesaVM = DespesaFaker.DespesasVMs().First();

            int idUsuario = despesaVM.IdUsuario;

            SetupBearerToken(idUsuario);

            _mockDespesaBusiness
                .Setup(business => business.FindById(despesaVM.Id, idUsuario))
                .Returns((DespesaVM)null);

            // Act
            var result = _despesaController.Get(despesaVM.Id) as ObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.IsType<BadRequestObjectResult>(result);
            var value = result.Value;
            var message = value?.GetType()?.GetProperty("message")?.GetValue(value, null) as string;
            Assert.Equal("Nenhuma despesa foi encontrada.", message);
            _mockDespesaBusiness.Verify(b => b.FindById(despesaVM.Id, idUsuario), Times.Once);
        }

        [Fact, Order(3)]
        public void GetById_Should_Returns_OkResults_With_Despesas()
        {
            // Arrange
            var despesa = DespesaFaker.Despesas().First();
            var despesaVM = new DespesaMap().Parse(despesa);

            int idUsuario = despesaVM.IdUsuario;

            int despesaId = despesa.Id;
            SetupBearerToken(idUsuario);

            _mockDespesaBusiness
                .Setup(business => business.FindById(despesaId, idUsuario))
                .Returns(despesaVM);

            // Act
            var result = _despesaController.Get(despesaId) as ObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.IsType<OkObjectResult>(result);
            var value = result.Value;

            var message = (bool)value?.GetType()?.GetProperty("message")?.GetValue(value, null);

            Assert.True(message);
            var _despesa =
                value?.GetType()?.GetProperty("despesa")?.GetValue(value, null) as DespesaVM;
            Assert.NotNull(_despesa);
            Assert.IsType<DespesaVM>(_despesa);
            _mockDespesaBusiness.Verify(b => b.FindById(despesaId, idUsuario), Times.Once);
        }

        [Fact, Order(4)]
        public void GetById_Should_Returns_BadRequest_When_Throws_Error()
        {
            // Arrange
            var despesaVM = DespesaFaker.DespesasVMs().First();

            int idUsuario = despesaVM.IdUsuario;

            SetupBearerToken(idUsuario);
            _mockDespesaBusiness
                .Setup(business => business.FindById(despesaVM.Id, idUsuario))
                .Throws(new Exception());

            // Act
            var result = _despesaController.Get(despesaVM.Id) as ObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.IsType<BadRequestObjectResult>(result);
            var value = result.Value;
            var message = value?.GetType()?.GetProperty("message")?.GetValue(value) as string;
            Assert.Equal("Não foi possível realizar a consulta da despesa.", message);
            _mockDespesaBusiness.Verify(b => b.FindById(despesaVM.Id, idUsuario), Times.Once);
        }

        [Fact, Order(8)]
        public void Post_Should_Create_Despesa()
        {
            // Arrange
            var _despesaVMs = DespesaFaker.DespesasVMs();
            var despesaVM = _despesaVMs[3];

            int idUsuario = despesaVM.IdUsuario;

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
            var _despesa =
                value?.GetType()?.GetProperty("despesa")?.GetValue(value, null) as DespesaVM;
            Assert.NotNull(_despesa);
            Assert.IsType<DespesaVM>(_despesa);
            _mockDespesaBusiness.Verify(b => b.Create(despesaVM), Times.Once());
        }

        [Fact, Order(10)]
        public void Post_Should_Returns_BadRequest_When_Throws_Error()
        {
            // Arrange
            var _despesaVMs = DespesaFaker.DespesasVMs();
            var despesaVM = _despesaVMs[3];

            int idUsuario = despesaVM.IdUsuario;

            SetupBearerToken(idUsuario);
            _mockDespesaBusiness
                .Setup(business => business.Create(despesaVM))
                .Throws(new Exception());

            // Act
            var result = _despesaController.Post(despesaVM) as ObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.IsType<BadRequestObjectResult>(result);
            var value = result.Value;
            var message = value?.GetType()?.GetProperty("message")?.GetValue(value, null) as string;
            Assert.Equal("Não foi possível realizar o cadastro da despesa.", message);
            _mockDespesaBusiness.Verify(b => b.Create(despesaVM), Times.Once);
        }

        [Fact, Order(12)]
        public void Put_Should_Update_Despesa()
        {
            // Arrange
            var _despesaVMs = DespesaFaker.DespesasVMs();
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
            var _despesaVMs = DespesaFaker.DespesasVMs();
            var despesaVM = _despesaVMs[3];

            int idUsuario = despesaVM.IdUsuario;

            SetupBearerToken(idUsuario);

            _mockDespesaBusiness
                .Setup(business => business.Update(despesaVM))
                .Returns((DespesaVM)null);

            // Act
            var result = _despesaController.Put(despesaVM) as ObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.IsType<BadRequestObjectResult>(result);
            var value = result.Value;
            var message = value?.GetType()?.GetProperty("message")?.GetValue(value, null) as string;
            Assert.Equal("Não foi possível atualizar o cadastro da despesa.", message);
            _mockDespesaBusiness.Verify(b => b.Update(despesaVM), Times.Once);
        }

        [Fact, Order(14)]
        public void Delete_Should_Returns_OkResult()
        {
            // Arrange
            var _despesaVMs = DespesaFaker.DespesasVMs();
            var despesaVM = _despesaVMs[2];

            int idUsuario = despesaVM.IdUsuario;

            SetupBearerToken(idUsuario);

            _mockDespesaBusiness.Setup(business => business.Delete(despesaVM)).Returns(true);
            _mockDespesaBusiness
                .Setup(business => business.FindById(despesaVM.Id, idUsuario))
                .Returns(despesaVM);

            // Act
            var result = _despesaController.Delete(despesaVM.Id) as ObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.IsType<OkObjectResult>(result);
            var value = result.Value;

            var message = (bool)value.GetType().GetProperty("message").GetValue(value, null);

            Assert.True(message);
            _mockDespesaBusiness.Verify(
                business => business.FindById(despesaVM.Id, idUsuario),
                Times.Once
            );
            _mockDespesaBusiness.Verify(b => b.Delete(despesaVM), Times.Once);
        }

        [Fact, Order(15)]
        public void Delete__With_InvalidToken_Returns_BadRequest()
        {
            // Arrange
            var _despesaVMs = DespesaFaker.DespesasVMs();
            var despesaVM = _despesaVMs[2];

            int idUsuario = despesaVM.IdUsuario;

            SetupBearerToken(0);

            _mockDespesaBusiness.Setup(business => business.Delete(despesaVM)).Returns(true);
            _mockDespesaBusiness
                .Setup(business => business.FindById(despesaVM.Id, idUsuario))
                .Returns(despesaVM);
            // Act
            var result = _despesaController.Delete(despesaVM.Id) as ObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.IsType<BadRequestObjectResult>(result);
            var value = result.Value;
            var message = value?.GetType()?.GetProperty("message")?.GetValue(value, null) as string;
            Assert.Equal("Usuário não permitido a realizar operação!", message);
            _mockDespesaBusiness.Verify(
                business => business.FindById(despesaVM.Id, idUsuario),
                Times.Never
            );
            _mockDespesaBusiness.Verify(b => b.Delete(despesaVM), Times.Never);
        }

        [Fact, Order(16)]
        public void Delete_Should_Returns_BadResquest_When_Despesa_Not_Deleted()
        {
            // Arrange
            var _despesaVMs = DespesaFaker.DespesasVMs();
            var despesaVM = _despesaVMs[2];

            int idUsuario = despesaVM.IdUsuario;

            SetupBearerToken(idUsuario);

            _mockDespesaBusiness.Setup(business => business.Delete(despesaVM)).Returns(false);
            _mockDespesaBusiness
                .Setup(business => business.FindById(despesaVM.Id, idUsuario))
                .Returns(despesaVM);

            // Act
            var result = _despesaController.Delete(despesaVM.Id) as ObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.IsType<BadRequestObjectResult>(result);
            var value = result.Value;
            var message = value?.GetType()?.GetProperty("message")?.GetValue(value, null) as string;
            Assert.Equal("Erro ao excluir Despesa!", message);
            _mockDespesaBusiness.Verify(
                business => business.FindById(despesaVM.Id, idUsuario),
                Times.Once
            );
            _mockDespesaBusiness.Verify(b => b.Delete(despesaVM), Times.Once);
        }
    }
}
