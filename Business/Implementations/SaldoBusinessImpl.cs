using Business.Abstractions;
using Repository.Persistency;

namespace Business.Implementations;
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
