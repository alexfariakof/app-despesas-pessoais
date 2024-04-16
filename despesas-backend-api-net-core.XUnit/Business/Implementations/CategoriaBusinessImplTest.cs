using Business.Dtos.Parser;
using Domain.Entities.Abstractions;
using MediatR;

namespace Business;

public class CategoriaBusinessImplTest
{
    private readonly Mock<IUnitOfWork<Categoria>> _unitOfWorkMock;
    private readonly Mock<IMediator> _mediator;
    private readonly Mock<IRepositorio<Categoria>> _repositorioMock;
    private readonly CategoriaBusinessImpl _categoriaBusiness;
    private readonly List<Categoria> _categorias;

    public CategoriaBusinessImplTest()
    {
        var usuario = UsuarioFaker.Instance.GetNewFaker();
        _categorias = CategoriaFaker.Instance.Categorias(usuario);
        _repositorioMock = Usings.MockRepositorio(_categorias);
        _unitOfWorkMock  = new Mock<IUnitOfWork<Categoria>>(MockBehavior.Default);
        _mediator = new Mock<IMediator>(MockBehavior.Default);
        _categoriaBusiness = new CategoriaBusinessImpl(_mediator.Object, _unitOfWorkMock.Object);
    }

    [Fact]
    public void Create_Shloud_Returns_Parsed_CategoriaVM()
    {
        // Arrange
        var categoria = _categorias.First();
        var categoriaVM = new CategoriaParser().Parse(categoria);

        _unitOfWorkMock.Setup(repo => repo.Repository.Insert(ref It.Ref<Categoria>.IsAny));

        // Act
        var result = _categoriaBusiness.Create(categoriaVM);

        // Assert
        Assert.NotNull(result);
        Assert.IsType<CategoriaDto>(result);
        Assert.Equal(categoriaVM.Id, result.Id);
        _unitOfWorkMock.Verify(repo => repo.Repository.Insert(ref It.Ref<Categoria>.IsAny), Times.Once);
    }

    [Fact]
    public async void FindAll_Should_Returns_List_Of_CategoriaVM()
    {
        // Arrange
        var categoria = _categorias.First();
        var mockCategorias = _categorias.FindAll(obj => obj.UsuarioId == categoria.UsuarioId);
        _unitOfWorkMock.Setup(repo => repo.Repository.GetAll()).Returns(async  () => mockCategorias);

        // Act
        var result = _categoriaBusiness.FindAll(categoria.UsuarioId);

        // Assert
        Assert.NotNull(result);
        Assert.IsType<List<CategoriaDto>>(result.Result);
        Assert.Equal(mockCategorias.Count, result.Result.Count);
        _unitOfWorkMock.Verify(repo => repo.Repository.GetAll(), Times.Once);
    }

    [Fact]
    public void FindById_Should_Returns_Parsed_CategoriaVM()
    {
        // Arrange
        var categoria = _categorias.First();
        var id = categoria.Id;
        var idUsuario = categoria.UsuarioId;

        _unitOfWorkMock.Setup(repo => repo.Repository.GetById(id)).Returns(async () => categoria);

        // Act
        var result = _categoriaBusiness.FindById(id, idUsuario);

        // Assert
        Assert.NotNull(result);
        Assert.IsType<CategoriaDto>(result);
        Assert.Equal(categoria.Id, result.Id);
        _unitOfWorkMock.Verify(repo => repo.Repository.GetById(id), Times.Once);
    }

    [Fact]
    public async void FindById_Should_Returns_Null()
    {
        // Arrange
        var categoria = _categorias.First();
        var id = categoria.Id;
        var idUsuario = categoria.UsuarioId;

        _unitOfWorkMock.Setup(repo => repo.Repository.GetById(id)).Returns(async () => (Categoria)null);

        // Act
        var result = _categoriaBusiness.FindById(0, idUsuario);

        // Assert
        Assert.Null(result);
        _unitOfWorkMock.Verify(repo => repo.Repository.GetById(id), Times.Never);
    }

    [Fact]
    public void Update_Should_Returns_Parsed_CategoriaVM()
    {
        // Arrange

        var categoriaVM = CategoriaFaker.Instance.GetNewFakerVM(null);

        var categoria = new CategoriaParser().Parse(categoriaVM);

        _unitOfWorkMock.Setup(repo => repo.Repository.Update(ref It.Ref<Categoria>.IsAny));

        // Act
        var result = _categoriaBusiness.Update(categoriaVM) as CategoriaDto;

        // Assert
        Assert.NotNull(result);
        Assert.IsType<CategoriaDto>(result);
        Assert.Equal(categoria.Id, result.Id);
        _unitOfWorkMock.Verify(repo => repo.Repository.Update(ref It.Ref<Categoria>.IsAny), Times.Once);
    }

    [Fact]
    public void Delete_Should_Returns_True()
    {
        // Arrange
        var categoria = _categorias.First();
        var objToDelete = new CategoriaParser().Parse(categoria);

        _unitOfWorkMock.Setup(repo => repo.Repository.Delete(It.IsAny<int>()));

        // Act
        var result = _categoriaBusiness.Delete(objToDelete);

        // Assert
        Assert.True(result);
        _unitOfWorkMock.Verify(repo => repo.Repository.Delete(It.IsAny<int>()), Times.Once);
    }
}
