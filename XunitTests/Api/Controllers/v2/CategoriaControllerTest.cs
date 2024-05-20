using Business.Dtos.v2;
using despesas_backend_api_net_core.Controllers.v2;
using Domain.Entities.ValueObjects;
using Microsoft.AspNetCore.Mvc;
using Fakers.v2;
using Business.Dtos.Core;
using Business.Abstractions;

namespace Api.Controllers.v2;
public sealed class CategoriaControllerTest
{
    private Mock<IBusinessBase<CategoriaDto, Categoria>> _mockCategoriaBusiness;
    private CategoriaController _categoriaController;

    public CategoriaControllerTest()
    {
        _mockCategoriaBusiness = new Mock<IBusinessBase<CategoriaDto, Categoria>>();
        _categoriaController = new CategoriaController(_mockCategoriaBusiness.Object);
    }

    [Fact]
    public void Get_Returns_Ok_Result()
    {
        // Arrange
        _mockCategoriaBusiness = new Mock<IBusinessBase<CategoriaDto, Categoria>>();
        _categoriaController = new CategoriaController(_mockCategoriaBusiness.Object);
        var categoriaDtos = CategoriaFaker.Instance.CategoriasVMs();
        var UsuarioId = categoriaDtos.First().UsuarioId;
        Usings.SetupBearerToken(UsuarioId, _categoriaController);
        _mockCategoriaBusiness.Setup(b => b.FindAll(UsuarioId)).Returns(categoriaDtos.FindAll(c => c.UsuarioId == UsuarioId));

        // Act
        var result = _categoriaController.Get() as OkObjectResult;

        // Assert
        Assert.NotNull(result);
        Assert.IsType<OkObjectResult>(result);
        Assert.IsType<List<CategoriaDto>>(result.Value);
        Assert.Equal(categoriaDtos.FindAll(c => c.UsuarioId == UsuarioId), result.Value);
    }

    [Fact]
    public void Get_Returns_Ok_Result_When_TryCatch_ThrowError()
    {
        // Arrange
        _mockCategoriaBusiness = new Mock<IBusinessBase<CategoriaDto, Categoria>>();
        _categoriaController = new CategoriaController(_mockCategoriaBusiness.Object);
        var categoriaDtos = CategoriaFaker.Instance.CategoriasVMs();
        var UsuarioId = categoriaDtos.First().UsuarioId;
        Usings.SetupBearerToken(UsuarioId, _categoriaController);
        _mockCategoriaBusiness.Setup(b => b.FindAll(UsuarioId)).Throws<Exception>();

        // Act
        var result = _categoriaController.Get() as OkObjectResult;

        // Assert
        Assert.NotNull(result);
        Assert.IsType<OkObjectResult>(result);
        Assert.IsType<List<CategoriaDto>>(result.Value);
        var lstCategoria = result.Value as List<CategoriaDto>;
        Assert.NotNull(lstCategoria);
        Assert.Empty(lstCategoria);
    }


    [Fact]
    public void GetById_Returns_Ok_Result()
    {
        // Arrange
        _mockCategoriaBusiness = new Mock<IBusinessBase<CategoriaDto, Categoria>>();
        _categoriaController = new CategoriaController(_mockCategoriaBusiness.Object);
        var categoriaDto = CategoriaFaker.Instance.CategoriasVMs().First();
        var idCategoria = categoriaDto.UsuarioId;
        Usings.SetupBearerToken(idCategoria, _categoriaController);
        _mockCategoriaBusiness.Setup(b => b.FindById(idCategoria, categoriaDto.UsuarioId)).Returns(categoriaDto);

        // Act
        var result = _categoriaController.GetById(idCategoria) as OkObjectResult;

        // Assert
        Assert.NotNull(result);
        Assert.IsType<CategoriaDto>(result.Value);
        Assert.Equal(categoriaDto, result.Value);
    }

