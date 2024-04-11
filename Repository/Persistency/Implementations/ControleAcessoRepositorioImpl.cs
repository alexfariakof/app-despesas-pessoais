using Domain.Core;
using Domain.Core.Interfaces;
using Domain.Entities;

namespace Repository.Persistency.Implementations;
public class ControleAcessoRepositorioImpl : IControleAcessoRepositorio
{
    private readonly RegisterContext? _context;
    private readonly ICrypto _crypto = Crypto.GetInstance;
    private readonly IEmailSender _emailSender;
    public ControleAcessoRepositorioImpl(RegisterContext context, IEmailSender emailSender)
    {
        _context = context;
        _emailSender = emailSender;
    }
    public bool Create(ControleAcesso controleAcesso)
    {
        if (FindByEmail(controleAcesso) != null) return false;            
        
        using (_context)
        {
            try
            {
                controleAcesso.Senha = _crypto.Encrypt(controleAcesso.Senha);
                _context.Add(controleAcesso);
                _context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception("ControleAcessoRepositorioImpl_Create_Exception", ex);
            }
        }
    }
    public ControleAcesso FindByEmail(ControleAcesso controleAcesso)
    {
        var result =_context.ControleAcesso.SingleOrDefault(prop => prop.Login.Equals(controleAcesso.Login));
        return result;
    }
    public Usuario GetUsuarioByEmail(string email)
    {
        return _context.Usuario.SingleOrDefault(prop => prop.Email.Equals(email));
    }
    public bool RecoveryPassword(string email)
    {
        ControleAcesso controleAcesso = FindByEmail(new ControleAcesso { Login = email });
        controleAcesso.Usuario = GetUsuarioByEmail(email);

        using (_context)
        {

            var result = _context.ControleAcesso.SingleOrDefault(prop => prop.Id.Equals(controleAcesso.Id));
            try
            {
                var senhaNova = Guid.NewGuid().ToString().Substring(0, 8);
                controleAcesso.Senha = _crypto.Encrypt(senhaNova);
                if (_emailSender.SendEmailPassword(controleAcesso.Usuario, senhaNova))
                {
                   _context.Entry(result).CurrentValues.SetValues(controleAcesso);
                   _context.SaveChanges();
                    return true;
                }
                return false;                    
            }
            catch 
            {
                return false;
            }
        }            
    }
    public bool ChangePassword(int idUsuario, string password)
    {
        Usuario? usuario = _context.Usuario.SingleOrDefault(prop => prop.Id.Equals(idUsuario));
        
        if (usuario is null) 
            return false;

        ControleAcesso controleAcesso = FindByEmail(new ControleAcesso { Login = usuario.Email });
        try
        {
            var result = _context.ControleAcesso.Single(prop => prop.Id.Equals(controleAcesso.Id));
            controleAcesso.Senha = _crypto.Encrypt(password);
           _context.Entry(result).CurrentValues.SetValues(controleAcesso);
            usuario.StatusUsuario = StatusUsuario.Ativo;
           _context.Entry(usuario).CurrentValues.SetValues(usuario);
           _context.SaveChanges();
            return true;
        }
        catch (Exception ex)
        {
            throw new Exception("ChangePassword_Erro", ex);
        }
    }    
    public bool isValidPasssword(ControleAcesso controleAcesso)
    {
        ControleAcesso _controleAcesso = FindByEmail(controleAcesso);
        
        if (_crypto.Decrypt(_controleAcesso.Senha).Equals(controleAcesso.Senha))
        {
            return true;
        }
        return false;
    }
}