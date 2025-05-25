using Domain.Entities;

namespace Business.Dtos.Core.Profile;
public class ControleAcessoProfile : AutoMapper.Profile
{
    public ControleAcessoProfile()
    {
        CreateMap<Business.Dtos.v1.ControleAcessoDto, Usuario>().ReverseMap();
        CreateMap<ControleAcesso, Business.Dtos.v1.ControleAcessoDto>().AfterMap((s, d) =>
        {
            d.Senha = "********";
        }).ReverseMap();


        CreateMap<Business.Dtos.v2.ControleAcessoDto, Usuario>().ReverseMap();
        CreateMap<ControleAcesso, Business.Dtos.v2.ControleAcessoDto>().AfterMap((s, d) =>
        {
            d.Senha = "********";
        }).ReverseMap();
    }
}