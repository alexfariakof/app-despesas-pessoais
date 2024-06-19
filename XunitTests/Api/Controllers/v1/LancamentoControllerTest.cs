using Business.Abstractions;
using Business.Dtos.v1;
using Despesas.WebApi.Controllers.v1;
using Microsoft.AspNetCore.Mvc;
using __mock__.v1;

namespace Api.Controllers.v1;
public sealed class LancamentoControllerTest
{
    private Mock<ILancamentoBusiness<LancamentoDto>> _mockLancamentoBusiness;
    private  LancamentoController _lancamentoController;
    private  List<LancamentoDto> _lancamentoDtos;

    public LancamentoControllerTest()
    {
        _mockLancamentoBusiness = new Mock<ILancamentoBusiness<LancamentoDto>>();
        _lancamentoController = new LancamentoController(_mockLancamentoBusiness.Object);
        _lancamentoDtos = LancamentoFaker.LancamentoDtos();
    }

    [Fact]
    public void Get_Should_Return_LancamentoDtos()
    {
        // Arrange
        var lancamentoDtos = _lancamentoDtos;
        int idUsuario = _lancamentoDtos.First().UsuarioId;

        DateTime anoMes = DateTime.Now;
        Usings.SetupBearerToken(idUsuario, _lancamentoController);
        _mockLancamentoBusiness.Setup(business => business.FindByMesAno(anoMes, idUsuario)).Returns(lancamentoDtos.FindAll(l => l.UsuarioId == idUsuario));

        // Act
        var result = _lancamentoController.Get(anoMes) as ObjectResult;

        // Assert
        // Assert
        Assert.NotNull(result);
        Assert.IsType<OkObjectResult>(result);
        var value = result.Value;
        var message = (bool?)value?.GetType()?.GetProperty("message")?.GetValue(value, null);
        var lancamentos = (List<LancamentoDto>?)value?.GetType()?.GetProperty("lancamentos")?.GetValue(value, null);
        Assert.True(message);
        Assert.NotNull(lancamentos);
        Assert.NotEmpty(lancamentos);
        var returnedLancamentoDtos = Assert.IsType<List<LancamentoDto>>(lancamentos);
        Assert.Equal(lancamentoDtos.FindAll(l => l.UsuarioId == idUsuario),returnedLancamentoDtos);
        _mockLancamentoBusiness.Verify(b => b.FindByMesAno(anoMes, idUsuario), Times.Once);
    }

    [Fact]
    public void Get_Returns_OkResult_With_Empty_List_When_Lancamento_IsNull()
    {
        // Arrange
        var lancamentoDtos = _lancamentoDtos;
        int idUsuario = _lancamentoDtos.First().UsuarioId;
        DateTime anoMes = DateTime.Now;
        Usings.SetupBearerToken(idUsuario, _lancamentoController);
        _mockLancamentoBusiness.Setup(business => business.FindByMesAno(anoMes, idUsuario)).Returns(() => null);

        // Act
        var result = _lancamentoController.Get(anoMes) as ObjectResult;

        // Assert
        Assert.NotNull(result);
        Assert.IsType<OkObjectResult>(result);
        var value = result.Value;
        var message = (bool?)value?.GetType()?.GetProperty("message")?.GetValue(value, null);
        var lancamentos = (List<LancamentoDto>?) value?.GetType()?.GetProperty("lancamentos")?.GetValue(value, null);
        Assert.True(message);
        Assert.NotNull(lancamentos);
        Assert.Empty(lancamentos);
        _mockLancamentoBusiness.Verify(b => b.FindByMesAno(anoMes, idUsuario), Times.Once);
    }

    [Fact]
    public void Get_Returns_OkResult_With_Empty_List_When_Lancamento_List_Count0()
    {
        // Arrange
        var lancamentoDtos = _lancamentoDtos;
        int idUsuario = _lancamentoDtos.First().UsuarioId;
        DateTime anoMes = DateTime.Now;
        Usings.SetupBearerToken(idUsuario, _lancamentoController);
        _mockLancamentoBusiness.Setup(business => business.FindByMesAno(anoMes, idUsuario)).Returns(new List<LancamentoDto>());
        
        // Act
        var result = _lancamentoController.Get(anoMes) as ObjectResult;

        // Assert
        Assert.NotNull(result);
        Assert.IsType<OkObjectResult>(result);
        var value = result.Value;
        var message = (bool?)value?.GetType()?.GetProperty("message")?.GetValue(value, null);
        var lancamentos = (List<LancamentoDto>?) value?.GetType()?.GetProperty("lancamentos")?.GetValue(value, null);
        Assert.True(message);
        Assert.NotNull(lancamentos);
        Assert.Empty(lancamentos);
        _mockLancamentoBusiness.Verify(b => b.FindByMesAno(anoMes, idUsuario), Times.Once);
    }

    [Fact]
    public void Get_Returns_OkResults_With_Empty_List_When_Throws_Error()
    {
        // Arrange
        var lancamentoDtos = _lancamentoDtos;
        int idUsuario = _lancamentoDtos.First().UsuarioId;
        DateTime anoMes = DateTime.Now;
        Usings.SetupBearerToken(idUsuario, _lancamentoController);
        _mockLancamentoBusiness.Setup(business => business.FindByMesAno(anoMes, idUsuario)).Throws(new Exception());

        // Act
        var result = _lancamentoController.Get(anoMes) as ObjectResult;

        // Assert
        Assert.NotNull(result);
        Assert.IsType<OkObjectResult>(result);
        var value = result.Value;
        var message = (bool?)value?.GetType()?.GetProperty("message")?.GetValue(value, null);
        var lancamentos = (List<LancamentoDto>?) value?.GetType()?.GetProperty("lancamentos")?.GetValue(value, null);
        Assert.True(message);
        Assert.NotNull(lancamentos);
        Assert.Empty(lancamentos);
        _mockLancamentoBusiness.Verify(b => b.FindByMesAno(anoMes, idUsuario), Times.Once);
    }
}
;