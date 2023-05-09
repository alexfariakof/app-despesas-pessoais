using despesas_backend_api_net_core.Interfaces;
using despesas_backend_api_net_core.Repositories.Generic;
using despesas_backend_api_net_core.Services;

namespace despesas_backend_api_net_core.Infrastructure.ExtensionMethods
{
    public static class CommonInjectDependence
    {
        public static IServiceCollection AddServices(this IServiceCollection services)
        {

            services.AddScoped<ICategoriaViewModelService, CategoriaViewModelService>();


            return services;
        }

        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services.AddScoped(typeof(IRepositorio<>), typeof(GenericRepositorio<>));

            return services;
        }
    }
}
