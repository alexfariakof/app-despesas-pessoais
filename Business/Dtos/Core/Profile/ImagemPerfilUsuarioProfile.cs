using Domain.Entities;

namespace Business.Dtos.Core.Profile;
public class ImagemPerfilUsuarioProfile : AutoMapper.Profile
{
    public ImagemPerfilUsuarioProfile()
    {
        CreateMap<Business.Dtos.v1.ImagemPerfilDto, ImagemPerfilUsuario>().ReverseMap();
        CreateMap<ImagemPerfilUsuario, Business.Dtos.v1.ImagemPerfilDto>().ReverseMap();

        CreateMap<Business.Dtos.v2.ImagemPerfilDto, ImagemPerfilUsuario>().ReverseMap();
        CreateMap<ImagemPerfilUsuario, Business.Dtos.v2.ImagemPerfilDto>().ReverseMap();

        CreateMap<Business.Dtos.v1.UsuarioDto, Usuario>().ReverseMap();
        CreateMap<Business.Dtos.v2.UsuarioDto, Usuario>().ReverseMap();
    }
}