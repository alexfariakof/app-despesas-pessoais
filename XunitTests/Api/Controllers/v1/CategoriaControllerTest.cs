using Business.Abstractions.Generic;
using Business.Dtos.Core;
using Business.Dtos.v1;
using Despesas.WebApi.Controllers.v1;
using Domain.Entities.ValueObjects;
using Microsoft.AspNetCore.Mvc;
using __mock__.v1;

namespace Api.Controllers.v1;
public sealed class CategoriaControllerTest
{
    private Mock<IBusiness<CategoriaDto, Categoria>> _mockCategoriaBusiness;
    private CategoriaController _categoriaController;

    public CategoriaControllerTest()
    {
        _mockCategoriaBusiness = new Mock<IBusiness<CategoriaDto, Categoria>>();
        _categoriaController = new CategoriaController(_mockCategoriaBusiness.Object);
    }

    [Fact]
    public void Get_Returns_Ok_Result()
    {
        // Arrange
        _mockCategoriaBusiness = new Mock<IBusiness<CategoriaDto, Categoria>>();
        _categoriaController = new CategoriaController(_mockCategoriaBusiness.Object);
        var categoriaDtos = CategoriaFaker.Instance.CategoriasVMs();
        var idUsuario = categoriaDtos.First().UsuarioId;
        Usings.SetupBearerToken(idUsuario, _categoriaController);
        _mockCategoriaBusiness.Setup(b => b.FindAll(idUsuario)).Returns(categoriaDtos.FindAll(c => c.UsuarioId == idUsuario));

        // Act
        var result = _categoriaController.Get() as OkObjectResult;

        // Assert
        Assert.NotNull(result);
        Assert.IsType<OkObjectResult>(result);
        Assert.IsType<List<CategoriaDto>>(result.Value);
        Assert.Equal(categoriaDtos.FindAll(c => c.UsuarioId == idUsuario), result.Value);
    }

    [Fact]
    public void GetById_Returns_Ok_Result()
    {
        // Arrange
        _mockCategoriaBusiness = new Mock<IBusiness<CategoriaDto, Categoria>>();
        _categoriaController = new CategoriaController(_mockCategoriaBusiness.Object);
        var categoriaDto = CategoriaFaker.Instance.CategoriasVMs().First();
        var idCategoria = categoriaDto.Id;
        Usings.SetupBearerToken(categoriaDto.UsuarioId, _categoriaController);
        _mockCategoriaBusiness.Setup(b => b.FindById(idCategoria, categoriaDto.UsuarioId)).Returns(categoriaDto);

        // Act
        var result = _categoriaController.GetById(idCategoria) as OkObjectResult;

        // Assert
        Assert.NotNull(result);
        Assert.IsType<CategoriaDto>(result.Value);
        Assert.Equal(categoriaDto, result.Value);
    }

    [Fact]
    public void GetByTipoCategoria_Returns_Ok_Result_TipoCategoria_Todas()
    {
        // Arrange
        _mockCategoriaBusiness = new Mock<IBusiness<CategoriaDto, Categoria>>();
        _categoriaController = new CategoriaController(_mockCategoriaBusiness.Object);
        var listCategoriaDto = CategoriaFaker.Instance.CategoriasVMs();

        var idUsuario = listCategoriaDto.First().Id;
        Usings.SetupBearerToken(idUsuario, _categoriaController);
        var tipoCategoria = TipoCategoriaDto.Todas;
        _mockCategoriaBusiness.Setup(b => b.FindAll(idUsuario)).Returns(listCategoriaDto);

        // Act
        var result = _categoriaController.GetByTipoCategoria(tipoCategoria) as ObjectResult;

        // Assert
        Assert.NotNull(result);
        Assert.IsType<OkObjectResult>(result);
        Assert.IsType<List<CategoriaDto>>(result.Value);
    }

