using Business.Dtos.Parser.Interfaces;
using Domain.Entities;

namespace Business.Dtos.Parser;
public class ReceitaParser: IParser<ReceitaVM, Receita>, IParser<Receita, ReceitaVM>
{    
    public Receita Parse(ReceitaVM origin)
    {
        if (origin == null) return new Receita();
        return new Receita
        {
            Id  = origin.Id,
            Data = origin.Data,
            Descricao = origin.Descricao,                
            Valor = origin.Valor,
            CategoriaId = origin.Categoria.Id,
            UsuarioId = origin.IdUsuario
        };
    }

    public ReceitaVM Parse(Receita origin)
    {
        if (origin == null) return new ReceitaVM();
        return new ReceitaVM
        {
            Id = origin.Id,
            Data = origin.Data,
            Descricao = origin.Descricao,
            Valor = origin.Valor,                
            Categoria = new CategoriaParser().Parse(origin.Categoria),
            IdUsuario = origin.UsuarioId,
            Usuario = new UsuarioParser().Parse(origin.Usuario)                
        };
    }

    public List<Receita> ParseList(List<ReceitaVM> origin)
    {
        if (origin == null) return new List<Receita>();
        return origin.Select(item => Parse(item)).ToList();
    }

    public List<ReceitaVM> ParseList(List<Receita> origin)
    {
        if (origin == null) return new List<ReceitaVM>();
        return origin.Select(item => Parse(item)).ToList();
    }
}