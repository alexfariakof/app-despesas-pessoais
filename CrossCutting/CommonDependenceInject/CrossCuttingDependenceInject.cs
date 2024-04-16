using CrossCutting.CQRS.Queries;
using Domain.Entities;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace CrossCutting.CommonDependenceInject;
public static class CrossCuttingDependenceInject
{
    public static IServiceCollection AddCrossCuttingConfiguration(this IServiceCollection services)
    {
        var myHandlers = AppDomain.CurrentDomain.Load("CrossCutting");
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(myHandlers));
        services.AddTransient<IRequestHandler<GetAllQuery<Categoria>, IEnumerable<Categoria>>, GetAllQueryHandler<Categoria>>();

        return services;
    }

}