    [Fact]
    public void GetByTipoCategoria_Returns_Ok_Result()
    {
        // Arrange
        _mockCategoriaBusiness = new Mock<IBusiness<CategoriaDto, Categoria>>();
        _categoriaController = new CategoriaController(_mockCategoriaBusiness.Object);
        var listCategoriaDto = CategoriaFaker.Instance.CategoriasVMs();
        var idUsuario = listCategoriaDto.First().Id;
        Usings.SetupBearerToken(idUsuario, _categoriaController);
        var tipoCategoria = TipoCategoriaDto.Despesa;
        _mockCategoriaBusiness.Setup(b => b.FindAll(idUsuario)).Returns(listCategoriaDto);

        // Act
        var result = _categoriaController.GetByTipoCategoria(tipoCategoria) as ObjectResult;

        // Assert
        Assert.NotNull(result);
        Assert.IsType<OkObjectResult>(result);
        Assert.IsType<List<CategoriaDto>>(result.Value);
    }

    [Fact]
    public void Post_Returns_Ok_Result()
    {
        // Arrange
        _mockCategoriaBusiness = new Mock<IBusiness<CategoriaDto, Categoria>>();
        _categoriaController = new CategoriaController(_mockCategoriaBusiness.Object);
        var obj = CategoriaFaker.Instance.CategoriasVMs().First();
        var categoriaDto = new CategoriaDto
        {
            Id = obj.Id,
            Descricao = obj.Descricao,
            UsuarioId = 1,
            IdTipoCategoria = (int)TipoCategoria.CategoriaType.Despesa
        };
        Usings.SetupBearerToken(categoriaDto.UsuarioId, _categoriaController);
        _mockCategoriaBusiness.Setup(b => b.Create(categoriaDto)).Returns(categoriaDto);

        // Act
        var result = _categoriaController.Post(categoriaDto) as ObjectResult;

        // Assert
        Assert.NotNull(result);
        Assert.IsType<OkObjectResult>(result);
        var value = result.Value;

        var categoria = value?.GetType()?.GetProperty("categoria")?.PropertyType;

        Assert.NotNull(categoria);
        value = result.Value;

        var message = (bool?)value?.GetType()?.GetProperty("message")?.GetValue(value, null);

        Assert.True(message);
    }

    [Fact]
    public void Post_Returns_Bad_Request_When_TipoCategoria_Todas()
    {
        // Arrange
        _mockCategoriaBusiness = new Mock<IBusiness<CategoriaDto, Categoria>>();
        _categoriaController = new CategoriaController(_mockCategoriaBusiness.Object);
        var obj = CategoriaFaker.Instance.CategoriasVMs().First();
        var categoriaDto = new CategoriaDto
        {
            Id = obj.Id,
            Descricao = obj.Descricao,
            UsuarioId = obj.UsuarioId,
            IdTipoCategoria = (int)TipoCategoriaDto.Todas
        };
        Usings.SetupBearerToken(categoriaDto.UsuarioId, _categoriaController);
        _mockCategoriaBusiness.Setup(b => b.Create(categoriaDto)).Returns(categoriaDto);

        // Act
        var result = _categoriaController.Post(categoriaDto) as ObjectResult;

        // Assert
        Assert.NotNull(result);
        Assert.IsType<BadRequestObjectResult>(result);
        var value = result.Value;

        var message = value?.GetType()?.GetProperty("message")?.GetValue(value, null) as string;

        Assert.Equal("Nenhum tipo de Categoria foi selecionado!", message);
    }

    [Fact]
    public void Post_Returns_Bad_Request_When_TryCatch_ThrowError()
    {
        // Arrange Para ocorrer esta situação o tipo de categotia não pode ser == Todas
        _mockCategoriaBusiness = new Mock<IBusiness<CategoriaDto, Categoria>>();
        _categoriaController = new CategoriaController(_mockCategoriaBusiness.Object);
        var categoriaDto = CategoriaFaker.Instance.CategoriasVMs().First();
        categoriaDto.IdTipoCategoria = (int)TipoCategoria.CategoriaType.Receita;

        Usings.SetupBearerToken(categoriaDto.UsuarioId, _categoriaController);

        _mockCategoriaBusiness.Setup(b => b.Create(categoriaDto)).Throws(new Exception());

        // Act
        var result = _categoriaController.Post(categoriaDto) as ObjectResult;

        // Assert
        Assert.NotNull(result);
        Assert.IsType<BadRequestObjectResult>(result);
        var value = result.Value;

        var message = value?.GetType()?.GetProperty("message")?.GetValue(value, null) as string;

        Assert.Equal(
            "Não foi possível realizar o cadastro de uma nova categoria, tente mais tarde ou entre em contato com o suporte.",
            message
        );
    }

