using Domain.Core;

namespace Domain.Entities;
public class ImagemPerfilUsuario : BaseModel
{
    public string Url { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string Type { get; set; } = string.Empty;
    public string ContentType { get; set; } = string.Empty;    
    public int UsuarioId { get; set; }
    public virtual Usuario Usuario { get; set; } = new();
}