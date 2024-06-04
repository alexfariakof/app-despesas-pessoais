using Business.Dtos.Parser.Interfaces;
using Business.Dtos.v1;
using Domain.Entities;

namespace Business.Dtos.Parser;
public class DespesaParser: IParser<DespesaDto, Despesa>, IParser<Despesa, DespesaDto>
{    
    public Despesa Parse(DespesaDto origin)
    {
        if (origin == null) return new Despesa();
        return new Despesa
        {
            Id  = origin.Id,
            Data = origin.Data.GetValueOrDefault(),
            Descricao = origin?.Descricao ?? "",                
            Valor = origin?.Valor ?? 0,
            DataVencimento = origin?.DataVencimento,
            CategoriaId = origin?.IdCategoria.GetValueOrDefault() ?? 0,
            UsuarioId = origin?.UsuarioId ?? 0,
        };
    }

    public DespesaDto Parse(Despesa origin)
    {
        if (origin == null) return new();
        return new DespesaDto
        {
            Id = origin.Id,
            Data = origin.Data,
            Descricao = origin.Descricao,
            Valor = origin.Valor,
            DataVencimento = origin.DataVencimento,
            IdCategoria = origin?.Categoria?.Id,
            UsuarioId = origin?.UsuarioId ?? 0     
        };
    }

    public List<Despesa> ParseList(List<DespesaDto> origin)
    {
        if (origin == null) return new List<Despesa>();
        return origin.Select(item => Parse(item)).ToList();
    }

    public List<DespesaDto> ParseList(List<Despesa> origin)
    {
        if (origin == null) return new List<DespesaDto>();
        return origin.Select(item => Parse(item)).ToList();
    }
}