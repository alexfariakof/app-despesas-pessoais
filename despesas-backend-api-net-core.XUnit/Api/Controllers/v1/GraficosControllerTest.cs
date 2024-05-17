using Business.Abstractions;
using despesas_backend_api_net_core.Controllers.v1;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Fakers.v1;

namespace Api.Controllers.v1;

public class GraficosControllerTest
{
    protected Mock<IGraficosBusiness> _mockGraficoBusiness;
    protected GraficosController _GraficoController;

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

        _GraficoController.ControllerContext = new ControllerContext
        {
            HttpContext = httpContext
        };
    }

    public GraficosControllerTest()
    {            
        _mockGraficoBusiness = new Mock<IGraficosBusiness>();
        _GraficoController = new GraficosController(_mockGraficoBusiness.Object);
    }

    [Fact]
    public void GetDadosGraficoPorAno_Should_Return_GraficoData()
    {
        // Arrange
        var dadosGrafico = GraficoFaker.GetNewFaker();
        int idUsuario = 1;
        DateTime anoMes = DateTime.Today;
        SetupBearerToken(idUsuario);

        _mockGraficoBusiness.Setup(business => business.GetDadosGraficoByAnoByIdUsuario(idUsuario, anoMes)).Returns(dadosGrafico);

        // Act
        var result = _GraficoController.GetByAnoByIdUsuario(anoMes);

        // Assert
        Assert.NotNull(result);
        var okResult = Assert.IsType<OkObjectResult>(result);
        var graficoData = okResult.Value;
    }

    [Fact]
    public void GetDadosGraficoPorAno_Returns_BadRequest_When_Throws_Error()
    {
        // Arrange
        var dadosGrafico = GraficoFaker.GetNewFaker();
        int idUsuario = 1;
        DateTime anoMes = DateTime.Today;
        SetupBearerToken(0);

        _mockGraficoBusiness.Setup(business => business.GetDadosGraficoByAnoByIdUsuario(idUsuario, anoMes)).Throws(new Exception());

        // Act
        var result = _GraficoController.GetByAnoByIdUsuario(anoMes) as ObjectResult;

        // Assert
        Assert.NotNull(result);
        Assert.IsType<BadRequestObjectResult>(result);
        var value = result.Value;
        var message = value?.GetType()?.GetProperty("message")?.GetValue(value, null) as string;
        Assert.Equal("Erro ao gerar dados do Gráfico!", message);
    }
}