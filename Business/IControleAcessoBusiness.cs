using Domain.Entities;

namespace Business;
public interface IControleAcessoBusiness
{
    Domain.Entities.Authentication FindByLogin(ControleAcesso controleAcesso);
    bool Create(ControleAcesso controleAcesso);
    bool ChangePassword(int idUsuario, string password);
    bool RecoveryPassword(string email);     
}