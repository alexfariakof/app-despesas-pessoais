using Microsoft.EntityFrameworkCore;
using Repository.Persistency.Implementations.Fixtures;

namespace Repository.Persistency.Implementations;
public sealed class LancamentoRepositorioImplTest: IClassFixture<LancamentoRepositorioFixture>
{
    private readonly LancamentoRepositorioFixture _fixture;

    public LancamentoRepositorioImplTest(LancamentoRepositorioFixture fixture)
    {
        _fixture = fixture;
    }

    [Fact]
    public void FindByMesAno_Should_Returns_List_lancamentos()
    {
        // Arrange
        var data = _fixture.MockAnoMes; 
        var idUsuario = _fixture.MockUsuario.Id; 

        // Act
        var result = _fixture.MockRepository.Object.FindByMesAno(data, idUsuario); 

        // Assert            
        Assert.NotNull(result);
        Assert.IsType<List<Lancamento>>(result);
        Assert.True(result.Count >= 1);
    }

    /*
    [Fact]
    public void FindByMesAno_Should_Returns_Null_List_lancamentos()
    {
        // Arrange
        var data = _fixture.MockAnoMes;
        var idUsuario = 0;

        // Act
        var result = _fixture.Repository.Object.FindByMesAno(data, idUsuario);

        // Assert            
        Assert.NotNull(result);
        Assert.IsType<List<Lancamento>>(result);
        Assert.True(result.Count == 0);
    }*/

    [Fact]
    public void FindByMesAno_Throws_Exception_When_Despesa_Execute_Where()
    {
        // Arrange
        var data = _fixture.MockAnoMes;
        var idUsuario = 0;

        var despesaDbSetMock = new Mock<DbSet<Despesa>>();
        despesaDbSetMock.As<IQueryable<Despesa>>().Setup(d => d.Provider).Throws<Exception>();
        var options = new DbContextOptionsBuilder<RegisterContext>().UseInMemoryDatabase(databaseName: "FindByMesAno_Throws_Exception_When_Despesa_Execute_Where").Options;
        var context = new RegisterContext(options);
        context.Despesa = despesaDbSetMock.Object;        
        _fixture.Repository = new Mock<LancamentoRepositorioImpl>(context);

        // Act
        Action result = () => _fixture.Repository.Object.FindByMesAno(data, idUsuario);

        // Assert
        Assert.NotNull(result);
        var exception = Assert.Throws<Exception>(() => _fixture.Repository.Object.FindByMesAno(data, idUsuario));
        Assert.Equal("LancamentoRepositorioImpl_FindByMesAno_Erro", exception.Message);
    }

    [Fact]
    public void FindByMesAno_Throws_Exception_When_Receita_Execute_Where()
    {
        // Arrange
        var data = _fixture.MockAnoMes;
        var idUsuario = 20;
        var receitaDbSetMock = new Mock<DbSet<Receita>>();
        receitaDbSetMock.As<IQueryable<Receita>>().Setup(d => d.Provider).Throws<Exception>();
        var options = new DbContextOptionsBuilder<RegisterContext>().UseInMemoryDatabase(databaseName: "FindByMesAno Throws Exception When Receita Execute Where").Options;
        var context = new RegisterContext(options);
        context.Receita = receitaDbSetMock.Object;
        _fixture.Repository = new Mock<LancamentoRepositorioImpl>(context);

        // Act
        Action result = () => _fixture.Repository.Object.FindByMesAno(data, idUsuario);

        // Assert
        Assert.NotNull(result);
        var exception = Assert.Throws<Exception>(() => _fixture.Repository.Object.FindByMesAno(data, idUsuario));
        Assert.Equal("LancamentoRepositorioImpl_FindByMesAno_Erro", exception.Message);
    }
}