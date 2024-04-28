using Business.Dtos;

namespace Business.Abstractions;
public interface IControleAcessoBusiness
{
    AuthenticationDto ValidateCredentials(LoginDto login);
    AuthenticationDto ValidateCredentials(string refreshToken);
    void Create(ControleAcessoDto controleAcessoDto);
    void ChangePassword(int idUsuario, string password);
    void RecoveryPassword(string email);
    void RevokeToken(int idUsurio);
}