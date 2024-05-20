using AutoMapper;
using Business.Dtos.v2;
using Fakers.v2;
using Repository.Persistency.UnitOfWork.Abstractions;
using Business.Dtos.Core.Profile;

namespace Business;

public class ReceitaBusinessImplTest
{
    private readonly Mock<IRepositorio<Receita>> _repositorioMock;
    private readonly Mock<IRepositorio<Categoria>> _repositorioMockCategoria;
    private readonly ReceitaBusinessImpl<ReceitaDto> _receitaBusiness;    
    private readonly IUnitOfWork<Receita> _UnitOfWork;
    private Mapper _mapper;

    public ReceitaBusinessImplTest()
    {
        _repositorioMock = new Mock<IRepositorio<Receita>>();
        _repositorioMockCategoria = new Mock<IRepositorio<Categoria>>();
        _UnitOfWork = new Mock<IUnitOfWork<Receita>>().Object;
        _mapper = new Mapper(new MapperConfiguration(cfg => { cfg.AddProfile<ReceitaProfile>(); }));
        _receitaBusiness = new ReceitaBusinessImpl<ReceitaDto>(_mapper, _UnitOfWork, _repositorioMock.Object, _repositorioMockCategoria.Object);
    }

    [Fact]
    public void Create_Should_Returns_Parsed_ReceitaDto()
    {
        // Arrange
        var receitas = ReceitaFaker.Instance.Receitas();
        var receita = receitas.First();
        var receitaDto = _mapper.Map<ReceitaDto>(receita);
        _repositorioMock.Setup(repo => repo.Insert(ref It.Ref<Receita>.IsAny));
        _repositorioMockCategoria.Setup(repo => repo.GetAll()).Returns(receitas.Select(r => r.Categoria ?? new()).ToList());

        // Act
        var result = _receitaBusiness.Create(receitaDto);

        // Assert
        Assert.NotNull(result);
        Assert.IsType<ReceitaDto>(result);
        Assert.Equal(receitaDto.Id, result.Id);
        _repositorioMock.Verify(repo => repo.Insert(ref It.Ref<Receita>.IsAny), Times.Once());
    }

    [Fact]
    public void FindAll_Should_Returns_List_Of_ReceitaDto()
    {
        // Arrange         
        var receitas = ReceitaFaker.Instance.Receitas();
        var receita = receitas.Last();
        var idUsuario = receita.UsuarioId;
        receitas = receitas.FindAll(r => r.UsuarioId == idUsuario);
        _repositorioMock.Setup(repo => repo.GetAll()).Returns(receitas);

        // Act
        var result = _receitaBusiness.FindAll(idUsuario);

        // Assert
        Assert.NotNull(result);
        Assert.IsType<List<ReceitaDto>>(result);
        Assert.Equal(receitas.Count, result.Count);
        _repositorioMock.Verify(repo => repo.GetAll(), Times.Once);
    }

    [Fact]
    public void FindById_Should_Returns_Parsed_ReceitaDto()
    {
        // Arrange        
        var receita = ReceitaFaker.Instance.Receitas().First();
        _repositorioMock.Setup(repo => repo.Get(It.IsAny<int>())).Returns(receita);

        // Act
        var result = _receitaBusiness.FindById(receita.Id, receita.UsuarioId);

        // Assert
        Assert.NotNull(result);
        Assert.IsType<ReceitaDto>(result);
        Assert.Equal(receita.Id, result.Id);
        _repositorioMock.Verify(repo => repo.Get(It.IsAny<int>()), Times.AtLeast(2));
    }

    [Fact]
    public void FindById_Should_Returns_Null_When_Parsed_ReceitaDto()
    {
        // Arrange
        var id = 0;
        var receita = ReceitaFaker.Instance.Receitas()[0];
        _repositorioMock.Setup(repo => repo.Get(id)).Returns((Receita)null);

        // Act
        var result = _receitaBusiness.FindById(id, 0);

        // Assert
        Assert.Null(result);
        _repositorioMock.Verify(repo => repo.Get(id), Times.Once);
    }

    [Fact]
    public void Update_Should_Returns_Parsed_ReceitaDto()
    {
        // Arrange
        var receitas = ReceitaFaker.Instance.Receitas();
        var receita = receitas.First();
        var receitaDto = _mapper.Map<ReceitaDto>(receita);            
        _repositorioMock.Setup(repo => repo.Update(ref It.Ref<Receita>.IsAny));
        _repositorioMock.Setup(repo => repo.Get(It.IsAny<int>())).Returns(receita);
        _repositorioMockCategoria.Setup(repo => repo.GetAll()).Returns(receitas.Select(r => r.Categoria ?? new()).ToList());

        // Act
        var result = _receitaBusiness.Update(receitaDto);

        // Assert
        Assert.NotNull(result);
        Assert.IsType<ReceitaDto>(result);
        Assert.Equal(receita.Id, result.Id);
        _repositorioMock.Verify(repo => repo.Update(ref It.Ref<Receita>.IsAny), Times.Once);
    }

    [Fact]
    public void Delete_Should_Returns_True()
    {
        // Arrange
        var receitas = ReceitaFaker.Instance.Receitas();
        var receita = receitas.First();
        var receitaDto = _mapper.Map<ReceitaDto>(receita);
        _repositorioMock.Setup(repo => repo.Delete(It.IsAny<Receita>())).Returns(true);
        _repositorioMock.Setup(repo => repo.Get(It.IsAny<int>())).Returns(receita);
        _repositorioMockCategoria.Setup(repo => repo.GetAll()).Returns(receitas.Select(r => r.Categoria ?? new()).ToList());

        // Act
        var result = _receitaBusiness.Delete(receitaDto);

        // Assert
        Assert.IsType<bool>(result);
        Assert.True(result);
        _repositorioMock.Verify(repo => repo.Delete(It.IsAny<Receita>()), Times.Once);
    }
    [Fact]
    public void IsCategoriaValid_Should_Throws_Exeption()
    {
        // Arrange
        var receita = ReceitaFaker.Instance.Receitas().First();
        var receitaDto = _mapper.Map<ReceitaDto>(receita);

        _repositorioMock.Setup(repo => repo.Insert(ref It.Ref<Receita>.IsAny)).Throws(() => new ArgumentException("Erro InvalidCategorie"));
        var categorias = CategoriaFaker.Instance.Categorias();
        _repositorioMockCategoria.Setup(repo => repo.GetAll()).Returns(categorias);

        // Act & Assert 
        Assert.Throws<ArgumentException>(() => _receitaBusiness.Create(receitaDto));
    }
}