    [Fact]
    public void Put_Returns_Ok_Result()
    {
        // Arrange
        _mockCategoriaBusiness = new Mock<IBusiness<CategoriaDto, Categoria>>();
        _categoriaController = new CategoriaController(_mockCategoriaBusiness.Object);
        var obj = CategoriaFaker.Instance.CategoriasVMs().First();
        var categoriaDto = new CategoriaDto
        {
            Id = obj.Id,
            Descricao = obj.Descricao,
            UsuarioId = obj.UsuarioId,
            IdTipoCategoria = (int)TipoCategoria.CategoriaType.Despesa
        };
        Usings.SetupBearerToken(categoriaDto.UsuarioId, _categoriaController);
        _mockCategoriaBusiness.Setup(b => b.Update(categoriaDto)).Returns(categoriaDto);

        // Act
        var result = _categoriaController.Put(categoriaDto) as ObjectResult;

        // Assert
        Assert.NotNull(result);
        var value = result.Value;
        var message = (bool?)value?.GetType()?.GetProperty("message")?.GetValue(value, null);
        Assert.True(message);
        var _categoriaDto = value?.GetType()?.GetProperty("categoria")?.GetValue(value, null);
        Assert.IsType<CategoriaDto>(_categoriaDto);
        Assert.Equal(_categoriaDto, categoriaDto);
    }

    [Fact]
    public void Put_Returns_Bad_Request_TipoCategoria_Todas()
    {
        // Arrange
        _mockCategoriaBusiness = new Mock<IBusiness<CategoriaDto, Categoria>>();
        _categoriaController = new CategoriaController(_mockCategoriaBusiness.Object);
        var categoriaDto = CategoriaFaker.Instance.CategoriasVMs().First();
        categoriaDto.IdTipoCategoria = 0;
        Usings.SetupBearerToken(categoriaDto.UsuarioId, _categoriaController);
        _mockCategoriaBusiness.Setup(b => b.Update(categoriaDto)).Returns(categoriaDto);

        // Act
        var result = _categoriaController.Put(categoriaDto) as BadRequestObjectResult;

        // Assert
        Assert.NotNull(result);
        Assert.IsType<BadRequestObjectResult>(result);
        var value = result.Value;
        var message = value?.GetType()?.GetProperty("message")?.GetValue(value, null) as string;
        Assert.Equal("Nenhum tipo de Categoria foi selecionado!", message);
    }

    [Fact]
    public void Put_Returns_Bad_Request_Categoria_Null()
    {
        // Arrange
        _mockCategoriaBusiness = new Mock<IBusiness<CategoriaDto, Categoria>>();
        _categoriaController = new CategoriaController(_mockCategoriaBusiness.Object);
        var categoriaDto = CategoriaFaker.Instance.CategoriasVMs().First();
        categoriaDto.IdTipoCategoria = 1;
        Usings.SetupBearerToken(categoriaDto.UsuarioId, _categoriaController);
        _mockCategoriaBusiness.Setup(b => b.Update(categoriaDto)).Returns((CategoriaDto)null);

        // Act
        var result = _categoriaController.Put(categoriaDto) as BadRequestObjectResult;

        // Assert
        Assert.NotNull(result);
        Assert.IsType<BadRequestObjectResult>(result);
        var value = result.Value;
        var message = value?.GetType()?.GetProperty("message")?.GetValue(value, null) as string;
        Assert.Equal("Erro ao atualizar categoria!", message);
    }

