using Domain.Entities;

namespace Business.Dtos.Core.Profile;
public class ReceitaProfile: AutoMapper.Profile
{
    public ReceitaProfile()
    {
        CreateMap<Business.Dtos.v1.ReceitaDto, Receita>().ReverseMap();
        CreateMap<Receita, Business.Dtos.v1.ReceitaDto>().ReverseMap();
        
        CreateMap<Business.Dtos.v2.ReceitaDto, Receita>().ReverseMap();
        CreateMap<Receita, Business.Dtos.v2.ReceitaDto>().ReverseMap();
    }
}