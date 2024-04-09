using Domain.Core;

namespace Domain.Entities;
public class Usuario : BaseModel
{
    public string? Nome { get; set; }
    public string? SobreNome { get; set; }
    public string? Telefone { get; set; }
    public string? Email { get; set; }
    public StatusUsuario StatusUsuario { get; set; }
    public PerfilUsuario PerfilUsuario { get; set; }
}