using Domain.Entities;

namespace Repository.Persistency;
public interface IControleAcessoRepositorioImpl
{
    void Create(ControleAcesso controleAcesso);
    ControleAcesso FindByEmail(ControleAcesso controleAcesso);
    Usuario GetUsuarioByEmail(string login);
    bool ChangePassword(int idUsuario, string password);
    bool RecoveryPassword(string email);
    bool IsValidPasssword(string login, string password);
}
