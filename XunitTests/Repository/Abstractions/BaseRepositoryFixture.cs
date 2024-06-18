using __mock__.Repository;
using Domain.Entities.ValueObjects;
using Microsoft.EntityFrameworkCore;

namespace Repository.Abstractions;

public sealed class BaseRepositoryFixture: IDisposable
{
    public RegisterContext Context { get; private set; }

    public BaseRepositoryFixture()
    {
        var options = new DbContextOptionsBuilder<RegisterContext>().UseInMemoryDatabase(databaseName: "BaseRepositoryFixturetDatabaseInMemory").Options;
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
        var receitas = MockReceita.Instance.GetReceitas();
        foreach (var receita in receitas)
        {
            receita.Usuario = usuario;
            receita.UsuarioId = usuario.Id;
            receita.Categoria = Context.Categoria.FirstOrDefault(c => c.Usuario.Id == usuario.Id && c.TipoCategoria == (int)TipoCategoria.CategoriaType.Receita);
            receita.CategoriaId = receita.Categoria.Id;
        }
        Context.Receita.AddRange(receitas);
        Context.SaveChanges();
    }

    public void Dispose()
    {
        Context.Dispose();
    }
}
