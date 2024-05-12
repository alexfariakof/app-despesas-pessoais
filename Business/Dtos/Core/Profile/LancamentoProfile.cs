using Domain.Entities;

namespace Business.Dtos.Core.Profile;
public class LancamentoProfile : AutoMapper.Profile
{
    public LancamentoProfile()
    {

        CreateMap<Lancamento, Business.Dtos.v1.LancamentoDto>()
            .ForMember(dest => dest.IdDespesa, opt => opt.MapFrom(src => src.DespesaId))
            .ForMember(dest => dest.IdReceita, opt => opt.MapFrom(src => src.ReceitaId))
            .ForMember(dest => dest.Categoria, opt => opt.MapFrom(src => src.Categoria.Descricao))
            .ForMember(dest => dest.TipoCategoria, opt => opt.MapFrom(src => src.Categoria.TipoCategoria.Name))
            .ForMember(dest => dest.Descricao, opt => opt.MapFrom(src => src.ReceitaId == 0 ? src.Despesa.Descricao : src.Receita.Descricao))
            .ReverseMap();

        CreateMap<Lancamento, Business.Dtos.v2.LancamentoDto>()
            .ForMember(dest => dest.IdDespesa, opt => opt.MapFrom(src => src.DespesaId))
            .ForMember(dest => dest.IdReceita, opt => opt.MapFrom(src => src.ReceitaId))
            .ForMember(dest => dest.Categoria, opt => opt.MapFrom(src => src.Categoria.Descricao))
            .ForMember(dest => dest.TipoCategoria, opt => opt.MapFrom(src => src.Categoria.TipoCategoria.Name))
            .ForMember(dest => dest.Descricao, opt => opt.MapFrom(src => src.ReceitaId == 0 ? src.Despesa.Descricao : src.Receita.Descricao))
            .ReverseMap();

    }
}