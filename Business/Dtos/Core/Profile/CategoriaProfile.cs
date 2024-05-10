using Domain.Entities;

namespace Business.Dtos.Core.Profile;
public class CategoriaProfile: AutoMapper.Profile
{
    public CategoriaProfile()
    {
        CreateMap<BaseCategoriaDto, Categoria>().ReverseMap();
        CreateMap<Categoria, BaseCategoriaDto>().ReverseMap();
    }
}