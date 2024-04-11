using System.Globalization;
using Domain.Entities;
using Business.Dtos.Parser.Interfaces;

namespace Business.Dtos.Parser;
public class LancamentoParser : IParser<LancamentoVM, Lancamento>, IParser<Lancamento, LancamentoVM>
{    
    public Lancamento Parse(Despesa origin)
    {
        if (origin == null) return new Lancamento();
        return new Lancamento
        {
            Id = BitConverter.ToInt32(Guid.NewGuid().ToByteArray(), 0),
            Valor = origin.Valor,
            Data = DateTime.Parse(origin.Data.ToString(), new CultureInfo("pt-BR")),
            Descricao = origin.Descricao,
            UsuarioId = origin.UsuarioId,
            Usuario = origin.Usuario,
            DespesaId = origin.Id,
            Despesa = origin,
            ReceitaId = 0,
            Receita = new Receita (),
            CategoriaId = origin.CategoriaId,
            Categoria = origin.Categoria,
            DataCriacao = DateTime.Now
        };
    }
    public Lancamento Parse(Receita origin)
    {
        if (origin == null) return new Lancamento();
        return new Lancamento
        {
            Id = BitConverter.ToInt32(Guid.NewGuid().ToByteArray(), 0),
            Valor = origin.Valor,
            Data = DateTime.Parse(origin.Data.ToString(), new CultureInfo("pt-BR")),
            Descricao = origin.Descricao,
            UsuarioId = origin.UsuarioId,
            Usuario = origin.Usuario,
            DespesaId = 0,
            Despesa = new Despesa(),
            ReceitaId = origin.Id,
            Receita = origin,
            CategoriaId = origin.CategoriaId,
            Categoria = origin.Categoria,
            DataCriacao = DateTime.Now
        };
    }
    public Lancamento Parse(LancamentoVM origin)
    {
        if (origin == null) return new Lancamento();
        return new Lancamento
        {
            Id = origin.Id,
            DespesaId = origin.IdDespesa,
            ReceitaId = origin.IdReceita,
            UsuarioId = origin.IdUsuario,
            Data = DateTime.Parse(origin.Data, new CultureInfo("pt-BR")),
            DataCriacao = DateTime.Now,
            Valor = origin.Valor,
            Despesa = new Despesa { Id = origin.IdDespesa, Descricao = origin.Descricao },
            Receita = new Receita { Id = origin.IdReceita, Descricao = origin.Descricao }
        };
    }
    public LancamentoVM Parse(Lancamento origin)
    {
        if (origin == null) return new LancamentoVM();
        return new LancamentoVM
        {
            Id = origin.Id,
            IdDespesa = origin?.DespesaId.Value ?? 0,
            IdReceita = origin?.ReceitaId.Value ?? 0,
            IdUsuario = origin.UsuarioId,
            Data = origin.Data.ToShortDateString(),
            Valor = origin.Valor,
            Descricao = origin.Descricao,
            TipoCategoria = origin.DespesaId == 0 ? "Receita" : "Despesa",
            Categoria = origin.Categoria.Descricao
        };
    }
    public List<Lancamento> ParseList(List<LancamentoVM> origin)
    {
        if (origin == null) return new List<Lancamento>();
        return origin.Select(item => Parse(item)).ToList();
    }

    public List<LancamentoVM> ParseList(List<Lancamento> origin)
    {
        if (origin == null) return new List<LancamentoVM>();
        return origin.Select(item => Parse(item)).ToList();
    }
    public List<Lancamento> ParseList(List<Despesa> origin)
    {
        if (origin == null) return new List<Lancamento>();
        return origin.Select(item => Parse(item)).ToList();
    }
    public List<Lancamento> ParseList(List<Receita> origin)
    {
        if (origin == null) return new List<Lancamento>();
        return origin.Select(item => Parse(item)).ToList();
    }
}