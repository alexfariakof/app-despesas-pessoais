using despesas_backend_api_net_core.Business;
using despesas_backend_api_net_core.Controllers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Xunit.Extensions.Ordering;

namespace Test.XUnit.Controllers
{
    [Order(14)]
    public class LancamentoControllerTest
    {
        protected Mock<ILancamentoBusiness> _mockLancamentoBusiness;
        protected LancamentoController _lancamentoController;
        protected List<LancamentoVM> _lancamentoVMs;

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

            _lancamentoController.ControllerContext = new ControllerContext
            {
                HttpContext = httpContext
            };
        }

        public LancamentoControllerTest()
        {            
            _mockLancamentoBusiness = new Mock<ILancamentoBusiness>();
            _lancamentoController = new LancamentoController(_mockLancamentoBusiness.Object);
            _lancamentoVMs = LancamentoFaker.LancamentoVMs(); 
        }

        [Fact, Order(1)]
        public void Get_Should_Return_LancamentoVMs()
        {
            // Arrange
            var lancamentoVMs = _lancamentoVMs;
            int idUsuario = _lancamentoVMs.First().IdUsuario;
            DateTime anoMes = DateTime.Now;
            SetupBearerToken(idUsuario);
            
            _mockLancamentoBusiness.Setup(business => business.FindByMesAno(anoMes, idUsuario)).Returns(lancamentoVMs.FindAll(l => l.IdUsuario == idUsuario));

            // Act
            var result = _lancamentoController.Get(anoMes, idUsuario) as ObjectResult;

            // Assert
            // Assert
            Assert.NotNull(result);
            Assert.IsType<OkObjectResult>(result);
            var value = result.Value;
            var message = (bool)value?.GetType()?.GetProperty("message")?.GetValue(value, null);
            var lancamentos = (List<LancamentoVM>)value?.GetType()?.GetProperty("lancamentos")?.GetValue(value, null);
            Assert.True(message);
            Assert.NotEmpty(lancamentos);            
            var returnedLancamentoVMs = Assert.IsType<List<LancamentoVM>>(lancamentos);
            Assert.Equal(lancamentoVMs.FindAll(l => l.IdUsuario == idUsuario), returnedLancamentoVMs);
            _mockLancamentoBusiness.Verify(b => b.FindByMesAno(anoMes, idUsuario), Times.Once);
        }

        [Fact, Order(4)]
        public void Get_With_InvalidToken_Returns_BadRequest()
        {
            // Arrange
            var lancamentoVMs = _lancamentoVMs;
            int idUsuario = _lancamentoVMs.First().IdUsuario;
            DateTime anoMes = DateTime.Now;
            SetupBearerToken(0);

            // Act
            var result = _lancamentoController.Get(anoMes, idUsuario) as ObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.IsType<BadRequestObjectResult>(result);
            var value = result.Value;
            var message = value?.GetType()?.GetProperty("message")?.GetValue(value, null) as string;
            Assert.Equal("Usuário não permitido a realizar operação!", message);
        }

        [Fact, Order(5)]
        public void Get_Returns_OkResult_With_Empty_List_When_Lancamento_IsNull()
        {
            // Arrange
            var lancamentoVMs = _lancamentoVMs;
            int idUsuario = _lancamentoVMs.First().IdUsuario;
            DateTime anoMes = DateTime.Now;
            SetupBearerToken(idUsuario);

            _mockLancamentoBusiness.Setup(business => business.FindByMesAno(anoMes, idUsuario)).Returns((List<LancamentoVM>)null);
            // Act
            var result = _lancamentoController.Get(anoMes, idUsuario) as ObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.IsType<OkObjectResult>(result);
            var value = result.Value;
            var message = (bool)value?.GetType()?.GetProperty("message")?.GetValue(value, null);
            var lancamentos = (List<LancamentoVM>)value?.GetType()?.GetProperty("lancamentos")?.GetValue(value, null);
            Assert.True(message);
            Assert.Empty(lancamentos);
            _mockLancamentoBusiness.Verify(b => b.FindByMesAno(anoMes, idUsuario), Times.Once);
        }

        [Fact, Order(6)]
        public void Get_Returns_OkResult_With_Empty_List_When_Lancamento_List_Count0()
        {
            // Arrange
            var lancamentoVMs = _lancamentoVMs;
            int idUsuario = _lancamentoVMs.First().IdUsuario;
            DateTime anoMes = DateTime.Now;
            SetupBearerToken(idUsuario);

            _mockLancamentoBusiness.Setup(business => business.FindByMesAno(anoMes, idUsuario)).Returns(new List<LancamentoVM>());
            // Act
            var result = _lancamentoController.Get(anoMes, idUsuario) as ObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.IsType<OkObjectResult>(result);
            var value = result.Value;
            var message = (bool)value?.GetType()?.GetProperty("message")?.GetValue(value, null);
            var lancamentos = (List<LancamentoVM>)value?.GetType()?.GetProperty("lancamentos")?.GetValue(value, null);
            Assert.True(message);
            Assert.Empty(lancamentos);
            _mockLancamentoBusiness.Verify(b => b.FindByMesAno(anoMes, idUsuario), Times.Once);
        }

        [Fact, Order(7)]
        public void Get_Returns_OkResults_With_Empty_List_When_Throws_Error()
        {
            // Arrange
            var lancamentoVMs = _lancamentoVMs;
            int idUsuario = _lancamentoVMs.First().IdUsuario;
            DateTime anoMes = DateTime.Now;
            SetupBearerToken(idUsuario);

            _mockLancamentoBusiness.Setup(business => business.FindByMesAno(anoMes, idUsuario)).Throws(new Exception());

            // Act
            var result = _lancamentoController.Get(anoMes, idUsuario) as ObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.IsType<OkObjectResult>(result);
            var value = result.Value;
            var message = (bool)value?.GetType()?.GetProperty("message")?.GetValue(value, null);
            var lancamentos = (List<LancamentoVM>)value?.GetType()?.GetProperty("lancamentos")?.GetValue(value, null);
            Assert.True(message);            
            Assert.Empty(lancamentos);
            _mockLancamentoBusiness.Verify(b => b.FindByMesAno(anoMes, idUsuario), Times.Once);
        }

    }
}