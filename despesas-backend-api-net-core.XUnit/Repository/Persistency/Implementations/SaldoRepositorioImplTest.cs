using Repository.Persistency.Abstractions;
using Fakers.v1;

namespace Repository.Persistency.Implementations;
public class SaldoRepositorioImplTest
{
    private readonly RegisterContext _context;
    private Mock<ISaldoRepositorio> _mockRepository;
    private Mock<SaldoRepositorioImpl> _repository;
    private Usuario _mockUsuario;
    private DateTime _mockAnoMes = DateTime.Today;
    public SaldoRepositorioImplTest()
    {
        var options = new DbContextOptionsBuilder<RegisterContext>().UseInMemoryDatabase(databaseName: "Saldo Repo Database InMemory").Options;
        _context = new RegisterContext(options);
        _mockUsuario = UsuarioFaker.Instance.GetNewFaker();
        _context.Usuario.AddRange(_mockUsuario);
        _context.Despesa.AddRange(DespesaFaker.Instance.Despesas(_mockUsuario, _mockUsuario.Id, 60));
        _context.Receita.AddRange(ReceitaFaker.Instance.Receitas(_mockUsuario, _mockUsuario.Id, 60));
        _context.SaveChanges();
        _repository = new Mock<SaldoRepositorioImpl>(MockBehavior.Strict, _context);
        _mockRepository = Mock.Get<ISaldoRepositorio>(_repository.Object);
    }

    [Fact]
    public void GetSaldo_Should_Returns_Saldo()
    {
        // Arrange
        var idUsuario = _mockUsuario.Id;

        // Act
        var result = _repository.Object.GetSaldo(idUsuario);

        // Assert            
        Assert.IsType<decimal>(result);
        Assert.True(result != 0);
    }

    [Fact]
    public void GetSaldo_Should_Returns_Saldo_Equal_0()
    {
        // Arrange
        var usuario = UsuarioFaker.Instance.GetNewFaker();
        var options = new DbContextOptionsBuilder<RegisterContext>().UseInMemoryDatabase(databaseName: "MemoryDatabase GetSaldo").Options;
        var context = new RegisterContext(options);
        var repository = new SaldoRepositorioImpl(context);

        // Act
        var result = repository.GetSaldo(usuario.Id);

        // Assert            
        Assert.IsType<decimal>(result);
        Assert.True(result == 0);
    }

    [Fact]
    public void GetSaldo_Throws_Exception_When_Despesa_Execute_Where()
    {
        // Arrange
        var usuario = UsuarioFaker.Instance.GetNewFaker();

        var despesaDbSetMock = new Mock<DbSet<Despesa>>();
        despesaDbSetMock.As<IQueryable<Despesa>>().Setup(d => d.Provider).Throws<Exception>();

        var options = new DbContextOptionsBuilder<RegisterContext>()
            .UseInMemoryDatabase(databaseName: "MemoryDatabase GetSaldo Throws Erro Despesa")
            .Options;
        var context = new RegisterContext(options);
        context.Despesa = despesaDbSetMock.Object;
        context.SaveChanges();
        var repository = new SaldoRepositorioImpl(context);

        // Act
        Action result = () => repository.GetSaldo(usuario.Id);

        // Assert
        Assert.NotNull(result);
        var exception = Assert.Throws<Exception>(() => repository.GetSaldo(usuario.Id));
        Assert.Equal("SaldoRepositorioImpl_GetSaldo_Erro", exception.Message);
    }

    [Fact]
    public void GetSaldo_Throws_Exception_When_Receita_Execute_Where()
    {
        // Arrange            
        var usuario = UsuarioFaker.Instance.GetNewFaker();
        var receitaDbSetMock = new Mock<DbSet<Receita>>();
        receitaDbSetMock.As<IQueryable<Receita>>().Setup(d => d.Provider).Throws<Exception>();
        var options = new DbContextOptionsBuilder<RegisterContext>()
            .UseInMemoryDatabase(databaseName: "MemoryDatabase GetSaldo Throws Erro Receita")
            .Options;
        var context = new RegisterContext(options);
        context.Receita = receitaDbSetMock.Object;
        context.SaveChanges();
        var repository = new SaldoRepositorioImpl(context);

        // Act
        Action result = () => repository.GetSaldo(usuario.Id);

        // Assert
        Assert.NotNull(result);
        var exception = Assert.Throws<Exception>(() => repository.GetSaldo(usuario.Id));
        Assert.Equal("SaldoRepositorioImpl_GetSaldo_Erro", exception.Message);
    }

    [Fact]
    public void GetSaldoByAno_Should_Returns_Saldo()
    {
        // Arrange
        var idUsuario = _mockUsuario.Id;

        // Act
        var result = _mockRepository.Object.GetSaldoByAno(_mockAnoMes, idUsuario);

        // Assert            
        Assert.IsType<decimal>(result);
        Assert.True(result != 0);
    }

    [Fact]
    public void GetSaldoByAno_Should_Returns_Saldo_Equal_0()
    {
        // Arrange
        var idUsuario = _mockUsuario.Id;
        var options = new DbContextOptionsBuilder<RegisterContext>().UseInMemoryDatabase(databaseName: "MemoryDatabase GetSaldoByAno").Options;
        var context = new RegisterContext(options);
        context.Despesa.AddRange(new List<Despesa>());
        context.Receita.AddRange(new List<Receita>());
        context.SaveChanges();
        var repository = new SaldoRepositorioImpl(context);

        // Act
        var result = repository.GetSaldoByAno(_mockAnoMes, idUsuario);

        // Assert            
        Assert.IsType<decimal>(result);
        Assert.True(result == 0);
    }

