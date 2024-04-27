using Business.Dtos;

namespace Business.Abstractions;
public interface ISaldoBusiness
{
    SaldoDto GetSaldo(int idUsuario);
    SaldoDto GetSaldoAnual(DateTime ano, int idUsuario);
    SaldoDto GetSaldoByMesAno(DateTime mesAno, int idUsuario);
}