    [Fact]
    public void GetById_Returns_Ok_Result_When_TryCatch_ThrowError()
    {
        // Arrange
        _mockCategoriaBusiness = new Mock<IBusinessBase<CategoriaDto, Categoria>>();
        _categoriaController = new CategoriaController(_mockCategoriaBusiness.Object);
        var categoriaDto = CategoriaFaker.Instance.CategoriasVMs().First();

        var idCategoria = categoriaDto.UsuarioId;

        Usings.SetupBearerToken(idCategoria, _categoriaController);

        _mockCategoriaBusiness.Setup(b => b.FindById(idCategoria, categoriaDto.UsuarioId)).Throws<Exception>();

        // Act
        var result = _categoriaController.GetById(idCategoria) as OkObjectResult;

        // Assert
        Assert.NotNull(result);
        Assert.IsType<CategoriaDto>(result.Value);
        Assert.NotNull(result.Value as CategoriaDto);
    }

    [Fact]
    public void GetByTipoCategoria_Returns_Ok_Result_TipoCategoria_Todas()
    {
        // Arrange
        _mockCategoriaBusiness = new Mock<IBusinessBase<CategoriaDto, Categoria>>();
        _categoriaController = new CategoriaController(_mockCategoriaBusiness.Object);
        var listCategoriaDto = CategoriaFaker.Instance.CategoriasVMs();

        var UsuarioId = listCategoriaDto.First().Id;

        Usings.SetupBearerToken(UsuarioId, _categoriaController);
        var tipoCategoria = TipoCategoriaDto.Todas;
        _mockCategoriaBusiness.Setup(b => b.FindAll(UsuarioId)).Returns(listCategoriaDto);

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
        _mockCategoriaBusiness = new Mock<IBusinessBase<CategoriaDto, Categoria>>();
        _categoriaController = new CategoriaController(_mockCategoriaBusiness.Object);
        List<CategoriaDto> listCategoriaDto = CategoriaFaker.Instance.CategoriasVMs();
        var UsuarioId = listCategoriaDto.First().Id;
        Usings.SetupBearerToken(UsuarioId, _categoriaController);
        var tipoCategoria = TipoCategoriaDto.Despesa;
        _mockCategoriaBusiness.Setup(b => b.FindAll(UsuarioId)).Returns(listCategoriaDto);

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
        _mockCategoriaBusiness = new Mock<IBusinessBase<CategoriaDto, Categoria>>();
        _categoriaController = new CategoriaController(_mockCategoriaBusiness.Object);
        var obj = CategoriaFaker.Instance.CategoriasVMs().First();
        var categoriaDto = new CategoriaDto
        {
            Id = obj.Id,
            Descricao = obj.Descricao,
            UsuarioId = 1,
            IdTipoCategoria = (TipoCategoriaDto)TipoCategoria.CategoriaType.Despesa
        };
        Usings.SetupBearerToken(categoriaDto.UsuarioId, _categoriaController);
        _mockCategoriaBusiness.Setup(b => b.Create(categoriaDto)).Returns(categoriaDto);

        // Act
        var result = _categoriaController.Post(categoriaDto) as ObjectResult;

        // Assert
        Assert.NotNull(result);
        Assert.IsType<OkObjectResult>(result);
        Assert.NotNull(result.Value);
        Assert.IsType<CategoriaDto>(result.Value);
    }

    [Fact]
    public void Post_Returns_Bad_Request_When_TipoCategoria_Todas()
    {
        // Arrange
        _mockCategoriaBusiness = new Mock<IBusinessBase<CategoriaDto, Categoria>>();
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
        var message = result.Value;
        Assert.Equal("Nenhum tipo de Categoria foi selecionado!", message);
    }