    [Fact]
    public void GetSaldoByAno_Throws_Exception_When_Despesa_Execute_Where()
    {
        // Arrange
        var idUsuario = 0;
        var despesaDbSetMock = new Mock<DbSet<Despesa>>();
        despesaDbSetMock.As<IQueryable<Despesa>>().Setup(d => d.Provider).Throws<Exception>();
        var options = new DbContextOptionsBuilder<RegisterContext>().UseInMemoryDatabase(databaseName: "MemoryDatabase GetSaldoAno Throws Erro Despesa").Options;
        var context = new RegisterContext(options);
        context.Despesa = despesaDbSetMock.Object;
        var repository = new SaldoRepositorioImpl(context);

        // Act
        Action result = () => repository.GetSaldoByAno(_mockAnoMes, idUsuario);

        // Assert
        Assert.NotNull(result);
        var exception = Assert.Throws<Exception>(() => repository.GetSaldoByAno(_mockAnoMes, idUsuario));
        Assert.Equal("SaldoRepositorioImpl_GetSaldoByAno_Erro", exception.Message);
    }

    [Fact]
    public void GetSaldoByAno_Throws_Exception_When_Receita_Execute_Where()
    {
        // Arrange            
        var idUsuario = 0;
        var receitaDbSetMock = new Mock<DbSet<Receita>>();
        receitaDbSetMock.As<IQueryable<Receita>>().Setup(d => d.Provider).Throws<Exception>();
        var options = new DbContextOptionsBuilder<RegisterContext>().UseInMemoryDatabase(databaseName: "MemoryDatabase GetSaldoByAno Throws Erro Receita").Options;
        var context = new RegisterContext(options);
        context.Receita = receitaDbSetMock.Object;
        var repository = new SaldoRepositorioImpl(context);

        // Act
        Action result = () => repository.GetSaldoByAno(_mockAnoMes, idUsuario);

        // Assert
        Assert.NotNull(result);
        var exception = Assert.Throws<Exception>(() => repository.GetSaldoByAno(_mockAnoMes, idUsuario));
        Assert.Equal("SaldoRepositorioImpl_GetSaldoByAno_Erro", exception.Message);
    }

    [Fact]
    public void GetSaldoByMesAno_Should_Returns_Saldo()
    {
        // Arrange
        var idUsuario = _mockUsuario.Id;

        // Act
        var result = _mockRepository.Object.GetSaldoByMesAno(_mockAnoMes, idUsuario);

        // Assert            
        Assert.IsType<decimal>(result);
        Assert.True(result != 0);
    }

    [Fact]
    public void GetSaldoByMesAno_Should_Returns_Saldo_Equal_0()
    {
        // Arrange
        var idUsuario = _mockUsuario.Id;
        var options = new DbContextOptionsBuilder<RegisterContext>().UseInMemoryDatabase(databaseName: "MemoryDatabase GetSaldoMesAno").Options;
        var context = new RegisterContext(options);
        context.Despesa.AddRange(new List<Despesa>());
        context.Receita.AddRange(new List<Receita>());
        context.SaveChanges();
        var repository = new SaldoRepositorioImpl(context);
        
       // Act
        var result = repository.GetSaldoByMesAno(_mockAnoMes, idUsuario);

        // Assert            
        Assert.IsType<decimal>(result);
        Assert.True(result == 0);
    }

    [Fact]
    public void GetSaldoByMesAno_Throws_Exception_When_Despesa_Execute_Where()
    {
        // Arrange
        var idUsuario = 0;
        var despesaDbSetMock = new Mock<DbSet<Despesa>>();
        despesaDbSetMock.As<IQueryable<Despesa>>().Setup(d => d.Provider).Throws<Exception>();
        var options = new DbContextOptionsBuilder<RegisterContext>().UseInMemoryDatabase(databaseName: "MemoryDatabase GetSaldoMesAno Throws Erro Despesas").Options;
        var context = new RegisterContext(options);
        context.Despesa = despesaDbSetMock.Object;
        var repository = new SaldoRepositorioImpl(context);

        // Act
        Action result = () => repository.GetSaldoByMesAno(_mockAnoMes, idUsuario);

        // Assert
        Assert.NotNull(result);
        var exception = Assert.Throws<Exception>(() => repository.GetSaldoByMesAno(_mockAnoMes, idUsuario));
        Assert.Equal("SaldoRepositorioImpl_GetSaldoByMesAno_Erro", exception.Message);
    }

    [Fact]
    public void GetSaldoByMesAno_Throws_Exception_When_Receita_Execute_Where()
    {
        // Arrange            
        var idUsuario = 0;
        var receitaDbSetMock = new Mock<DbSet<Receita>>();
        receitaDbSetMock.As<IQueryable<Receita>>().Setup(d => d.Provider).Throws<Exception>();
        var options = new DbContextOptionsBuilder<RegisterContext>().UseInMemoryDatabase(databaseName: "MemoryDatabase GetSaldoMesAno Throws Erro Receitas").Options;
        var context = new RegisterContext(options);
        context.Receita = receitaDbSetMock.Object;
        var repository = new SaldoRepositorioImpl(context);

        // Act
        Action result = () => repository.GetSaldoByMesAno(_mockAnoMes, idUsuario);

        // Assert
        Assert.NotNull(result);
        var exception = Assert.Throws<Exception>(() => repository.GetSaldoByMesAno(_mockAnoMes, idUsuario));
        Assert.Equal("SaldoRepositorioImpl_GetSaldoByMesAno_Erro", exception.Message);
    }
}