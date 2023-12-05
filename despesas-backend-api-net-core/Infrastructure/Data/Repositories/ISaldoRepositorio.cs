namespace despesas_backend_api_net_core.Infrastructure.Data.Repositories
{
    public interface ISaldoRepositorio
    {
        decimal GetSaldo(int idUsuario);
        decimal GetSaldoByAno(DateTime ano, int idUsuario);
        decimal GetSaldoByMesAno(DateTime mesAno, int idUsuario);
    }
}
