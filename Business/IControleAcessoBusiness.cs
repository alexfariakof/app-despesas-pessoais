using despesas_backend_api_net_core.Domain.Entities;
using despesas_backend_api_net_core.Domain.VO;

namespace despesas_backend_api_net_core.Business
{
    public interface IControleAcessoBusiness
    {
        object FindByLogin(ControleAcesso controleAcesso);
        bool Create(ControleAcesso controleAcesso);
        bool RecoveryPassword(string email);
    }
}
