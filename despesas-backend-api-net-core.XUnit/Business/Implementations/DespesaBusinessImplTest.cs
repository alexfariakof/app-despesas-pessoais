using Xunit.Extensions.Ordering;

namespace Business;

[Order(103)]
public class DespesaBusinessImplTest
{
    private readonly Mock<IRepositorio<Despesa>> _repositorioMock;
    private readonly Mock<IRepositorio<Categoria>> _repositorioCategoria;
    private readonly DespesaBusinessImpl _despesaBusiness;

    public DespesaBusinessImplTest()
    {
        _repositorioMock = new Mock<IRepositorio<Despesa>>();
        _repositorioCategoria = new Mock<IRepositorio<Categoria>>();
        _despesaBusiness = new DespesaBusinessImpl(_repositorioMock.Object, _repositorioCategoria.Object);
    }

    [Fact]
    public void Create_Should_Returns_Parsed_Despesa_VM()
    {
        // Arrange
        var despesa = DespesaFaker.Despesas().First();
        var despesaVM = new DespesaMap().Parse(despesa);

        _repositorioMock.Setup(repo => repo.Insert(ref It.Ref<Despesa>.IsAny));
        var categorias = CategoriaFaker.Categorias(despesa.Usuario, TipoCategoria.Despesa, despesa.UsuarioId);
        categorias.Add(despesa.Categoria);
        _repositorioCategoria.Setup(repo => repo.GetAll()).Returns(categorias);
        
        // Act
        var result = _despesaBusiness.Create(despesaVM);

        // Assert
        Assert.NotNull(result);
        Assert.IsType<DespesaVM>(result);
        Assert.Equal(despesaVM.Id, result.Id);
        _repositorioMock.Verify(repo => repo.Insert(ref It.Ref<Despesa>.IsAny), Times.Once);
    }

    [Fact]
    public void FindAll_Should_Returns_List_Of_DespesaVM()
    {
        // Arrange                     
        var despesas = DespesaFaker.Despesas();
        var despesa = despesas.First();
        var idUsuario = despesa.UsuarioId;
        despesas = despesas.FindAll(d => d.UsuarioId == idUsuario);
        _repositorioMock.Setup(repo => repo.GetAll()).Returns(despesas);

        // Act
        var result = _despesaBusiness.FindAll(idUsuario);

        // Assert
        Assert.NotNull(result);
        Assert.IsType<List<DespesaVM>>(result);
        Assert.Equal(despesas.Count, result.Count);
        _repositorioMock.Verify(repo => repo.GetAll(), Times.Once);
    }

    [Fact]
    public void FindById_Should_Returns_Parsed_DespesaVM()
    {
        // Arrange
        var despesa = DespesaFaker.Despesas().First();
        var id = despesa.Id;

        _repositorioMock.Setup(repo => repo.Get(id)).Returns(despesa);

        // Act
        var result = _despesaBusiness.FindById(id, despesa.UsuarioId);

        // Assert
        Assert.NotNull(result);
        Assert.IsType<DespesaVM>(result);
        Assert.Equal(despesa.Id, result.Id);
        _repositorioMock.Verify(repo => repo.Get(id), Times.Once);
    }

    [Fact]
    public void FindById_Should_Returns_Null_When_Parsed_DespesaVM()
    {
        // Arrange
        var despesa = DespesaFaker.Despesas().First();
        var id = despesa.Id;

        _repositorioMock.Setup(repo => repo.Get(id)).Returns(despesa);

        // Act
        var result = _despesaBusiness.FindById(id, 0);

        // Assert
        Assert.Null(result);
        _repositorioMock.Verify(repo => repo.Get(id), Times.Once);
    }

    [Fact]
    public void Update_Should_Returns_Parsed_DespesaVM()
    {
        // Arrange         
        var despesa = DespesaFaker.Despesas().First();
        despesa.Descricao = "Teste Update Despesa";
        var despesaVM = new DespesaMap().Parse(despesa);        

        _repositorioMock.Setup(repo => repo.Update(ref It.Ref<Despesa>.IsAny));
        _repositorioCategoria.Setup(repo => repo.GetAll()).Returns(CategoriaFaker.Categorias(despesa.Usuario, TipoCategoria.Despesa, despesa.UsuarioId));

        // Act
        var result = _despesaBusiness.Update(despesaVM);

        // Assert
        Assert.NotNull(result);
        Assert.IsType<DespesaVM>(result);
        Assert.Equal(despesa.Id, result.Id);
        Assert.Equal(despesa.Descricao, result.Descricao);
        _repositorioMock.Verify(repo => repo.Update(ref It.Ref<Despesa>.IsAny), Times.Once);
    }

    [Fact]
    public void Delete_Should_Returns_True()
    {
        // Arrange
        var despesa = DespesaFaker.Despesas().First();
        _repositorioMock.Setup(repo => repo.Delete(It.IsAny<Despesa>())).Returns(true);
        var despesaVM = new DespesaMap().Parse(despesa);
        
        // Act
        var result = _despesaBusiness.Delete(despesaVM);

        // Assert
        Assert.IsType<bool>(result);
        Assert.True(result);
        _repositorioMock.Verify(repo => repo.Delete(It.IsAny<Despesa>()), Times.Once);
    }

    [Fact]
    public void IsCategoriaValid_Should_Throws_Exeption()
    {
        // Arrange
        var despesa = DespesaFaker.Despesas().First();
        var despesaVM = new DespesaMap().Parse(despesa);

        _repositorioMock.Setup(repo => repo.Insert(ref It.Ref<Despesa>.IsAny));
        var categorias = CategoriaFaker.Categorias();
        _repositorioCategoria.Setup(repo => repo.GetAll()).Returns(categorias);

        // Act & Assert 
        Assert.Throws<ArgumentException>(() => _despesaBusiness.Create(despesaVM));
    }
}
