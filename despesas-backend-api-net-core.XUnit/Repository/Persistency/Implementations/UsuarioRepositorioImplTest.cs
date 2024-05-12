using Domain.Entities.ValueObjects;
using Fakers.v1;

namespace Repository.Persistency.Implementations;
public class UsuarioRepositorioImplTest
{
    private Mock<RegisterContext> _mockRegisterContext;
    private Mock<UsuarioRepositorioImpl> _mockRepository;

    public UsuarioRepositorioImplTest()
    {
        var options = new DbContextOptionsBuilder<RegisterContext>().UseInMemoryDatabase(databaseName: "UsuarioRpository").Options;
        _mockRegisterContext = new Mock<RegisterContext>(options);
        var dbSetMock = Usings.MockDbSet(UsuarioFaker.Instance.GetNewFakersUsuarios());
        var dbSetMockControleAcesso = Usings.MockDbSet(ControleAcessoFaker.Instance.ControleAcessos());
        var dbSetMockCategoria = Usings.MockDbSet(new List<Categoria>());

        _mockRegisterContext.Setup(c => c.Set<Usuario>()).Returns(dbSetMock.Object);
        _mockRegisterContext.Setup(c => c.Set<ControleAcesso>()).Returns(dbSetMockControleAcesso.Object);
        _mockRegisterContext.Setup(c => c.Set<Categoria>()).Returns(dbSetMockCategoria.Object);
        _mockRepository = new Mock<UsuarioRepositorioImpl>(_mockRegisterContext);
    }

    [Fact]
    public void Insert_Should_Add_Item_And_SaveChanges()
    {
        // Arrange
        var newUser = UsuarioFaker.Instance.GetNewFaker();
        var repository = new UsuarioRepositorioImpl(_mockRegisterContext.Object);

        // Act
        repository.Insert(ref newUser);
        var insertedUser = newUser;

        // Assert
        Assert.NotNull(insertedUser);
        Assert.Equal(newUser, insertedUser);

        // Verifique se o método SaveChanges foi chamado no contexto
        _mockRegisterContext.Verify(c => c.SaveChanges(), Times.Once);
    }

    [Fact]
    public void Insert_Should_Throws_Erro_When_Try_To_Insert_New_Usuario()
    {
        // Arrange
        var newUser = (Usuario?)null;
        var repository = new UsuarioRepositorioImpl(_mockRegisterContext.Object);

        // Act

        Action result = () => repository.Insert(ref newUser);

        // Assert
        Assert.NotNull(result);

        var exception = Assert.Throws<Exception>(() => repository.Insert(ref newUser));

        Assert.Equal("Erro ao inserir um novo usuário!", exception.Message);
        _mockRegisterContext.Verify(c => c.SaveChanges(), Times.Never);
    }

    [Fact]
    public void GetAll_Should_Throws_Exception()
    {
        // Arrange
        _mockRegisterContext.Setup(c => c.Set<Usuario>()).Returns((DbSet<Usuario>)null);
        _mockRepository = new Mock<UsuarioRepositorioImpl>(_mockRegisterContext.Object);

        // Act
        Action result = () => _mockRepository.Object.GetAll();

        // Assert
        Assert.NotNull(result);
        var exception = Assert.Throws<Exception>(() => _mockRepository.Object.GetAll());
        Assert.Equal("Erro ao gerar resgistros de todos os usuários!", exception.Message);
    }

    [Fact]
    public void Update_Should_Update_Item_And_SaveChanges()
    {
        // Arrange
        var dataSet = UsuarioFaker.Instance.GetNewFakersUsuarios();
        var existingItem = dataSet.First();
        var dbContext = new RegisterContext(new DbContextOptionsBuilder<RegisterContext>().UseInMemoryDatabase(databaseName: "TestDatabase").Options);
        var _mockRepository = new Mock<UsuarioRepositorioImpl>(dbContext);

        // Act
        _mockRepository.Object.Insert(ref existingItem);
        var result = existingItem;
        var updatedItem = new Usuario
        {
            Id = result.Id,
            Nome = "Teste Update Item",
            Email = "Teste@teste.com",
            SobreNome = result.SobreNome,
            PerfilUsuario = PerfilUsuario.PerfilType.Administrador,
            StatusUsuario = StatusUsuario.Ativo,
            Telefone = result.Telefone
        };

        _mockRepository.Object.Update(ref updatedItem);
        result = updatedItem;

        // Assert
        Assert.NotNull(result);
        Assert.NotEqual(existingItem, result);
        Assert.Equal(updatedItem.Email, result.Email);
        Assert.Equal(updatedItem, result);
    }

