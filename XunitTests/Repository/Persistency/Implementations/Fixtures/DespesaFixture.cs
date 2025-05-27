using __mock__.Repository;
using Domain.Entities.ValueObjects;
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
            despesa.Categoria = Context.Categoria.FirstOrDefault(c => c.Usuario.Id == usuario.Id && c.TipoCategoria == (int)TipoCategoria.CategoriaType.Despesa);
            despesa.CategoriaId = despesa.Categoria.Id;
        }
        Context.AddRange(despesas);
        Context.SaveChanges();
    }

    public void Dispose()
    {
        Context.Dispose();
    }
}
