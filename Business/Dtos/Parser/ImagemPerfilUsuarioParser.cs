using Business.Dtos.Parser.Interfaces;
using Business.Dtos.v1;
using Domain.Entities;

namespace Business.Dtos.Parser;
public class ImagemPerfilUsuarioParser : IParser<ImagemPerfilDto, ImagemPerfilUsuario>, IParser<ImagemPerfilUsuario, ImagemPerfilDto>
{
    public ImagemPerfilUsuario Parse(ImagemPerfilDto origin)
    {
        if (origin == null) return new ImagemPerfilUsuario();
        return new ImagemPerfilUsuario
        {
            Id = origin.Id,
            Name = origin.Name,
            ContentType = origin.ContentType,
            Url = origin.Url,
            UsuarioId = origin.IdUsuario,
        };
    }

    public ImagemPerfilDto Parse(ImagemPerfilUsuario origin)
    {
        if (origin == null) return new ImagemPerfilDto();
        return new ImagemPerfilDto
        {
            Id = origin.Id,
            Name = origin.Name,
            ContentType = origin.ContentType,
            Url = origin.Url,
            IdUsuario = origin.UsuarioId,
        };
    }

    public List<ImagemPerfilUsuario> ParseList(List<ImagemPerfilDto> origin)
    {
        if (origin == null) return new List<ImagemPerfilUsuario>();
        return origin.Select(item => Parse(item)).ToList();
    }

    public List<ImagemPerfilDto> ParseList(List<ImagemPerfilUsuario> origin)
    {
        if (origin == null) return new List<ImagemPerfilDto>();
        return origin.Select(item => Parse(item)).ToList();
    }
}