using AutoMapper;
using Business.Dtos.v2;
using __mock__.v2;
using Domain.Entities.ValueObjects;
using Repository.Persistency.UnitOfWork.Abstractions;
using Business.Dtos.Core.Profile;
using Business.Implementations;
using Repository.Persistency.Generic;

namespace Business;

public class DespesaBusinessImplTest
{
    private readonly Mock<IUnitOfWork<Despesa>> _unitOfWork;
    private readonly Mock<IRepositorio<Despesa>> _repositorioMock;
    private readonly Mock<IRepositorio<Categoria>> _repositorioCategoria;
    private readonly DespesaBusinessImpl<DespesaDto> _despesaBusiness;
    private Mapper _mapper;

    public DespesaBusinessImplTest()
    {        
        _unitOfWork = new Mock<IUnitOfWork<Despesa>>(MockBehavior.Default);
        _repositorioMock = new Mock<IRepositorio<Despesa>>();
        _repositorioCategoria = new Mock<IRepositorio<Categoria>>(MockBehavior.Default);
        _mapper = new Mapper(new MapperConfiguration(cfg => { cfg.AddProfile<DespesaProfile>(); }));
        _despesaBusiness = new DespesaBusinessImpl<DespesaDto>(_mapper, _unitOfWork.Object, _repositorioMock.Object,  _repositorioCategoria.Object);
    }

    [Fact]
    public void Create_Should_Returns_Parsed_Despesa_VM()
    {
        // Arrange
        var despesa = DespesaFaker.Instance.Despesas().First();
        var despesaDto = _mapper.Map<DespesaDto>(despesa);
        _repositorioMock.Setup(repo => repo.Insert(ref It.Ref<Despesa>.IsAny));
        var categorias = CategoriaFaker.Instance.Categorias(despesa.Usuario, (int)TipoCategoria.CategoriaType.Despesa, despesa.UsuarioId);
        categorias.Add(despesa.Categoria ?? new());
        _repositorioCategoria.Setup(repo => repo.GetAll()).Returns(categorias);
        
        // Act
        var result = _despesaBusiness.Create(despesaDto);

        // Assert
        Assert.NotNull(result);
        Assert.IsType<DespesaDto>(result);
        Assert.Equal(despesaDto.Id, result.Id);
        _repositorioMock.Verify(repo => repo.Insert(ref It.Ref<Despesa>.IsAny), Times.Once);
    }

    [Fact]
    public void FindAll_Should_Returns_List_Of_DespesaDto()
    {
        // Arrange                     
        var despesas = DespesaFaker.Instance.Despesas();
        var despesa = despesas.First();
        var idUsuario = despesa.UsuarioId;
        despesas = despesas.FindAll(d => d.UsuarioId == idUsuario);
        _repositorioMock.Setup(repo => repo.GetAll()).Returns(despesas);

        // Act
        var result = _despesaBusiness.FindAll(idUsuario);

        // Assert
        Assert.NotNull(result);
        Assert.IsType<List<DespesaDto>>(result);
        Assert.Equal(despesas.Count, result.Count);
        _repositorioMock.Verify(repo => repo.GetAll(), Times.Once);
    }

    [Fact]
    public void FindById_Should_Returns_Parsed_DespesaDto()
    {
        // Arrange
        var despesa = DespesaFaker.Instance.Despesas().First();
        var id = despesa.Id;

        _repositorioMock.Setup(repo => repo.Get(id)).Returns(despesa);

        // Act
        var result = _despesaBusiness.FindById(id, despesa.UsuarioId);

        // Assert
        Assert.NotNull(result);
        Assert.IsType<DespesaDto>(result);
        Assert.Equal(despesa.Id, result.Id);
        _repositorioMock.Verify(repo => repo.Get(id), Times.AtLeast(2));
    }

    [Fact]
    public void FindById_Should_Returns_Null_When_Parsed_DespesaDto()
    {
        // Arrange
        var despesa = DespesaFaker.Instance.Despesas().First();
        var id = despesa.Id;

        _repositorioMock.Setup(repo => repo.Get(id)).Returns<Despesa>(null);

        // Act
        var result = _despesaBusiness.FindById(id, 0);

        // Assert
        Assert.Null(result);
        _repositorioMock.Verify(repo => repo.Get(id), Times.Once);
    }

    [Fact]
    public void Update_Should_Returns_Parsed_DespesaDto()
    {
        // Arrange         
        var despesas = DespesaFaker.Instance.Despesas();
        var despesa = despesas.First();
        despesa.Descricao = "Teste Update Despesa";
        var despesaDto = _mapper.Map<DespesaDto>(despesa);        
        _repositorioMock.Setup(repo => repo.Update(ref It.Ref<Despesa>.IsAny));
        _repositorioMock.Setup(repo => repo.Get(It.IsAny<int>())).Returns(despesa);
        _repositorioCategoria.Setup(repo => repo.GetAll()).Returns(despesas.Select(d => d.Categoria ?? new()).ToList());

        // Act
        var result = _despesaBusiness.Update(despesaDto);

        // Assert
        Assert.NotNull(result);
        Assert.IsType<DespesaDto>(result);
        Assert.Equal(despesa.Id, result.Id);
        Assert.Equal(despesa.Descricao, result.Descricao);
        _repositorioMock.Verify(repo => repo.Update(ref It.Ref<Despesa>.IsAny), Times.Once);
    }

    [Fact]
    public void Delete_Should_Returns_True()
    {
        // Arrange
        var despesa = DespesaFaker.Instance.Despesas().First();
        _repositorioMock.Setup(repo => repo.Delete(It.IsAny<Despesa>())).Returns(true);
        _repositorioMock.Setup(repo => repo.Get(It.IsAny<int>())).Returns(despesa);
        var despesaDto = _mapper.Map<DespesaDto>(despesa);
        
        // Act
        var result = _despesaBusiness.Delete(despesaDto);

        // Assert
        Assert.IsType<bool>(result);
        Assert.True(result);
        _repositorioMock.Verify(repo => repo.Delete(It.IsAny<Despesa>()), Times.Once);
    }

    [Fact]
    public void IsCategoriaValid_Should_Throws_Exeption()
    {
        // Arrange
        var despesa = DespesaFaker.Instance.Despesas().First();
        var despesaDto = _mapper.Map<DespesaDto>(despesa);
        var categorias = CategoriaFaker.Instance.Categorias();
        _repositorioMock.Setup(repo => repo.Insert(ref It.Ref<Despesa>.IsAny)).Throws<Exception>();        
        _repositorioCategoria.Setup(repo => repo.GetAll()).Throws(new ArgumentException("Erro Categoria inexistente ou não cadastrada!"));

        // Act & Assert 
        Assert.Throws<ArgumentException>(() => _despesaBusiness.Create(despesaDto));
    }
}
