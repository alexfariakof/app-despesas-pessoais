using Business.Dtos.Parser.Interfaces;
using Domain.Entities;

namespace Business.Dtos.Parser;
public class CategoriaParser: IParser<CategoriaVM, Categoria>, IParser<Categoria, CategoriaVM>
{
    public Categoria Parse(CategoriaVM origin)
    {
        if (origin == null) return new Categoria();
        return new Categoria
        {
            Id = origin.Id,
            Descricao = origin.Descricao,
            TipoCategoria = origin.IdTipoCategoria == 1 ? TipoCategoria.Despesa : TipoCategoria.Receita,
            UsuarioId = origin.IdUsuario
        };
    }

    public CategoriaVM Parse(Categoria origin)
    {
        if (origin == null) return new CategoriaVM();
        return new CategoriaVM
        {
            Id = origin.Id,
            Descricao = origin.Descricao,
            IdTipoCategoria = (int)origin.TipoCategoria,
            IdUsuario = origin.UsuarioId                
        };
    }

    public List<Categoria> ParseList(List<CategoriaVM> origin)
    {
        if (origin == null) return new List<Categoria>();
        return origin.Select(item => Parse(item)).ToList();
    }

    public List<CategoriaVM> ParseList(List<Categoria> origin)
    {
        if (origin == null) return new List<CategoriaVM>();
        return origin.Select(item => Parse(item)).ToList();
    }
}