    [Fact]
    public void Post_Returns_Bad_Request_When_TryCatch_ThrowError()
    {
        // Arrange Para ocorrer esta situação o tipo de categotia não pode ser == Todas
        _mockCategoriaBusiness = new Mock<IBusinessBase<CategoriaDto, Categoria>>();
        _categoriaController = new CategoriaController(_mockCategoriaBusiness.Object);
        var categoriaDto = CategoriaFaker.Instance.CategoriasVMs().First();
        categoriaDto.IdTipoCategoria = (TipoCategoriaDto)TipoCategoria.CategoriaType.Receita;
        Usings.SetupBearerToken(categoriaDto.UsuarioId, _categoriaController);

        _mockCategoriaBusiness.Setup(b => b.Create(categoriaDto)).Throws(new Exception());

        // Act
        var result = _categoriaController.Post(categoriaDto) as ObjectResult;

        // Assert
        Assert.NotNull(result);
        Assert.IsType<BadRequestObjectResult>(result);
        var message = result.Value;
        Assert.Equal("Não foi possível realizar o cadastro de uma nova categoria, tente mais tarde ou entre em contato com o suporte.", message);
    }

    [Fact]
    public void Put_Returns_Ok_Result()
    {
        // Arrange
        _mockCategoriaBusiness = new Mock<IBusinessBase<CategoriaDto, Categoria>>();
        _categoriaController = new CategoriaController(_mockCategoriaBusiness.Object);
        var obj = CategoriaFaker.Instance.CategoriasVMs().First();
        var categoriaDto = new CategoriaDto
        {
            Id = obj.Id,
            Descricao = obj.Descricao,
            UsuarioId = obj.UsuarioId,
            IdTipoCategoria = (TipoCategoriaDto)TipoCategoria.CategoriaType.Despesa
        };
        Usings.SetupBearerToken(categoriaDto.UsuarioId, _categoriaController);
        _mockCategoriaBusiness.Setup(b => b.Update(categoriaDto)).Returns(categoriaDto);

        // Act
        var result = _categoriaController.Put(categoriaDto) as ObjectResult;

        // Assert
        Assert.NotNull(result);
        Assert.IsType<CategoriaDto>(result.Value);
        Assert.Equal(result.Value, categoriaDto);
    }

    [Fact]
    public void Put_Returns_Bad_Request_TipoCategoria_Todas()
    {
        // Arrange
        _mockCategoriaBusiness = new Mock<IBusinessBase<CategoriaDto, Categoria>>();
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
        var message = result.Value;
        Assert.Equal("Nenhum tipo de Categoria foi selecionado!", message);
    }

    [Fact]
    public void Put_Returns_Bad_Request_Categoria_Null()
    {
        // Arrange
        _mockCategoriaBusiness = new Mock<IBusinessBase<CategoriaDto, Categoria>>();
        _categoriaController = new CategoriaController(_mockCategoriaBusiness.Object);
        var categoriaDto = CategoriaFaker.Instance.CategoriasVMs().First();
        categoriaDto.IdTipoCategoria = (TipoCategoriaDto)1;
        Usings.SetupBearerToken(categoriaDto.UsuarioId, _categoriaController);
        _mockCategoriaBusiness.Setup(b => b.Update(categoriaDto)).Returns<CategoriaDto>(null);

        // Act
        var result = _categoriaController.Put(categoriaDto) as BadRequestObjectResult;

        // Assert
        Assert.NotNull(result);
        Assert.IsType<BadRequestObjectResult>(result);
        var message = result.Value;
        Assert.Equal("Erro ao atualizar categoria!", message);
    }

