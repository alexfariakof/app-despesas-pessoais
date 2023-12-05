using despesas_backend_api_net_core.Domain.Entities;

namespace despesas_backend_api_net_core.Infrastructure.Data.Repositories
{
    public interface ILancamentoRepositorio
    {
        List<Lancamento> FindByMesAno(DateTime data, int idUsuario);
    }
}
