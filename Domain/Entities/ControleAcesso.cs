using Domain.Core;

namespace Domain.Entities;
public class ControleAcesso : BaseModel
{
    public string Login { get; set; } = string.Empty;
    public string Senha { get; set; } = string.Empty;
    public int UsuarioId { get; set; }
    public virtual Usuario? Usuario { get; set; }
}