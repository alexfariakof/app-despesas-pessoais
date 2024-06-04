using Business.Dtos.v2;
using Business.HyperMedia.Constants;
using Microsoft.AspNetCore.Mvc;
using System.Text;

namespace Business.HyperMedia.Enricher;

public class DespesaEnricher : ContentResponseEnricher<DespesaDto>
{
    private readonly object _lock = new object();
    protected override Task EnrichModel(DespesaDto content, IUrlHelper urlHelper)
    {
        var path = "despesa";
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

        var categoriaContent = content.Categoria;
        path = "categoria";
        link = GetLink(categoriaContent.Id, urlHelper, path);

        categoriaContent.Links.Add(new HyperMediaLink()
        {
            Action = HttpActionVerb.GET,
            Href = link,
            Rel = RelationType.self,
            Type = ResponseTypeFormat.DefaultGet
        });

        categoriaContent.Links.Add(new HyperMediaLink()
        {
            Action = HttpActionVerb.POST,
            Href = link,
            Rel = RelationType.self,
            Type = ResponseTypeFormat.DefaultPost
        });


        categoriaContent.Links.Add(new HyperMediaLink()
        {
            Action = HttpActionVerb.PUT,
            Href = link,
            Rel = RelationType.self,
            Type = ResponseTypeFormat.DefaultPut
        });

        categoriaContent.Links.Add(new HyperMediaLink()
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
