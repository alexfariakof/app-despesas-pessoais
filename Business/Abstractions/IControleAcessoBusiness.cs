using Business.Dtos.Core;

namespace Business.Abstractions;
public interface IControleAcessoBusiness<DtoCa, DtoLogin> where DtoCa : class where DtoLogin : class
{
    BaseAuthenticationDto ValidateCredentials(DtoLogin login);
    BaseAuthenticationDto ValidateCredentials(string refreshToken);
    void Create(DtoCa controleAcessoDto);
    void ChangePassword(int idUsuario, string password);
    void RecoveryPassword(string email);
    void RevokeToken(int idUsurio);
}