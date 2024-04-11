using Business.Dtos.Parser.Interfaces;
using Domain.Entities;

namespace Business.Dtos.Parser;
public class UsuarioParser : IParser<UsuarioVM, Usuario>, IParser<Usuario, UsuarioVM>
{
    public Usuario Parse(UsuarioVM origin)
    {
        if (origin == null) return new Usuario();
        return new Usuario
        {
            Id  = origin.Id,
            Email = origin.Email,
            Nome = origin.Nome,
            SobreNome = origin.SobreNome,
            Telefone = origin.Telefone,
            PerfilUsuario = origin.PerfilUsuario                
        };
    }

    public UsuarioVM Parse(Usuario origin)
    {
        if (origin == null) return new UsuarioVM();
        return new UsuarioVM
        {
            Id = origin.Id,
            Email = origin.Email,
            Nome = origin.Nome,
            SobreNome = origin.SobreNome,
            Telefone = origin.Telefone,
            PerfilUsuario = origin.PerfilUsuario
        };
    }

    public List<Usuario> ParseList(List<UsuarioVM> origin)
    {
        if (origin == null) return new List<Usuario>();
        return origin.Select(item => Parse(item)).ToList();
    }

    public List<UsuarioVM> ParseList(List<Usuario> origin)
    {
        if (origin == null) return new List<UsuarioVM>();
        return origin.Select(item => Parse(item)).ToList();
    }
}