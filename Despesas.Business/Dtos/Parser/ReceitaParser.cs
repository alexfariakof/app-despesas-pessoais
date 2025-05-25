using Business.Dtos.Parser.Interfaces;
using Business.Dtos.v1;
using Domain.Entities;

namespace Business.Dtos.Parser;
public class ReceitaParser : IParser<ReceitaDto, Receita>, IParser<Receita, ReceitaDto>
{
    public Receita Parse(ReceitaDto origin)
    {
        if (origin == null) return new();
        return new Receita
        {
            Id = origin.Id,
            Data = origin.Data.GetValueOrDefault(),
            Descricao = origin?.Descricao ?? "",
            Valor = origin?.Valor ?? 0,
            CategoriaId = origin.IdCategoria.GetValueOrDefault(),
            UsuarioId = origin.UsuarioId
        };
    }

    public ReceitaDto Parse(Receita origin)
    {
        if (origin == null) return new();
        return new ReceitaDto
        {
            Id = origin.Id,
            Data = origin.Data,
            Descricao = origin.Descricao,
            Valor = origin.Valor,
            IdCategoria = origin?.Categoria?.Id,
            UsuarioId = origin.UsuarioId
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