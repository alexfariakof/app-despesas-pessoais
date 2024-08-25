﻿using Bogus;
using Business.Dtos.Core;
using Business.Dtos.v2;
using Domain.Entities.ValueObjects;

namespace __mock__.v2;
public sealed class CategoriaFaker
{
    static int counter = 1;
    static int counterVM = 1;

    private static CategoriaFaker? _instance;
    private static readonly object LockObject = new object();
    public static CategoriaFaker Instance
    {
        get
        {
            lock (LockObject)
            {
                return _instance ??= new CategoriaFaker();
            }
        }
    }

    public Categoria GetNewFaker(Usuario usuario, TipoCategoria? tipoCategoria = null, Guid? idUsuario = null)
    {
        if (idUsuario == null)
            usuario = UsuarioFaker.Instance.GetNewFaker();

        var categoriaFaker = new Faker<Categoria>()
            .RuleFor(c => c.Id, Guid.NewGuid())
            .RuleFor(c => c.Descricao, f => f.Commerce.Categories(10)[1])
            .RuleFor(c => c.UsuarioId, usuario.Id)
            .RuleFor(c => c.Usuario, usuario)
            .Generate();
        categoriaFaker.TipoCategoria = tipoCategoria == null ? counter % 2 == 0 ? new TipoCategoria(TipoCategoria.CategoriaType.Receita) : new TipoCategoria(TipoCategoria.CategoriaType.Despesa) : tipoCategoria;
        counter++;
        return categoriaFaker;
    }

    public CategoriaDto GetNewFakerVM(UsuarioDtoBase BaseUsuarioDto, TipoCategoriaDto? tipoCategoria = null, Guid? idUsuario = null)
    {
        if (idUsuario == null)
            BaseUsuarioDto = UsuarioFaker.Instance.GetNewFakerVM();

        var categoriaFaker = new Faker<CategoriaDto>()
        .RuleFor(c => c.Id, Guid.NewGuid())
        .RuleFor(c => c.Descricao, f => f.Commerce.ProductName())
        .RuleFor(c => c.UsuarioId, f => BaseUsuarioDto.Id)
        .Generate();
        categoriaFaker.IdTipoCategoria = tipoCategoria.Equals(null) ? counter % 2 == 0 ? TipoCategoriaDto.Despesa : TipoCategoriaDto.Receita : (TipoCategoriaDto)tipoCategoria;
        counterVM++;
        return categoriaFaker;
    }

    public List<CategoriaDto> CategoriasVMs(UsuarioDtoBase? baseUsuarioDto = null, TipoCategoriaDto tipoCategoria = TipoCategoriaDto.Todas, Guid? idUsuario = null)
    {
        var listCategoriaDto = new List<CategoriaDto>();
        for (int i = 0; i < 10; i++)
        {
            if (idUsuario == null)
                baseUsuarioDto = UsuarioFaker.Instance.GetNewFakerVM(Guid.NewGuid());

            baseUsuarioDto = baseUsuarioDto ?? new UsuarioDto();
            var categoriaDto = GetNewFakerVM(baseUsuarioDto, tipoCategoria);
            listCategoriaDto.Add(categoriaDto);
        }
        return listCategoriaDto;
    }

    public List<Categoria> Categorias(Usuario? usuario = null, TipoCategoria? tipoCategoria = null, Guid? idUsuario = null)
    {
        var listCategoria = new List<Categoria>();
        for (int i = 0; i < 10; i++)
        {
            Categoria categoria;

            if (idUsuario == null)
            {
                usuario = UsuarioFaker.Instance.GetNewFaker(Guid.NewGuid());
                categoria = GetNewFaker(usuario, tipoCategoria);
            }
            else
            {
                usuario = usuario ?? new();
                categoria = GetNewFaker(usuario, tipoCategoria, idUsuario);
            }

            listCategoria.Add(categoria);

        }
        return listCategoria;
    }
}
