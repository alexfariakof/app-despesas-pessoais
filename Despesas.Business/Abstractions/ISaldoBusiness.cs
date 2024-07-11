using Business.Dtos.v2;

namespace Business.Abstractions;
public interface ISaldoBusiness
{
    SaldoDto GetSaldo(Guid idUsuario);
    SaldoDto GetSaldoAnual(DateTime ano, Guid idUsuario);
    SaldoDto GetSaldoByMesAno(DateTime mesAno, Guid idUsuario);
}
