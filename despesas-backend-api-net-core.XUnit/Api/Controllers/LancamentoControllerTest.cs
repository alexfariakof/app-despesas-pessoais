using Business.Abstractions;
using despesas_backend_api_net_core.Controllers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Api.Controllers;

public class LancamentoControllerTest
{
    protected Mock<ILancamentoBusiness> _mockLancamentoBusiness;
    protected LancamentoController _lancamentoController;
    protected List<LancamentoDto> _lancamentoDtos;

    private void SetupBearerToken(int idUsuario)
    {
        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, idUsuario.ToString())
        };
        var identity = new ClaimsIdentity(claims, "IdUsuario");
        var claimsPrincipal = new ClaimsPrincipal(identity);

        var httpContext = new DefaultHttpContext { User = claimsPrincipal };
        httpContext.Request.Headers["Authorization"] = "Bearer " + Usings.GenerateJwtToken(idUsuario);
        _lancamentoController.ControllerContext = new ControllerContext { HttpContext = httpContext };
    }

    public LancamentoControllerTest()
    {
        _mockLancamentoBusiness = new Mock<ILancamentoBusiness>();
        _lancamentoController = new LancamentoController(_mockLancamentoBusiness.Object);
        _lancamentoDtos = LancamentoFaker.LancamentoDtos();
    }

    [Fact]
    public void Get_Should_Return_LancamentoDtos()
    {
        // Arrange
        var lancamentoDtos = _lancamentoDtos;
        int idUsuario = _lancamentoDtos.First().IdUsuario;
        DateTime anoMes = DateTime.Now;
        SetupBearerToken(idUsuario);
        _mockLancamentoBusiness.Setup(business => business.FindByMesAno(anoMes, idUsuario)).Returns(lancamentoDtos.FindAll(l => l.IdUsuario == idUsuario));

        // Act
        var result = _lancamentoController.Get(anoMes) as ObjectResult;

        // Assert
        // Assert
        Assert.NotNull(result);
        Assert.IsType<OkObjectResult>(result);
        var value = result.Value;
        var message = (bool)(value?.GetType()?.GetProperty("message")?.GetValue(value, null) ?? false);
        var lancamentos = (List<LancamentoDto>?)value?.GetType()?.GetProperty("lancamentos")?.GetValue(value, null);
        Assert.True(message);
        Assert.NotNull(lancamentos);
        Assert.NotEmpty(lancamentos);
        var returnedLancamentoDtos = Assert.IsType<List<LancamentoDto>>(lancamentos);
        Assert.Equal(lancamentoDtos.FindAll(l => l.IdUsuario == idUsuario),returnedLancamentoDtos);
        _mockLancamentoBusiness.Verify(b => b.FindByMesAno(anoMes, idUsuario), Times.Once);
    }

    [Fact]
    public void Get_Returns_OkResult_With_Empty_List_When_Lancamento_IsNull()
    {
        // Arrange
        var lancamentoDtos = _lancamentoDtos;
        int idUsuario = _lancamentoDtos.First().IdUsuario;
        DateTime anoMes = DateTime.Now;
        SetupBearerToken(idUsuario);
        _mockLancamentoBusiness.Setup(business => business.FindByMesAno(anoMes, idUsuario)).Returns((List<LancamentoDto>)null);

        // Act
        var result = _lancamentoController.Get(anoMes) as ObjectResult;

        // Assert
        Assert.NotNull(result);
        Assert.IsType<OkObjectResult>(result);
        var value = result.Value;

        var message = (bool)(value?.GetType()?.GetProperty("message")?.GetValue(value, null) ?? false);

        var lancamentos = (List<LancamentoDto>?)value?.GetType()?.GetProperty("lancamentos")?.GetValue(value, null);
        Assert.True(message);
        Assert.NotNull(lancamentos);
        Assert.Empty(lancamentos);
        _mockLancamentoBusiness.Verify(b => b.FindByMesAno(anoMes, idUsuario), Times.Once);
    }

    [Fact]
    public void Get_Returns_OkResult_With_Empty_List_When_Lancamento_List_Count0()
    {
        // Arrange
        var lancamentoDtos = _lancamentoDtos;
        int idUsuario = _lancamentoDtos.First().IdUsuario;
        DateTime anoMes = DateTime.Now;
        SetupBearerToken(idUsuario);
        _mockLancamentoBusiness.Setup(business => business.FindByMesAno(anoMes, idUsuario)).Returns(new List<LancamentoDto>());
        
       // Act
        var result = _lancamentoController.Get(anoMes) as ObjectResult;

        // Assert
        Assert.NotNull(result);
        Assert.IsType<OkObjectResult>(result);
        var value = result.Value;
        var message = (bool)(value?.GetType()?.GetProperty("message")?.GetValue(value, null) ?? false);
        var lancamentos = (List<LancamentoDto>?)value?.GetType()?.GetProperty("lancamentos")?.GetValue(value, null);
        Assert.True(message);
        Assert.NotNull(lancamentos);
        Assert.Empty(lancamentos);
        _mockLancamentoBusiness.Verify(b => b.FindByMesAno(anoMes, idUsuario), Times.Once);
    }

    [Fact]
    public void Get_Returns_OkResults_With_Empty_List_When_Throws_Error()
    {
        // Arrange
        var lancamentoDtos = _lancamentoDtos;
        int idUsuario = _lancamentoDtos.First().IdUsuario;
        DateTime anoMes = DateTime.Now;
        SetupBearerToken(idUsuario);
        _mockLancamentoBusiness.Setup(business => business.FindByMesAno(anoMes, idUsuario)).Throws(new Exception());

        // Act
        var result = _lancamentoController.Get(anoMes) as ObjectResult;

        // Assert
        Assert.NotNull(result);
        Assert.IsType<OkObjectResult>(result);
        var value = result.Value;
        var message = (bool)(value?.GetType()?.GetProperty("message")?.GetValue(value, null) ?? false);
        var lancamentos = (List<LancamentoDto>?)value?.GetType()?.GetProperty("lancamentos")?.GetValue(value, null);
        Assert.True(message);
        Assert.NotNull(lancamentos);
        Assert.Empty(lancamentos);
        _mockLancamentoBusiness.Verify(b => b.FindByMesAno(anoMes, idUsuario), Times.Once);
    }
}
