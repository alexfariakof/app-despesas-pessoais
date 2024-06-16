using __mock__.Repository;
using Domain.Entities.ValueObjects;
using Repository.Persistency.Implementations.Fixtures;

namespace Repository.Persistency.Implementations;
public sealed class DespesaRepositorioImplTest : IClassFixture<DespesaFixture>
{
    private readonly DespesaFixture _fixture;
    private DespesaRepositorioImpl _repository;

    public DespesaRepositorioImplTest(DespesaFixture fixture)
    {
        _fixture = fixture;
        _repository = new DespesaRepositorioImpl(_fixture.Context);
    }

    [Fact]
    public void Insert_Should_Add_Item_And_SaveChanges()
    {
        // Arrange
        var categoria = _fixture.Context.Categoria.Last();
        var usuario = _fixture.Context.Usuario.Last();
        var newDespesa = MockDespesa.Instance.GetDespesa();
        newDespesa.CategoriaId = _fixture.Context.Categoria.First().Id;
        newDespesa.Categoria = null;
        newDespesa.Id = 0;

        // Act
        _repository.Insert(ref newDespesa);

        // Assert
        Assert.NotNull(newDespesa);
        Assert.NotEqual(0, newDespesa.Id);
    }

    [Fact]
    public void Insert_Should_Throw_Exception_When_Try_To_Insert_Null()
    {
        // Arrange
        Despesa? newDespesa = null;

        // Act & Assert
        Assert.Throws<NullReferenceException>(() => _repository.Insert(ref newDespesa));
    }

    [Fact]
    public void Update_Should_Update_Item_And_SaveChanges()
    {
        // Arrange
        var existingItem = _fixture.Context.Despesa.First();
        var updatedItem = new Despesa
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
        var result = _fixture.Context.Despesa.Find(updatedItem.Id);

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
    public void Update_Should_Throw_Exception_When_Despesa_Not_Found()
    {
        // Arrange
        var updatedItem = new Despesa { Id = 999 };

        // Act & Assert
        Assert.Throws<InvalidOperationException>(() => _repository.Update(ref updatedItem));
    }

    [Fact]
    public void GetAll_Should_Return_All_Despesas()
    {
        // Act
        var result = _repository.GetAll();

        // Assert
        Assert.NotNull(result);
        Assert.True(result.Count > 0);
    }

    [Fact]
    public void Get_Should_Return_Despesa_By_Id()
    {
        // Arrange
        var existingItem = _fixture.Context.Despesa.LastOrDefault(d => d.Categoria.TipoCategoria == (int)TipoCategoria.CategoriaType.Despesa);

        // Act
        var result = _repository.Get(existingItem.Id);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(existingItem.Id, result.Id);
    }

    [Fact]
    public void Get_Should_Throw_Exception_When_Despesa_Not_Found()
    {
        // Act & Assert
        var result = _repository.Get(999);
        Assert.Null(result);
    }

    [Fact]
    public void Find_Should_Return_Despesa_By_Expression()
    {
        // Arrange
        var categoria = _fixture.Context.Categoria.FirstOrDefault(c => c.TipoCategoria == (int)TipoCategoria.CategoriaType.Despesa);
        var expected = _fixture.Context.Despesa.Where(d => d.CategoriaId == categoria.Id);

        // Act
        var result = _repository.Find(d => d.CategoriaId == categoria.Id);

        // Assert
        Assert.NotNull(result);
        Assert.True(result.Count() > 0);
    }
}
