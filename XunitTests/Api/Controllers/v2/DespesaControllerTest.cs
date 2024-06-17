using Business.Dtos.v2;
using Despesas.WebApi.Controllers.v2;
using Microsoft.AspNetCore.Mvc;
using __mock__.v2;
using AutoMapper;
using Business.Dtos.Core.Profile;
using Business.Abstractions;

namespace Api.Controllers.v2;

public sealed class DespesaControllerTest
{
    private Mock<IBusinessBase<DespesaDto, Despesa>> _mockDespesaBusiness;
    private DespesaController _despesaController;
    private Mapper _mapper;

    public DespesaControllerTest()
    {
        _mockDespesaBusiness = new Mock<IBusinessBase<DespesaDto, Despesa>>();
        _despesaController = new DespesaController(_mockDespesaBusiness.Object);
        _mapper = new Mapper(new MapperConfiguration(cfg => { cfg.AddProfile<DespesaProfile>(); }));
    }

    [Fact]
    public void Get_Should_Return_All_Despesas_From_Usuario()
    {
        // Arrange
        var _despesaDtos = DespesaFaker.Instance.DespesasVMs();
        int idUsuario = _despesaDtos.First().UsuarioId;
        Usings.SetupBearerToken(idUsuario, _despesaController);
        _mockDespesaBusiness.Setup(business => business.FindAll(idUsuario)).Returns(_despesaDtos);

        // Act
        var result = _despesaController.Get() as ObjectResult;

        // Assert
        Assert.NotNull(result);
        Assert.IsType<OkObjectResult>(result);
        Assert.Equal(_despesaDtos, result.Value);
        _mockDespesaBusiness.Verify(b => b.FindAll(idUsuario), Times.Once);
    }

    [Fact]
    public void Get_Should_Returns_OkResults_With_Null_List_When_TryCatch_ThrowsError()
    {
        // Arrange
        var _despesaDtos = DespesaFaker.Instance.DespesasVMs();
        int idUsuario = _despesaDtos.First().UsuarioId;
        Usings.SetupBearerToken(idUsuario, _despesaController);
        _mockDespesaBusiness.Setup(business => business.FindAll(idUsuario)).Throws<Exception>();

        // Act
        var result = _despesaController.Get() as ObjectResult;

        // Assert
        Assert.NotNull(result);
        Assert.IsType<OkObjectResult>(result);
        Assert.NotNull(result.Value);
        Assert.IsType<List<DespesaDto>>(result.Value);
        var lstDespesas = result.Value as List<DespesaDto>;
        Assert.NotNull(lstDespesas);
        Assert.Empty(lstDespesas);
        _mockDespesaBusiness.Verify(b => b.FindAll(idUsuario), Times.Once);
    }


    [Fact]
    public void GetById_Should_Returns_BadRequest_When_Despesa_NULL()
    {
        // Arrange
        var despesaDto = DespesaFaker.Instance.DespesasVMs().First();
        int idUsuario = despesaDto.UsuarioId;
        Usings.SetupBearerToken(idUsuario, _despesaController);
        _mockDespesaBusiness.Setup(business => business.FindById(despesaDto.Id, idUsuario)).Returns<DespesaDto>(null);

        // Act
        var result = _despesaController.Get(despesaDto.Id) as ObjectResult;

        // Assert
        Assert.NotNull(result);
        Assert.IsType<BadRequestObjectResult>(result);
        var message = result.Value;
        Assert.Equal("Nenhuma despesa foi encontrada.", message);
        _mockDespesaBusiness.Verify(b => b.FindById(despesaDto.Id, idUsuario), Times.Once);
    }

    [Fact]
    public void GetById_Should_Returns_OkResults_With_Despesas()
    {
        // Arrange
        var despesa = DespesaFaker.Instance.Despesas().First();
        var despesaDto = _mapper.Map<DespesaDto>(despesa);
        int idUsuario = despesaDto.UsuarioId;
        int despesaId = despesa.Id;
        Usings.SetupBearerToken(idUsuario, _despesaController);
        _mockDespesaBusiness.Setup(business => business.FindById(despesaId, idUsuario)).Returns(despesaDto);

        // Act
        var result = _despesaController.Get(despesaId) as ObjectResult;

        // Assert
        Assert.NotNull(result);
        Assert.IsType<OkObjectResult>(result);
        var _despesa = result.Value;
        Assert.NotNull(_despesa);
        Assert.IsType<DespesaDto>(_despesa);
        _mockDespesaBusiness.Verify(b => b.FindById(despesaId, idUsuario), Times.Once);
    }

    [Fact]
    public void GetById_Should_Returns_BadRequest_When_Throws_Error()
    {
        // Arrange
        var despesaDto = DespesaFaker.Instance.DespesasVMs().First();
        int idUsuario = despesaDto.UsuarioId;
        Usings.SetupBearerToken(idUsuario, _despesaController);
        _mockDespesaBusiness.Setup(business => business.FindById(despesaDto.Id, idUsuario)).Throws(new Exception());

        // Act
        var result = _despesaController.Get(despesaDto.Id) as ObjectResult;

        // Assert
        Assert.NotNull(result);
        Assert.IsType<BadRequestObjectResult>(result);
        var message = result.Value;
        Assert.Equal("Não foi possível realizar a consulta da despesa.", message);
        _mockDespesaBusiness.Verify(b => b.FindById(despesaDto.Id, idUsuario), Times.Once);
    }

