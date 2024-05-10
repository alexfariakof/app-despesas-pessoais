﻿using Business.Dtos.Parser;
using despesas_backend_api_net_core.Controllers.v1;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Api.Controllers.v1;

public class DespesaControllerTest
{
    protected Mock<IBusiness<DespesaDto>> _mockDespesaBusiness;
    protected DespesaController _despesaController;

    private void SetupBearerToken(int idUsuario)
    {
        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, idUsuario.ToString())
        };
        var identity = new ClaimsIdentity(claims, "IdUsuario");
        var claimsPrincipal = new ClaimsPrincipal(identity);

        var httpContext = new DefaultHttpContext { User = claimsPrincipal };
        httpContext.Request.Headers["Authorization"] =
            "Bearer " + Usings.GenerateJwtToken(idUsuario);

        _despesaController.ControllerContext = new ControllerContext
        {
            HttpContext = httpContext
        };
    }

    public DespesaControllerTest()
    {
        _mockDespesaBusiness = new Mock<IBusiness<DespesaDto>>();
        _despesaController = new DespesaController(_mockDespesaBusiness.Object);
    }

    [Fact]
    public void Get_Should_Return_All_Despesas_From_Usuario()
    {
        // Arrange
        var _despesaDtos = DespesaFaker.Instance.DespesasVMs();

        int idUsuario = _despesaDtos.First().IdUsuario;

        SetupBearerToken(idUsuario);
        _mockDespesaBusiness
            .Setup(business => business.FindAll(idUsuario))
            .Returns(_despesaDtos);

        // Act
        var result = _despesaController.Get() as ObjectResult;

        // Assert
        Assert.NotNull(result);
        Assert.IsType<OkObjectResult>(result);
        Assert.Equal(_despesaDtos, result.Value);
        _mockDespesaBusiness.Verify(b => b.FindAll(idUsuario), Times.Once);
    }

    [Fact]
    public void GetById_Should_Returns_BadRequest_When_Despesa_NULL()
    {
        // Arrange
        var despesaDto = DespesaFaker.Instance.DespesasVMs().First();

        int idUsuario = despesaDto.IdUsuario;

        SetupBearerToken(idUsuario);

        _mockDespesaBusiness
            .Setup(business => business.FindById(despesaDto.Id, idUsuario))
            .Returns((DespesaDto)null);

        // Act
        var result = _despesaController.Get(despesaDto.Id) as ObjectResult;

        // Assert
        Assert.NotNull(result);
        Assert.IsType<BadRequestObjectResult>(result);
        var value = result.Value;
        var message = value?.GetType()?.GetProperty("message")?.GetValue(value, null) as string;
        Assert.Equal("Nenhuma despesa foi encontrada.", message);
        _mockDespesaBusiness.Verify(b => b.FindById(despesaDto.Id, idUsuario), Times.Once);
    }

    [Fact]
    public void GetById_Should_Returns_OkResults_With_Despesas()
    {
        // Arrange
        var despesa = DespesaFaker.Instance.Despesas().First();
        var despesaDto = new DespesaParser().Parse(despesa);

        int idUsuario = despesaDto.UsuarioId;

        int despesaId = despesa.Id;
        SetupBearerToken(idUsuario);

        _mockDespesaBusiness
            .Setup(business => business.FindById(despesaId, idUsuario))
            .Returns(despesaDto);

        // Act
        var result = _despesaController.Get(despesaId) as ObjectResult;

        // Assert
        Assert.NotNull(result);
        Assert.IsType<OkObjectResult>(result);
        var value = result.Value;

        var message = (bool)value?.GetType()?.GetProperty("message")?.GetValue(value, null);

        Assert.True(message);
        var _despesa =
            value?.GetType()?.GetProperty("despesa")?.GetValue(value, null) as DespesaDto;
        Assert.NotNull(_despesa);
        Assert.IsType<DespesaDto>(_despesa);
        _mockDespesaBusiness.Verify(b => b.FindById(despesaId, idUsuario), Times.Once);
    }

    [Fact]
    public void GetById_Should_Returns_BadRequest_When_Throws_Error()
    {
        // Arrange
        var despesaDto = DespesaFaker.Instance.DespesasVMs().First();

        int idUsuario = despesaDto.IdUsuario;

        SetupBearerToken(idUsuario);
        _mockDespesaBusiness
            .Setup(business => business.FindById(despesaDto.Id, idUsuario))
            .Throws(new Exception());

        // Act
        var result = _despesaController.Get(despesaDto.Id) as ObjectResult;

        // Assert
        Assert.NotNull(result);
        Assert.IsType<BadRequestObjectResult>(result);
        var value = result.Value;
        var message = value?.GetType()?.GetProperty("message")?.GetValue(value) as string;
        Assert.Equal("Não foi possível realizar a consulta da despesa.", message);
        _mockDespesaBusiness.Verify(b => b.FindById(despesaDto.Id, idUsuario), Times.Once);
    }

    [Fact]
    public void Post_Should_Create_Despesa()
    {
        // Arrange
        var _despesaDtos = DespesaFaker.Instance.DespesasVMs();
        var despesaDto = _despesaDtos[3];

        int idUsuario = despesaDto.IdUsuario;

        SetupBearerToken(idUsuario);
        _mockDespesaBusiness.Setup(business => business.Create(despesaDto)).Returns(despesaDto);

        // Act
        var result = _despesaController.Post(despesaDto) as ObjectResult;

        // Assert
        Assert.NotNull(result);
        Assert.IsType<OkObjectResult>(result);
        var value = result.Value;

        var message = (bool)value.GetType().GetProperty("message").GetValue(value, null);

        Assert.True(message);
        var _despesa =
            value?.GetType()?.GetProperty("despesa")?.GetValue(value, null) as DespesaDto;
        Assert.NotNull(_despesa);
        Assert.IsType<DespesaDto>(_despesa);
        _mockDespesaBusiness.Verify(b => b.Create(despesaDto), Times.Once());
    }

    [Fact]
    public void Post_Should_Returns_BadRequest_When_Throws_Error()
    {
        // Arrange
        var _despesaDtos = DespesaFaker.Instance.DespesasVMs();
        var despesaDto = _despesaDtos[3];

        int idUsuario = despesaDto.IdUsuario;

        SetupBearerToken(idUsuario);
        _mockDespesaBusiness
            .Setup(business => business.Create(despesaDto))
            .Throws(new Exception());

        // Act
        var result = _despesaController.Post(despesaDto) as ObjectResult;

        // Assert
        Assert.NotNull(result);
        Assert.IsType<BadRequestObjectResult>(result);
        var value = result.Value;
        var message = value?.GetType()?.GetProperty("message")?.GetValue(value, null) as string;
        Assert.Equal("Não foi possível realizar o cadastro da despesa.", message);
        _mockDespesaBusiness.Verify(b => b.Create(despesaDto), Times.Once);
    }

    [Fact]
    public void Put_Should_Update_Despesa()
    {
        // Arrange
        var _despesaDtos = DespesaFaker.Instance.DespesasVMs();
        var despesaDto = _despesaDtos[4];

        int idUsuario = despesaDto.IdUsuario;

        SetupBearerToken(idUsuario);
        _mockDespesaBusiness.Setup(business => business.Update(despesaDto)).Returns(despesaDto);

        // Act
        var result = _despesaController.Put(despesaDto) as ObjectResult;

        // Assert
        Assert.NotNull(result);
        Assert.IsType<OkObjectResult>(result);
        var value = result.Value;

        var message = (bool)value.GetType().GetProperty("message").GetValue(value, null);

        Assert.True(message);

        var _despesa = (DespesaDto)value.GetType().GetProperty("despesa").GetValue(value, null);

        Assert.NotNull(_despesa);
        Assert.IsType<DespesaDto>(_despesa);
        _mockDespesaBusiness.Verify(b => b.Update(despesaDto), Times.Once);
    }

    [Fact]
    public void Put_Should_Returns_BadRequest_When_Despesa_Return_Null()
    {
        // Arrange
        var _despesaDtos = DespesaFaker.Instance.DespesasVMs();
        var despesaDto = _despesaDtos[3];

        int idUsuario = despesaDto.IdUsuario;

        SetupBearerToken(idUsuario);

        _mockDespesaBusiness
            .Setup(business => business.Update(despesaDto))
            .Returns((DespesaDto)null);

        // Act
        var result = _despesaController.Put(despesaDto) as ObjectResult;

        // Assert
        Assert.NotNull(result);
        Assert.IsType<BadRequestObjectResult>(result);
        var value = result.Value;
        var message = value?.GetType()?.GetProperty("message")?.GetValue(value, null) as string;
        Assert.Equal("Não foi possível atualizar o cadastro da despesa.", message);
        _mockDespesaBusiness.Verify(b => b.Update(despesaDto), Times.Once);
    }

    [Fact]
    public void Delete_Should_Returns_OkResult()
    {
        // Arrange
        var _despesaDtos = DespesaFaker.Instance.DespesasVMs();
        var despesaDto = _despesaDtos[2];

        int idUsuario = despesaDto.IdUsuario;

        SetupBearerToken(idUsuario);

        _mockDespesaBusiness.Setup(business => business.Delete(despesaDto)).Returns(true);
        _mockDespesaBusiness
            .Setup(business => business.FindById(despesaDto.Id, idUsuario))
            .Returns(despesaDto);

        // Act
        var result = _despesaController.Delete(despesaDto.Id) as ObjectResult;

        // Assert
        Assert.NotNull(result);
        Assert.IsType<OkObjectResult>(result);
        var value = result.Value;

        var message = (bool)value.GetType().GetProperty("message").GetValue(value, null);

        Assert.True(message);
        _mockDespesaBusiness.Verify(
            business => business.FindById(despesaDto.Id, idUsuario),
            Times.Once
        );
        _mockDespesaBusiness.Verify(b => b.Delete(despesaDto), Times.Once);
    }

    [Fact]
    public void Delete__With_InvalidToken_Returns_BadRequest()
    {
        // Arrange
        var _despesaDtos = DespesaFaker.Instance.DespesasVMs();
        var despesaDto = _despesaDtos[2];

        int idUsuario = despesaDto.IdUsuario;

        SetupBearerToken(0);

        _mockDespesaBusiness.Setup(business => business.Delete(despesaDto)).Returns(true);
        _mockDespesaBusiness
            .Setup(business => business.FindById(despesaDto.Id, idUsuario))
            .Returns(despesaDto);
        // Act
        var result = _despesaController.Delete(despesaDto.Id) as ObjectResult;

        // Assert
        Assert.NotNull(result);
        Assert.IsType<BadRequestObjectResult>(result);
        var value = result.Value;
        var message = value?.GetType()?.GetProperty("message")?.GetValue(value, null) as string;
        Assert.Equal("Usuário não permitido a realizar operação!", message);
        _mockDespesaBusiness.Verify(
            business => business.FindById(despesaDto.Id, idUsuario),
            Times.Never
        );
        _mockDespesaBusiness.Verify(b => b.Delete(despesaDto), Times.Never);
    }

    [Fact]
    public void Delete_Should_Returns_BadResquest_When_Despesa_Not_Deleted()
    {
        // Arrange
        var _despesaDtos = DespesaFaker.Instance.DespesasVMs();
        var despesaDto = _despesaDtos[2];

        int idUsuario = despesaDto.IdUsuario;

        SetupBearerToken(idUsuario);

        _mockDespesaBusiness.Setup(business => business.Delete(despesaDto)).Returns(false);
        _mockDespesaBusiness
            .Setup(business => business.FindById(despesaDto.Id, idUsuario))
            .Returns(despesaDto);

        // Act
        var result = _despesaController.Delete(despesaDto.Id) as ObjectResult;

        // Assert
        Assert.NotNull(result);
        Assert.IsType<BadRequestObjectResult>(result);
        var value = result.Value;
        var message = value?.GetType()?.GetProperty("message")?.GetValue(value, null) as string;
        Assert.Equal("Erro ao excluir Despesa!", message);
        _mockDespesaBusiness.Verify(
            business => business.FindById(despesaDto.Id, idUsuario),
            Times.Once
        );
        _mockDespesaBusiness.Verify(b => b.Delete(despesaDto), Times.Once);
    }
}
