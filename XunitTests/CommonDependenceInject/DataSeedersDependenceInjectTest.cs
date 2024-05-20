using DataSeeders;
using despesas_backend_api_net_core.CommonDependenceInject;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Repository.CommonDependenceInject;

namespace CommonDependenceInject;

public sealed class DataSeedersDependenceInjectTest
{
    [Fact]
    public void AddDataSeeders_Should_Register_DataSeeder_Service()
    {
        // Arrange
        var builder = WebApplication.CreateBuilder();
        var services = builder.Services;

        // Act
        services.AddDataSeeders();

        // Assert
        var dataSeederService = services.FirstOrDefault(descriptor => descriptor.ServiceType == typeof(IDataSeeder));
        Assert.NotNull(dataSeederService);
        Assert.Equal(ServiceLifetime.Transient, dataSeederService.Lifetime);
    }

    [Fact]
    public void RunDataSeeders_Should_Invoke_SeedData_Method()
    {
        // Arrange
        var builder = WebApplication.CreateBuilder();
        var services = builder.Services;
        services.CreateDataBaseInMemory();
        services.AddDataSeeders();
        var app = builder.Build();

        // Act
        app.RunDataSeeders();

        // Assert
        using (var scope = app.Services.CreateScope())
        {
            var dataSeeder = scope.ServiceProvider.GetRequiredService<IDataSeeder>();
            Assert.NotNull(dataSeeder);
        }
    }
}