using Business.Dtos;
using Domain.Entities;

namespace Business.Abstractions;
public interface IControleAcessoBusiness
{
    Dtos.AuthenticationDto FindByLogin(ControleAcessoDto controleAcesso);
    void Create(ControleAcesso controleAcesso);
    bool ChangePassword(int idUsuario, string password);
    bool RecoveryPassword(string email);     
}