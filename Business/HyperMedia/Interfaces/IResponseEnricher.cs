using Microsoft.AspNetCore.Mvc.Filters;

namespace Business.HyperMedia.Interfaces;
public interface IResponseEnricher
{
    bool CanEnrich(ResultExecutingContext response);

    Task Enrich(ResultExecutingContext response);
}