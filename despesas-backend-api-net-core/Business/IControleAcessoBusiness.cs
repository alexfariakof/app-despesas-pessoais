using despesas_backend_api_net_core.Domain.Entities;
using despesas_backend_api_net_core.Domain.VM;

namespace despesas_backend_api_net_core.Business
{
    public interface IControleAcessoBusiness
    {
        AuthResponse FindByLogin(ControleAcesso controleAcesso);
        bool Create(ControleAcesso controleAcesso);
        bool ChangePassword(int idUsuario, string password);
        bool RecoveryPassword(string email);     
    }
}