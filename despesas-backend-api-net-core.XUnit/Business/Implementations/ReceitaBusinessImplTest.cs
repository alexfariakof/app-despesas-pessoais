using Business.Dtos.Parser;

namespace Business;

public class ReceitaBusinessImplTest
{
    private readonly Mock<IRepositorio<Receita>> _repositorioMock;
    private readonly Mock<IRepositorio<Categoria>> _repositorioMockCategoria;
    private readonly ReceitaBusinessImpl _receitaBusiness;
    public ReceitaBusinessImplTest()
    {
        _repositorioMock = new Mock<IRepositorio<Receita>>();
        _repositorioMockCategoria = new Mock<IRepositorio<Categoria>>();
        _receitaBusiness = new ReceitaBusinessImpl(_repositorioMock.Object, _repositorioMockCategoria.Object);
    }

    [Fact]
    public void Create_Should_Returns_Parsed_ReceitaVM()
    {
        // Arrange
        var receita = ReceitaFaker.Instance.Receitas().First();
        var receitaVM = new ReceitaParser().Parse(receita);

        _repositorioMock.Setup(repo => repo.Insert(ref It.Ref<Receita>.IsAny));
        _repositorioMockCategoria.Setup(repo => repo.GetAll()).Returns(CategoriaFaker.Instance.Categorias(receita.Usuario, TipoCategoria.Receita, receita.UsuarioId));
        // Act
        var result = _receitaBusiness.Create(receitaVM);

        // Assert
        Assert.NotNull(result);
        Assert.IsType<ReceitaVM>(result);
        Assert.Equal(receitaVM.Id, result.Id);
        _repositorioMock.Verify(repo => repo.Insert(ref It.Ref<Receita>.IsAny), Times.Once());
    }

    [Fact]
    public void FindAll_Should_Returns_List_Of_ReceitaVM()
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
        Assert.IsType<List<ReceitaVM>>(result);
        Assert.Equal(receitas.Count, result.Count);
        _repositorioMock.Verify(repo => repo.GetAll(), Times.Once);
    }

    [Fact]
    public void FindById_Should_Returns_Parsed_ReceitaVM()
    {
        // Arrange
        var id = ReceitaFaker.Instance.ReceitasVMs().First().Id;
        var receita = ReceitaFaker.Instance.Receitas().First();

        _repositorioMock.Setup(repo => repo.Get(id)).Returns(receita);

        // Act
        var result = _receitaBusiness.FindById(id, receita.UsuarioId);

        // Assert
        Assert.NotNull(result);
        Assert.IsType<ReceitaVM>(result);
        Assert.Equal(receita.Id, result.Id);
        _repositorioMock.Verify(repo => repo.Get(id), Times.Once);
    }

    [Fact]
    public void FindById_Should_Returns_Null_When_Parsed_ReceitaVM()
    {
        // Arrange
        var id = 0;
        var receita = ReceitaFaker.Instance.Receitas()[0];

        _repositorioMock.Setup(repo => repo.Get(id)).Returns(receita);

        // Act
        var result = _receitaBusiness.FindById(id, 0);

        // Assert
        Assert.Null(result);
        _repositorioMock.Verify(repo => repo.Get(id), Times.Once);
    }

    [Fact]
    public void Update_Should_Returns_Parsed_ReceitaVM()
    {
        // Arrange
        var receita = ReceitaFaker.Instance.Receitas().First();
        var receitaVM = new ReceitaParser().Parse(receita);            

        _repositorioMock.Setup(repo => repo.Update(ref It.Ref<Receita>.IsAny));
        var categorias = CategoriaFaker.Instance.Categorias(receita.Usuario, TipoCategoria.Despesa, receita.UsuarioId);
        _repositorioMockCategoria.Setup(repo => repo.GetAll()).Returns(categorias);

        // Act
        var result = _receitaBusiness.Update(receitaVM);

        // Assert
        Assert.NotNull(result);
        Assert.IsType<ReceitaVM>(result);
        Assert.Equal(receita.Id, result.Id);
        _repositorioMock.Verify(repo => repo.Update(ref It.Ref<Receita>.IsAny), Times.Once);
    }

    [Fact]
    public void Delete_Should_Returns_True()
    {
        // Arrange
        var receita = ReceitaFaker.Instance.Receitas().First();
        var receitaVM = new ReceitaParser().Parse(receita);
        _repositorioMock.Setup(repo => repo.Delete(It.IsAny<Receita>())).Returns(true);
        
        // Act
        var result = _receitaBusiness.Delete(receitaVM);

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
        var receitaVM = new ReceitaParser().Parse(receita);

        _repositorioMock.Setup(repo => repo.Insert(ref It.Ref<Receita>.IsAny)).Throws(() => new ArgumentException("Erro InvalidCategorie"));
        var categorias = CategoriaFaker.Instance.Categorias();
        _repositorioMockCategoria.Setup(repo => repo.GetAll()).Returns(categorias);

        // Act & Assert 
        Assert.Throws<ArgumentException>(() => _receitaBusiness.Create(receitaVM));
    }
}