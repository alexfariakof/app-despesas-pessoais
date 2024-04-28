using Business.HyperMedia;
using Business.HyperMedia.Abstractions;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Business.Dtos;
public class ImagemPerfilDto : BaseModelDto, ISupportHyperMedia
{
    [Url(ErrorMessage = "Url inválida.")]
    public string? Url { get; set; }
    
    [JsonIgnore]
    public string? Name { get; set; }
    [JsonIgnore]
    public string? Type { get; set; }
    [JsonIgnore]
    public string? ContentType { get; set; }

    [JsonIgnore]
    public byte[]? Arquivo { get; set; }
    public IList<HyperMediaLink> Links { get; set; } = new List<HyperMediaLink>();
}