    [Fact]
    public void Update_Should_Throws_Exception()
    {
        // Arrange
        var lstUsuarios = UsuarioFaker.Instance.GetNewFakersUsuarios();
        var lstControleAcesso = ControleAcessoFaker.Instance.ControleAcessos();
        var existingItem = lstUsuarios.First();
        var options = new DbContextOptionsBuilder<RegisterContext>().UseInMemoryDatabase(databaseName: "TestDatabase").Options;
        var _dbContextMock = new RegisterContext(options);
        _dbContextMock.Usuario.AddRange(lstUsuarios);
        _dbContextMock.SaveChanges();

        var _mockRepository = new Mock<UsuarioRepositorioImpl>(_dbContextMock);

        // Act
        var updatedItem = new Usuario
        {
            Id = existingItem.Id,
            Nome = "Teste Update Item",
            Email = "Teste@teste.com",
            SobreNome = existingItem.SobreNome,
            PerfilUsuario = PerfilUsuario.PerfilType.Administrador,
            StatusUsuario = StatusUsuario.Ativo,
            Telefone = existingItem.Telefone
        };

        // Act
        Action result = () => _mockRepository.Object.Update(ref updatedItem);

        // Assert
        Assert.NotNull(result);
        var exception = Assert.Throws<Exception>(() => _mockRepository.Object.Update(ref updatedItem));
        Assert.Equal("Erro ao atualizar usuário!", exception.Message);
        _mockRegisterContext.Verify(c => c.SaveChanges(), Times.Never);
    }

    [Fact]
    public void Delete_Should_Set_Inativo_And_Return_True_When_Usuario_IsDeleted()
    {
        // Arrange
        var lstUsuarios = UsuarioFaker.Instance.GetNewFakersUsuarios();
        var usuario = lstUsuarios.First();
        var options = new DbContextOptionsBuilder<RegisterContext>().UseInMemoryDatabase(databaseName: "TestDatabase").Options;
        var _dbContextMock = new RegisterContext(options);
        _dbContextMock.Usuario.AddRange(lstUsuarios);
        _dbContextMock.SaveChanges();
        var _mockRepository = new Mock<UsuarioRepositorioImpl>(_dbContextMock);

        // Act
        bool result = _mockRepository.Object.Delete(usuario);

        // Assert
        Assert.IsType<bool>(result);
        Assert.True(result);
    }

    [Fact]
    public void Update_Should_Try_Update_Item_And_Return_Erro()
    {
        // Arrange
        var dataSet = UsuarioFaker.Instance.GetNewFakersUsuarios();
        var existingItem = dataSet.First();
        var _dbContextMock = new RegisterContext(new DbContextOptionsBuilder<RegisterContext>().UseInMemoryDatabase(databaseName: "TestDatabase").Options);
        _dbContextMock.AddRange(dataSet);
        var _mockRepository = new Mock<UsuarioRepositorioImpl>(_dbContextMock);

        // Act && Assert
        var exception = Assert.Throws<Exception>(() => _mockRepository.Object.Update(ref existingItem));
        Assert.Equal("Erro ao atualizar usuário!", exception.Message);
    }

    [Fact]
    public void Delete_With_Non_Existing_Item_Should_Not_Remove_Item_And_Return_False()
    {
        // Arrange
        var dataSet = UsuarioFaker.Instance.GetNewFakersUsuarios(10);
        var item = new Usuario { Id = 0 };
        var dbSetMock = Usings.MockDbSet(dataSet);
        _mockRegisterContext.Setup(c => c.Set<Usuario>()).Returns(dbSetMock.Object);
        var _mockRepository = new Mock<UsuarioRepositorioImpl>(_mockRegisterContext.Object);

        // Act
        var result = Assert.Throws<Exception>(() => _mockRepository.Object.Delete(item));

        // Assert
        Assert.IsType<Exception>(result);
        _mockRegisterContext.Verify(c => c.SaveChanges(), Times.Never);
    }

    [Fact]
    public void Delete_Should_Throw_Exception()
    {
        // Arrange
        Usuario? item = (Usuario?)null;
        _mockRegisterContext.Setup(c => c.Remove(It.IsAny<Usuario>())).Throws<Exception>();
        var _mockRepository = new Mock<UsuarioRepositorioImpl>(_mockRegisterContext.Object);

        // Act & Assert
        var exception = Assert.Throws<Exception>(() => _mockRepository.Object.Delete(item));
        Assert.Equal("Erro ao deletar usuário!", exception.Message);
        _mockRegisterContext.Verify(c => c.Remove(It.IsAny<Usuario>()), Times.Never);
    }


}
