using Business.Abstractions;
using Despesas.WebApi.Controllers.v2;
using Microsoft.AspNetCore.Mvc;
using __mock__.v2;

namespace Api.Controllers.v2;

public sealed class GraficosControllerTest
{
    private Mock<IGraficosBusiness> _mockGraficoBusiness;
    private GraficosController _GraficoController;  

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
        Usings.SetupBearerToken(idUsuario, _GraficoController);
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
        Usings.SetupBearerToken(0, _GraficoController);
        _mockGraficoBusiness.Setup(business => business.GetDadosGraficoByAnoByIdUsuario(It.IsAny<int>(), It.IsAny<DateTime>())).Throws(new Exception());

        // Act
        var result = _GraficoController.GetByAnoByIdUsuario(anoMes) as ObjectResult;

        // Assert
        Assert.NotNull(result);
        Assert.IsType<BadRequestObjectResult>(result);
        Assert.Equal("Erro ao gerar dados do Gráfico!", result.Value);
    }
}