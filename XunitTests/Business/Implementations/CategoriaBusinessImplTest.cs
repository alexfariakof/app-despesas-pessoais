using AutoMapper;
using Business.Dtos.v2;
using MediatR;
using Repository.Persistency.UnitOfWork.Abstractions;
using Fakers.v2;
using Business.Dtos.Core.Profile;
using System.Linq.Expressions;

namespace Business;

public class CategoriaBusinessImplTest
{
    private readonly Mock<IUnitOfWork<Categoria>> _unitOfWorkMock;
    private readonly Mock<IMediator> _mediator;
    private readonly Mock<IRepositorio<Categoria>> _repositorioMock;
    private readonly CategoriaBusinessImpl<CategoriaDto> _categoriaBusiness;
    private readonly List<Categoria> _categorias;
    private Mapper _mapper;

    public CategoriaBusinessImplTest()
    {
        var usuario = UsuarioFaker.Instance.GetNewFaker();
        _categorias = CategoriaFaker.Instance.Categorias(usuario);
        _repositorioMock = Usings.MockRepositorio(_categorias);
        _unitOfWorkMock  = new Mock<IUnitOfWork<Categoria>>(MockBehavior.Default);
        _mediator = new Mock<IMediator>(MockBehavior.Default);
        _mapper = new Mapper(new MapperConfiguration(cfg => { cfg.AddProfile<CategoriaProfile>(); }));
        _categoriaBusiness = new CategoriaBusinessImpl<CategoriaDto>(_mediator.Object, _mapper, _unitOfWorkMock.Object, _repositorioMock.Object);
    }

    [Fact]
    public void Create_Shloud_Returns_Parsed_CategoriaDto()
    {
        // Arrange
        var categoria = _categorias.First();
        var categoriaDto = _mapper.Map<CategoriaDto>(categoria);
        _repositorioMock.Setup(repo => repo.Insert(ref It.Ref<Categoria>.IsAny));
        _unitOfWorkMock.Setup(repo => repo.Repository.Insert(It.IsAny<Categoria>()));

        // Act
        var result = _categoriaBusiness.Create(categoriaDto);

        // Assert
        Assert.NotNull(result);
        Assert.IsType<CategoriaDto>(result);
        Assert.Equal(categoriaDto.Id, result.Id);
        _unitOfWorkMock.Verify(repo => repo.Repository.Insert(It.IsAny<Categoria>()), Times.Never);
        _repositorioMock.Verify(repo => repo.Insert(ref It.Ref<Categoria>.IsAny), Times.Once);
    }

    [Fact]
    public void FindAll_Should_Returns_List_Of_CategoriaDto()
    {
        // Arrange
        var categoria = _categorias.First();
        var mockCategorias = _categorias.FindAll(obj => obj.UsuarioId == categoria.UsuarioId);
        _repositorioMock.Setup(repo => repo.GetAll()).Returns(() => mockCategorias);
        _unitOfWorkMock.Setup(repo => repo.Repository.GetAll()).Returns(async () => mockCategorias);

        // Act
        var result = _categoriaBusiness.FindAll(categoria.UsuarioId);

        // Assert
        Assert.NotNull(result);
        Assert.IsType<List<CategoriaDto>>(result);
        Assert.Equal(mockCategorias.Count, result.Count);
        _unitOfWorkMock.Verify(repo => repo.Repository.GetAll(), Times.Never);
        _repositorioMock.Verify(repo => repo.GetAll(), Times.Once);
    }

    [Fact]
    public void FindById_Should_Returns_Parsed_CategoriaDto()
    {
        // Arrange
        var categoria = _categorias.First();
        _repositorioMock.Setup(repo => repo.Find(It.IsAny<Expression<Func<Categoria, bool>>>())).Returns(_categorias.AsEnumerable());
        _unitOfWorkMock.Setup(repo => repo.Repository.GetById(It.IsAny<int>())).Returns(async () => categoria);

        // Act
        var result = _categoriaBusiness.FindById(categoria.Id, categoria.UsuarioId);

        // Assert
        Assert.NotNull(result);
        Assert.IsType<CategoriaDto>(result);
        Assert.Equal(categoria.Id, result.Id);
        _repositorioMock.Verify(repo => repo.Find(It.IsAny<Expression<Func<Categoria, bool>>>()), Times.Once);
        _unitOfWorkMock.Verify(repo => repo.Repository.GetById(It.IsAny<int>()), Times.Never);
    }

    [Fact]
    public void FindById_Should_Returns_Empty_Categoria()
    {
        // Arrange

        var categoria = _categorias.First();
        _repositorioMock.Setup(repo => repo.Find(It.IsAny<Expression<Func<Categoria, bool>>>())).Returns(Enumerable.Empty<Categoria>());
        _unitOfWorkMock.Setup(repo => repo.Repository.Find(It.IsAny<Expression<Func<Categoria, bool>>>())).Returns(async () => Enumerable.Empty<Categoria>());

        // Act
        var result = _categoriaBusiness.FindById(0, categoria.UsuarioId);

        // Assert
        Assert.Null(result);
        _repositorioMock.Verify(repo => repo.Find(It.IsAny<Expression<Func<Categoria, bool>>>()), Times.Once);
        _unitOfWorkMock.Verify(repo => repo.Repository.Find(It.IsAny<Expression<Func<Categoria, bool>>>()), Times.Never);
    }


    [Fact]
    public void Update_Should_Returns_Parsed_CategoriaDto()
    {
        // Arrange
        var categoria = CategoriaFaker.Instance.GetNewFaker(UsuarioFaker.Instance.GetNewFaker());
        var categoriaDto = _mapper.Map<CategoriaDto>(categoria);        
        _repositorioMock.Setup(repo => repo.Get(It.IsAny<int>())).Returns(categoria);
        _repositorioMock.Setup(repo => repo.Update(ref It.Ref<Categoria>.IsAny));
        _unitOfWorkMock.Setup(repo => repo.Repository.Update(It.IsAny<Categoria>()));        
        
        // Act
        var result = _categoriaBusiness.Update(categoriaDto) as CategoriaDto;

        // Assert
        Assert.NotNull(result);
        Assert.IsType<CategoriaDto>(result);
        Assert.Equal(categoria.Id, result.Id);
        _unitOfWorkMock.Verify(repo => repo.Repository.Update(It.IsAny<Categoria>()), Times.Never);
        _repositorioMock.Verify(repo => repo.Update(ref It.Ref<Categoria>.IsAny), Times.Once);
        _repositorioMock.Verify(repo => repo.Get(It.IsAny<int>()), Times.Once);
    }

    [Fact]
    public void Delete_Should_Returns_True()
    {
        // Arrange
        var categoria = _categorias.First();
        var objToDelete =  _mapper.Map<CategoriaDto>(categoria);
        _repositorioMock.Setup(repo => repo.Delete(It.IsAny<Categoria>()));
        _unitOfWorkMock.Setup(repo => repo.Repository.Delete(It.IsAny<int>()));

        // Act
        var result = _categoriaBusiness.Delete(objToDelete);

        // Assert
        Assert.True(result);
        _unitOfWorkMock.Verify(repo => repo.Repository.Delete(It.IsAny<int>()), Times.Never);
        _repositorioMock.Verify(repo => repo.Delete(It.IsAny<Categoria>()), Times.Once);
    }
}
