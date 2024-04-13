using Business.Abstractions;
using Business.Dtos;
using Business.Generic;
using Business.Implementations;
using Domain.Entities;
using Domain.Entities.Abstractions;
using Microsoft.Extensions.DependencyInjection;
using Repository.Persistency.UnitOfWork;

namespace Business.CommonInjectDependence;
public static class ServicesInjectDependence
{
    public static IServiceCollection AddServices(this IServiceCollection services)
    {
        services.AddScoped(typeof(IBusiness<DespesaVM>), typeof(DespesaBusinessImpl));
        services.AddScoped(typeof(IBusiness<ReceitaVM>), typeof(ReceitaBusinessImpl));
        services.AddScoped(typeof(IUnitOfWork<>), typeof(UnitOfWork<>));
        services.AddScoped(typeof(BusinessBase<CategoriaVM, Categoria>), typeof(CategoriaBusinessImpl));
        services.AddScoped(typeof(IControleAcessoBusiness), typeof(ControleAcessoBusinessImpl));
        services.AddScoped(typeof(ILancamentoBusiness), typeof(LancamentoBusinessImpl));
        services.AddScoped(typeof(IUsuarioBusiness), typeof(UsuarioBusinessImpl));
        services.AddScoped(typeof(IImagemPerfilUsuarioBusiness), typeof(ImagemPerfilUsuarioBusinessImpl));
        services.AddScoped(typeof(ISaldoBusiness), typeof(SaldoBusinessImpl));
        services.AddScoped(typeof(IGraficosBusiness), typeof(GraficosBusinessImpl));

        return services;
    }
}