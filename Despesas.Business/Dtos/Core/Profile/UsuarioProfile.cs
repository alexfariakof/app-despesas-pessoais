using Domain.Entities;

namespace Business.Dtos.Core.Profile;
public class UsuarioProfile : AutoMapper.Profile
{
    public UsuarioProfile()
    {

        CreateMap<Business.Dtos.v1.UsuarioDto, Usuario>().ReverseMap();
        CreateMap<Usuario, Business.Dtos.v1.UsuarioDto>().ReverseMap();

        CreateMap<Business.Dtos.v2.UsuarioDto, Usuario>().ReverseMap();
        CreateMap<Usuario, Business.Dtos.v2.UsuarioDto>().ReverseMap();
    }
}