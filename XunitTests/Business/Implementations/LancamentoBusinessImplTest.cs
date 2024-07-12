using AutoMapper;
using Business.Dtos.v1;
using Repository.Persistency.Abstractions;
using __mock__.v1;
using Business.Dtos.Core.Profile;
using Business.Implementations;

namespace Business;
public class LancamentoBusinessImplTest
{
    private readonly Mock<ILancamentoRepositorio> _repositorioMock;
    private readonly LancamentoBusinessImpl<LancamentoDto> _lancamentoBusiness;
    private Mapper _mapper;

    public LancamentoBusinessImplTest()
    {
        _repositorioMock = new Mock<ILancamentoRepositorio>();
        _mapper = new Mapper(new MapperConfiguration(cfg => { cfg.AddProfile<LancamentoProfile>(); }));
        _lancamentoBusiness = new LancamentoBusinessImpl<LancamentoDto>(_mapper, _repositorioMock.Object);
    }

    [Fact]
    public void FindByMesAno_Should_Return_List_Of_LancamentoDto()
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
        Assert.IsType<List<LancamentoDto>>(result);            
        Assert.Equal(lancamentos.FindAll(l => l.UsuarioId == idUsuario).Count, result.Count);
        _repositorioMock.Verify(r => r.FindByMesAno(data, idUsuario), Times.Once);
    }

    /*
    [Fact]
    public void GetSaldo_Should_Return_Saldo_As_Decimal()
    {
        // Arrange
        var idUsuario = Guid.NewGuid();
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
        var idUsuario = Guid.NewGuid();
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