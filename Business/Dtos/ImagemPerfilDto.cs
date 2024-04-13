using System.Text.Json.Serialization;

namespace Business.Dtos;
public class ImagemPerfilDto : BaseModelDto
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