using Business.HyperMedia.Enricher;
using Business.HyperMedia.Filters;
using Microsoft.Extensions.DependencyInjection;


namespace Business.CommonDependenceInject;

public static class HyperMediaDependenceInject
{
    public static void AddHyperMediaHATEOAS(this IServiceCollection services)
    {
        var filterOptions = new HyperMediaFilterOptions();
        filterOptions.ContentResponseEnricherList.Add(new CategoriaEnricher());
        filterOptions.ContentResponseEnricherList.Add(new DespesaEnricher());
        filterOptions.ContentResponseEnricherList.Add(new ReceitaEnricher());
        filterOptions.ContentResponseEnricherList.Add(new UsuarioEnricher());
        filterOptions.ContentResponseEnricherList.Add(new ImagemPerfilEnricher());
        filterOptions.ContentResponseEnricherList.Add(new SaldoEnricher());
        services.AddSingleton(filterOptions);

    }
}