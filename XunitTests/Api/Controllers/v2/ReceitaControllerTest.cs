using Business.Abstractions;
using Business.Dtos.v2;
using Despesas.WebApi.Controllers.v2;
using Microsoft.AspNetCore.Mvc;
using __mock__.v2;
using AutoMapper;
using Business.Dtos.Core.Profile;

namespace Api.Controllers.v2;

public sealed class ReceitaControllerTest
{
    private Mock<IBusinessBase<ReceitaDto, Receita>> _mockReceitaBusiness;
    private ReceitaController _receitaController;
    private List<ReceitaDto> _receitaDtos;
    private Mapper _mapper;

    public ReceitaControllerTest()
    {
        _mockReceitaBusiness = new Mock<IBusinessBase<ReceitaDto, Receita>>();
        _receitaController = new ReceitaController(_mockReceitaBusiness.Object);
        _mapper = new Mapper(new MapperConfiguration(cfg => { cfg.AddProfile<ReceitaProfile>(); }));
        _receitaDtos = _mapper.Map<List<ReceitaDto>>(ReceitaFaker.Instance.Receitas());
    }

    [Fact]
    public void Get_Should_Returns_OkResults_With_Null_List_When_TryCatch_ThrowsError()
    {
        // Arrange
        Guid idUsuario = _receitaDtos.First().UsuarioId;
        Usings.SetupBearerToken(idUsuario, _receitaController);
        _mockReceitaBusiness.Setup(business => business.FindAll(idUsuario)).Throws<Exception>();

        // Act
        var result = _receitaController.Get() as ObjectResult;

        // Assert
        Assert.NotNull(result);
        Assert.IsType<OkObjectResult>(result);
        Assert.NotNull(result.Value);
        Assert.IsType<List<ReceitaDto>>(result.Value);
        var lstReceita = result.Value as List<ReceitaDto>;
        Assert.NotNull(lstReceita);
        Assert.Empty(lstReceita);
        _mockReceitaBusiness.Verify(b => b.FindAll(idUsuario), Times.Once);
    }

    [Fact]
    public void Get_Should_Return_All_Receitas_From_Usuario()
    {
        // Arrange
        Guid idUsuario = _receitaDtos.First().UsuarioId;
        Usings.SetupBearerToken(idUsuario, _receitaController);
        _mockReceitaBusiness.Setup(business => business.FindAll(idUsuario)).Returns(_receitaDtos);

        // Act
        var result = _receitaController.Get() as ObjectResult;

        // Assert
        Assert.NotNull(result);
        Assert.IsType<OkObjectResult>(result);
        Assert.Equal(_receitaDtos, result.Value);
        _mockReceitaBusiness.Verify(b => b.FindAll(idUsuario), Times.Once);
    }

    [Fact]
    public void GetById_Should_Returns_BadRequest_When_Receita_NULL()
    {
        // Arrange
        var receitaDto = _receitaDtos.First();
        var idUsuario = receitaDto.UsuarioId;
        Usings.SetupBearerToken(idUsuario, _receitaController);
        _mockReceitaBusiness.Setup(business => business.FindById(receitaDto.Id, idUsuario)).Returns(() => null);

        // Act
        var result = _receitaController.GetById(receitaDto.Id) as ObjectResult;

        // Assert
        Assert.NotNull(result);
        Assert.IsType<BadRequestObjectResult>(result);
        var message = result.Value;
        Assert.Equal("Nenhuma receita foi encontrada.", message);
        _mockReceitaBusiness.Verify(b => b.FindById(receitaDto.Id, idUsuario), Times.Once);
    }

    [Fact]
    public void GetById_Should_Returns_OkResults_With_Despesas()
    {
        // Arrange
        var receita = _receitaDtos.Last();
        Guid idUsuario = receita.UsuarioId;
        var receitaId = receita.Id;
        Usings.SetupBearerToken(idUsuario, _receitaController);
        _mockReceitaBusiness.Setup(business => business.FindById(receitaId, idUsuario)).Returns(receita);

        // Act
        var result = _receitaController.GetById(receitaId) as ObjectResult;

        // Assert
        Assert.NotNull(result);
        Assert.IsType<OkObjectResult>(result);
        var _receita = result.Value as ReceitaDto;
        Assert.NotNull(_receita);
        Assert.IsType<ReceitaDto>(_receita);
        _mockReceitaBusiness.Verify(b => b.FindById(receitaId, idUsuario), Times.Once);
    }

    [Fact]
    public void GetById_Should_Returns_BadRequest_When_Throws_Error()
    {
        // Arrange
        var receitaDto = _receitaDtos.First();
        var idUsuario = receitaDto.UsuarioId;
        Usings.SetupBearerToken(idUsuario, _receitaController);
        _mockReceitaBusiness.Setup(business => business.FindById(receitaDto.Id, idUsuario)).Throws(new Exception());

        // Act
        var result = _receitaController.GetById(receitaDto.Id) as ObjectResult;

        // Assert
        Assert.NotNull(result);
        Assert.IsType<BadRequestObjectResult>(result);
        var message = result.Value;
        Assert.Equal("Não foi possível realizar a consulta da receita.", message);
        _mockReceitaBusiness.Verify(b => b.FindById(receitaDto.Id, idUsuario), Times.Once);
    }

    [Fact]
    public void Post_Should_Create_Receita()
    {
        // Arrange
        var receitaDto = _receitaDtos[3];
        Guid idUsuario = receitaDto.UsuarioId;
        Usings.SetupBearerToken(idUsuario, _receitaController);
        _mockReceitaBusiness.Setup(business => business.Create(receitaDto)).Returns(receitaDto);

        // Act
        var result = _receitaController.Post(receitaDto) as ObjectResult;

        // Assert
        Assert.NotNull(result);
        Assert.IsType<OkObjectResult>(result);
        var _receita = result.Value as ReceitaDto;
        Assert.NotNull(_receita);
        Assert.IsType<ReceitaDto>(_receita);
        _mockReceitaBusiness.Verify(b => b.Create(receitaDto), Times.Once());
    }

