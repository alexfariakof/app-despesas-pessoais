using despesas_backend_api_net_core.Domain.Entities;

namespace despesas_backend_api_net_core.Business
{
    public interface IGraficosBusiness
    {
        Grafico GetDadosGraficoByAnoByIdUsuario(int idUsuario, DateTime data);
    }
}
