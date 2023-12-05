using despesas_backend_api_net_core.Domain.Entities;

namespace despesas_backend_api_net_core.Infrastructure.Data.Repositories
{
    public interface IGraficosRepositorio
    {
        Grafico GetDadosGraficoByAno(int idUsuario, DateTime data);
    }
}
