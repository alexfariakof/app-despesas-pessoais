using Fakers.v1;

namespace Repository.Persistency.Generic;
public sealed class GenericRepositorioTest
{
    private Mock<RegisterContext> _dbContextMock;
    public GenericRepositorioTest()
    {
        // Arrange
        var options = new DbContextOptionsBuilder<RegisterContext>().UseInMemoryDatabase(databaseName: "GenericRepositorioTest").Options;
        _dbContextMock = new Mock<RegisterContext>(options);
        _dbContextMock.Setup(c => c.Set<List<Categoria>>());
    }

    [Fact]
    public void Insert_Should_Add_Item_And_SaveChanges()
    {
        // Arrange
        var item = new Categoria();
        var repository = new GenericRepositorio<Categoria>(_dbContextMock.Object);

        // Act
        repository.Insert(ref item);

        // Assert
        _dbContextMock.Verify(c => c.SaveChanges(), Times.Once);
        Assert.NotNull(item?.Id);
    }

    [Fact]
    public void GetAll_Should_Return_All_Items()
    {
        // Arrange
        var items = CategoriaFaker.Instance.Categorias();
        var dataSet = items;
        var dbSetMock = Usings.MockDbSet(dataSet);
        _dbContextMock.Setup(c => c.Set<Categoria>()).Returns(dbSetMock.Object);
        var repository = new GenericRepositorio<Categoria>(_dbContextMock.Object);

        // Act
        var result = repository.GetAll();

        // Assert
        Assert.Equal(items.Count, result.Count());
        Assert.Equal(items, result);
    }

    [Fact]
    public void Get_Should_Return_Item_With_Matching_Id()
    {
        // Arrange
        var itens = UsuarioFaker.Instance.GetNewFakersUsuarios();
        var item = itens.First();
        var itemId = item.Id;

        var dataSet = itens;
        var dbSetMock = Usings.MockDbSet(dataSet);
        _dbContextMock.Setup(c => c.Set<Usuario>()).Returns(dbSetMock.Object);
        var repository = new GenericRepositorio<Usuario>(_dbContextMock.Object);

        // Act
        var result = repository.Get(itemId);

        // Assert
        Assert.NotNull(result);
        Assert.IsType<Usuario>(result);
        Assert.Equal(item, result);
    }

    [Fact]
    public void Update_Should_Update_Item_And_SaveChanges()
    {
        // Arrange
        var dataSet = CategoriaFaker.Instance.Categorias();
        var existingItem = dataSet.First();
        var dbContext = new RegisterContext(new DbContextOptionsBuilder<RegisterContext>().UseInMemoryDatabase(databaseName: "Update_Should_Update_Item_And_SaveChanges").Options);

        var repository = new GenericRepositorio<Categoria>(dbContext);

        // Act
        repository.Insert(ref existingItem);
        var updatedItem = existingItem;
        updatedItem.Descricao = "Teste Update Item";
        repository.Update(ref updatedItem);
        var result = updatedItem;

        // Assert
        Assert.NotNull(result);
        Assert.Equal(existingItem, result);
        Assert.Equal(updatedItem, result);
    }

    /// <summary>
    /// Testa se o método Delete é executado quando o tipo do objeto é Usuário e seta como Intativo sem realizar deleção.
    /// </summary>
    [Fact]
    public void Delete_Should_Set_Inativo_And_Return_True_When_Usuario_IsDeleted()
    {
        // Arrange
        var lstUsuarios = UsuarioFaker.Instance.GetNewFakersUsuarios();
        var usuario = lstUsuarios.First();
        var options = new DbContextOptionsBuilder<RegisterContext>().UseInMemoryDatabase(databaseName: "Delete_Should_Set_Inativo_And_Return_True_When_Usuario_IsDeleted").Options;
        var _dbContextMock = new RegisterContext(options);
        _dbContextMock.Usuario.AddRange(lstUsuarios.Take(2));
        _dbContextMock.SaveChanges();
        var _repository = new GenericRepositorio<Usuario>(_dbContextMock);

        // Act
        bool result = _repository.Delete(usuario);

        // Assert
        Assert.True(result);
    }

    [Fact]
    public void Update_Should_Try_Update_Item_And_Return_Null()
    {
        // Arrange
        var dataSet = CategoriaFaker.Instance.Categorias();
        var existingItem = dataSet.First();
        var dbContext = new RegisterContext(new DbContextOptionsBuilder<RegisterContext>().UseInMemoryDatabase(databaseName: "Update_Should_Try_Update_Item_And_Return_Null").Options);
        var repository = new GenericRepositorio<Categoria>(dbContext);

        // Act &  Assert 
        var exception = Assert.Throws<Exception>(() => repository.Update(ref existingItem));
    }

    [Fact]
    public void Delete_With_Existing_Item_Should_Remove_Item_And_SaveChanges()
    {
        // Arrange
        var lstCategorias = CategoriaFaker.Instance.Categorias();
        var item = lstCategorias.Last();
        var dataSet = lstCategorias;

        var dbSetMock = Usings.MockDbSet(dataSet);
        _dbContextMock.Setup(c => c.Set<Categoria>()).Returns(dbSetMock.Object);
        var repository = new GenericRepositorio<Categoria>(_dbContextMock.Object);

        // Act
        var result = repository.Delete(item);

        // Assert
        Assert.True(result);
        _dbContextMock.Verify(c => c.SaveChanges(), Times.Once);
    }

    [Fact]
    public void Delete_With_Non_Existing_Item_Should_Not_Remove_Item_And_Return_False()
    {
        // Arrange
        var dataSet = CategoriaFaker.Instance.Categorias();
        var item = new Categoria { Id = 0 };
        var dbSetMock = Usings.MockDbSet(dataSet);
        _dbContextMock.Setup(c => c.Set<Categoria>()).Returns(dbSetMock.Object);
        var repository = new GenericRepositorio<Categoria>(_dbContextMock.Object);

        // Act
        var result = repository.Delete(item);

        // Assert            
        Assert.False(result);
        _dbContextMock.Verify(c => c.SaveChanges(), Times.Never);
    }

    [Fact]
    public void Delete_Should_Throw_Exception()
    {
        // Arrange
        var lstCategorias = CategoriaFaker.Instance.Categorias();
        var item = lstCategorias.Last();
        var dataSet = lstCategorias;

        var dbSetMock = Usings.MockDbSet(dataSet);

        _dbContextMock.Setup(c => c.Set<Categoria>()).Returns(dbSetMock.Object);
        _dbContextMock.Setup(c => c.Remove(It.IsAny<Categoria>())).Throws<Exception>();
        var repository = new GenericRepositorio<Categoria>(_dbContextMock.Object);

        // Act and Assert
        var exception = Assert.Throws<Exception>(() => repository.Delete(item));
        Assert.Equal("GenericRepositorio_Delete", exception.Message);
    }
}