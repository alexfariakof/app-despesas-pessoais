using Domain.Entities.ValueObjects;
using Fakers.v1;
using Repository.Persistency.Abstractions;

namespace Repository.Persistency.Implementations.Fixtures;
public class GraficoRepositorioFixture : IDisposable
{
    public RegisterContext Context { get; private set; }
    public Mock<IGraficosRepositorio> MockRepository { get; private set; }
    public Mock<GraficosRepositorioImpl> Repository { get; private set; }
    public Usuario MockUsuario { get; private set; }
    public DateTime MockAnoMes { get; private set; } = DateTime.Today;

    public GraficoRepositorioFixture()
    {
        var options = new DbContextOptionsBuilder<RegisterContext>().UseInMemoryDatabase(databaseName: "Grafico Repo Database InMemory").Options;
        Context = new RegisterContext(options);
        MockUsuario = UsuarioFaker.Instance.GetNewFaker();
        Context.AddRange(MockUsuario);
        Context.SaveChanges();
        Context.TipoCategoria.Add(new TipoCategoria(TipoCategoria.CategoriaType.Despesa));
        Context.TipoCategoria.Add(new TipoCategoria(TipoCategoria.CategoriaType.Receita));
        Context.SaveChanges();

        var despesas = DespesaFaker.Instance.Despesas(MockUsuario, MockUsuario.Id, 60);
        despesas.ForEach(d => d.Categoria.TipoCategoria = Context.TipoCategoria.First(tc => tc.Id == 1));
        Context.AddRange(despesas);
        despesas = DespesaFaker.Instance.Despesas(MockUsuario, MockUsuario.Id, 60);
        despesas.ForEach(d => d.Categoria.TipoCategoria = Context.TipoCategoria.First(tc => tc.Id == 1));
        Context.AddRange(despesas);
        Context.SaveChanges();

        Repository = new Mock<GraficosRepositorioImpl>(MockBehavior.Default, Context);
        MockRepository = Mock.Get<IGraficosRepositorio>(Repository.Object);
    }

    public void Dispose()
    {
        Context.Dispose();
    }
}