    [Fact]
    public void Post_Should_Returns_BadRequest_When_Throws_Error()
    {
        // Arrange
        var receitaDto = _receitaDtos[3];
        Guid idUsuario = receitaDto.UsuarioId;
        Usings.SetupBearerToken(idUsuario, _receitaController);
        _mockReceitaBusiness.Setup(business => business.Create(receitaDto)).Throws(new Exception());

        // Act
        var result = _receitaController.Post(receitaDto) as ObjectResult;

        // Assert
        Assert.NotNull(result);
        Assert.IsType<BadRequestObjectResult>(result);
        var message = result.Value;
        Assert.Equal("Não foi possível realizar o cadastro da receita!", message);
        _mockReceitaBusiness.Verify(b => b.Create(receitaDto), Times.Once);
    }

    [Fact]
    public void Put_Should_Update_Receita()
    {
        // Arrange
        var receitaDto = _receitaDtos[4];
        Guid idUsuario = receitaDto.UsuarioId;
        Usings.SetupBearerToken(idUsuario, _receitaController);
        _mockReceitaBusiness.Setup(business => business.Update(receitaDto)).Returns(receitaDto);

        // Act
        var result = _receitaController.Put(receitaDto) as ObjectResult;

        // Assert
        Assert.NotNull(result);
        Assert.IsType<OkObjectResult>(result);
        var _receita = result.Value as ReceitaDto;
        Assert.NotNull(_receita);
        Assert.IsType<ReceitaDto>(_receita);
        _mockReceitaBusiness.Verify(b => b.Update(receitaDto), Times.Once);
    }

    [Fact]
    public void Put_Should_Returns_BadRequest_When_Receita_Return_Null()
    {
        // Arrange
        var receitaDto = _receitaDtos[3];
        Guid idUsuario = receitaDto.UsuarioId;
        Usings.SetupBearerToken(idUsuario, _receitaController);
        _mockReceitaBusiness.Setup(business => business.Update(receitaDto)).Returns(() => null);

        // Act
        var result = _receitaController.Put(receitaDto) as ObjectResult;

        // Assert
        Assert.NotNull(result);
        Assert.IsType<BadRequestObjectResult>(result);
        var message = result.Value;
        Assert.Equal("Não foi possível atualizar o cadastro da receita.", message);
        _mockReceitaBusiness.Verify(b => b.Update(receitaDto), Times.Once);
    }

    [Fact]
    public void Delete_Should_Returns_OkResult()
    {
        // Arrange
        var receitaDto = _receitaDtos[2];
        Guid idUsuario = receitaDto.UsuarioId;
        Usings.SetupBearerToken(idUsuario, _receitaController);
        _mockReceitaBusiness.Setup(business => business.Delete(receitaDto)).Returns(true);
        _mockReceitaBusiness.Setup(business => business.FindById(receitaDto.Id, idUsuario)).Returns(receitaDto);

        // Act
        var result = _receitaController.Delete(receitaDto.Id) as ObjectResult;

        // Assert
        Assert.NotNull(result);
        Assert.IsType<OkObjectResult>(result);
        var message = (bool?)result.Value;
        Assert.True(message);
        _mockReceitaBusiness.Verify(business => business.FindById(receitaDto.Id, idUsuario), Times.Once);
        _mockReceitaBusiness.Verify(b => b.Delete(receitaDto), Times.Once);
    }

    [Fact]
    public void Delete_With_InvalidToken_Returns_BadRequest()
    {
        // Arrange
        var receitaDto = _receitaDtos[2];
        Guid idUsuario = receitaDto.UsuarioId;
        Usings.SetupBearerToken(Guid.Empty, _receitaController);
        _mockReceitaBusiness.Setup(business => business.Delete(receitaDto)).Returns(true);
        _mockReceitaBusiness.Setup(business => business.FindById(receitaDto.Id, idUsuario)).Returns(receitaDto);

        // Act
        var result = _receitaController.Delete(receitaDto.Id) as ObjectResult;

        // Assert
        Assert.NotNull(result);
        Assert.IsType<BadRequestObjectResult>(result);
        var message = result.Value;
        Assert.Equal("Usuário não permitido a realizar operação!", message);
        _mockReceitaBusiness.Verify(business => business.FindById(receitaDto.Id, idUsuario), Times.Never);
        _mockReceitaBusiness.Verify(b => b.Delete(receitaDto), Times.Never);
    }

    [Fact]
    public void Delete_Should_Returns_BadResquest_When_Receita_Not_Deleted()
    {
        // Arrange
        var receitaDto = _receitaDtos[2];
        Guid idUsuario = receitaDto.UsuarioId;
        Usings.SetupBearerToken(idUsuario, _receitaController);
        _mockReceitaBusiness.Setup(business => business.Delete(receitaDto)).Returns(false);
        _mockReceitaBusiness.Setup(business => business.FindById(receitaDto.Id, idUsuario)).Returns(receitaDto);

        // Act
        var result = _receitaController.Delete(receitaDto.Id) as ObjectResult;

        // Assert
        Assert.NotNull(result);
        Assert.IsType<BadRequestObjectResult>(result);
        var message = result.Value;
        Assert.Equal("Erro ao excluir Receita!", message);
        _mockReceitaBusiness.Verify(business => business.FindById(receitaDto.Id, idUsuario), Times.Once);
        _mockReceitaBusiness.Verify(b => b.Delete(receitaDto), Times.Once);
    }
}
