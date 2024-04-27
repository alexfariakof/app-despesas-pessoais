using despesas_backend_api_net_core.HyperMedia.Abstractions;

namespace despesas_backend_api_net_core.HyperMedia.Filters;

public class HyperMediaFilterOptions
{
    public IList<IResponseEnricher> ContentResponseEnricherList { get; set; } = new List<IResponseEnricher>();
}