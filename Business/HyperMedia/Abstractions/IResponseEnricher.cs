using Microsoft.AspNetCore.Mvc.Filters;

namespace Business.HyperMedia.Abstractions;
public interface IResponseEnricher
{
    bool CanEnrich(ResultExecutingContext response);

    Task Enrich(ResultExecutingContext response);
}