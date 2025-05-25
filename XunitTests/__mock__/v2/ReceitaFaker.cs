﻿using Bogus;
using Business.Dtos.Core;
using Business.Dtos.v2;
using Domain.Entities.ValueObjects;

namespace __mock__.v2;
public sealed class ReceitaFaker
{
    static int counter = 1;
    static int counterVM = 1;

    private static ReceitaFaker? _instance;
    private static readonly object LockObject = new object();
    public static ReceitaFaker Instance
    {
        get
        {
            lock (LockObject)
            {
                return _instance ??= new ReceitaFaker();
            }
        }
    }


    public Receita GetNewFaker(Usuario usuario, Categoria categoria)
    {
        var receitaFaker = new Faker<Receita>()
            .RuleFor(r => r.Id, f => Guid.NewGuid())
            .RuleFor(r => r.Data, new DateTime(DateTime.Now.Year, new Random().Next(1, 13), 1))
            .RuleFor(r => r.Descricao, f => f.Commerce.ProductName())
            .RuleFor(r => r.Valor, f => f.Random.Decimal(1, 900000))
            .RuleFor(r => r.UsuarioId, usuario.Id)
            .RuleFor(r => r.Usuario, usuario)
            .RuleFor(r => r.Categoria, CategoriaFaker.Instance.GetNewFaker(usuario, (int)TipoCategoria.CategoriaType.Receita, usuario.Id))
            .Generate();
        receitaFaker.Categoria = receitaFaker.Categoria ?? new();
        receitaFaker.CategoriaId = receitaFaker.Categoria.Id;
        counter++;
        return receitaFaker;

    }

    public ReceitaDto GetNewFakerVM(Guid idUsuario, Guid idCategoria)
    {

        var receitaFaker = new Faker<ReceitaDto>()
            .RuleFor(r => r.Id, f => Guid.NewGuid())
            .RuleFor(r => r.Data, new DateTime(DateTime.Now.Year, new Random().Next(1, 13), 1))
            .RuleFor(r => r.Descricao, f => f.Commerce.ProductName())
            .RuleFor(r => r.Valor, f => f.Random.Decimal(1, 900000))
            .RuleFor(r => r.UsuarioId, idUsuario)
            .RuleFor(r => r.IdCategoria, CategoriaFaker.Instance.GetNewFakerVM(UsuarioFaker.Instance.GetNewFakerVM(idUsuario), TipoCategoriaDto.Receita, idUsuario).Id)
            .Generate();
        counterVM++;
        return receitaFaker;
    }

    public List<ReceitaDto> ReceitasVMs(UsuarioDtoBase? usuarioDto = null, Guid? idUsuario = null)
    {
        var listReceitaDto = new List<ReceitaDto>();
        for (int i = 0; i < 10; i++)
        {
            if (idUsuario == null)
                usuarioDto = UsuarioFaker.Instance.GetNewFakerVM(Guid.NewGuid());

            usuarioDto = usuarioDto ?? new UsuarioDto();
            var categoriaDto = CategoriaFaker.Instance.GetNewFakerVM(usuarioDto);

            var receitaDto = GetNewFakerVM(usuarioDto.Id, categoriaDto.Id);
            listReceitaDto.Add(receitaDto);
        }
        return listReceitaDto;
    }

    public List<Receita> Receitas(Usuario? usuario = null, int? idUsuario = null, int count = 10)
    {
        var listReceita = new List<Receita>();
        for (int i = 0; i < count; i++)
        {
            if (idUsuario == null)
                usuario = UsuarioFaker.Instance.GetNewFaker(Guid.NewGuid());

            usuario = usuario ?? new();
            var categoria = CategoriaFaker.Instance.GetNewFaker(usuario, (int)TipoCategoria.CategoriaType.Receita, usuario.Id);

            var receita = GetNewFaker(usuario, categoria);
            listReceita.Add(receita);
        }
        return listReceita;
    }
}
