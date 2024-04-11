using Domain.Core;
using Domain.Entities;
using Repository.Persistency.Generic;

namespace Repository.Persistency.Implementations;
public class UsuarioRepositorioImpl : IRepositorio<Usuario>
{
    private readonly RegisterContext _context;    
    public UsuarioRepositorioImpl(RegisterContext context)
    {
        _context = context;
    }
    public void Insert(ref Usuario item)
    {
        try
        {

            var controleAcesso = new ControleAcesso
            {
                Login = item.Email,
                UsuarioId = item.Id,
                Usuario = item,
                Senha = Crypto.GetInstance.Encrypt(Guid.NewGuid().ToString().Substring(0, 8))
            };
            _context.Add(controleAcesso);
            _context.SaveChanges();
        }
        catch 
        {
            throw new Exception("Erro ao inserir um novo usuário!");
        }
    }
    public List<Usuario> GetAll()
    {
        try
        {
            return _context.Usuario.ToList();
        }
        catch 
        {
            throw new Exception("Erro ao gerar resgistros de todos os usuários!");
        }
    }
    public Usuario Get(int id)
    {
        return _context.Usuario.Single(prop => prop.Id.Equals(id));
    }
    public void Update(ref Usuario obj)
    {
        try
        {
            var usuarioId = obj.Id;
            var controleAcesso = _context.ControleAcesso.Single(prop => prop.UsuarioId.Equals(usuarioId));
            if (controleAcesso == null)
                throw new Exception();

            controleAcesso.Login = obj.Email;
            _context.Entry(controleAcesso).CurrentValues.SetValues(controleAcesso);
            var usaurio = _context.Usuario.SingleOrDefault(prop => prop.Id.Equals(usuarioId));
            _context?.Entry(usaurio).CurrentValues.SetValues(obj);
            _context?.SaveChanges();
        }
        catch 
        {
            throw new Exception("Erro ao atualizar usuário!");
        }
    }
    public bool Delete(Usuario obj)
    {            
        try
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
        catch 
        {
            throw new Exception("Erro ao deletar usuário!");
        }
    }
    public bool Exists(int? id)
    {
        return _context.Usuario.Any(prop => prop.Id.Equals(id));
    }
}