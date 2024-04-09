using Domain.VM;
using Repository.Mapping;
using Repository.Persistency;

namespace Business.Implementations;
public class LancamentoBusinessImpl : ILancamentoBusiness
{
    private readonly ILancamentoRepositorio _repositorio;
    private readonly LancamentoMap _converter;
    public LancamentoBusinessImpl(ILancamentoRepositorio repositorio)
    {
        _repositorio = repositorio;
        _converter = new LancamentoMap();
    }
    public List<LancamentoVM> FindByMesAno(DateTime data, int idUsuario)
    {
       return  _converter.ParseList(_repositorio.FindByMesAno(data, idUsuario));
    }
}