using Business.Dtos.Parser.Interfaces;
using Business.Dtos.v1;
using Domain.Entities;
using Domain.Entities.ValueObjects;

namespace Business.Dtos.Parser;
public class CategoriaParser: IParser<CategoriaDto, Categoria>, IParser<Categoria, CategoriaDto>
{
    public Categoria Parse(CategoriaDto origin)
    {
        if (origin == null) return new();
        return new Categoria
        {
            Id = origin.Id,
            Descricao = origin.Descricao ?? "",
            TipoCategoria = origin?.IdTipoCategoria == 1 ? (int)TipoCategoria.CategoriaType.Despesa : (int)TipoCategoria.CategoriaType.Receita,
            UsuarioId = origin?.UsuarioId ?? 0
        };
    }

    public CategoriaDto Parse(Categoria origin)
    {
        if (origin == null) return new();
        return new CategoriaDto
        {
            Id = origin.Id,
            Descricao = origin.Descricao,
            IdTipoCategoria = origin?.TipoCategoria?.Id ?? 0,
            UsuarioId = origin?.UsuarioId ?? 0
        };
    }

    public List<Categoria> ParseList(List<CategoriaDto> origin)
    {
        if (origin == null) return new List<Categoria>();
        return origin.Select(item => Parse(item)).ToList();
    }

    public List<CategoriaDto> ParseList(List<Categoria> origin)
    {
        if (origin == null) return new List<CategoriaDto>();
        return origin.Select(item => Parse(item)).ToList();
    }
}
