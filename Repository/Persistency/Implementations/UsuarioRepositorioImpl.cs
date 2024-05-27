using Domain.Entities;
using Domain.Entities.ValueObjects;
using Repository.Abastractions;
using Repository.Persistency.Generic;

namespace Repository.Persistency.Implementations;
public class UsuarioRepositorioImpl : BaseRepository<Usuario>, IRepositorio<Usuario>
{
    private readonly RegisterContext _context;
    public UsuarioRepositorioImpl(RegisterContext context) : base(context)
    {
        _context = context;
    }

    public override void Insert(ref Usuario entity)
    {
        var controleAcesso = new ControleAcesso();
        controleAcesso.CreateAccount(entity, Guid.NewGuid().ToString().Substring(0, 8));
        controleAcesso.Usuario.PerfilUsuario = _context.Set<PerfilUsuario>().First(perfil => perfil.Id.Equals(controleAcesso.Usuario.PerfilUsuario.Id));
        controleAcesso.Usuario.Categorias.ToList().ForEach(c => c.TipoCategoria = _context.Set<TipoCategoria>().First(tc => tc.Id.Equals(c.TipoCategoria.Id)));
        _context.Add(controleAcesso);
        _context.SaveChanges();
    }

    public override List<Usuario> GetAll()
    {
        return _context.Usuario.ToList();
    }

    public override Usuario Get(int id)
    {
        return _context.Usuario.Single(prop => prop.Id.Equals(id));
    }

    public override void Update(ref Usuario entity)
    {
        var usuarioId = entity.Id;
        var usuario = _context.Set<Usuario>().Single(prop => prop.Id.Equals(usuarioId));
        if (usuario == null)
            throw new AggregateException("Usuário não possui conta de acesso!");

        usuario.PerfilUsuario = _context.Set<PerfilUsuario>().First(perfil => perfil.Id.Equals(usuario.PerfilUsuario.Id));
        usuario.Categorias.ToList().ForEach(c => c.TipoCategoria = _context.Set<TipoCategoria>().First(tc => tc.Id.Equals(c.TipoCategoria.Id)));
        _context.Usuario.Entry(usuario).CurrentValues.SetValues(entity);
        _context.SaveChanges();
        entity = usuario;
    }

    public override bool Delete(Usuario obj)
    {
        var result = _context.Usuario.SingleOrDefault(prop => prop.Id.Equals(obj.Id));
        if (result != null)
        {
            result.StatusUsuario = StatusUsuario.Inativo;
            _context.Entry(result).CurrentValues.SetValues(result);
            _context.SaveChanges();
            return true;
        }
        return false;
    }

    public override bool Exists(int? id)
    {
        return _context.Usuario.Any(prop => prop.Id.Equals(id));
    }
}