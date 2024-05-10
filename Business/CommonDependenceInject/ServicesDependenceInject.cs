using Business.Abstractions;
using Business.Dtos.Core;
using Business.Generic;
using Business.Implementations;
using Domain.Core;
using Domain.Core.Interfaces;
using Domain.Entities;
using Domain.Entities.Abstractions;
using Microsoft.Extensions.DependencyInjection;
using Repository.Persistency.UnitOfWork;

namespace Business.CommonDependenceInject;
public static class ServicesDependenceInject
{
    public static IServiceCollection AddServices(this IServiceCollection services)
    {
        services.AddTransient(typeof(IBusiness<>), typeof(GenericBusiness<>));
        services.AddTransient(typeof(IBusiness<BaseCategoriaDto>), typeof(CategoriaBusinessImpl));
        services.AddTransient(typeof(IBusiness<BaseDespesaDto>), typeof(DespesaBusinessImpl));
        services.AddTransient(typeof(IBusiness<BaseReceitaDto>), typeof(ReceitaBusinessImpl));
        
        services.AddScoped(typeof(IUnitOfWork<>), typeof(UnitOfWork<>));
        services.AddScoped(typeof(BusinessBase<BaseCategoriaDto, Categoria>), typeof(CategoriaBusinessImpl));
        services.AddScoped(typeof(BusinessBase<BaseDespesaDto, Despesa>), typeof(DespesaBusinessImpl));
        services.AddScoped(typeof(BusinessBase<BaseReceitaDto, Receita>), typeof(ReceitaBusinessImpl));

        services.AddScoped(typeof(IControleAcessoBusiness), typeof(ControleAcessoBusinessImpl));
        services.AddScoped(typeof(ILancamentoBusiness), typeof(LancamentoBusinessImpl));
        services.AddScoped(typeof(IUsuarioBusiness), typeof(UsuarioBusinessImpl));
        services.AddScoped(typeof(IImagemPerfilUsuarioBusiness), typeof(ImagemPerfilUsuarioBusinessImpl));
        services.AddScoped(typeof(ISaldoBusiness), typeof(SaldoBusinessImpl));
        services.AddScoped(typeof(IGraficosBusiness), typeof(GraficosBusinessImpl));
        services.AddScoped<IEmailSender, EmailSender>();
        return services;
    }
}


