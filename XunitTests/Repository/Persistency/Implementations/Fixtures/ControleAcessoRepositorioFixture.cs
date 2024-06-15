using Domain.Entities.ValueObjects;
using Repository.Persistency.Abstractions;
using Microsoft.EntityFrameworkCore;
using __mock__.Repository;

namespace Repository.Persistency.Implementations.Fixtures;
public sealed class ControleAcessoRepositorioFixture : IDisposable
{
    public RegisterContext Context { get; private set; }
    public Mock<ControleAcessoRepositorioImpl> Repository { get; set; }
    public Mock<IControleAcessoRepositorioImpl> MockRepository { get; private set; }

    public ControleAcessoRepositorioFixture()
    {
        var options = new DbContextOptionsBuilder<RegisterContext>().UseInMemoryDatabase(databaseName: "ControleAcessoRepositorioImpl").Options;
        Context = new RegisterContext(options);
        Context.PerfilUsuario.Add(new PerfilUsuario(PerfilUsuario.Perfil.Admin));
        Context.PerfilUsuario.Add(new PerfilUsuario(PerfilUsuario.Perfil.User));
        Context.SaveChanges();

        var lstControleAcesso = MockControleAcesso.Instance.GetControleAcessos();
        lstControleAcesso.ForEach(c => c.Usuario.PerfilUsuario = Context.PerfilUsuario.First(tc => tc.Id == c.Usuario.PerfilUsuario.Id));
        Context.AddRange(lstControleAcesso);
        Context.SaveChanges();

        Repository = new Mock<ControleAcessoRepositorioImpl>(Context);
        MockRepository = Mock.Get<IControleAcessoRepositorioImpl>(Repository.Object);
    }

    public void Dispose()
    {
        Context.Dispose();
    }
}
