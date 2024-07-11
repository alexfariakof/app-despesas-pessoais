namespace Repository.Persistency.Abstractions;
public interface ISaldoRepositorio
{
    decimal GetSaldo(Guid idUsuario);
    decimal GetSaldoByAno(DateTime ano, Guid idUsuario);
    decimal GetSaldoByMesAno(DateTime mesAno, Guid idUsuario);
}
