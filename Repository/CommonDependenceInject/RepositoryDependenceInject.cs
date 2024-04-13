using Domain.Core;
using Domain.Core.Interfaces;
using Domain.Entities;
using Microsoft.Extensions.DependencyInjection;
using Repository.Persistency;
using Repository.Persistency.Generic;
using Repository.Persistency.Implementations;

namespace Repository.CommonDependenceInject;
public static class RepositoryDependenceInject
{
    public static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        services.AddScoped(typeof(IRepositorio<>), typeof(GenericRepositorio<>));
        services.AddScoped(typeof(IRepositorio<Usuario>), typeof(UsuarioRepositorioImpl));
        services.AddScoped<IControleAcessoRepositorioImpl, ControleAcessoRepositorioImpl>();
        services.AddScoped<IEmailSender, EmailSender>();
        services.AddScoped(typeof(ILancamentoRepositorio), typeof(LancamentoRepositorioImpl));
        services.AddScoped(typeof(ISaldoRepositorio), typeof(SaldoRepositorioImpl));
        services.AddScoped(typeof(IGraficosRepositorio), typeof(GraficosRepositorioImpl));

        return services;
    }
}