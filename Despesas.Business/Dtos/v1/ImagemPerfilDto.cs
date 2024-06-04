using AutoMapper.Configuration.Annotations;
using Business.Dtos.Core;
using System.Text.Json.Serialization;

namespace Business.Dtos.v1;
public class ImagemPerfilDto : ImagemPerfilDtoBase
{
    [Ignore]
    public override string? Url { get; set; }

    [JsonIgnore]
    public override string? Name { get; set; }
    [JsonIgnore]
    public override string? Type { get; set; }
    [JsonIgnore]
    public override string? ContentType { get; set; }

    [JsonIgnore]
    public override byte[]? Arquivo { get; set; }

}