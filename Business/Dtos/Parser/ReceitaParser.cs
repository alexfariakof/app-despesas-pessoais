using Business.Dtos.Parser.Interfaces;
using Domain.Entities;

namespace Business.Dtos.Parser;
public class ReceitaParser: IParser<ReceitaDto, Receita>, IParser<Receita, ReceitaDto>
{    
    public Receita Parse(ReceitaDto origin)
    {
        if (origin == null) return new Receita();
        return new Receita
        {
            Id  = origin.Id,
            Data = origin.Data.Value,
            Descricao = origin.Descricao,                
            Valor = origin.Valor,
            CategoriaId = origin.Categoria.Id,
            UsuarioId = origin.IdUsuario
        };
    }

    public ReceitaDto Parse(Receita origin)
    {
        if (origin == null) return new ReceitaDto();
        return new ReceitaDto
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

    public List<Receita> ParseList(List<ReceitaDto> origin)
    {
        if (origin == null) return new List<Receita>();
        return origin.Select(item => Parse(item)).ToList();
    }

    public List<ReceitaDto> ParseList(List<Receita> origin)
    {
        if (origin == null) return new List<ReceitaDto>();
        return origin.Select(item => Parse(item)).ToList();
    }
}