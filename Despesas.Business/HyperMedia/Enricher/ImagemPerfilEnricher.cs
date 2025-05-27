using Business.Dtos.v2;
using Business.HyperMedia.Constants;
using Microsoft.AspNetCore.Mvc;
using System.Text;

namespace Business.HyperMedia.Enricher;

public class ImagemPerfilEnricher : ContentResponseEnricher<ImagemPerfilDto>
{
    private readonly object _lock = new object();
    protected override Task EnrichModel(ImagemPerfilDto content, IUrlHelper urlHelper)
    {
        var path = "usuario/imagemperfil";
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

    private string GetLink(Guid id, IUrlHelper urlHelper, string path)
    {
        lock (_lock)
        {
            var url = new { controller = path, id = id };
            return new StringBuilder(urlHelper.Link("DefaultApi", url).Replace("%2F", "/")).ToString();
        }
    }
}
