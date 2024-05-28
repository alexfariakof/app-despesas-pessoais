using Domain.Entities;
using Domain.Entities.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Repository.Persistency.Abstractions;
using System.Linq.Expressions;

namespace Repository.Persistency.Implementations;
public class ControleAcessoRepositorioImpl : IControleAcessoRepositorioImpl
{
    public RegisterContext Context { get; }
    public ControleAcessoRepositorioImpl(RegisterContext context)
    {
        Context = context;
    }

    public void Create(ControleAcesso controleAcesso)
    {
        var existingEntity = Context.Set<ControleAcesso>().SingleOrDefault(c => c.Login.Equals(controleAcesso.Login));
        if (existingEntity != null) throw new ArgumentException("Usuário já cadastrado!");

        try
        {
            controleAcesso.Usuario.PerfilUsuario = Context.Set<PerfilUsuario>().First(perfil => perfil.Id.Equals(controleAcesso.Usuario.PerfilUsuario.Id));
            controleAcesso.Usuario.Categorias.ToList().ForEach(c => c.TipoCategoria = Context.Set<TipoCategoria>().First(tc => tc.Id.Equals(c.TipoCategoria.Id)));
            Context.Add(controleAcesso);
            Context.SaveChanges();
        }
        catch (Exception ex)
        {
            throw new Exception("ControleAcessoRepositorioImpl_Create_Exception", ex);
        }

    }

    public bool RecoveryPassword(string email, string newPassword)
    {
        try
        {
            var entity = Context.Set<ControleAcesso>().First(c => c.Login.Equals(email));
            var controleAcesso = entity as ControleAcesso;
            controleAcesso.Senha = newPassword;
            Context.ControleAcesso.Entry(entity).CurrentValues.SetValues(controleAcesso);
            Context.SaveChanges();
            return true;
        }
        catch
        {
            return false;
        }
    }

    public bool ChangePassword(int idUsuario, string password)
    {
        var usuario = Context.Set<Usuario>().SingleOrDefault(prop => prop.Id.Equals(idUsuario));
        if (usuario is null) return false;

        try
        {
            var controleAcesso = Context.Set<ControleAcesso>().First(c => c.Login.Equals(usuario.Email));
            controleAcesso.Senha = password;
            Context.ControleAcesso.Update(controleAcesso);
            Context.SaveChanges();
            return true;
        }
        catch (Exception ex)
        {
            throw new Exception("ChangePassword_Erro", ex);
        }
    }

    public void RevokeRefreshToken(int idUsuario)
    {
        var controleAcesso = Context.ControleAcesso.SingleOrDefault(prop => prop.Id.Equals(idUsuario));
        if (controleAcesso is null) throw new ArgumentException("Token inexistente!");
        controleAcesso.RefreshToken = null;
        controleAcesso.RefreshTokenExpiry = null;
        Context.SaveChanges();
    }

    public ControleAcesso FindByRefreshToken(string refreshToken)
    {
        return Context.Set<ControleAcesso>().Include(x => x.Usuario).First(prop => prop.RefreshToken.Equals(refreshToken));
    }

    public void RefreshTokenInfo(ControleAcesso controleAcesso)
    {
        Context.ControleAcesso.Update(controleAcesso);
        Context.SaveChanges();
    }

    public ControleAcesso? Find(Expression<Func<ControleAcesso, bool>> expression)
    {
        return Context.ControleAcesso.Include(x => x.Usuario).SingleOrDefault(expression);
    }
}