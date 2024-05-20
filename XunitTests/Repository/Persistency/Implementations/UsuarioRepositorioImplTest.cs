using Domain.Entities.ValueObjects;
using Fakers.v1;

namespace Repository.Persistency.Implementations;
public sealed class UsuarioRepositorioImplTest
{
    private RegisterContext _context;
    private UsuarioRepositorioImpl _repository;

    public UsuarioRepositorioImplTest()
    {
        var options = new DbContextOptionsBuilder<RegisterContext>().UseInMemoryDatabase(databaseName: "UsuarioRepositorioImplTestDatabaseInMemory").Options;
        _context = new RegisterContext(options);

        _context.TipoCategoria.Add(new TipoCategoria(TipoCategoria.CategoriaType.Despesa));
        _context.TipoCategoria.Add(new TipoCategoria(TipoCategoria.CategoriaType.Receita));
        _context.SaveChanges();

        _context.PerfilUsuario.Add(new PerfilUsuario(PerfilUsuario.PerfilType.Administrador));
        _context.PerfilUsuario.Add(new PerfilUsuario(PerfilUsuario.PerfilType.Usuario));
        _context.SaveChanges();

        var lstControleAcesso = ControleAcessoFaker.Instance.ControleAcessos(20);
        lstControleAcesso.ForEach(c => c.Usuario.PerfilUsuario = _context.PerfilUsuario.First(tc => tc.Id == c.Usuario.PerfilUsuario.Id));
        _context.AddRange(lstControleAcesso);
        _context.SaveChanges();

        var usuarios = lstControleAcesso.Select(c => c.Usuario).ToList();
        var categorias = usuarios.SelectMany(u => u.Categorias).ToList();
        categorias.ForEach(c => c.TipoCategoria = _context.TipoCategoria.First(tc => tc.Id == c.Id));
        var dbSetMockCategoria = Usings.MockDbSet(categorias);        
        _repository = new UsuarioRepositorioImpl(_context);
    }
/*
    [Fact]
    public void Insert_Should_Add_Item_And_SaveChanges()
    {
        // Arrange
        var newUser = UsuarioFaker.Instance.GetNewFaker();
        
        // Act
        _repository.Insert(ref newUser);
        var insertedUser = newUser;

        // Assert
        Assert.NotNull(insertedUser);
        Assert.Equal(newUser, insertedUser);
    }

    [Fact]
    public void Insert_Should_Throws_Erro_When_Try_To_Insert_New_Usuario()
    {
        // Arrange
        var newUser = (Usuario?)null;
    
        // Act
        Action result = () => _repository.Insert(ref newUser);

        // Assert
        Assert.NotNull(result);
        var exception = Assert.Throws<NullReferenceException>(() => _repository.Insert(ref newUser));
    }

    
    [Fact]
    public void Update_Should_Update_Item_And_SaveChanges()
    {
        // Arrange
        var options = new DbContextOptionsBuilder<RegisterContext>().UseInMemoryDatabase(databaseName: "Update_Should_Update_Item_And_SaveChanges").Options;
        var context = new RegisterContext(options);
        context.PerfilUsuario.Add(new PerfilUsuario(PerfilUsuario.PerfilType.Administrador));
        context.PerfilUsuario.Add(new PerfilUsuario(PerfilUsuario.PerfilType.Usuario));
        context.SaveChanges();
        var lstControleAcesso = ControleAcessoFaker.Instance.ControleAcessos(2);
        lstControleAcesso.ForEach(c => c.Usuario.PerfilUsuario = context.PerfilUsuario.First(tc => tc.Id == c.Usuario.PerfilUsuario.Id));
        context.AddRange(lstControleAcesso);
        context.SaveChanges();
        var repository = new UsuarioRepositorioImpl(context);
        var existingItem = context.Usuario.First() as Usuario;

        // Act
        var updatedItem = new Usuario
        {
            Id = existingItem.Id,
            Nome = "Teste Update Item",
            Email = "Teste@teste.com",
            SobreNome = existingItem.SobreNome,
            PerfilUsuario = new PerfilUsuario(PerfilUsuario.PerfilType.Administrador),
            StatusUsuario = StatusUsuario.Ativo,
            Telefone = existingItem.Telefone
        };

        repository.Update(ref updatedItem);

        // Assert
        Assert.NotNull(updatedItem);
        Assert.NotEqual(existingItem, updatedItem);
        Assert.Equal(updatedItem.Email, updatedItem.Email);
        Assert.Equal(updatedItem, updatedItem);
    }

    [Fact]
    public void Delete_With_Non_Existing_Item_Should_Not_Remove_Item_And_Return_False()
    {
        // Arrange
        var entity = new Usuario { Id = 0 };

        // Act
        var result = _repository.Delete(entity);

        // Assert
        Assert.IsType<bool>(result);
        Assert.False(result);
    }
*/
}
