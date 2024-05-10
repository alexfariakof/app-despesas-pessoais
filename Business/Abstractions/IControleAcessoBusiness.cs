using Business.Dtos.Core;

namespace Business.Abstractions;
public interface IControleAcessoBusiness
{
    BaseAuthenticationDto ValidateCredentials(BaseLoginDto login);
    BaseAuthenticationDto ValidateCredentials(string refreshToken);
    void Create(BaseControleAcessoDto controleAcessoDto);
    void ChangePassword(int idUsuario, string password);
    void RecoveryPassword(string email);
    void RevokeToken(int idUsurio);
}