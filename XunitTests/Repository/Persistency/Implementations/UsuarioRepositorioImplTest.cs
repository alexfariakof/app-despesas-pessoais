using Domain.Entities.ValueObjects;
using __mock__.Repository;
using Repository.Persistency.Implementations.Fixtures;

namespace Repository.Persistency.Implementations;

public sealed class UsuarioRepositorioImplTest : IClassFixture<DatabaseFixture>
{
    private readonly DatabaseFixture _fixture;
    private UsuarioRepositorioImpl _repository;

    public UsuarioRepositorioImplTest(DatabaseFixture fixture)
    {
        _fixture = fixture;
        _repository = new UsuarioRepositorioImpl(_fixture.Context);
    }

    [Fact]
    public void Insert_Should_Add_Item_And_SaveChanges()
    {
        // Arrange
        var newUser = MockUsuario.Instance.GetUsuario();

        // Act
        _repository.Insert(ref newUser);
        var insertedUser = _fixture.Context.Usuario.Find(newUser.Id);

        // Assert
        Assert.NotNull(insertedUser);
        Assert.Equal(newUser, insertedUser);
    }

    [Fact]
    public void Insert_Should_Throws_Exception_When_Try_To_Insert_Null()
    {
        // Arrange
        Usuario? newUser = null;

        // Act & Assert
        Assert.Throws<NullReferenceException>(() => _repository.Insert(ref newUser));
    }

    [Fact]
    public void Update_Should_Update_Item_And_SaveChanges()
    {
        // Arrange
        var existingItem = _fixture.Context.Usuario.First(u => u.PerfilUsuario.Id == 2);
        var updatedItem = new Usuario
        {
            Id = existingItem.Id,
            Nome = "Teste Update Item",
            Email = "Teste@teste.com",
            SobreNome = existingItem.SobreNome,
            PerfilUsuario = new PerfilUsuario(PerfilUsuario.Perfil.Admin),
            StatusUsuario = StatusUsuario.Ativo,
            Telefone = existingItem.Telefone
        };

        // Act
        _repository.Update(ref updatedItem);
        var result = _repository.Get(updatedItem.Id);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(updatedItem.Id, result.Id);
        Assert.Equal(updatedItem.Nome, result.Nome);
        Assert.Equal(updatedItem.Email, result.Email);
        Assert.Equal(updatedItem.SobreNome, result.SobreNome);
        Assert.Equal(updatedItem.PerfilUsuario, result.PerfilUsuario);
        Assert.Equal(updatedItem.StatusUsuario, result.StatusUsuario);
        Assert.Equal(updatedItem.Telefone, result.Telefone);
    }

    [Fact]
    public void Update_Should_Throws_Exception_When_User_Not_Found()
    {
        // Arrange
        var updatedItem = new Usuario { Id = Guid.NewGuid() };

        // Act & Assert
        Assert.Throws<InvalidOperationException>(() => _repository.Update(ref updatedItem));
    }

    [Fact]
    public void Delete_With_Existing_Item_Should_Set_Status_Inativo_And_SaveChanges()
    {
        // Arrange
        var existingItem = _fixture.Context.Usuario.First();

        // Act
        var result = _repository.Delete(existingItem);
        var deletedItem = _fixture.Context.Usuario.Find(existingItem.Id);

        // Assert
        Assert.True(result);
        Assert.NotNull(deletedItem);
        Assert.Equal(StatusUsuario.Inativo, deletedItem.StatusUsuario);
    }

    [Fact]
    public void Delete_With_Non_Existing_Item_Should_Return_False()
    {
        // Arrange
        var entity = new Usuario { Id = Guid.NewGuid() };

        // Act
        var result = _repository.Delete(entity);

        // Assert
        Assert.False(result);
    }

    [Fact]
    public void GetAll_Should_Return_All_Users()
    {
        // Act
        var result = _repository.GetAll();

        // Assert
        Assert.NotNull(result);
        Assert.Equal(_fixture.Context.Usuario.ToList(), result);
    }

    [Fact]
    public void Get_Should_Return_User_By_Id()
    {
        // Arrange
        var existingItem = _fixture.Context.Usuario.First();

        // Act
        var result = _repository.Get(existingItem.Id);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(existingItem, result);
    }

    [Fact]
    public void Get_Should_Throws_Exception_When_User_Not_Found()
    {
        // Act & Assert
        Assert.Throws<InvalidOperationException>(() => _repository.Get(Guid.NewGuid()));
    }

    [Fact]
    public void Exists_Should_Return_True_When_User_Exists()
    {
        // Arrange
        var existingItem = _fixture.Context.Usuario.First();

        // Act
        var result = _repository.Exists(existingItem.Id);

        // Assert
        Assert.True(result);
    }

    [Fact]
    public void Exists_Should_Return_False_When_User_Not_Exists()
    {
        // Act
        var result = _repository.Exists(Guid.NewGuid());

        // Assert
        Assert.False(result);
    }
}
