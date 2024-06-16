using __mock__.Repository;
using Microsoft.EntityFrameworkCore;

namespace Repository.Persistency.Implementations.Fixtures;

public sealed class DatabaseFixture : IDisposable
{
    public RegisterContext Context { get; private set; }

    public DatabaseFixture()
    {
        var options = new DbContextOptionsBuilder<RegisterContext>().UseInMemoryDatabase(databaseName: "UsuarioRepositorioImplTestDatabaseInMemory").Options;
        Context = new RegisterContext(options);
        Context.Database.EnsureCreated();

        var lstControleAcesso = MockControleAcesso.Instance.GetControleAcessos();
        lstControleAcesso.ForEach(c => c.Usuario = c.Usuario.CreateUsuario(c.Usuario));
        lstControleAcesso.ForEach(c => c.Usuario.PerfilUsuario = Context.PerfilUsuario.First(tc => tc.Id == c.Usuario.PerfilUsuario.Id));
        lstControleAcesso.Select(c => c.Usuario).ToList()
            .SelectMany(u => u.Categorias).ToList()
            .ForEach(c => c.TipoCategoria = Context.TipoCategoria.First(tc => tc.Id == c.TipoCategoria.Id));
        Context.AddRange(lstControleAcesso);

        Context.SaveChanges();
    }

    public void Dispose()
    {
        Context.Dispose();
    }
}
