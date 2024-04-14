using Microsoft.Extensions.DependencyInjection;

namespace CrossCutting.CommonDependenceInject;
public static class CrossCuttingDependenceInject
{
    public static IServiceCollection AddCrossCuttingConfiguration(this IServiceCollection services)
    {
        var myHandlers = AppDomain.CurrentDomain.Load("Business");
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(myHandlers));
        return services;
    }

}