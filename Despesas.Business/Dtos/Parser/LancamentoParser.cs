﻿using Domain.Entities;
using Business.Dtos.Parser.Interfaces;
using Business.Dtos.v1;

namespace Business.Dtos.Parser;
public class LancamentoParser : IParser<LancamentoDto, Lancamento>, IParser<Lancamento, LancamentoDto>
{    
    public Lancamento Parse(Despesa origin)
    {
        if (origin == null) return new Lancamento();
        return new Lancamento
        {
            Id = BitConverter.ToInt32(Guid.NewGuid().ToByteArray(), 0),
            Valor = origin.Valor,
            Data = origin.Data,
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
            Data = origin.Data,
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
    public Lancamento Parse(LancamentoDto origin)
    {
        if (origin == null) return new Lancamento();
        return new Lancamento
        {
            Id = origin.Id,
            DespesaId = origin.IdDespesa,
            ReceitaId = origin.IdReceita,
            UsuarioId = origin.UsuarioId,
            Data = DateTime.Parse(origin?.Data ?? DateTime.Now.ToString()),
            DataCriacao = DateTime.Now,
            Valor = origin.Valor,
            Despesa = new Despesa { Id = origin.IdDespesa, Descricao = origin?.Descricao ?? ""},
            Receita = new Receita { Id = origin?.IdReceita ?? 0, Descricao = origin?.Descricao ?? "" }
        };
    }
    public LancamentoDto Parse(Lancamento origin)
    {
        if (origin == null) return new LancamentoDto();
        return new LancamentoDto
        {
            Id = origin.Id,
            IdDespesa = origin?.DespesaId.GetValueOrDefault() ?? 0,
            IdReceita = origin?.ReceitaId.GetValueOrDefault() ?? 0,
            UsuarioId = origin?.UsuarioId ?? 0,
            Data = origin?.Data.ToShortDateString(),
            Valor = origin?.Valor ?? 0,
            Descricao = origin?.Descricao,
            TipoCategoria = origin?.DespesaId == 0 ? "Receita" : "Despesa",
            Categoria = origin?.Categoria?.Descricao
        };
    }
    public List<Lancamento> ParseList(List<LancamentoDto> origin)
    {
        if (origin == null) return new List<Lancamento>();
        return origin.Select(item => Parse(item)).ToList();
    }

    public List<LancamentoDto> ParseList(List<Lancamento> origin)
    {
        if (origin == null) return new List<LancamentoDto>();
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