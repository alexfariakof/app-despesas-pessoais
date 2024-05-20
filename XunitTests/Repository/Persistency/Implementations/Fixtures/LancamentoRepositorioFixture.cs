using Domain.Entities.ValueObjects;
using Fakers.v1;
using Repository.Persistency.Abstractions;

namespace Repository.Persistency.Implementations.Fixtures;
public sealed class LancamentoRepositorioFixture : IDisposable
{
    public RegisterContext Context;
    public Mock<ILancamentoRepositorio> MockRepository;
    public Mock<LancamentoRepositorioImpl> Repository;
    public Usuario MockUsuario;
    public DateTime MockAnoMes { get; private set; } = DateTime.Today;
    public LancamentoRepositorioFixture()
    {
        var options = new DbContextOptionsBuilder<RegisterContext>().UseInMemoryDatabase(databaseName: "LancamentoRepositorioDatabaseInMemory").Options;
        Context = new RegisterContext(options);
        MockUsuario = UsuarioFaker.Instance.GetNewFaker();
        Context.AddRange(MockUsuario);
        Context.SaveChanges();
        Context.TipoCategoria.Add(new TipoCategoria(TipoCategoria.CategoriaType.Despesa));
        Context.TipoCategoria.Add(new TipoCategoria(TipoCategoria.CategoriaType.Receita));
        Context.SaveChanges();

        var despesas = DespesaFaker.Instance.Despesas(MockUsuario, MockUsuario.Id, 10);
        despesas.ForEach(d => d.Categoria.TipoCategoria = Context.TipoCategoria.First(tc => tc.Id == 1));
        Context.AddRange(despesas);
        despesas = DespesaFaker.Instance.Despesas(MockUsuario, MockUsuario.Id, 10);
        despesas.ForEach(d => d.Categoria.TipoCategoria = Context.TipoCategoria.First(tc => tc.Id == 1));
        Context.AddRange(despesas);
        Context.SaveChanges();

        var receitas = ReceitaFaker.Instance.Receitas(MockUsuario, MockUsuario.Id, 10);
        receitas.ForEach(r => r.Categoria.TipoCategoria = Context.TipoCategoria.First(tc => tc.Id == 2));
        Context.AddRange(receitas);
        receitas = ReceitaFaker.Instance.Receitas(MockUsuario, MockUsuario.Id, 10);
        receitas.ForEach(r => r.Categoria.TipoCategoria = Context.TipoCategoria.First(tc => tc.Id == 2));
        Context.AddRange(receitas);
        Context.SaveChanges();
        Repository = new Mock<LancamentoRepositorioImpl>(MockBehavior.Default, Context);
        MockRepository = Mock.Get<ILancamentoRepositorio>(Repository.Object);       
    }

    public void Dispose()
    {
        Context.Dispose();
    }
}