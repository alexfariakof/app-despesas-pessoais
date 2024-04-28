using Business.HyperMedia.Abstractions;

namespace Business.HyperMedia.Filters;

public class HyperMediaFilterOptions
{
    public IList<IResponseEnricher> ContentResponseEnricherList { get; set; } = new List<IResponseEnricher>();
}