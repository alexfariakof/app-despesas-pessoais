using __mock__.v2;
using Microsoft.EntityFrameworkCore;

namespace Repository.Persistency.Implementations.Fixtures;

public class DespesaFixture : IDisposable
{
    public RegisterContext Context { get; private set; }

    public DespesaFixture()
    {
        var options = new DbContextOptionsBuilder<RegisterContext>().UseInMemoryDatabase(databaseName: "DespesaRepositorioImplTestDatabaseInMemory").Options;
        Context = new RegisterContext(options);
        Context.Database.EnsureCreated();

        var controleAcesso = ControleAcessoFaker.Instance.GetNewFaker();
        controleAcesso.Usuario.CreateUsuario(controleAcesso.Usuario);
        controleAcesso.Usuario.PerfilUsuario = Context.PerfilUsuario.First(tc => tc.Id == controleAcesso.Usuario.PerfilUsuario.Id);
        controleAcesso.Usuario.Categorias.ToList()
            .ForEach(c => c.TipoCategoria = Context.TipoCategoria.First(tc => tc.Id == c.TipoCategoria.Id));
        Context.Add(controleAcesso);
        Context.SaveChanges();

        var ususario = Context.Usuario.First();
        var despesa = DespesaFaker.Instance.GetNewFaker(ususario, null);
        despesa.Categoria = Context.Categoria.First(c => c.Usuario.Id == ususario.Id && c.TipoCategoria == 1);
        Context.Add(despesa);
        Context.SaveChanges();

        controleAcesso = ControleAcessoFaker.Instance.GetNewFaker();
        controleAcesso.Usuario.CreateUsuario(controleAcesso.Usuario);
        controleAcesso.Usuario.PerfilUsuario = Context.PerfilUsuario.First(tc => tc.Id == controleAcesso.Usuario.PerfilUsuario.Id);
        controleAcesso.Usuario.Categorias.ToList()
            .ForEach(c => c.TipoCategoria = Context.TipoCategoria.First(tc => tc.Id == c.TipoCategoria.Id));
        Context.Add(controleAcesso);
        Context.SaveChanges();

        ususario = Context.Usuario.First();
        despesa = DespesaFaker.Instance.GetNewFaker(ususario, null);
        despesa.Categoria = Context.Categoria.First(c => c.Usuario.Id == ususario.Id && c.TipoCategoria == 1);
        Context.Add(despesa);
        Context.SaveChanges();


    }

    public void Dispose()
    {
        Context.Dispose();
    }
}
