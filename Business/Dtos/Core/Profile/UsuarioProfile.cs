using Domain.Entities;

namespace Business.Dtos.Core.Profile;
public class UsuarioProfile: AutoMapper.Profile
{
    public UsuarioProfile()
    {
        CreateMap<Usuario, BaseUsuarioDto>().ReverseMap();
        CreateMap<BaseUsuarioDto, Usuario>().ReverseMap();
    }
}