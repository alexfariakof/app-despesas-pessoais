using Business.Dtos;
using Domain.Entities;

namespace Business.Abstractions;
public interface IControleAcessoBusiness
{
    AuthenticationDto ValidateCredentials(ControleAcessoDto controleAcesso);
    AuthenticationDto ValidateCredentials(AuthenticationDto authenticationDto, int idUsuario);
    void Create(ControleAcesso controleAcesso);
    bool ChangePassword(int idUsuario, string password);
    bool RecoveryPassword(string email);
    bool RevokeToken(int idUsurio);
}