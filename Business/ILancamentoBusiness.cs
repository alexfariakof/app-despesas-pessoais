using despesas_backend_api_net_core.Domain.Entities;
using despesas_backend_api_net_core.Domain.VM;

namespace despesas_backend_api_net_core.Business
{
    public interface ILancamentoBusiness
    {
        List<LancamentoVM> FindByMesAno(DateTime data, int idUsuario);
        decimal GetSaldo(int idUsuario);
    }
}
