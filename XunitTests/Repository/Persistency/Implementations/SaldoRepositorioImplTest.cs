﻿using __mock__.Repository;
using Microsoft.EntityFrameworkCore;
using Repository.Persistency.Implementations.Fixtures;

namespace Repository.Persistency.Implementations;

public sealed class SaldoRepositorioImplTest : IClassFixture<SaldoRepositorioFixture>
{
    private readonly SaldoRepositorioFixture _fixture;

    public SaldoRepositorioImplTest(SaldoRepositorioFixture fixture)
    {
        _fixture = fixture;
    }

    [Fact]
    public void GetSaldo_Should_Returns_Saldo()
    {
        // Arrange
        var idUsuario = _fixture.Context.Usuario.First().Id;

        // Act
        var result = _fixture.Repository.Object.GetSaldo(idUsuario);

        // Assert            
        Assert.IsType<decimal>(result);
        Assert.True(result != 0);
    }

    [Fact]
    public void GetSaldo_Should_Returns_Saldo_Equal_0()
    {
        // Arrange
        var usuario = MockUsuario.Instance.GetUsuario();
        var options = new DbContextOptionsBuilder<RegisterContext>().UseInMemoryDatabase(databaseName: "GetSaldo_Should_Returns_Saldo_Equal_0").Options;
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
        var usuario = MockUsuario.Instance.GetUsuario();

        var despesaDbSetMock = new Mock<DbSet<Despesa>>();
        despesaDbSetMock.As<IQueryable<Despesa>>().Setup(d => d.Provider).Throws<Exception>();

        var options = new DbContextOptionsBuilder<RegisterContext>().UseInMemoryDatabase(databaseName: "GetSaldo_Throws_Exception_When_Despesa_Execute_Where").Options;
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
        var usuario = MockUsuario.Instance.GetUsuario();
        var receitaDbSetMock = new Mock<DbSet<Receita>>();
        receitaDbSetMock.As<IQueryable<Receita>>().Setup(d => d.Provider).Throws<Exception>();
        var options = new DbContextOptionsBuilder<RegisterContext>().UseInMemoryDatabase(databaseName: "GetSaldo_Throws_Exception_When_Receita_Execute_Where").Options;
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
        var idUsuario = _fixture.Context.Usuario.Last().Id;

        // Act
        var result = _fixture.MockRepository.Object.GetSaldoByAno(_fixture.MockAnoMes, idUsuario);

        // Assert            
        Assert.IsType<decimal>(result);
        Assert.True(result != 0);
    }

    [Fact]
    public void GetSaldoByAno_Should_Returns_Saldo_Equal_0()
    {
        // Arrange
        var idUsuario = _fixture.Context.Usuario.Last().Id;
        var options = new DbContextOptionsBuilder<RegisterContext>().UseInMemoryDatabase(databaseName: "GetSaldoByAno_Should_Returns_Saldo_Equal_0").Options;
        var context = new RegisterContext(options);
        context.Despesa.AddRange(new List<Despesa>());
        context.Receita.AddRange(new List<Receita>());
        context.SaveChanges();
        var repository = new SaldoRepositorioImpl(context);

        // Act
        var result = repository.GetSaldoByAno(_fixture.MockAnoMes, idUsuario);

        // Assert            
        Assert.IsType<decimal>(result);
        Assert.True(result == 0);
    }

    [Fact]
    public void GetSaldoByAno_Throws_Exception_When_Despesa_Execute_Where()
    {
        // Arrange
        var idUsuario = Guid.Empty;
        var despesaDbSetMock = new Mock<DbSet<Despesa>>();
        despesaDbSetMock.As<IQueryable<Despesa>>().Setup(d => d.Provider).Throws<Exception>();
        var options = new DbContextOptionsBuilder<RegisterContext>().UseInMemoryDatabase(databaseName: "GetSaldoByAno_Throws_Exception_When_Despesa_Execute_Where").Options;
        var context = new RegisterContext(options);
        context.Despesa = despesaDbSetMock.Object;
        var repository = new SaldoRepositorioImpl(context);

        // Act
        Action result = () => repository.GetSaldoByAno(_fixture.MockAnoMes, idUsuario);

        // Assert
        Assert.NotNull(result);
        var exception = Assert.Throws<Exception>(() => repository.GetSaldoByAno(_fixture.MockAnoMes, idUsuario));
        Assert.Equal("SaldoRepositorioImpl_GetSaldoByAno_Erro", exception.Message);
    }

