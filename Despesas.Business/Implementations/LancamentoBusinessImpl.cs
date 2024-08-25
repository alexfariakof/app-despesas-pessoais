using AutoMapper;
using Business.Abstractions;
using Repository.Persistency.Abstractions;

namespace Business.Implementations;
public class LancamentoBusinessImpl<Dto> : ILancamentoBusiness<Dto> where Dto : class, new()
{
    private readonly ILancamentoRepositorio _repositorio;
    private readonly IMapper _mapper;
    public LancamentoBusinessImpl(IMapper mapper, ILancamentoRepositorio repositorio)
    {
        _mapper = mapper;
        _repositorio = repositorio;

    }

    public List<Dto> FindByMesAno(DateTime data, Guid idUsuario)
    {
       return  _mapper.Map<List<Dto>>(_repositorio.FindByMesAno(data, idUsuario));
    }
}