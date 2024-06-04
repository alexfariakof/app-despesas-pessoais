using DataSeeders;
using DataSeeders.Implementations;

namespace Despesas.WebApi.CommonDependenceInject;
public static class DataSeedersDependenceInject
{
    public static void AddDataSeeders(this IServiceCollection services)
    {
        services.AddTransient<IDataSeeder, DataSeeder>();
    }

    public static void RunDataSeeders(this WebApplication app)
    {
        using (var scope = app.Services.CreateScope())
        {
            var services = scope.ServiceProvider;
            var dataSeeder = services.GetRequiredService<IDataSeeder>();
            dataSeeder.SeedData();
        }
    }
}