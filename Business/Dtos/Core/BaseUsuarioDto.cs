using Domain.Entities;

namespace Business.Dtos.Core;
public abstract class BaseUsuarioDto : BaseModelDto
{
    public string? Nome { get; set; }
    public string? SobreNome { get; set; }
    public string? Telefone { get; set; }
    public string? Email { get; set; }
    public PerfilUsuario PerfilUsuario { get; set; }

}