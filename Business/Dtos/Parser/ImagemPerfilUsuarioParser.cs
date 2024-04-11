using Business.Dtos.Parser.Interfaces;
using Domain.Entities;

namespace Business.Dtos.Parser;
public class ImagemPerfilUsuarioParser : IParser<ImagemPerfilVM, ImagemPerfilUsuario>, IParser<ImagemPerfilUsuario, ImagemPerfilVM>
{
    public ImagemPerfilUsuario Parse(ImagemPerfilVM origin)
    {
        if (origin == null) return new ImagemPerfilUsuario();
        return new ImagemPerfilUsuario
        {
            Id = origin.Id,
            Name = origin.Name,
            Type = origin.Type,          
            Url = origin.Url,
            UsuarioId = origin.IdUsuario,
        };
    }

    public ImagemPerfilVM Parse(ImagemPerfilUsuario origin)
    {
        if (origin == null) return new ImagemPerfilVM();
        return new ImagemPerfilVM
        {
            Id = origin.Id,
            Name = origin.Name,
            Type = origin.Type,
            Url = origin.Url,
            IdUsuario = origin.UsuarioId,
        };
    }

    public List<ImagemPerfilUsuario> ParseList(List<ImagemPerfilVM> origin)
    {
        if (origin == null) return new List<ImagemPerfilUsuario>();
        return origin.Select(item => Parse(item)).ToList();
    }

    public List<ImagemPerfilVM> ParseList(List<ImagemPerfilUsuario> origin)
    {
        if (origin == null) return new List<ImagemPerfilVM>();
        return origin.Select(item => Parse(item)).ToList();
    }
}