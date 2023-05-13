using despesas_backend_api_net_core.Business.Generic;
using despesas_backend_api_net_core.Infrastructure.Data.Repositories.Generic;

namespace despesas_backend_api_net_core.Infrastructure.ExtensionMethods
{
    public static class CommonInjectDependence
    {
        public static IServiceCollection AddServices(this IServiceCollection services)
        {

            services.AddScoped(typeof(IBusiness<>), typeof(GenericBusiness<>));


            return services;
        }

        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services.AddScoped(typeof(IRepositorio<>), typeof(GenericRepositorio<>));

            return services;
        }
    }
}
