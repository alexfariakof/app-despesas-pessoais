using Domain.Entities;

namespace Business.Dtos.Core.Profile;
public class ControleAcessoProfile: AutoMapper.Profile
{
    public ControleAcessoProfile()
    {
        CreateMap<Business.Dtos.v1.ControleAcessoDto, Categoria>().ReverseMap();
        CreateMap<Categoria, Business.Dtos.v1.ControleAcessoDto>().AfterMap((s, d) =>
        {        
            d.Senha= "********";
        })
        .ReverseMap();
        
        CreateMap<Business.Dtos.v1.UsuarioDto, Usuario>().ReverseMap();


        CreateMap<Business.Dtos.v2.ControleAcessoDto, Categoria>().ReverseMap();
        CreateMap<Categoria, Business.Dtos.v2.ControleAcessoDto>().AfterMap((s, d) =>
        {
            d.Senha = "********";
        })
        .ReverseMap();

        CreateMap<Business.Dtos.v2.UsuarioDto, Usuario>().ReverseMap();

    }    
}