using despesas_backend_api_net_core.HyperMedia.Enricher;
using despesas_backend_api_net_core.HyperMedia.Filters;

namespace despesas_backend_api_net_core.CommonDependenceInject;
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