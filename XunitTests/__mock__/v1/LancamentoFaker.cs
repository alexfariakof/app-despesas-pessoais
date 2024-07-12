﻿using Bogus;
using Business.Dtos.v1;

namespace __mock__.v1;
public sealed class LancamentoFaker
{
    static int counter = 1;
    static int counterVM = 1;

    public static Lancamento GetNewFaker(
        Usuario usuario,
        Despesa despesa,
        Receita receita,
        Categoria categoria
    )
    {
        var lancamentoFaker = new Faker<Lancamento>()
            .RuleFor(l => l.Id, f => Guid.NewGuid())
            .RuleFor(l => l.Valor, f => f.Random.Decimal(1, 90000))
            .RuleFor(l => l.Data, new DateTime(DateTime.Now.Year, new Random().Next(1, 13), 1))
            .RuleFor(l => l.Descricao, f => f.Commerce.ProductName())
            .RuleFor(l => l.UsuarioId, usuario.Id)
            .RuleFor(l => l.Usuario, usuario)
            .RuleFor(l => l.DespesaId, despesa.Id)
            .RuleFor(l => l.Despesa, despesa)
            .RuleFor(l => l.ReceitaId, receita.Id)
            .RuleFor(l => l.Receita, receita)
            .RuleFor(l => l.CategoriaId, categoria.Id)
            .RuleFor(l => l.Categoria, categoria)
            .Generate();
        counter++;
        return lancamentoFaker;
    }

    public static LancamentoDto GetNewFakerVM(
        Guid idUsuario,
        Guid idDespesa,
        Guid idReceita,
        Categoria categoria
    )
    {
        var lancamentoDtoFaker = new Faker<LancamentoDto>()
            .RuleFor(l => l.Id, f => Guid.NewGuid() )
            .RuleFor(l => l.Valor, f => f.Random.Decimal(1, 90000))
            .RuleFor(
                l => l.Data,
                f => new DateTime(DateTime.Now.Year, new Random().Next(1, 13), 1).ToString()
            )
            .RuleFor(l => l.Descricao, f => f.Commerce.ProductName())
            .RuleFor(l => l.UsuarioId, idUsuario)
            .RuleFor(l => l.IdDespesa, idDespesa)
            .RuleFor(l => l.IdReceita, idReceita)            
            .RuleFor(l => l.Categoria, categoria.Descricao)
            .Generate();        
        lancamentoDtoFaker.TipoCategoria = counter % 2 == 0 ? "Despesa" : "Receita";
        counterVM++;
        return lancamentoDtoFaker;
    }

    public static List<LancamentoDto> LancamentoDtos(
        Usuario? usuario = null,
        int? idUsuario = null
    )
    {
        var listLancamentoDto = new List<LancamentoDto>();
        for (int i = 0; i < 10; i++)
        {
            if (idUsuario == null)
                usuario = UsuarioFaker.Instance.GetNewFaker();
            usuario = usuario ?? new();
            var categoria = CategoriaFaker.Instance.GetNewFaker(usuario);

            var despesa = DespesaFaker.Instance.GetNewFaker(usuario, categoria);
            var receita = ReceitaFaker.Instance.GetNewFaker(usuario, categoria);
            var lancamentoDto = GetNewFakerVM(usuario.Id, despesa.Id, receita.Id, categoria);
            listLancamentoDto.Add(lancamentoDto);
        }

        return listLancamentoDto;
    }

    public static List<Lancamento> Lancamentos(Usuario? usuario = null, Guid? idUsuario = null)
    {
        var listLancamento = new List<Lancamento>();
        for (int i = 0; i < 10; i++)
        {
            if (idUsuario == null)
                usuario = UsuarioFaker.Instance.GetNewFaker();

            usuario = usuario ?? new();
            var categoria = CategoriaFaker.Instance.GetNewFaker(usuario, null, usuario.Id);

            var despesa = DespesaFaker.Instance.GetNewFaker(usuario, categoria);
            var receita = ReceitaFaker.Instance.GetNewFaker(usuario, categoria);
            var lancamento = GetNewFaker(usuario, despesa, receita, categoria);
            listLancamento.Add(lancamento);
        }
        return listLancamento;
    }
}
