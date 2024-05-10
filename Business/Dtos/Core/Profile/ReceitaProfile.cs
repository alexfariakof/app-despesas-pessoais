using Domain.Entities;

namespace Business.Dtos.Core.Profile;
public class ReceitaProfile: AutoMapper.Profile
{
    public ReceitaProfile()
    {
        CreateMap<BaseReceitaDto, Categoria>().ReverseMap();
        CreateMap<Categoria, BaseReceitaDto>().ReverseMap();
        CreateMap<BaseCategoriaDto, Categoria>().ReverseMap();
        CreateMap<BaseUsuarioDto, Usuario>().ReverseMap();
    }
}