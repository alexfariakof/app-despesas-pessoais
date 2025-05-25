using System.ComponentModel.DataAnnotations;

namespace Business.Dtos.Core;
public abstract class ControleAcessoDtoBase : ModelDtoBase
{
    [Required]
    public virtual string? Nome { get; set; }

    public virtual string? SobreNome { get; set; }

    [Required]
    public virtual string? Telefone { get; set; }

    [EmailAddress]
    [Required]
    public virtual string? Email { get; set; }

    [Required]
    public virtual string? Senha { get; set; }

    [Required]
    public virtual string? ConfirmaSenha { get; set; }
}