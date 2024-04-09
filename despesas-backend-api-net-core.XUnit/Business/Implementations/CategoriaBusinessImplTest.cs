using Xunit.Extensions.Ordering;

namespace Business;

[Order(101)]
public class CategoriaBusinessImplTest
{
    private readonly Mock<IRepositorio<Categoria>> _repositorioMock;
    private readonly CategoriaBusinessImpl _categoriaBusiness;
    private readonly List<Categoria> _categorias;

    public CategoriaBusinessImplTest()
    {
        var usuario = UsuarioFaker.GetNewFaker();
        _categorias = CategoriaFaker.Categorias(usuario);
        _repositorioMock = Usings.MockRepositorio(_categorias);
        _categoriaBusiness = new CategoriaBusinessImpl(_repositorioMock.Object);
    }

    [Fact]
    public void Create_Shloud_Returns_Parsed_CategoriaVM()
    {
        // Arrange
        var categoria = _categorias.First();
        var categoriaVM = new CategoriaMap().Parse(categoria);

        _repositorioMock.Setup(repo => repo.Insert(ref It.Ref<Categoria>.IsAny));

        // Act
        var result = _categoriaBusiness.Create(categoriaVM);

        // Assert
        Assert.NotNull(result);
        Assert.IsType<CategoriaVM>(result);
        Assert.Equal(categoriaVM.Id, result.Id);
        _repositorioMock.Verify(repo => repo.Insert(ref It.Ref<Categoria>.IsAny), Times.Once);
    }

    [Fact]
    public void FindAll_Should_Returns_List_Of_CategoriaVM()
    {
        // Arrange
        var categoria = _categorias.First();
        var mockCategorias = _categorias.FindAll(obj => obj.UsuarioId == categoria.UsuarioId);
        _repositorioMock.Setup(repo => repo.GetAll()).Returns(mockCategorias);

        // Act
        var result = _categoriaBusiness.FindAll(categoria.UsuarioId);

        // Assert
        Assert.NotNull(result);
        Assert.IsType<List<CategoriaVM>>(result);
        Assert.Equal(mockCategorias.Count, result.Count);
        _repositorioMock.Verify(repo => repo.GetAll(), Times.Once);
    }

    [Fact]
    public void FindById_Should_Returns_Parsed_CategoriaVM()
    {
        // Arrange
        var categoria = _categorias.First();
        var id = categoria.Id;
        var idUsuario = categoria.UsuarioId;

        _repositorioMock.Setup(repo => repo.Get(id)).Returns(categoria);

        // Act
        var result = _categoriaBusiness.FindById(id, idUsuario);

        // Assert
        Assert.NotNull(result);
        Assert.IsType<CategoriaVM>(result);
        Assert.Equal(categoria.Id, result.Id);
        _repositorioMock.Verify(repo => repo.Get(id), Times.Once);
    }

    [Fact]
    public void FindById_Should_Returns_Null()
    {
        // Arrange
        var categoria = _categorias.First();
        var id = categoria.Id;
        var idUsuario = categoria.UsuarioId;

        _repositorioMock.Setup(repo => repo.Get(id)).Returns((Categoria)null);

        // Act
        var result = _categoriaBusiness.FindById(0, idUsuario);

        // Assert
        Assert.Null(result);
        _repositorioMock.Verify(repo => repo.Get(id), Times.Never);
    }

    [Fact]
    public void Update_Should_Returns_Parsed_CategoriaVM()
    {
        // Arrange

        var categoriaVM = CategoriaFaker.GetNewFakerVM(null);

        var categoria = new CategoriaMap().Parse(categoriaVM);

        _repositorioMock.Setup(repo => repo.Update(ref It.Ref<Categoria>.IsAny));

        // Act
        var result = _categoriaBusiness.Update(categoriaVM) as CategoriaVM;

        // Assert
        Assert.NotNull(result);
        Assert.IsType<CategoriaVM>(result);
        Assert.Equal(categoria.Id, result.Id);
        _repositorioMock.Verify(repo => repo.Update(ref It.Ref<Categoria>.IsAny), Times.Once);
    }

    [Fact]
    public void Delete_Should_Returns_True()
    {
        // Arrange
        var categoria = _categorias.First();
        var objToDelete = new CategoriaMap().Parse(categoria);

        _repositorioMock.Setup(repo => repo.Delete(It.IsAny<Categoria>())).Returns(true);

        // Act
        var result = _categoriaBusiness.Delete(objToDelete);

        // Assert
        Assert.True(result);
        _repositorioMock.Verify(repo => repo.Delete(It.IsAny<Categoria>()), Times.Once);
    }
}
