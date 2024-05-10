using Domain.Entities;

namespace Business.Dtos.Core.Profile;
public class DespesaProfile: AutoMapper.Profile
{
    public DespesaProfile()
    {
        CreateMap<BaseDespesaDto, Categoria>().ReverseMap();
        CreateMap<Categoria, BaseDespesaDto>().ReverseMap();
        CreateMap<BaseCategoriaDto, Categoria>().ReverseMap();
        CreateMap<BaseUsuarioDto, Usuario>().ReverseMap();
    }
}