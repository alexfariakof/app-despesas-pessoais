using Business;
using Business.Generic;
using Business.Implementations;
using DataSeeders;
using DataSeeders.Implementations;
using Domain.VM;
using Microsoft.EntityFrameworkCore;
using Repository;

namespace despesas_backend_api_net_core.CommonInjectDependence;
public static class CommonInjectDependence
{
    public static IServiceCollection AddServices(this IServiceCollection services)
    {
        services.AddScoped(typeof(IBusiness<DespesaVM>), typeof(DespesaBusinessImpl));
        services.AddScoped(typeof(IBusiness<ReceitaVM>), typeof(ReceitaBusinessImpl));
        services.AddScoped(typeof(IBusiness<CategoriaVM>), typeof(CategoriaBusinessImpl));
        services.AddScoped(typeof(IControleAcessoBusiness), typeof(ControleAcessoBusinessImpl));
        services.AddScoped(typeof(ILancamentoBusiness), typeof(LancamentoBusinessImpl));
        services.AddScoped(typeof(IUsuarioBusiness), typeof(UsuarioBusinessImpl));
        services.AddScoped(typeof(IImagemPerfilUsuarioBusiness), typeof(ImagemPerfilUsuarioBusinessImpl));
        services.AddScoped(typeof(ISaldoBusiness), typeof(SaldoBusinessImpl));
        services.AddScoped(typeof(IGraficosBusiness), typeof(GraficosBusinessImpl));

        return services;
    }
    public static void CreateDataBaseInMemory(this IServiceCollection services)
    {
        services.AddDbContext<RegisterContext>(c => c.UseInMemoryDatabase("Register"));
        services.AddTransient<IDataSeeder, DataSeeder>();
    }
}