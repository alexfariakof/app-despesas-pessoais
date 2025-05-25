using Domain.Entities.ValueObjects;
using __mock__.Repository;
using Repository.Persistency.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace Repository.Persistency.Implementations.Fixtures;
public sealed class GraficoRepositorioFixture : IDisposable
{
    public RegisterContext Context { get; private set; }
    public Mock<IGraficosRepositorio> MockRepository { get; private set; }
    public Mock<GraficosRepositorioImpl> Repository { get; private set; }
    public Usuario UsuarioMock { get; private set; }
    public DateTime MockAnoMes { get; private set; } = DateTime.Today;

    public GraficoRepositorioFixture()
    {
        var options = new DbContextOptionsBuilder<RegisterContext>().UseInMemoryDatabase(databaseName: "Grafico Repo Database InMemory").Options;
        Context = new RegisterContext(options);
        UsuarioMock = MockUsuario.Instance.GetUsuario();
        Context.AddRange(UsuarioMock);
        Context.SaveChanges();
        Context.TipoCategoria.Add(new TipoCategoria(TipoCategoria.CategoriaType.Despesa));
        Context.TipoCategoria.Add(new TipoCategoria(TipoCategoria.CategoriaType.Receita));
        Context.SaveChanges();

        var despesas = MockDespesa.Instance.GetDespesas();
        foreach (var despesa in despesas)
        {
            despesa.Usuario = UsuarioMock;
            despesa.Categoria = Context.Categoria
                .FirstOrDefault(c => c.Usuario.Id == UsuarioMock.Id && c.TipoCategoria == 1);
        }
        Context.AddRange(despesas);

        var receitas = MockReceita.Instance.GetReceitas();
        foreach (var receita in receitas)
        {
            receita.Usuario = UsuarioMock;
            receita.Categoria = Context.Categoria
                .FirstOrDefault(c => c.Usuario.Id == UsuarioMock.Id && c.TipoCategoria == 1);
        }
        Context.AddRange(receitas);
        Context.SaveChanges();

        Repository = new Mock<GraficosRepositorioImpl>(MockBehavior.Default, Context);
        MockRepository = Mock.Get<IGraficosRepositorio>(Repository.Object);
    }

    public void Dispose()
    {
        Context.Dispose();
    }
}