    [Fact]
    public void GetSaldoByAno_Throws_Exception_When_Receita_Execute_Where()
    {
        // Arrange            
        var idUsuario = Guid.Empty;
        var receitaDbSetMock = new Mock<DbSet<Receita>>();
        receitaDbSetMock.As<IQueryable<Receita>>().Setup(d => d.Provider).Throws<Exception>();
        var options = new DbContextOptionsBuilder<RegisterContext>().UseInMemoryDatabase(databaseName: "GetSaldoByAno_Throws_Exception_When_Receita_Execute_Where").Options;
        var context = new RegisterContext(options);
        context.Receita = receitaDbSetMock.Object;
        var repository = new SaldoRepositorioImpl(context);

        // Act
        Action result = () => repository.GetSaldoByAno(_fixture.MockAnoMes, idUsuario);

        // Assert
        Assert.NotNull(result);
        var exception = Assert.Throws<Exception>(() => repository.GetSaldoByAno(_fixture.MockAnoMes, idUsuario));
        Assert.Equal("SaldoRepositorioImpl_GetSaldoByAno_Erro", exception.Message);
    }

    [Fact]
    public void GetSaldoByMesAno_Should_Returns_Saldo()
    {
        // Arrange
        var idUsuario = _fixture.Context.Usuario.Last().Id;

        // Act
        var result = _fixture.MockRepository.Object.GetSaldoByMesAno(_fixture.MockAnoMes, idUsuario);

        // Assert            
        Assert.IsType<decimal>(result);
        Assert.True(result != 0);
    }

    [Fact]
    public void GetSaldoByMesAno_Should_Returns_Saldo_Equal_0()
    {
        // Arrange
        var idUsuario = _fixture.Context.Usuario.First().Id;
        var options = new DbContextOptionsBuilder<RegisterContext>().UseInMemoryDatabase(databaseName: "GetSaldoByMesAno_Should_Returns_Saldo_Equal_0").Options;
        var context = new RegisterContext(options);
        context.Despesa.AddRange(new List<Despesa>());
        context.Receita.AddRange(new List<Receita>());
        context.SaveChanges();
        var repository = new SaldoRepositorioImpl(context);

        // Act
        var result = repository.GetSaldoByMesAno(_fixture.MockAnoMes, idUsuario);

        // Assert            
        Assert.IsType<decimal>(result);
        Assert.True(result == 0);
    }

    [Fact]
    public void GetSaldoByMesAno_Throws_Exception_When_Despesa_Execute_Where()
    {
        // Arrange
        var idUsuario = Guid.Empty;
        var despesaDbSetMock = new Mock<DbSet<Despesa>>();
        despesaDbSetMock.As<IQueryable<Despesa>>().Setup(d => d.Provider).Throws<Exception>();
        var options = new DbContextOptionsBuilder<RegisterContext>().UseInMemoryDatabase(databaseName: "GetSaldoByMesAno_Throws_Exception_When_Despesa_Execute_Where").Options;
        var context = new RegisterContext(options);
        context.Despesa = despesaDbSetMock.Object;
        var repository = new SaldoRepositorioImpl(context);

        // Act
        Action result = () => repository.GetSaldoByMesAno(_fixture.MockAnoMes, idUsuario);

        // Assert
        Assert.NotNull(result);
        var exception = Assert.Throws<Exception>(() => repository.GetSaldoByMesAno(_fixture.MockAnoMes, idUsuario));
        Assert.Equal("SaldoRepositorioImpl_GetSaldoByMesAno_Erro", exception.Message);
    }

    [Fact]
    public void GetSaldoByMesAno_Throws_Exception_When_Receita_Execute_Where()
    {
        // Arrange            
        var idUsuario = Guid.Empty;
        var receitaDbSetMock = new Mock<DbSet<Receita>>();
        receitaDbSetMock.As<IQueryable<Receita>>().Setup(d => d.Provider).Throws<Exception>();
        var options = new DbContextOptionsBuilder<RegisterContext>().UseInMemoryDatabase(databaseName: "GetSaldoByMesAno_Throws_Exception_When_Receita_Execute_Where").Options;
        var context = new RegisterContext(options);
        context.Receita = receitaDbSetMock.Object;
        var repository = new SaldoRepositorioImpl(context);

        // Act
        Action result = () => repository.GetSaldoByMesAno(_fixture.MockAnoMes, idUsuario);

        // Assert
        Assert.NotNull(result);
        var exception = Assert.Throws<Exception>(() => repository.GetSaldoByMesAno(_fixture.MockAnoMes, idUsuario));
        Assert.Equal("SaldoRepositorioImpl_GetSaldoByMesAno_Erro", exception.Message);
    }
}
