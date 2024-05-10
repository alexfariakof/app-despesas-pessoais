using Domain.Entities;

namespace Business.Dtos.Core.Profile;
public class CategoriaProfile: AutoMapper.Profile
{
    public CategoriaProfile()
    {
        CreateMap<Business.Dtos.v1.CategoriaDto, Categoria>().ReverseMap();
        CreateMap<Categoria, Business.Dtos.v1.CategoriaDto>().ReverseMap();

        CreateMap<Business.Dtos.v2.CategoriaDto, Categoria>().ReverseMap();
        CreateMap<Categoria, Business.Dtos.v2.CategoriaDto>().ReverseMap();
    }
}