using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Repository.Persistency;
using Repository.Persistency.Generic;
using Repository.Persistency.Implementations;

namespace Repository.CommonDependenceInject;
public static class RepositoryDependenceInject
{
    public static void CreateDataBaseInMemory(this IServiceCollection services)
    {
        services.AddDbContext<RegisterContext>(c => c.UseInMemoryDatabase("Register"));
    }

    public static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        services.AddScoped(typeof(IRepositorio<>), typeof(GenericRepositorio<>));
        //services.AddScoped(typeof(IRepositorio<Categoria>), typeof(CategoriaRepositorioImpl));
        //services.AddScoped(typeof(IRepositorio<Despesa>), typeof(DespesaRepositorioImpl));
        //services.AddScoped(typeof(IRepositorio<Receita>), typeof(ReceitaRepositorioImpl));
        services.AddScoped(typeof(IRepositorio<Usuario>), typeof(UsuarioRepositorioImpl));
        services.AddScoped<IControleAcessoRepositorioImpl, ControleAcessoRepositorioImpl>();
        services.AddScoped(typeof(ILancamentoRepositorio), typeof(LancamentoRepositorioImpl));
        services.AddScoped(typeof(ISaldoRepositorio), typeof(SaldoRepositorioImpl));
        services.AddScoped(typeof(IGraficosRepositorio), typeof(GraficosRepositorioImpl));
        return services;
    }
}