using Domain.Entities;

namespace Business.Dtos.Core.Profile;
public class LancamentoProfile : AutoMapper.Profile
{
    public LancamentoProfile()
    {
        CreateMap<Business.Dtos.v1.LancamentoDto, Lancamento>().ReverseMap();
        CreateMap<Lancamento, Business.Dtos.v1.LancamentoDto>().ReverseMap();
        CreateMap<Business.Dtos.v1.UsuarioDto, Usuario>().ReverseMap();
        CreateMap<Business.Dtos.v1.CategoriaDto, Categoria>().ReverseMap();
        CreateMap<Business.Dtos.v1.DespesaDto, Despesa>().ReverseMap();
        CreateMap<Business.Dtos.v1.ReceitaDto, Receita>().ReverseMap();


        CreateMap<Business.Dtos.v2.LancamentoDto, Lancamento>().ReverseMap();
        CreateMap<Lancamento, Business.Dtos.v2.LancamentoDto>().ReverseMap();
        CreateMap<Business.Dtos.v2.UsuarioDto, Usuario>().ReverseMap();
        CreateMap<Business.Dtos.v2.CategoriaDto, Categoria>().ReverseMap();
        CreateMap<Business.Dtos.v2.DespesaDto, Despesa>().ReverseMap();
        CreateMap<Business.Dtos.v2.ReceitaDto, Receita>().ReverseMap();
    }
}