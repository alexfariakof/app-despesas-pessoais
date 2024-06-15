using __mock__.Repository;
using Microsoft.EntityFrameworkCore;
using Repository.Persistency.Implementations.Fixtures;

namespace Repository.Persistency.Implementations;

public sealed class GraficoRepositorioImplTest : IClassFixture<GraficoRepositorioFixture>
{
    private readonly GraficoRepositorioFixture _fixture;

    public GraficoRepositorioImplTest(GraficoRepositorioFixture fixture)
    {
        _fixture = fixture;
    }   

    [Fact]
    public void GetDadosGraficoByAno_Should_Return_Grafico()
    {
        // Arrange
        var data = _fixture.MockAnoMes;
        var idUsuario = _fixture.UsuarioMock.Id;

        // Act
        var result = _fixture.MockRepository.Object.GetDadosGraficoByAno(idUsuario, data);

        // Assert
        Assert.NotNull(result);
        Assert.IsType<Grafico>(result);
        Assert.NotEmpty(result.SomatorioDespesasPorAno ?? new());
        Assert.NotEmpty(result.SomatorioReceitasPorAno ?? new());
    }

    [Fact]
    public void GetDadosGraficoByAno_Throws_Exception_And_Returns_Grafico_With_Default_Values()
    {
        // Arrange
        var usuario = MockUsuario.Instance.GetUsuario();
        var data = _fixture.MockAnoMes;

        var despesaDbSetMock = new Mock<DbSet<Despesa>>();
        despesaDbSetMock.As<IQueryable<Despesa>>().Setup(d => d.Provider).Throws<Exception>();

        var options = new DbContextOptionsBuilder<RegisterContext>().UseInMemoryDatabase(databaseName: "GetDadosGraficoByAno_Throws_Exception_And_Returns_Grafico_With_Default_Values").Options;
        var context = new RegisterContext(options);
        context.Despesa = despesaDbSetMock.Object;
        context.SaveChanges();

        var repository = new GraficosRepositorioImpl(context);

        // Act
        var result = repository.GetDadosGraficoByAno(usuario.Id, data);

        // Assert
        Assert.NotNull(result);
        Assert.IsType<Grafico>(result);
        Assert.NotEmpty(result.SomatorioDespesasPorAno ?? new());
        Assert.NotEmpty(result.SomatorioReceitasPorAno ?? new());
    }
}
