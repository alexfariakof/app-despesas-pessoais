using Domain.Entities;

namespace Business.Dtos.Core.Profile;
public class LancamentoProfile : AutoMapper.Profile
{
    public LancamentoProfile()
    {
        CreateMap<Business.Dtos.v1.LancamentoDto, Lancamento>().ReverseMap();
        CreateMap<Lancamento, Business.Dtos.v1.LancamentoDto>().ReverseMap();


        CreateMap<Business.Dtos.v2.LancamentoDto, Lancamento>().ReverseMap();
        CreateMap<Lancamento, Business.Dtos.v2.LancamentoDto>().ReverseMap();
    }
}