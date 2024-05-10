using Domain.Entities;

namespace Business.Dtos.Core.Profile;
public class ControleAcessoProfile: AutoMapper.Profile
{
    public ControleAcessoProfile()
    {
        CreateMap<BaseControleAcessoDto, Categoria>().ReverseMap();
        CreateMap<Categoria, BaseControleAcessoDto>().AfterMap((s, d) =>
        {        
            d.Senha= "********";
        })
        .ReverseMap();
        
        CreateMap<BaseUsuarioDto, Usuario>().ReverseMap();

    }    
}