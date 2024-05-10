using Domain.Entities;

namespace Business.Dtos.Core.Profile;
public class DespesaProfile: AutoMapper.Profile
{
    public DespesaProfile()
    {

        CreateMap<Business.Dtos.v1.DespesaDto, Categoria>().ReverseMap();
        CreateMap<Categoria, Business.Dtos.v1.DespesaDto>().ReverseMap();
        CreateMap<Business.Dtos.v1.CategoriaDto, Categoria>().ReverseMap();
        CreateMap<Business.Dtos.v1.UsuarioDto, Usuario>().ReverseMap();


        CreateMap<Business.Dtos.v2.DespesaDto, Categoria>().ReverseMap();
        CreateMap<Categoria, Business.Dtos.v2.DespesaDto>().ReverseMap();
        CreateMap<Business.Dtos.v2.CategoriaDto, Categoria>().ReverseMap();
        CreateMap<Business.Dtos.v2.UsuarioDto, Usuario>().ReverseMap();

    }
}