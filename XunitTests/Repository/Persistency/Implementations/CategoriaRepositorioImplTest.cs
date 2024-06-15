using __mock__.Repository;
using Repository.Persistency.Implementations.Fixtures;

namespace Repository.Persistency.Implementations;

public sealed class CategoriaRepositorioImplTest : IClassFixture<DatabaseFixture>
{
    private readonly DatabaseFixture _fixture;
    private CategoriaRepositorioImpl _repository;

    public CategoriaRepositorioImplTest(DatabaseFixture fixture)
    {
        _fixture = fixture;
        _repository = new CategoriaRepositorioImpl(_fixture.Context);
    }

    [Fact]
    public void Insert_Should_Add_Item_And_SaveChanges()
    {
        // Arrange
        var usuario = _fixture.Context.Usuario.Last();
        var newCategoria = MockCategoria.Instance.GetCategoria();


        // Act
        _repository.Insert(ref newCategoria);
        var insertedCategoria = _fixture.Context.Categoria.Find(newCategoria.Id);

        // Assert
        Assert.NotNull(insertedCategoria);
        Assert.Equal(newCategoria, insertedCategoria);
    }

    [Fact]
    public void Insert_Should_Throws_Exception_When_Try_To_Insert_Null()
    {
        // Arrange
        Categoria? newCategoria = null;

        // Act & Assert
        Assert.Throws<InvalidOperationException>(() => _repository.Insert(ref newCategoria));
    }

    [Fact]
    public void Update_Should_Update_Item_And_SaveChanges()
    {
        // Arrange
        var existingItem = _fixture.Context.Categoria.First();
        var updatedItem = new Categoria
        {
            Id = existingItem.Id,
            UsuarioId = existingItem.UsuarioId,
            Descricao = "Teste Update Descricao",
            TipoCategoria = existingItem.TipoCategoria,
            Usuario = existingItem.Usuario
        };

        // Act
        _repository.Update(ref updatedItem);
        var result = _fixture.Context.Categoria.Find(updatedItem.Id);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(existingItem.Id, result.Id);
        Assert.Equal(existingItem.UsuarioId, result.UsuarioId);
        Assert.Equal(existingItem.Descricao, result.Descricao);
        Assert.Equal(existingItem.TipoCategoria, result.TipoCategoria);
        Assert.Equal(existingItem.Usuario, result.Usuario);
    }

    [Fact]
    public void Update_Should_Throws_Exception_When_Categoria_Not_Found()
    {
        // Arrange
        var updatedItem = new Categoria { Id = 999 };

        // Act & Assert
        Assert.Throws<InvalidOperationException>(() => _repository.Update(ref updatedItem));
    }

    [Fact]
    public void GetAll_Should_Return_All_Categorias()
    {
        // Act
        var result = _repository.GetAll();

        // Assert
        Assert.NotNull(result);
        Assert.True(result.Count() > 0);
    }

    [Fact]
    public void Get_Should_Return_Categoria_By_Id()
    {
        // Arrange
        var existingItem = _fixture.Context.Categoria.First();

        // Act
        var result = _repository.Get(existingItem.Id);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(existingItem.Id, result.Id);
    }

    [Fact]
    public void Get_Should_Throws_Exception_When_Categoria_Not_Found()
    {
        // Act & Assert
        var result = _repository.Get(999);
        Assert.Null(result);
    }

    [Fact]
    public void Find_Should_Return_Categoria_By_Expression()
    {
        // Arrange
        var tipoCategoria = _fixture.Context.TipoCategoria.First();
        var expected = _fixture.Context.Categoria.Where(c => c.TipoCategoria.Id == tipoCategoria.Id);

        // Act
        var result = _repository.Find(c => c.TipoCategoria.Id == tipoCategoria.Id);

        // Assert
        Assert.NotNull(result);
        Assert.True(result.Count() > 0);
    }
}
