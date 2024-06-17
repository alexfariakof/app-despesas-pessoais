using Domain.Entities;
using Domain.Entities.ValueObjects;
using Repository.Abastractions;
using Repository.Persistency.Generic;

namespace Repository.Persistency.Implementations;
public class UsuarioRepositorioImpl : BaseRepository<Usuario>, IRepositorio<Usuario>
{
    public RegisterContext Context { get; }
    public UsuarioRepositorioImpl(RegisterContext context) : base(context)
    {
        Context = context;
    }

    public override void Insert(ref Usuario entity)
    {
        var controleAcesso = new ControleAcesso();
        controleAcesso.Usuario = entity;
        controleAcesso.CreateAccount(entity, Guid.NewGuid().ToString().Substring(0, 8));
        controleAcesso.Usuario.PerfilUsuario = Context.Set<PerfilUsuario>().First(perfil => perfil.Id.Equals(controleAcesso.Usuario.PerfilUsuario.Id));
        controleAcesso.Usuario.Categorias.ToList().ForEach(c => c.TipoCategoria = Context.Set<TipoCategoria>().First(tc => tc.Id.Equals(c.TipoCategoria.Id)));
        Context.Add(controleAcesso);
        Context.SaveChanges();
    }

    public override List<Usuario> GetAll()
    {
        return Context.Usuario.ToList();
    }

    public override Usuario Get(int id)
    {
        return Context.Usuario.Single(prop => prop.Id.Equals(id));
    }

    public override void Update(ref Usuario entity)
    {
        var usuarioId = entity.Id;
        var usuario = Context.Set<Usuario>().Single(prop => prop.Id.Equals(usuarioId));
        if (usuario == null)
            throw new AggregateException("Usuário não possui conta de acesso!");

        usuario.PerfilUsuario = Context.Set<PerfilUsuario>().First(perfil => perfil.Id.Equals(usuario.PerfilUsuario.Id));
        usuario.Categorias.ToList().ForEach(c => c.TipoCategoria = Context.Set<TipoCategoria>().First(tc => tc.Id.Equals(c.TipoCategoria.Id)));
        Context.Usuario.Entry(usuario).CurrentValues.SetValues(entity);
        Context.SaveChanges();
        entity = usuario;
    }

    public override bool Delete(Usuario obj)
    {
        var result = Context.Usuario.SingleOrDefault(prop => prop.Id.Equals(obj.Id));
        if (result != null)
        {
            result.StatusUsuario = StatusUsuario.Inativo;
            Context.Entry(result).CurrentValues.SetValues(result);
            Context.SaveChanges();
            return true;
        }
        return false;
    }

    public override bool Exists(int? id)
    {
        return Context.Usuario.Any(prop => prop.Id.Equals(id));
    }
}