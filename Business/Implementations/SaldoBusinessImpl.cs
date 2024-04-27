using Business.Abstractions;
using Business.Dtos;
using Repository.Persistency;

namespace Business.Implementations;
public class SaldoBusinessImpl : ISaldoBusiness
{
    private readonly ISaldoRepositorio _repositorio;

    public SaldoBusinessImpl(ISaldoRepositorio repositorio)
    {
        _repositorio = repositorio;
    }
    public SaldoDto GetSaldo(int idUsuario)
    {
        return _repositorio.GetSaldo(idUsuario);
    }
    public SaldoDto GetSaldoAnual(DateTime ano, int idUsuario)
    {
        return _repositorio.GetSaldoByAno(ano, idUsuario);
    }
    public SaldoDto GetSaldoByMesAno(DateTime mesAno, int idUsuario)
    {
        return _repositorio.GetSaldoByMesAno(mesAno, idUsuario);
    }
}
