using __mock__.Repository;
using Microsoft.EntityFrameworkCore;

namespace Repository.Persistency.Implementations.Fixtures;
public sealed class LancamentoRepositorioFixture : IDisposable
{
    public RegisterContext Context { get; private set; }
    public LancamentoRepositorioImpl MockRepository;
    public DateTime MockAnoMes { get; private set; } = DateTime.Today;
    public LancamentoRepositorioFixture()
    {
        var options = new DbContextOptionsBuilder<RegisterContext>().UseInMemoryDatabase(databaseName: "LancamentoRepositorioDatabaseInMemory").Options;
        Context = new RegisterContext(options);
        Context.Database.EnsureDeleted();
        Context.Database.EnsureCreated();

        var controleAcesso = MockControleAcesso.Instance.GetControleAcesso();
        controleAcesso.Usuario.CreateUsuario(controleAcesso.Usuario);
        controleAcesso.Usuario.PerfilUsuario = Context.PerfilUsuario.First(tc => tc.Id == controleAcesso.Usuario.PerfilUsuario.Id);
        controleAcesso.Usuario.Categorias.ToList()
            .ForEach(c => c.TipoCategoria = Context.TipoCategoria.First(tc => tc.Id == c.TipoCategoria.Id));
        Context.Add(controleAcesso);
        Context.SaveChanges();
        var usuario = Context.Usuario.First();

        var despesas = MockDespesa.Instance.GetDespesas();
        foreach (var despesa in despesas)
        {
            despesa.Usuario = usuario;
            despesa.UsuarioId = usuario.Id;
            despesa.Data = MockAnoMes;
            despesa.Categoria = Context.Categoria
                .FirstOrDefault(c => c.Usuario.Id == usuario.Id && c.TipoCategoria == 1);
            despesa.CategoriaId = despesa.Categoria.Id;
        }
        Context.AddRange(despesas);

        var receitas = MockReceita.Instance.GetReceitas();
        foreach (var receita in receitas)
        {
            receita.Usuario = usuario;
            receita.UsuarioId = usuario.Id;
            receita.Data = MockAnoMes;
            receita.Categoria = Context.Categoria
                .FirstOrDefault(c => c.Usuario.Id == usuario.Id && c.TipoCategoria == 2);
            receita.CategoriaId = receita.Categoria.Id;
        }
        Context.AddRange(receitas);
        Context.SaveChanges();
        MockRepository = new LancamentoRepositorioImpl(Context);
    }

    public void Dispose()
    {
        Context.Dispose();
    }
}
