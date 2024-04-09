using Domain.Core;

namespace Domain.Entities;
public class ControleAcesso : BaseModel
{
    public string? Login { get; set; }
    public string? Senha { get; set; }
    public int UsuarioId { get; set; }
    public virtual Usuario? Usuario { get; set; }
}