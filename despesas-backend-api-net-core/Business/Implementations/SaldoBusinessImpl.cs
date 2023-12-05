using despesas_backend_api_net_core.Infrastructure.Data.Repositories;

namespace despesas_backend_api_net_core.Business.Implementations
{
    public class SaldoBusinessImpl : ISaldoBusiness
    {
        private readonly ISaldoRepositorio _repositorio;

        public SaldoBusinessImpl(ISaldoRepositorio repositorio)
        {
            _repositorio = repositorio;
        }
        public decimal GetSaldo(int idUsuario)
        {
            return _repositorio.GetSaldo(idUsuario);
        }
        public decimal GetSaldoAnual(DateTime ano, int idUsuario)
        {
            return _repositorio.GetSaldoByAno(ano, idUsuario);
        }
        public decimal GetSaldoByMesAno(DateTime mesAno, int idUsuario)
        {
            return _repositorio.GetSaldoByMesAno(mesAno, idUsuario);
        }
    }
}
