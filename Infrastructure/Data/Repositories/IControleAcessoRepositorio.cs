using despesas_backend_api_net_core.Domain.Entities;
using despesas_backend_api_net_core.Domain.VO;

namespace despesas_backend_api_net_core.Infrastructure.Data.Repositories
{
    public interface IControleAcessoRepositorio
    { 
        ControleAcesso FindByEmail(ControleAcesso controleAcesso);
        Usuario GetUsuarioByEmail(string login);
        bool  Create(ControleAcesso controleAcesso);

        bool RecoveryPassword(string email);

    }
}