    [Fact]
    public void Post_Should_Create_Despesa()
    {
        // Arrange
        var _despesaDtos = DespesaFaker.Instance.DespesasVMs();
        var despesaDto = _despesaDtos[3];
        int idUsuario = despesaDto.UsuarioId;
        Usings.SetupBearerToken(idUsuario, _despesaController);
        _mockDespesaBusiness.Setup(business => business.Create(despesaDto)).Returns(despesaDto);

        // Act
        var result = _despesaController.Post(despesaDto) as ObjectResult;

        // Assert
        Assert.NotNull(result);
        Assert.IsType<OkObjectResult>(result);
        var _despesa = result.Value;
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
        int idUsuario = despesaDto.UsuarioId;

        Usings.SetupBearerToken(idUsuario, _despesaController);
        _mockDespesaBusiness.Setup(business => business.Create(despesaDto)).Throws(new Exception());

        // Act
        var result = _despesaController.Post(despesaDto) as ObjectResult;

        // Assert
        Assert.NotNull(result);
        Assert.IsType<BadRequestObjectResult>(result);
        var message = result.Value;
        Assert.Equal("Não foi possível realizar o cadastro da despesa.", message);
        _mockDespesaBusiness.Verify(b => b.Create(despesaDto), Times.Once);
    }

    [Fact]
    public void Put_Should_Update_Despesa()
    {
        // Arrange
        var _despesaDtos = DespesaFaker.Instance.DespesasVMs();
        var despesaDto = _despesaDtos[4];
        int idUsuario = despesaDto.UsuarioId;
        Usings.SetupBearerToken(idUsuario, _despesaController);
        _mockDespesaBusiness.Setup(business => business.Update(despesaDto)).Returns(despesaDto);

        // Act
        var result = _despesaController.Put(despesaDto) as ObjectResult;

        // Assert
        Assert.NotNull(result);
        Assert.IsType<OkObjectResult>(result);
        var _despesa = result.Value;
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
        int idUsuario = despesaDto.UsuarioId;
        Usings.SetupBearerToken(idUsuario, _despesaController);
        _mockDespesaBusiness.Setup(business => business.Update(despesaDto)).Returns<DespesaDto>(null);

        // Act
        var result = _despesaController.Put(despesaDto) as ObjectResult;

        // Assert
        Assert.NotNull(result);
        Assert.IsType<BadRequestObjectResult>(result);
        var message = result.Value;
        Assert.Equal("Não foi possível atualizar o cadastro da despesa.", message);
        _mockDespesaBusiness.Verify(b => b.Update(despesaDto), Times.Once);
    }

    [Fact]
    public void Delete_Should_Returns_OkResult()
    {
        // Arrange
        var _despesaDtos = DespesaFaker.Instance.DespesasVMs();
        var despesaDto = _despesaDtos[2];
        int idUsuario = despesaDto.UsuarioId;
        Usings.SetupBearerToken(idUsuario, _despesaController);
        _mockDespesaBusiness.Setup(business => business.Delete(despesaDto)).Returns(true);
        _mockDespesaBusiness.Setup(business => business.FindById(despesaDto.Id, idUsuario)).Returns(despesaDto);

        // Act
        var result = _despesaController.Delete(despesaDto.Id) as ObjectResult;

        // Assert
        Assert.NotNull(result);
        Assert.IsType<OkObjectResult>(result);
        var message = (bool?)result.Value;
        Assert.True(message);
        _mockDespesaBusiness.Verify(business => business.FindById(despesaDto.Id, idUsuario),Times.Once);
        _mockDespesaBusiness.Verify(b => b.Delete(despesaDto), Times.Once);
    }

    [Fact]
    public void Delete__With_InvalidToken_Returns_BadRequest()
    {
        // Arrange
        var _despesaDtos = DespesaFaker.Instance.DespesasVMs();
        var despesaDto = _despesaDtos[2];
        int idUsuario = despesaDto.UsuarioId;
        Usings.SetupBearerToken(0, _despesaController);
        _mockDespesaBusiness.Setup(business => business.Delete(despesaDto)).Returns(true);
        _mockDespesaBusiness.Setup(business => business.FindById(despesaDto.Id, idUsuario)).Returns(despesaDto);
        
        // Act
        var result = _despesaController.Delete(despesaDto.Id) as ObjectResult;

        // Assert
        Assert.NotNull(result);
        Assert.IsType<BadRequestObjectResult>(result);
        var message = result.Value;
        Assert.Equal("Usuário não permitido a realizar operação!", message);
        _mockDespesaBusiness.Verify(business => business.FindById(despesaDto.Id, idUsuario),Times.Never);
        _mockDespesaBusiness.Verify(b => b.Delete(despesaDto), Times.Never);
    }

    [Fact]
    public void Delete_Should_Returns_BadResquest_When_Despesa_Not_Deleted()
    {
        // Arrange
        var _despesaDtos = DespesaFaker.Instance.DespesasVMs();
        var despesaDto = _despesaDtos[2];
        int idUsuario = despesaDto.UsuarioId;
        Usings.SetupBearerToken(idUsuario, _despesaController);
        _mockDespesaBusiness.Setup(business => business.Delete(despesaDto)).Returns(false);
        _mockDespesaBusiness.Setup(business => business.FindById(despesaDto.Id, idUsuario)).Returns(despesaDto);

        // Act
        var result = _despesaController.Delete(despesaDto.Id) as ObjectResult;

        // Assert
        Assert.NotNull(result);
        Assert.IsType<BadRequestObjectResult>(result);
        var message = result.Value;
        Assert.Equal("Erro ao excluir Despesa!", message);
        _mockDespesaBusiness.Verify(business => business.FindById(despesaDto.Id, idUsuario),Times.Once);
        _mockDespesaBusiness.Verify(b => b.Delete(despesaDto), Times.Once);
    }
}