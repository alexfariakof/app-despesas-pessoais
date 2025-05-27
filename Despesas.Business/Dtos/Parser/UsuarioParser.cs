using Business.Dtos.Parser.Interfaces;
using Business.Dtos.v1;
using Domain.Entities;

namespace Business.Dtos.Parser;
public class UsuarioParser : IParser<UsuarioDto, Usuario>, IParser<Usuario, UsuarioDto>
{
    public Usuario Parse(UsuarioDto origin)
    {
        if (origin == null) return new();
        return new Usuario
        {
            Id = origin.Id,
            Email = origin?.Email ?? "",
            Nome = origin?.Nome ?? "",
            SobreNome = origin?.SobreNome ?? "",
            Telefone = origin?.Telefone ?? "",
            PerfilUsuario = origin?.PerfilUsuario
        };
    }

    public UsuarioDto Parse(Usuario origin)
    {
        if (origin == null) return new();
        return new UsuarioDto
        {
            Id = origin.Id,
            Email = origin.Email,
            Nome = origin.Nome,
            SobreNome = origin.SobreNome,
            Telefone = origin.Telefone,
            PerfilUsuario = origin?.PerfilUsuario ?? new()
        };
    }

    public List<Usuario> ParseList(List<UsuarioDto> origin)
    {
        if (origin == null) return new List<Usuario>();
        return origin.Select(item => Parse(item)).ToList();
    }

    public List<UsuarioDto> ParseList(List<Usuario> origin)
    {
        if (origin == null) return new List<UsuarioDto>();
        return origin.Select(item => Parse(item)).ToList();
    }
}