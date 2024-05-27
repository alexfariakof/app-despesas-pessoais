using Fakers.v2;

namespace Repository.Persistency.Implementations.Fixtures;

public class ReceitaFixture : IDisposable
{
    public RegisterContext Context { get; set; }

    public ReceitaFixture()
    {
        var options = new DbContextOptionsBuilder<RegisterContext>().UseInMemoryDatabase(databaseName: "ReceitaRepositorioImplTestDatabaseInMemory").Options;
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
        var receita = ReceitaFaker.Instance.GetNewFaker(ususario, null);
        receita.Categoria = Context.Categoria.First(c => c.Usuario.Id == ususario.Id && c.TipoCategoria == 1);
        Context.Add(receita);
        Context.SaveChanges();

        controleAcesso = ControleAcessoFaker.Instance.GetNewFaker();
        controleAcesso.Usuario.CreateUsuario(controleAcesso.Usuario);
        controleAcesso.Usuario.PerfilUsuario = Context.PerfilUsuario.First(tc => tc.Id == controleAcesso.Usuario.PerfilUsuario.Id);
        controleAcesso.Usuario.Categorias.ToList()
            .ForEach(c => c.TipoCategoria = Context.TipoCategoria.First(tc => tc.Id == c.TipoCategoria.Id));
        Context.Add(controleAcesso);
        Context.SaveChanges();

        ususario = Context.Usuario.First();
        receita = ReceitaFaker.Instance.GetNewFaker(ususario, null);
        receita.Categoria = Context.Categoria.First(c => c.Usuario.Id == ususario.Id && c.TipoCategoria == 1);
        Context.Add(receita);
        Context.SaveChanges();

    }

    public void Dispose()
    {
        Context.Dispose();
    }
}
