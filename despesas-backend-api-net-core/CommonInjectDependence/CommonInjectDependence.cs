using Business;
using Business.Generic;
using Business.Implementations;
using DataSeeders;
using DataSeeders.Implementations;
using Domain.Core;
using Domain.Core.Interface;
using Domain.Entities;
using Domain.VM;
using Microsoft.EntityFrameworkCore;
using Repository;
using Repository.Persistency;
using Repository.Persistency.Generic;
using Repository.Persistency.Implementations;


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

    public static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        services.AddScoped(typeof(IRepositorio<>), typeof(GenericRepositorio<>));
        services.AddScoped(typeof(IRepositorio<Usuario>), typeof(UsuarioRepositorioImpl));
        services.AddScoped<IControleAcessoRepositorio, ControleAcessoRepositorioImpl>();
        services.AddScoped<IEmailSender, EmailSender>();
        services.AddScoped(typeof(ILancamentoRepositorio), typeof(LancamentoRepositorioImpl));
        services.AddScoped(typeof(ISaldoRepositorio), typeof(SaldoRepositorioImpl));
        services.AddScoped(typeof(IGraficosRepositorio), typeof(GraficosRepositorioImpl));

        return services;
    }

    public static void CreateDataBaseInMemory(this IServiceCollection services)
    {
        services.AddDbContext<RegisterContext>(c => c.UseInMemoryDatabase("Register"));
        services.AddTransient<IDataSeeder, DataSeeder>();
    }
}