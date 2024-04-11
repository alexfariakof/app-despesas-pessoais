using Business.Dtos;
using Business.Dtos.Parser;
using Repository.Persistency;

namespace Business.Implementations;
public class LancamentoBusinessImpl : ILancamentoBusiness
{
    private readonly ILancamentoRepositorio _repositorio;
    private readonly LancamentoParser _converter;
    public LancamentoBusinessImpl(ILancamentoRepositorio repositorio)
    {
        _repositorio = repositorio;
        _converter = new LancamentoParser();
    }
    public List<LancamentoVM> FindByMesAno(DateTime data, int idUsuario)
    {
       return  _converter.ParseList(_repositorio.FindByMesAno(data, idUsuario));
    }
}