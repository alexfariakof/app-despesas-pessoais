using Domain.Core;

namespace Domain.Entities;
public class ImagemPerfilUsuario : BaseModel
{
    public string? Url { get; set; }
    public string? Name { get; set; }
    public string? Type { get; set; }
    public int UsuarioId { get; set; }
    public virtual Usuario? Usuario { get; set; }        
}