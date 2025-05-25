using Business.Abstractions;
using Despesas.WebApi.Controllers.v1;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers.v1;

public sealed class SaldoControllerTest
{
    private Mock<ISaldoBusiness> _mockSaldoBusiness;
    private SaldoController _SaldoController;
    public SaldoControllerTest()
    {
        _mockSaldoBusiness = new Mock<ISaldoBusiness>();
        _SaldoController = new SaldoController(_mockSaldoBusiness.Object);
    }

    [Fact]
    public void GetSaldo_Should_Return_Saldo()
    {
        // Arrange
        var idUsuario = Guid.NewGuid();
        Usings.SetupBearerToken(idUsuario, _SaldoController);
        decimal saldo = 1000.99m;
        _mockSaldoBusiness.Setup(business => business.GetSaldo(idUsuario)).Returns(saldo);

        // Act
        var result = _SaldoController.Get() as ObjectResult;

        // Assert
        Assert.NotNull(result);
        var okResult = Assert.IsType<OkObjectResult>(result);
        var value = okResult.Value;
        var message = (bool?)value?.GetType()?.GetProperty("message")?.GetValue(value, null);
        var returnedSaldo = value?.GetType()?.GetProperty("saldo")?.GetValue(value, null) as Business.Dtos.v2.SaldoDto;
        Assert.True(message);
        Assert.IsType<Business.Dtos.v2.SaldoDto>(returnedSaldo);
        Assert.Equal(saldo, returnedSaldo.saldo);
    }

    [Fact]
    public void GetSaldo_Returns_BadRequest_When_Throws_Error()
    {
        // Arrange
        var idUsuario = Guid.Empty;
        Usings.SetupBearerToken(idUsuario, _SaldoController);
        _mockSaldoBusiness.Setup(business => business.GetSaldo(idUsuario)).Throws(new Exception());

        // Act
        var result = _SaldoController.Get() as ObjectResult;

        // Assert
        Assert.NotNull(result);
        Assert.IsType<BadRequestObjectResult>(result);
        var value = result.Value;
        var message = value?.GetType()?.GetProperty("message")?.GetValue(value, null) as string;
        Assert.Equal("Erro ao gerar saldo!", message);
        _mockSaldoBusiness.Verify(b => b.GetSaldo(idUsuario), Times.Once);
    }

    [Fact]
    public void GetSaldoByAno_Should_Return_Saldo()
    {
        // Arrange
        var idUsuario = Guid.NewGuid();
        Usings.SetupBearerToken(idUsuario, _SaldoController);
        decimal saldo = 897.99m;
        _mockSaldoBusiness.Setup(business => business.GetSaldoAnual(DateTime.Today, idUsuario)).Returns(saldo);

        // Act
        var result = _SaldoController.GetSaldoByAno(DateTime.Today) as ObjectResult;

        // Assert
        Assert.NotNull(result);
        var okResult = Assert.IsType<OkObjectResult>(result);
        var value = okResult.Value;
        var message = (bool?)value?.GetType()?.GetProperty("message")?.GetValue(value, null);
        var returnedSaldo = value?.GetType()?.GetProperty("saldo")?.GetValue(value, null) as Business.Dtos.v2.SaldoDto;
        Assert.True(message);
        Assert.IsType<Business.Dtos.v2.SaldoDto>(returnedSaldo);
        Assert.Equal(saldo, returnedSaldo.saldo);
    }

    [Fact]
    public void GetSaldoByAno_Returns_BadRequest_When_Throws_Error()
    {
        // Arrange
        var idUsuario = Guid.Empty;
        Usings.SetupBearerToken(idUsuario, _SaldoController);
        _mockSaldoBusiness.Setup(business => business.GetSaldoAnual(DateTime.Today, idUsuario)).Throws(new Exception());

        // Act
        var result = _SaldoController.GetSaldoByAno(DateTime.Today) as ObjectResult;

        // Assert
        Assert.NotNull(result);
        Assert.IsType<BadRequestObjectResult>(result);
        var value = result.Value;
        var message = value?.GetType()?.GetProperty("message")?.GetValue(value, null) as string;
        Assert.Equal("Erro ao gerar saldo!", message);
        _mockSaldoBusiness.Verify(b => b.GetSaldoAnual(DateTime.Today, idUsuario), Times.Once);
    }

    [Fact]
    public void GetSaldoByMesAno_Should_Return_Saldo()
    {
        // Arrange
        var idUsuario = Guid.NewGuid();
        Usings.SetupBearerToken(idUsuario, _SaldoController);
        decimal saldo = 178740.99m;
        _mockSaldoBusiness.Setup(business => business.GetSaldoByMesAno(DateTime.Today, idUsuario)).Returns(saldo);

        // Act
        var result = _SaldoController.GetSaldoByMesAno(DateTime.Today) as ObjectResult;

        // Assert
        Assert.NotNull(result);
        var okResult = Assert.IsType<OkObjectResult>(result);
        var value = okResult.Value;
        var message = (bool?)value?.GetType()?.GetProperty("message")?.GetValue(value, null);
        var returnedSaldo = value?.GetType()?.GetProperty("saldo")?.GetValue(value, null) as Business.Dtos.v2.SaldoDto;
        Assert.True(message);
        Assert.IsType<Business.Dtos.v2.SaldoDto>(returnedSaldo);
        Assert.Equal(saldo, returnedSaldo.saldo);
    }

    [Fact]
    public void GetSaldoByMesAno_Returns_BadRequest_When_Throws_Error()
    {
        // Arrange
        var idUsuario = Guid.Empty;
        Usings.SetupBearerToken(idUsuario, _SaldoController);
        _mockSaldoBusiness.Setup(business => business.GetSaldoByMesAno(DateTime.Today, idUsuario)).Throws(new Exception());

        // Act
        var result = _SaldoController.GetSaldoByMesAno(DateTime.Today) as ObjectResult;

        // Assert
        Assert.NotNull(result);
        Assert.IsType<BadRequestObjectResult>(result);
        var value = result.Value;
        var message = value?.GetType()?.GetProperty("message")?.GetValue(value, null) as string;
        Assert.Equal("Erro ao gerar saldo!", message);
        _mockSaldoBusiness.Verify(b => b.GetSaldoByMesAno(DateTime.Today, idUsuario), Times.Once);
    }
}