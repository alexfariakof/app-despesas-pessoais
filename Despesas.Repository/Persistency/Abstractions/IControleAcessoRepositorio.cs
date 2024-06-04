using Domain.Entities;
using System.Linq.Expressions;

namespace Repository.Persistency.Abstractions;
public interface IControleAcessoRepositorioImpl
{
    void Create(ControleAcesso controleAcesso);
    ControleAcesso? Find(Expression<Func<ControleAcesso, bool>> expression);    
    bool ChangePassword(int idUsuario, string password);
    bool RecoveryPassword(string email, string newPassword);    
    void RevokeRefreshToken(int idUsuario);
    ControleAcesso FindByRefreshToken(string refreshToken);
    void RefreshTokenInfo(ControleAcesso controleAcesso);
}
