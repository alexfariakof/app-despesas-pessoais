using Microsoft.AspNetCore.Mvc.Filters;

namespace despesas_backend_api_net_core.HyperMedia.Abstractions;
public interface IResponseEnricher
{
    bool CanEnrich(ResultExecutingContext response);

    Task Enrich(ResultExecutingContext response);
}