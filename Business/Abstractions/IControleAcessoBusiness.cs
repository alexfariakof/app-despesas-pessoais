using Business.Dtos;
using Domain.Entities;

namespace Business.Abstractions;
public interface IControleAcessoBusiness
{
    AuthenticationDto ValidateCredentials(ControleAcessoDto controleAcesso);
    AuthenticationDto ValidateCredentials(AuthenticationDto authenticationDto, int idUsuario);
    void Create(ControleAcesso controleAcesso);
    void ChangePassword(int idUsuario, string password);
    void RecoveryPassword(string email);
    void RevokeToken(int idUsurio);
}