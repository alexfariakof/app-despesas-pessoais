namespace Business.Dtos.Core;
public abstract class BaseImagemPerfilDto : BaseModelDto
{
    public string? Url { get; set; }
    public string? Name { get; set; }
    public string? Type { get; set; }
    public string? ContentType { get; set; }
    public byte[]? Arquivo { get; set; }
}