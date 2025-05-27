using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Migrations.MsSqlServer.CommonInjectDependence;
public static class MigrationsMsSqlServerInjectDependence
{
    public static IServiceCollection ConfigureMsSqlServerMigrationsContext(this IServiceCollection services, IConfiguration configuration)
    {
        var name = typeof(MsSqlServerContext).Assembly.FullName;
        services.AddDbContext<MsSqlServerContext>(options => options.UseSqlServer(configuration.GetConnectionString("Migrations.MsSqlConnectionString"), builder => builder.MigrationsAssembly(name)));
        return services;
    }
}