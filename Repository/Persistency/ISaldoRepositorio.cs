namespace Repository.Persistency;
public interface ISaldoRepositorio
{
    decimal GetSaldo(int idUsuario);
    decimal GetSaldoByAno(DateTime ano, int idUsuario);
    decimal GetSaldoByMesAno(DateTime mesAno, int idUsuario);
}
