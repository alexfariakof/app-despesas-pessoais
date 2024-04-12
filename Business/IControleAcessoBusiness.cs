using Business.Dtos;
using Domain.Entities;

namespace Business;
public interface IControleAcessoBusiness
{
    Dtos.Authentication FindByLogin(ControleAcessoVM controleAcesso);
    void Create(ControleAcesso controleAcesso);
    bool ChangePassword(int idUsuario, string password);
    bool RecoveryPassword(string email);     
}