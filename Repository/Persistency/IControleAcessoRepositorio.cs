using Domain.Entities;

namespace Repository.Persistency;
public interface IControleAcessoRepositorioImpl
{
    void Create(ControleAcesso controleAcesso);
    ControleAcesso FindById(int idUsuario);
    ControleAcesso FindByEmail(ControleAcesso controleAcesso);    
    ControleAcesso FindByRefreshToken(string refreshToken);
    bool ChangePassword(int idUsuario, string password);
    bool RecoveryPassword(string email);
    bool IsValidPasssword(string login, string password);
    void RevokeToken(int  idUsuario);
    void RefreshTokenInfo(ControleAcesso controleAcesso);
}
