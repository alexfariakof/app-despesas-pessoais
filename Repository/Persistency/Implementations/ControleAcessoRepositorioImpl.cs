using Domain.Core;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Repository.Persistency.Abstractions;

namespace Repository.Persistency.Implementations;
public class ControleAcessoRepositorioImpl : IControleAcessoRepositorioImpl
{
    private readonly RegisterContext? _context;

    public ControleAcessoRepositorioImpl(RegisterContext context)
    {
        _context = context;
    }
    public void Create(ControleAcesso controleAcesso)
    {
        if (FindByEmail(controleAcesso) != null) throw new ArgumentException("Usuário já cadastrado!"); ;            
        
        using (_context)
        {
            try
            {
                _context.Add(controleAcesso);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception("ControleAcessoRepositorioImpl_Create_Exception", ex);
            }
        }
    }

    public ControleAcesso FindById(int idUsuario)
    {
        return _context.ControleAcesso.Include(x => x.Usuario).SingleOrDefault(prop => prop.Id.Equals(idUsuario));
    }

    public ControleAcesso FindByRefreshToken(string refreshToken)
    {
        return _context.ControleAcesso.Include(x => x.Usuario).SingleOrDefault(prop => prop.RefreshToken.Equals(refreshToken));
    }

    public ControleAcesso FindByEmail(ControleAcesso controleAcesso)
    {
        var result =_context.ControleAcesso.Include(x => x.Usuario).SingleOrDefault(prop => prop.Login.Equals(controleAcesso.Login));
        return result;
    }

    public bool RecoveryPassword(string email)
    {
        using (_context)
        {
            ControleAcesso controleAcesso = FindByEmail(new ControleAcesso { Login = email });
            var result = _context.ControleAcesso.SingleOrDefault(prop => prop.Id.Equals(controleAcesso.Id));
            try
            {
                controleAcesso.Senha = Guid.NewGuid().ToString().Substring(0, 8);
                _context.ControleAcesso.Update(controleAcesso);
               _context.SaveChanges();
               return true;
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
        if (usuario is null)  return false;

        try
        {
            ControleAcesso controleAcesso = FindByEmail(new ControleAcesso { Login = usuario.Email });
            controleAcesso.Senha = password;
            _context.ControleAcesso.Update(controleAcesso);
           _context.SaveChanges();
            return true;
        }
        catch (Exception ex)
        {
            throw new Exception("ChangePassword_Erro", ex);
        }
    }

    public bool IsValidPasssword(string email, string senha)
    {
        var senhaToCompare = _context.ControleAcesso.SingleOrDefault(prop => prop.Login.Equals(email)).Senha;
        return senha.Equals(Crypto.GetInstance.Decrypt(senhaToCompare));
    }

    public void RevokeToken(int idUsuario)
    {
        var controleAcesso = _context.ControleAcesso.SingleOrDefault(prop => prop.Id.Equals(idUsuario));        
        if (controleAcesso is null) throw new ArgumentException("Token inexistente!");
        controleAcesso.RefreshToken = null;
        controleAcesso.RefreshTokenExpiry = null;
        _context.SaveChanges();
    }

    public void RefreshTokenInfo(ControleAcesso controleAcesso)
    {
        _context.ControleAcesso.Update(controleAcesso);
        _context.SaveChanges();
    }
}