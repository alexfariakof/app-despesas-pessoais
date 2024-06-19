using Domain.Entities.ValueObjects;

namespace Business.Dtos.Core;
public abstract class UsuarioDtoBase : ModelDtoBase
{
    public virtual string? Nome { get; set; }
    public virtual string? SobreNome { get; set; }
    public virtual string? Telefone { get; set; }
    public virtual string? Email { get; set; }
    public virtual PerfilUsuario? PerfilUsuario { get; set; }
}