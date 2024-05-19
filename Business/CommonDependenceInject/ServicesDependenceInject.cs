using Business.Abstractions;
using Business.Abstractions.Generic;
using Business.Implementations;
using Domain.Core;
using Domain.Core.Interfaces;
using Domain.Entities;
using Microsoft.Extensions.DependencyInjection;
using Repository.Persistency.UnitOfWork;
using Repository.Persistency.UnitOfWork.Abstractions;

namespace Business.CommonDependenceInject;
public static class ServicesDependenceInject
{
    public static IServiceCollection AddServices(this IServiceCollection services)
    {
        services.AddScoped(typeof(IBusiness<Business.Dtos.v1.CategoriaDto, Categoria>), typeof(CategoriaBusinessImpl<Business.Dtos.v1.CategoriaDto>));
        services.AddScoped(typeof(IBusiness<Business.Dtos.v1.DespesaDto, Despesa>), typeof(DespesaBusinessImpl<Business.Dtos.v1.DespesaDto>));
        services.AddScoped(typeof(IBusiness<Business.Dtos.v1.ReceitaDto, Receita>), typeof(ReceitaBusinessImpl<Business.Dtos.v1.ReceitaDto>));
        services.AddScoped(typeof(IControleAcessoBusiness<Business.Dtos.v1.ControleAcessoDto, Business.Dtos.v1.LoginDto>), typeof(ControleAcessoBusinessImpl<Business.Dtos.v1.ControleAcessoDto, Business.Dtos.v1.LoginDto>));
        services.AddScoped(typeof(ILancamentoBusiness<Business.Dtos.v1.LancamentoDto>), typeof(LancamentoBusinessImpl<Business.Dtos.v1.LancamentoDto>));
        services.AddScoped(typeof(IUsuarioBusiness<Business.Dtos.v1.UsuarioDto>), typeof(UsuarioBusinessImpl<Business.Dtos.v1.UsuarioDto>));
        services.AddScoped(typeof(IImagemPerfilUsuarioBusiness<Business.Dtos.v1.ImagemPerfilDto, Business.Dtos.v1.UsuarioDto>), typeof(ImagemPerfilUsuarioBusinessImpl<Business.Dtos.v1.ImagemPerfilDto, Business.Dtos.v1.UsuarioDto>));

        services.AddScoped(typeof(IUnitOfWork<>), typeof(UnitOfWork<>));
        services.AddScoped(typeof(BusinessBase<Business.Dtos.v2.CategoriaDto, Categoria>), typeof(CategoriaBusinessImpl<Business.Dtos.v2.CategoriaDto>));
        services.AddScoped(typeof(BusinessBase<Business.Dtos.v2.DespesaDto, Despesa>), typeof(DespesaBusinessImpl<Business.Dtos.v2.DespesaDto>));
        services.AddScoped(typeof(BusinessBase<Business.Dtos.v2.ReceitaDto, Receita>), typeof(ReceitaBusinessImpl<Business.Dtos.v2.ReceitaDto>));

        services.AddScoped(typeof(IControleAcessoBusiness<Business.Dtos.v2.ControleAcessoDto, Business.Dtos.v2.LoginDto>), typeof(ControleAcessoBusinessImpl<Business.Dtos.v2.ControleAcessoDto, Business.Dtos.v2.LoginDto>));
        services.AddScoped(typeof(ILancamentoBusiness<Business.Dtos.v2.LancamentoDto>), typeof(LancamentoBusinessImpl<Business.Dtos.v2.LancamentoDto>));
        services.AddScoped(typeof(IUsuarioBusiness<Business.Dtos.v2.UsuarioDto>), typeof(UsuarioBusinessImpl<Business.Dtos.v2.UsuarioDto>));
        services.AddScoped(typeof(IImagemPerfilUsuarioBusiness<Business.Dtos.v2.ImagemPerfilDto, Business.Dtos.v2.UsuarioDto>), typeof(ImagemPerfilUsuarioBusinessImpl<Business.Dtos.v2.ImagemPerfilDto, Business.Dtos.v2.UsuarioDto>));
        services.AddScoped(typeof(ISaldoBusiness), typeof(SaldoBusinessImpl));
        services.AddScoped(typeof(IGraficosBusiness), typeof(GraficosBusinessImpl));
        services.AddScoped<IEmailSender, EmailSender>();
        return services;
    }
}