    [Fact]
    public void Delete_Returns_Ok_Result()
    {
        // Arrange
        _mockCategoriaBusiness = new Mock<IBusiness<CategoriaDto, Categoria>>();
        _categoriaController = new CategoriaController(_mockCategoriaBusiness.Object);
        var obj = CategoriaFaker.Instance.CategoriasVMs().Last();
        var categoriaDto = new CategoriaDto
        {
            Id = obj.Id,
            Descricao = obj.Descricao,
            UsuarioId = 10,
            IdTipoCategoria = (int)TipoCategoriaDto.Receita
        };
        Usings.SetupBearerToken(10, _categoriaController);
        _mockCategoriaBusiness.Setup(b => b.Delete(It.IsAny<CategoriaDto>())).Returns(true);
        _mockCategoriaBusiness.Setup(b => b.FindById(categoriaDto.Id, categoriaDto.UsuarioId)).Returns(categoriaDto);

        // Act
        var result = _categoriaController.Delete(categoriaDto.Id) as ObjectResult;

        // Assert
        Assert.NotNull(result);
        Assert.IsType<OkObjectResult>(result);
        var value = result.Value;

        var message = (bool?)value?.GetType()?.GetProperty("message")?.GetValue(value, null);

        Assert.True(message);
        _mockCategoriaBusiness.Verify(b => b.FindById(categoriaDto.Id, categoriaDto.UsuarioId),Times.Once);
        _mockCategoriaBusiness.Verify(b => b.Delete(categoriaDto), Times.Once);
    }

    [Fact]
    public void Delete_Returns_OK_Result_Message_False()
    {
        // Arrange
        _mockCategoriaBusiness = new Mock<IBusiness<CategoriaDto, Categoria>>();
        _categoriaController = new CategoriaController(_mockCategoriaBusiness.Object);
        var obj = CategoriaFaker.Instance.CategoriasVMs().Last();
        var categoriaDto = new CategoriaDto
        {
            Id = obj.Id,
            Descricao = obj.Descricao,
            UsuarioId = 100,
            IdTipoCategoria = (int)TipoCategoria.CategoriaType.Receita
        };
        Usings.SetupBearerToken(100, _categoriaController);
        _mockCategoriaBusiness.Setup(b => b.Delete(categoriaDto)).Returns(false);
        _mockCategoriaBusiness.Setup(b => b.FindById(categoriaDto.Id, categoriaDto.UsuarioId)).Returns(categoriaDto);

        // Act
        var result = _categoriaController.Delete(categoriaDto.Id) as ObjectResult;

        // Assert
        Assert.NotNull(result);
        Assert.IsType<BadRequestObjectResult>(result);
        var value = result.Value;

        var message = (bool?)value?.GetType()?.GetProperty("message")?.GetValue(value, null);

        Assert.False(message);
        _mockCategoriaBusiness.Verify(b => b.FindById(categoriaDto.Id, categoriaDto.UsuarioId),Times.Once);
        _mockCategoriaBusiness.Verify(b => b.Delete(categoriaDto), Times.Once);
    }

    [Fact]
    public void Delete_Returns_BadRequest()
    {
        // Arrange
        _mockCategoriaBusiness = new Mock<IBusiness<CategoriaDto, Categoria>>();
        _categoriaController = new CategoriaController(_mockCategoriaBusiness.Object);
        var categoriaDto = CategoriaFaker.Instance.CategoriasVMs().First();
        Usings.SetupBearerToken(0, _categoriaController);
        _mockCategoriaBusiness.Setup(b => b.Delete(categoriaDto)).Returns(false);
        _mockCategoriaBusiness.Setup(b => b.FindById(categoriaDto.Id, categoriaDto.UsuarioId)).Returns(categoriaDto);

        // Act
        var result = _categoriaController.Delete(categoriaDto.Id) as BadRequestObjectResult;

        // Assert
        Assert.NotNull(result);
        Assert.IsType<BadRequestObjectResult>(result);

        _mockCategoriaBusiness.Verify(
            b => b.FindById(categoriaDto.Id, categoriaDto.UsuarioId),
            Times.Never
        );

        _mockCategoriaBusiness.Verify(b => b.Delete(categoriaDto), Times.Never);
    }
}
