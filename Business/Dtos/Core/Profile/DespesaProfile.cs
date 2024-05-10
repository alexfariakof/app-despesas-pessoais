using Domain.Entities;

namespace Business.Dtos.Core.Profile;
public class DespesaProfile: AutoMapper.Profile
{
    public DespesaProfile()
    {

        CreateMap<Business.Dtos.v1.DespesaDto, Despesa>().ReverseMap();
        CreateMap<Despesa, Business.Dtos.v1.DespesaDto>().ReverseMap();


        CreateMap<Business.Dtos.v2.DespesaDto, Despesa>().ReverseMap();
        CreateMap<Despesa, Business.Dtos.v2.DespesaDto>().ReverseMap();

    }
}