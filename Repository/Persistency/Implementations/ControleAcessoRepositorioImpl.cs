using Domain.Entities;
using Domain.Entities.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Repository.Persistency.Abstractions;
using System.Linq.Expressions;

namespace Repository.Persistency.Implementations;
public class ControleAcessoRepositorioImpl : IControleAcessoRepositorioImpl
{
    private readonly RegisterContext _context;

    public ControleAcessoRepositorioImpl(RegisterContext context)
    {
        _context = context;
    }

    public void Create(ControleAcesso controleAcesso)
    {
        var existingEntity = _context.Set<ControleAcesso>().SingleOrDefault(c => c.Login.Equals(controleAcesso.Login));
        if (existingEntity != null) throw new ArgumentException("Usuário já cadastrado!");

        try
        {
            controleAcesso.Usuario.PerfilUsuario = this._context.Set<PerfilUsuario>().First(perfil => perfil.Id.Equals(controleAcesso.Usuario.PerfilUsuario.Id));
            controleAcesso.Usuario.Categorias.ToList().ForEach(c => c.TipoCategoria = this._context.Set<TipoCategoria>().First(tc => tc.Id.Equals(c.TipoCategoria.Id)));
            _context.Add(controleAcesso);
            _context.SaveChanges();
        }
        catch (Exception ex)
        {
            throw new Exception("ControleAcessoRepositorioImpl_Create_Exception", ex);
        }

    }

    public bool RecoveryPassword(string email)
    {
        try
        {
            var entity = _context.Set<ControleAcesso>().First(c => c.Login.Equals(email));
            var controleAcesso = entity as ControleAcesso;
            controleAcesso.Senha = Guid.NewGuid().ToString().Substring(0, 8);
            this._context.ControleAcesso.Entry(entity).CurrentValues.SetValues(controleAcesso);
            _context.SaveChanges();
            return true;

        }
        catch
        {
            return false;
        }
    }

    public bool ChangePassword(int idUsuario, string password)
    {
        var usuario = _context.Set<Usuario>().SingleOrDefault(prop => prop.Id.Equals(idUsuario));
        if (usuario is null) return false;

        try
        {
            var controleAcesso = _context.Set<ControleAcesso>().First(c => c.Login.Equals(usuario.Email));
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

    public bool IsValidPassword(string email, string encryptyPassword)
    {
        var senhaToCompare = _context.Set<ControleAcesso>().Single(prop => prop.Login.Equals(email)).Senha;
        return encryptyPassword.Equals(senhaToCompare);
    }

    public void RevokeRefreshToken(int idUsuario)
    {
        var controleAcesso = _context.ControleAcesso.SingleOrDefault(prop => prop.Id.Equals(idUsuario));
        if (controleAcesso is null) throw new ArgumentException("Token inexistente!");
        controleAcesso.RefreshToken = null;
        controleAcesso.RefreshTokenExpiry = null;
        _context.SaveChanges();
    }

    public ControleAcesso FindByRefreshToken(string refreshToken)
    {
        return _context.Set<ControleAcesso>().Include(x => x.Usuario).First(prop => prop.RefreshToken.Equals(refreshToken));
    }

    public void RefreshTokenInfo(ControleAcesso controleAcesso)
    {
        _context.ControleAcesso.Update(controleAcesso);
        _context.SaveChanges();
    }

    public ControleAcesso? Find(Expression<Func<ControleAcesso, bool>> expression)
    {
        return _context.ControleAcesso.Include(x => x.Usuario).SingleOrDefault(expression);
    }
}