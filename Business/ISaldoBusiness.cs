namespace Business;
public interface ISaldoBusiness
{
    decimal GetSaldo(int idUsuario);
    decimal GetSaldoAnual(DateTime ano, int idUsuario);
    decimal GetSaldoByMesAno(DateTime mesAno, int idUsuario);
}
