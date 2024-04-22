using Business.Abstractions;
using despesas_backend_api_net_core.Controllers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Api.Controllers;

public class SaldoControllerTest
{
    protected Mock<ISaldoBusiness> _mockSaldoBusiness;
    protected SaldoController _SaldoController;

    private void SetupBearerToken(int idUsuario)
    {
        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, idUsuario.ToString())
        };
        var identity = new ClaimsIdentity(claims, "IdUsuario");
        var claimsPrincipal = new ClaimsPrincipal(identity);
        var httpContext = new DefaultHttpContext { User = claimsPrincipal };
        httpContext.Request.Headers["Authorization"] = "Bearer " + Usings.GenerateJwtToken(idUsuario); 
        _SaldoController.ControllerContext = new ControllerContext { HttpContext = httpContext };
    }

    public SaldoControllerTest()
    {
        _mockSaldoBusiness = new Mock<ISaldoBusiness>();
        _SaldoController = new SaldoController(_mockSaldoBusiness.Object);
    }

    [Fact]
    public void GetSaldo_Should_Return_Saldo()
    {
        // Arrange
        int idUsuario = 1;
        SetupBearerToken(idUsuario);
        decimal saldo = 1000.99m;
        _mockSaldoBusiness.Setup(business => business.GetSaldo(idUsuario)).Returns(saldo);

        // Act
        var result = _SaldoController.Get() as ObjectResult;

        // Assert
        Assert.NotNull(result);
        var okResult = Assert.IsType<OkObjectResult>(result);
        var returnedSaldo = (decimal)okResult.Value;
        Assert.IsType<decimal>(returnedSaldo);
        Assert.Equal(saldo, returnedSaldo);
    }

    [Fact]
    public void GetSaldo_Returns_BadRequest_When_Throws_Error()
    {
        // Arrange
        int idUsuario = 1;
        SetupBearerToken(1);
        _mockSaldoBusiness.Setup(business => business.GetSaldo(idUsuario)).Throws(new Exception());

        // Act
        var result = _SaldoController.Get() as ObjectResult;

        // Assert
        Assert.NotNull(result);
        Assert.IsType<BadRequestObjectResult>(result);
        var message = result.Value;
        Assert.Equal("Erro ao gerar saldo!", message);
        _mockSaldoBusiness.Verify(b => b.GetSaldo(idUsuario), Times.Once);
    }

    [Fact]
    public void GetSaldoByAno_Should_Return_Saldo()
    {
        // Arrange
        int idUsuario = 1;
        SetupBearerToken(idUsuario);
        decimal saldo = 897.99m;
        _mockSaldoBusiness.Setup(business => business.GetSaldoAnual(DateTime.Today, idUsuario)).Returns(saldo);

        // Act
        var result = _SaldoController.GetSaldoByAno(DateTime.Today) as ObjectResult;

        // Assert
        Assert.NotNull(result);
        var okResult = Assert.IsType<OkObjectResult>(result);
        var returnedSaldo = (decimal)okResult.Value;
        Assert.IsType<decimal>(returnedSaldo);
        Assert.Equal(saldo, returnedSaldo);
    }

    [Fact]
    public void GetSaldoByAno_Returns_BadRequest_When_Throws_Error()
    {
        // Arrange
        int idUsuario = 1;
        SetupBearerToken(1);
        _mockSaldoBusiness.Setup(business => business.GetSaldoAnual(DateTime.Today, idUsuario)).Throws(new Exception());

        // Act
        var result = _SaldoController.GetSaldoByAno(DateTime.Today) as ObjectResult;

        // Assert
        Assert.NotNull(result);
        Assert.IsType<BadRequestObjectResult>(result);
        var message  = result.Value;
        Assert.Equal("Erro ao gerar saldo!", message);
        _mockSaldoBusiness.Verify(b => b.GetSaldoAnual(DateTime.Today, idUsuario), Times.Once);
    }

    [Fact]
    public void GetSaldoByMesAno_Should_Return_Saldo()
    {
        // Arrange
        int idUsuario = 1;
        SetupBearerToken(idUsuario);
        decimal saldo = 178740.99m;
        _mockSaldoBusiness.Setup(business => business.GetSaldoByMesAno(DateTime.Today, idUsuario)).Returns(saldo);

        // Act
        var result = _SaldoController.GetSaldoByMesAno(DateTime.Today) as ObjectResult;

        // Assert
        Assert.NotNull(result);
        var okResult = Assert.IsType<OkObjectResult>(result);
        var returnedSaldo = (decimal)okResult.Value;
        Assert.IsType<decimal>(returnedSaldo);
        Assert.Equal(saldo, returnedSaldo);
    }

    [Fact]
    public void GetSaldoByMesAno_Returns_BadRequest_When_Throws_Error()
    {
        // Arrange
        int idUsuario = 1;
        SetupBearerToken(idUsuario);
        _mockSaldoBusiness.Setup(business => business.GetSaldoByMesAno(DateTime.Today, idUsuario)).Throws(new Exception());

        // Act
        var result = _SaldoController.GetSaldoByMesAno(DateTime.Today) as ObjectResult;

        // Assert
        Assert.NotNull(result);
        Assert.IsType<BadRequestObjectResult>(result);
        var message = result.Value;
        Assert.Equal("Erro ao gerar saldo!", message);
        _mockSaldoBusiness.Verify(b => b.GetSaldoByMesAno(DateTime.Today, idUsuario),Times.Once);
    }
}
