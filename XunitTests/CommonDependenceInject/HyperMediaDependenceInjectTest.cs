using Business.CommonDependenceInject;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Builder;
using Business.HyperMedia.Enricher;
using Business.HyperMedia.Filters;

namespace CommonDependenceInject;
public class HyperMediaDependenceInjectTest
{
    [Fact]
    public void AddHyperMediaHATEOAS_Should_Register_Enricher_Services()
    {
        // Arrange
        var builder = WebApplication.CreateBuilder();
        var services = builder.Services;

        // Act
        services.AddHyperMediaHATEOAS();

        // Assert
        AssertContainsEnricherService<CategoriaEnricher>(services);
        AssertContainsEnricherService<DespesaEnricher>(services);
        AssertContainsEnricherService<ReceitaEnricher>(services);
        AssertContainsEnricherService<UsuarioEnricher>(services);
        AssertContainsEnricherService<ImagemPerfilEnricher>(services);
        AssertContainsEnricherService<SaldoEnricher>(services);
    }

    private static void AssertContainsEnricherService<TEnricher>(IServiceCollection services)
    {
        var serviceDescriptor = services.SingleOrDefault(descriptor => descriptor.ServiceType == typeof(HyperMediaFilterOptions) && descriptor.Lifetime == ServiceLifetime.Singleton);
        Assert.NotNull(serviceDescriptor);
    }
}

