using Business.Dtos.Core;

namespace Business.Abstractions;
public interface IControleAcessoBusiness<DtoCa, DtoLogin> where DtoCa : class where DtoLogin : class
{
    AuthenticationDto ValidateCredentials(DtoLogin loginDto);
    AuthenticationDto ValidateCredentials(string refreshToken);
    void Create(DtoCa controleAcessoDto);
    void ChangePassword(Guid idUsuario, string password);
    void RecoveryPassword(string email);
    void RevokeToken(Guid idUsurio);
}