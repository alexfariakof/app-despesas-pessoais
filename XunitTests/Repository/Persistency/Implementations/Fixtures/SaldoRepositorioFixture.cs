using Domain.Entities.ValueObjects;
using __mock__.Repository;
using Repository.Persistency.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace Repository.Persistency.Implementations.Fixtures;
public sealed class SaldoRepositorioFixture : IDisposable
{
    public RegisterContext Context { get; private set; }
    public Mock<ISaldoRepositorio> MockRepository { get; private set; }
    public Mock<SaldoRepositorioImpl> Repository { get; private set; }
    public DateTime MockAnoMes { get; private set; } = DateTime.Today;

    public SaldoRepositorioFixture()
    {
        var options = new DbContextOptionsBuilder<RegisterContext>().UseInMemoryDatabase(databaseName: "SaldoRepoDatabaseInMemory").Options;
        Context = new RegisterContext(options);
        var usaurio = MockUsuario.Instance.GetUsuario();
        Context.AddRange(usaurio);
        Context.SaveChanges();
        Context.TipoCategoria.Add(new TipoCategoria(TipoCategoria.CategoriaType.Despesa));
        Context.TipoCategoria.Add(new TipoCategoria(TipoCategoria.CategoriaType.Receita));
        Context.SaveChanges();

        var despesas = MockDespesa.Instance.GetDespesas();
        foreach (var despesa in despesas)
        {
            despesa.Usuario = usaurio;
            despesa.Data = MockAnoMes;
            despesa.Categoria = Context.Categoria
                .FirstOrDefault(c => c.Usuario.Id == usaurio.Id && c.TipoCategoria == 1);
        }
        Context.AddRange(despesas);

        var receitas = MockReceita.Instance.GetReceitas();
        foreach (var receita in receitas)
        {
            receita.Usuario = usaurio;
            receita.Data = MockAnoMes;
            receita.Categoria = Context.Categoria
                .FirstOrDefault(c => c.Usuario.Id == usaurio.Id && c.TipoCategoria == 2);
        }
        Context.AddRange(receitas);
        Context.SaveChanges();
        Repository = new Mock<SaldoRepositorioImpl>(MockBehavior.Default, Context);
        MockRepository = Mock.Get<ISaldoRepositorio>(Repository.Object);
    }

    public void Dispose()
    {
        Context.Dispose();
    }
}
