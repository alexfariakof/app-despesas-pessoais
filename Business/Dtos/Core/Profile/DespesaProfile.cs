using Domain.Entities;
using Domain.Entities.ValueObjects;

namespace Business.Dtos.Core.Profile;
public class DespesaProfile: AutoMapper.Profile
{
    public DespesaProfile()
    {
        CreateMap<Business.Dtos.v1.DespesaDto, Despesa>().ReverseMap();

        CreateMap<Despesa, Business.Dtos.v1.DespesaDto>()
            .ForMember(dest => dest.IdCategoria, opt => opt.MapFrom(src => src.CategoriaId))
            .ForMember(dest => dest.IdCategoria, opt => opt.MapFrom(src => src.Categoria.Id))
            .ReverseMap();


        CreateMap<Business.Dtos.v2.DespesaDto, Despesa>()
            .ReverseMap();

        CreateMap<Despesa, Business.Dtos.v2.DespesaDto>()
            .ForMember(dest => dest.IdCategoria, opt => opt.MapFrom(src => src.CategoriaId))
            .ForMember(dest => dest.IdCategoria, opt => opt.MapFrom(src => src.Categoria.Id))
            .ForMember(dest => dest.Categoria, opt => opt.MapFrom(src => src.Categoria))
            .ForPath(dest => dest.Categoria.IdTipoCategoria, opt => opt.MapFrom(src => TipoCategoria.CategoriaType.Despesa))
            .ReverseMap();


        CreateMap<Categoria, Business.Dtos.v2.CategoriaDto>()
        .ForMember(dest => dest.IdTipoCategoria, opt => opt.MapFrom(src => src.TipoCategoria.Id))
        .ReverseMap();

        CreateMap<TipoCategoriaDto, TipoCategoria>().ReverseMap();
    }
}