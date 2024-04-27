using Business.Dtos;
using Business.HyperMedia;
using despesas_backend_api_net_core.HyperMedia.Constants;
using Microsoft.AspNetCore.Mvc;
using System.Text;

namespace despesas_backend_api_net_core.HyperMedia.Enricher;

public class UsuarioEnricher : ContentResponseEnricher<UsuarioDto>
{
    private readonly object _lock = new object();
    protected override Task EnrichModel(UsuarioDto content, IUrlHelper urlHelper)
    {
        var path = "usuario";
        string link = GetLink(content.Id, urlHelper, path);

        content.Links.Add(new HyperMediaLink() 
        { 
            Action = HttpActionVerb.GET,
            Href = link,
            Rel = RelationType.self,
            Type = ResponseTypeFormat.DefaultGet
        });

        content.Links.Add(new HyperMediaLink()
        {
            Action = HttpActionVerb.POST,
            Href = link,
            Rel = RelationType.self,
            Type = ResponseTypeFormat.DefaultPost
        });


        content.Links.Add(new HyperMediaLink()
        {
            Action = HttpActionVerb.PUT,
            Href = link,
            Rel = RelationType.self,
            Type = ResponseTypeFormat.DefaultPut
        });

        content.Links.Add(new HyperMediaLink()
        {
            Action = HttpActionVerb.DELETE,
            Href = link,
            Rel = RelationType.self,
            Type = ResponseTypeFormat.DefaultDelete
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
