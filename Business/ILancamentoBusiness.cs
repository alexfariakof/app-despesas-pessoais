using despesas_backend_api_net_core.Domain.VO;

namespace despesas_backend_api_net_core.Business
{
    public interface ILancamentoBusiness
    {
        List<LancamentoVO> FindByMesAno(DateTime data, int idUsuario);
        decimal GetSaldo(int idUsuario);
    }
}
