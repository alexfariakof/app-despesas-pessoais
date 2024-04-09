using Domain.Entities;

namespace Repository.Persistency;
public interface IControleAcessoRepositorio
{
    bool Create(ControleAcesso controleAcesso);
    ControleAcesso FindByEmail(ControleAcesso controleAcesso);
    Usuario GetUsuarioByEmail(string login);
    bool ChangePassword(int idUsuario, string password);
    bool RecoveryPassword(string email);
    bool isValidPasssword(ControleAcesso controleAcesso);
}
