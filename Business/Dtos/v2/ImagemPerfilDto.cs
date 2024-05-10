using Business.Dtos.Core;
using Business.HyperMedia;
using Business.HyperMedia.Abstractions;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Business.Dtos.v2;
public class ImagemPerfilDto : BaseImagemPerfilDto, ISupportHyperMedia
{
    [Url(ErrorMessage = "Url inválida.")]
    public override string? Url { get; set; }

    [JsonIgnore]
    public override string? Name { get; set; }
    [JsonIgnore]
    public override string? Type { get; set; }
    [JsonIgnore]
    public override string? ContentType { get; set; }

    [JsonIgnore]
    public override byte[]? Arquivo { get; set; }
    public IList<HyperMediaLink> Links { get; set; } = new List<HyperMediaLink>();
}