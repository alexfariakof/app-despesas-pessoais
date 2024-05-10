using Business.Dtos.Core;
using System.Text.Json.Serialization;

namespace Business.Dtos.v1;
public class ImagemPerfilDto : BaseImagemPerfilDto
{
    public string? Url { get; set; }

    [JsonIgnore]
    public string? Name { get; set; }
    [JsonIgnore]
    public string? Type { get; set; }
    [JsonIgnore]
    public string? ContentType { get; set; }

    [JsonIgnore]
    public byte[]? Arquivo { get; set; }

}