using __mock__.Repository;
using Domain.Entities.ValueObjects;
using Repository.Persistency.Implementations.Fixtures;

namespace Repository.Persistency.Implementations;

public sealed class ReceitaRepositorioImplTest : IClassFixture<ReceitaFixture>
{
    private readonly ReceitaFixture _fixture;
    private ReceitaRepositorioImpl _repository;

    public ReceitaRepositorioImplTest(ReceitaFixture fixture)
    {
        _fixture = fixture;
        _repository = new ReceitaRepositorioImpl(_fixture.Context);
    }

    [Fact]
    public void Insert_Should_Add_Item_And_SaveChanges()
    {
        // Arrange
        var categoria = _fixture.Context.Categoria.Last();
        var usuario = _fixture.Context.Usuario.Last();
        var newReceita = MockReceita.Instance.GetReceita();
        newReceita.CategoriaId = _fixture.Context.Categoria.First().Id;
        newReceita.Categoria = null;
        newReceita.Id = Guid.Empty;

        // Act
        _repository.Insert(ref newReceita);

        // Assert
        Assert.NotNull(newReceita);
        Assert.NotEqual(Guid.Empty, newReceita.Id);
    }

    [Fact]
    public void Insert_Should_Throw_Exception_When_Try_To_Insert_Null()
    {
        // Arrange
        Receita? newReceita = null;

        // Act & Assert
        Assert.Throws<NullReferenceException>(() => _repository.Insert(ref newReceita));
    }

    [Fact]
    public void Update_Should_Update_Item_And_SaveChanges()
    {
        // Arrange
        var existingItem = _fixture.Context.Receita.First();
        var updatedItem = new Receita
        {
            Id = existingItem.Id,
            CategoriaId = existingItem.CategoriaId,
            Descricao = "Teste Update Descricao",
            Valor = existingItem.Valor,
            Categoria = existingItem.Categoria,
            Usuario = existingItem.Usuario
        };

        // Act
        _repository.Update(ref updatedItem);
        var result = _fixture.Context.Receita.Find(updatedItem.Id);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(existingItem.Id, result.Id);
        Assert.Equal(existingItem.CategoriaId, result.CategoriaId);
        Assert.Equal(existingItem.Descricao, result.Descricao);
        Assert.Equal(existingItem.Valor, result.Valor);
        Assert.Equal(existingItem.Categoria, result.Categoria);
        Assert.Equal(existingItem.Usuario, result.Usuario);
    }

    [Fact]
    public void Update_Should_Throw_Exception_When_Receita_Not_Found()
    {
        // Arrange
        var updatedItem = new Receita { Id = Guid.NewGuid() };

        // Act & Assert
        Assert.Throws<InvalidOperationException>(() => _repository.Update(ref updatedItem));
    }

    [Fact]
    public void GetAll_Should_Return_All_Receitas()
    {
        // Act
        var result = _repository.GetAll();

        // Assert
        Assert.NotNull(result);
        Assert.True(result.Count > 0);
    }

    [Fact]
    public void Get_Should_Return_Receita_By_Id()
    {
        // Arrange
        var existingItem = _fixture.Context.Receita.LastOrDefault(r => r.Categoria.TipoCategoria == (int)TipoCategoria.CategoriaType.Receita);

        // Act
        var result = _repository.Get(existingItem.Id);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(existingItem.Id, result.Id);
    }

    [Fact]
    public void Get_Should_Throw_Exception_When_Receita_Not_Found()
    {
        // Act & Assert
        var result = _repository.Get(Guid.NewGuid());
        Assert.Null(result);
    }

    [Fact]
    public void Find_Should_Return_Receita_By_Expression()
    {
        // Arrange
        var categoria = _fixture.Context.Categoria.FirstOrDefault(r => r.TipoCategoria == (int)TipoCategoria.CategoriaType.Receita);
        var expected = _fixture.Context.Receita.Where(r => r.CategoriaId == categoria.Id).ToList();

        // Act
        var result = _repository.Find(r => r.CategoriaId == categoria.Id);

        // Assert
        Assert.NotNull(result);
        Assert.True(result.Count() > 0);
    }    
}
