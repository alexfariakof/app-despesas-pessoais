using despesas_backend_api_net_core.Domain.Entities;
using despesas_backend_api_net_core.Infrastructure.Data.Repositories;

namespace despesas_backend_api_net_core.Business.Implementations
{
    public class GraficosBusinessImpl : IGraficosBusiness
    {
        private readonly IGraficosRepositorio _repositorio;

        public GraficosBusinessImpl(IGraficosRepositorio repositorio)
        {
            _repositorio = repositorio;
        }
        public Grafico GetDadosGraficoByAnoByIdUsuario(int idUsuario, DateTime data)
        {
            return _repositorio.GetDadosGraficoByAno(idUsuario, data);
        }
    }
}
