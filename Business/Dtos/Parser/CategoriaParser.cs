using Business.Dtos.Parser.Interfaces;
using Business.Dtos.v1;
using Domain.Entities;
using Domain.Entities.ValueObjects;

namespace Business.Dtos.Parser;
public class CategoriaParser: IParser<CategoriaDto, Categoria>, IParser<Categoria, CategoriaDto>
{
    public Categoria Parse(CategoriaDto origin)
    {
        if (origin == null) return new Categoria();
        return new Categoria
        {
            Id = origin.Id,
            Descricao = origin.Descricao,
            TipoCategoria = origin.IdTipoCategoria == 1 ? TipoCategoria.TipoCategoriaType.Despesa : TipoCategoria.TipoCategoriaType.Receita,
            UsuarioId = origin.UsuarioId
        };
    }

    public CategoriaDto Parse(Categoria origin)
    {
        if (origin == null) return new CategoriaDto();
        return new CategoriaDto
        {
            Id = origin.Id,
            Descricao = origin.Descricao,
            IdTipoCategoria = origin.TipoCategoria.Id,
            UsuarioId = origin.UsuarioId                
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
