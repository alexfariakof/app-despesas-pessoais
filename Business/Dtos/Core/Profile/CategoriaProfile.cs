using Domain.Entities;
using Domain.Entities.ValueObjects;

namespace Business.Dtos.Core.Profile;
public class CategoriaProfile: AutoMapper.Profile
{
    public CategoriaProfile()
    {
        CreateMap<Business.Dtos.v1.CategoriaDto, Categoria>()
            .ForMember(dest => dest.TipoCategoria, opt => opt.MapFrom(src => (TipoCategoria.TipoCategoriaType)src.IdTipoCategoria))
            .ReverseMap();
        CreateMap<Categoria, Business.Dtos.v1.CategoriaDto>()
            .ForMember(dest => dest.IdTipoCategoria, opt => opt.MapFrom(src => src.TipoCategoria.Id))
            .ReverseMap();

        CreateMap<Business.Dtos.v2.CategoriaDto, Categoria>()
            .ForMember(dest => dest.TipoCategoria, opt => opt.MapFrom(src => (TipoCategoria.TipoCategoriaType)src.IdTipoCategoria))
            .ReverseMap();
        CreateMap<Categoria, Business.Dtos.v2.CategoriaDto>()
            .ForMember(dest => dest.IdTipoCategoria, opt => opt.MapFrom(src => src.TipoCategoria.Id))
            .ReverseMap();

        CreateMap<TipoCategoriaDto, TipoCategoria>().ReverseMap();
    }
}