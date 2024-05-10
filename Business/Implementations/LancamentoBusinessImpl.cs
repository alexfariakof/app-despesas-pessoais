using AutoMapper;
using Business.Abstractions;
using Business.Dtos.Core;
using Repository.Persistency;

namespace Business.Implementations;
public class LancamentoBusinessImpl : ILancamentoBusiness
{
    private readonly ILancamentoRepositorio _repositorio;
    private readonly IMapper _mapper;
    public LancamentoBusinessImpl(IMapper mapper, ILancamentoRepositorio repositorio)
    {
        _mapper = mapper;
        _repositorio = repositorio;

    }
    public List<BaseLancamentoDto> FindByMesAno(DateTime data, int idUsuario)
    {
       return  _mapper.Map<List<BaseLancamentoDto>>(_repositorio.FindByMesAno(data, idUsuario));
    }
}