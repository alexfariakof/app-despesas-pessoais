using Domain.Entities;
using Domain.Entities.ValueObjects;

namespace Business.Dtos.Core.Profile;
public class ReceitaProfile : AutoMapper.Profile
{
    public ReceitaProfile()
    {
        CreateMap<Business.Dtos.v1.ReceitaDto, Receita>().ReverseMap();
        CreateMap<Receita, Business.Dtos.v1.ReceitaDto>()
            .ForMember(dest => dest.IdCategoria, opt => opt.MapFrom(src => src.CategoriaId))
            .ForMember(dest => dest.IdCategoria, opt => opt.MapFrom(src => src.Categoria.Id))
            .ReverseMap();

        CreateMap<Business.Dtos.v2.ReceitaDto, Receita>().ReverseMap();
        CreateMap<Receita, Business.Dtos.v2.ReceitaDto>()
            .ForMember(dest => dest.IdCategoria, opt => opt.MapFrom(src => src.CategoriaId))
            .ForMember(dest => dest.IdCategoria, opt => opt.MapFrom(src => src.Categoria.Id))
            .ForMember(dest => dest.Categoria, opt => opt.MapFrom(src => src.Categoria))
            .ForPath(dest => dest.Categoria.IdTipoCategoria, opt => opt.MapFrom(src => TipoCategoria.CategoriaType.Receita))
            .ReverseMap();

        CreateMap<Categoria, Business.Dtos.v2.CategoriaDto>()
            .ForMember(dest => dest.IdTipoCategoria, opt => opt.MapFrom(src => src.TipoCategoria.Id))
            .ReverseMap();

        CreateMap<TipoCategoriaDto, TipoCategoria>().ReverseMap();
    }
}