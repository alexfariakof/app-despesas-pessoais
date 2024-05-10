using Domain.Entities;

namespace Business.Dtos.Core.Profile;
public class LancamentoProfile : AutoMapper.Profile
{
    public LancamentoProfile()
    {
        CreateMap<BaseLancamentoDto, Lancamento>().ReverseMap();
        CreateMap<Lancamento, BaseLancamentoDto>().ReverseMap();
        CreateMap<BaseUsuarioDto, Usuario>().ReverseMap();
        CreateMap<BaseCategoriaDto, Categoria>().ReverseMap();
        CreateMap<BaseDespesaDto, Despesa>().ReverseMap();
        CreateMap<BaseReceitaDto, Receita>().ReverseMap();
    }
}