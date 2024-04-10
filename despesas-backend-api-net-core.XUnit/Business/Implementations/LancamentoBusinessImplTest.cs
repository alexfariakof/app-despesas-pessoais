namespace Business;
public class LancamentoBusinessImplTest
{
    private readonly Mock<ILancamentoRepositorio> _repositorioMock;
    private readonly LancamentoBusinessImpl _lancamentoBusiness;

    public LancamentoBusinessImplTest()
    {
        _repositorioMock = new Mock<ILancamentoRepositorio>();
        _lancamentoBusiness = new LancamentoBusinessImpl(_repositorioMock.Object);
    }

    [Fact]
    public void FindByMesAno_Should_Return_List_Of_LancamentoVM()
    {
        // Arrange            
        var lancamentos = LancamentoFaker.Lancamentos();
        var data = lancamentos.First().Data;
        var idUsuario = lancamentos.First().UsuarioId;
        
        _repositorioMock.Setup(r => r.FindByMesAno(data, idUsuario)).Returns(lancamentos.FindAll(l => l.UsuarioId == idUsuario));

        // Act
        var result = _lancamentoBusiness.FindByMesAno(data, idUsuario);

        // Assert
        Assert.NotNull(result);
        Assert.IsType<List<LancamentoVM>>(result);            
        Assert.Equal(lancamentos.FindAll(l => l.UsuarioId == idUsuario).Count, result.Count);
        _repositorioMock.Verify(r => r.FindByMesAno(data, idUsuario), Times.Once);
    }

    /*
    [Fact]
    public void GetSaldo_Should_Return_Saldo_As_Decimal()
    {
        // Arrange
        var idUsuario = 1;
        var saldo = 100.50m;
        _repositorioMock.Setup(r => r.GetSaldo(idUsuario)).Returns(saldo);

        // Act
        var result = _lancamentoBusiness.GetSaldo(idUsuario);

        // Assert
        Assert.Equal(saldo, result);
        _repositorioMock.Verify(r => r.GetSaldo(idUsuario), Times.Once);
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
        var result = _lancamentoBusiness.GetDadosGraficoByAnoByIdUsuario(idUsuario, data);

        // Assert
        Assert.NotNull(result);
        Assert.IsType<Grafico>(result);
        _repositorioMock.Verify(r => r.GetDadosGraficoByAno(idUsuario, data), Times.Once);
    }
    */
}