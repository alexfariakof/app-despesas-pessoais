using System.Text.Json.Serialization;

namespace Domain.VM;
public class ImagemPerfilVM : BaseModelVM
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