    [Fact]
    public void Delete_Returns_Ok_Result()
    {
        // Arrange
        _mockCategoriaBusiness = new Mock<IBusinessBase<CategoriaDto, Categoria>>();
        _categoriaController = new CategoriaController(_mockCategoriaBusiness.Object);
        var obj = CategoriaFaker.Instance.CategoriasVMs().Last();
        var categoriaDto = new CategoriaDto
        {
            Id = obj.Id,
            Descricao = obj.Descricao,
            UsuarioId = 10,
            IdTipoCategoria = (TipoCategoriaDto)TipoCategoria.CategoriaType.Receita
        };
        Usings.SetupBearerToken(10, _categoriaController);
        _mockCategoriaBusiness.Setup(b => b.Delete(It.IsAny<CategoriaDto>())).Returns(true);
        _mockCategoriaBusiness.Setup(b => b.FindById(categoriaDto.Id, categoriaDto.UsuarioId)).Returns(categoriaDto);

        // Act
        var result = _categoriaController.Delete(categoriaDto.Id) as ObjectResult;

        // Assert
        Assert.NotNull(result);
        Assert.IsType<OkObjectResult>(result);
        var message = (bool?)result.Value;
        Assert.True(message);
        _mockCategoriaBusiness.Verify(b => b.FindById(categoriaDto.Id, categoriaDto.UsuarioId), Times.Once);
        _mockCategoriaBusiness.Verify(b => b.Delete(categoriaDto), Times.Once);
    }

    [Fact]
    public void Delete_Returns_OK_Result_Message_False()
    {
        // Arrange
        _mockCategoriaBusiness = new Mock<IBusinessBase<CategoriaDto, Categoria>>();
        _categoriaController = new CategoriaController(_mockCategoriaBusiness.Object);
        var obj = CategoriaFaker.Instance.CategoriasVMs().Last();
        var categoriaDto = new CategoriaDto
        {
            Id = obj.Id,
            Descricao = obj.Descricao,
            UsuarioId = 100,
            IdTipoCategoria = (TipoCategoriaDto)TipoCategoria.CategoriaType.Receita
        };
        Usings.SetupBearerToken(100, _categoriaController);
        _mockCategoriaBusiness.Setup(b => b.Delete(categoriaDto)).Returns(false);
        _mockCategoriaBusiness.Setup(b => b.FindById(categoriaDto.Id, categoriaDto.UsuarioId)).Returns(categoriaDto);

        // Act
        var result = _categoriaController.Delete(categoriaDto.Id) as ObjectResult;

        // Assert
        Assert.NotNull(result);
        Assert.IsType<OkObjectResult>(result);
        var message = (bool?)result.Value;
        Assert.False(message);
        _mockCategoriaBusiness.Verify(b => b.FindById(categoriaDto.Id, categoriaDto.UsuarioId), Times.Once);
        _mockCategoriaBusiness.Verify(b => b.Delete(categoriaDto), Times.Once);
    }

    [Fact]
    public void Delete_Returns_BadRequest()
    {
        // Arrange
        _mockCategoriaBusiness = new Mock<IBusinessBase<CategoriaDto, Categoria>>();
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
        _mockCategoriaBusiness.Verify(b => b.FindById(categoriaDto.Id, categoriaDto.UsuarioId), Times.Never);
        _mockCategoriaBusiness.Verify(b => b.Delete(categoriaDto), Times.Never);
    }

    [Fact]
    public void Delete_Returns_BadRequest_When_TryCatch_ThrowError()
    {
        // Arrange
        _mockCategoriaBusiness = new Mock<IBusinessBase<CategoriaDto, Categoria>>();
        _categoriaController = new CategoriaController(_mockCategoriaBusiness.Object);
        var categoriaDto = CategoriaFaker.Instance.CategoriasVMs().First();
        Usings.SetupBearerToken(categoriaDto.UsuarioId, _categoriaController);
        _mockCategoriaBusiness.Setup(b => b.FindById(categoriaDto.Id, categoriaDto.UsuarioId)).Returns(categoriaDto);
        _mockCategoriaBusiness.Setup(b => b.Delete(It.IsAny<CategoriaDto>())).Throws<Exception>();

        // Act
        var result = _categoriaController.Delete(categoriaDto.Id) as BadRequestObjectResult;

        // Assert
        Assert.NotNull(result);
        Assert.IsType<BadRequestObjectResult>(result);
        _mockCategoriaBusiness.Verify(b => b.FindById(It.IsAny<int>(), It.IsAny<int>()), Times.Once);
        _mockCategoriaBusiness.Verify(b => b.Delete(It.IsAny<CategoriaDto>()), Times.Once);
    }
}