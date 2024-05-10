using Domain.Entities;

namespace Business.Dtos.Core.Profile;
public class ImagemPerfilUsuarioProfile : AutoMapper.Profile
{
    public ImagemPerfilUsuarioProfile()
    {
        CreateMap<BaseImagemPerfilDto, ImagemPerfilUsuario>().ReverseMap();
        CreateMap<ImagemPerfilUsuario, BaseImagemPerfilDto>().ReverseMap();
    }
}