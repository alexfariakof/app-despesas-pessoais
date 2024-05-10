using Domain.Entities;

namespace Business.Dtos.Core.Profile;
public class ReceitaProfile: AutoMapper.Profile
{
    public ReceitaProfile()
    {
        CreateMap<Business.Dtos.v1.ReceitaDto, Categoria>().ReverseMap();
        CreateMap<Categoria, Business.Dtos.v1.ReceitaDto>().ReverseMap();
        CreateMap<Business.Dtos.v1.CategoriaDto, Categoria>().ReverseMap();
        CreateMap<Business.Dtos.v1.UsuarioDto, Usuario>().ReverseMap();


        CreateMap<Business.Dtos.v2.ReceitaDto, Categoria>().ReverseMap();
        CreateMap<Categoria, Business.Dtos.v2.ReceitaDto>().ReverseMap();
        CreateMap<Business.Dtos.v2.CategoriaDto, Categoria>().ReverseMap();
        CreateMap<Business.Dtos.v2.UsuarioDto, Usuario>().ReverseMap();
    }
}