using Repository.Persistency.Abstractions;
using Fakers.v1;

namespace Business;
public class GraficosBusinessImplTest
{
    private readonly Mock<IGraficosRepositorio> _repositorioMock;
    private readonly GraficosBusinessImpl _graficosBusinessImpl;

    public GraficosBusinessImplTest()
    {
        _repositorioMock = new Mock<IGraficosRepositorio>();
        _graficosBusinessImpl = new GraficosBusinessImpl(_repositorioMock.Object);
    }

    [Fact]
    public void GetDadosGraficoByAnoByIdUsuario_Should_Return_Grafico()
    {
        // Arrange
        var idUsuario = 1;
        var data = new DateTime(2023, 10, 1);
        var graficoData = GraficoFaker.GetNewFaker();
        _repositorioMock.Setup(r => r.GetDadosGraficoByAno(idUsuario, data)).Returns(graficoData);

        // Act
        var result = _graficosBusinessImpl.GetDadosGraficoByAnoByIdUsuario(idUsuario, data);

        // Assert
        Assert.NotNull(result);
        Assert.IsType<Grafico>(result);
        _repositorioMock.Verify(r => r.GetDadosGraficoByAno(idUsuario, data), Times.Once);
    } 
}