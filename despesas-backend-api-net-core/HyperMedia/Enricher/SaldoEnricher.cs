using Business.Dtos;
using Business.HyperMedia;
using despesas_backend_api_net_core.HyperMedia.Constants;
using Microsoft.AspNetCore.Mvc;
using System.Text;

namespace despesas_backend_api_net_core.HyperMedia.Enricher;

public class SaldoEnricher : ContentResponseEnricher<SaldoDto>
{
    private readonly object _lock = new object();
    protected override Task EnrichModel(SaldoDto content, IUrlHelper urlHelper)
    {
        var path = "saldo";
        string link = GetLink(0, urlHelper, path);

        content.Links.Add(new HyperMediaLink() 
        { 
            Action = HttpActionVerb.GET,
            Href = link,
            Rel = RelationType.self,
            Type = ResponseTypeFormat.DefaultGet
        });

        path = "saldo/ByAno";
        link = GetLink(0, urlHelper, path);

        content.Links.Add(new HyperMediaLink()
        {
            Action = HttpActionVerb.GET,
            Href = link,
            Rel = RelationType.self,
            Type = ResponseTypeFormat.DefaultGet
        });

        path = "saldo/ByMesAno";
        link = GetLink(0, urlHelper, path);

        content.Links.Add(new HyperMediaLink()
        {
            Action = HttpActionVerb.GET,
            Href = link,
            Rel = RelationType.self,
            Type = ResponseTypeFormat.DefaultGet
        });

        return Task.CompletedTask;
    }

    private string GetLink(int id, IUrlHelper urlHelper, string path)
    {
        lock (_lock)
        {
            var url = new { controller = path, id = id };
            return new StringBuilder(urlHelper.Link("DefaultApi", url).Replace("%2F", "/")).ToString();
        }
    }
}
