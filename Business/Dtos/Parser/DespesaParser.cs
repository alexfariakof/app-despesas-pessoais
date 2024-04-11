using Business.Dtos.Parser.Interfaces;
using Domain.Entities;

namespace Business.Dtos.Parser;
public class DespesaParser: IParser<DespesaVM, Despesa>, IParser<Despesa, DespesaVM>
{    
    public Despesa Parse(DespesaVM origin)
    {
        if (origin == null) return new Despesa();
        return new Despesa
        {
            Id  = origin.Id,
            Data = origin.Data,
            Descricao = origin.Descricao,                
            Valor = origin.Valor,
            DataVencimento = origin.DataVencimento,
            CategoriaId = origin.Categoria.Id,
            UsuarioId = origin.IdUsuario,
        };
    }

    public DespesaVM Parse(Despesa origin)
    {
        if (origin == null) return new DespesaVM();
        return new DespesaVM
        {
            Id = origin.Id,
            Data = origin.Data,
            Descricao = origin.Descricao,
            Valor = origin.Valor,
            DataVencimento = origin.DataVencimento,
            Categoria = new CategoriaParser().Parse(origin.Categoria),
            IdUsuario = origin.UsuarioId,
            Usuario = new UsuarioParser().Parse(origin.Usuario),                
        };
    }

    public List<Despesa> ParseList(List<DespesaVM> origin)
    {
        if (origin == null) return new List<Despesa>();
        return origin.Select(item => Parse(item)).ToList();
    }

    public List<DespesaVM> ParseList(List<Despesa> origin)
    {
        if (origin == null) return new List<DespesaVM>();
        return origin.Select(item => Parse(item)).ToList();
    }
}