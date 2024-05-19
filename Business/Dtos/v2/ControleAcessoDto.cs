using Business.Dtos.Core;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Business.Dtos.v2;
public class ControleAcessoDto : ControleAcessoDtoBase, IValidatableObject
{
    [Required(ErrorMessage = "O campo Nome é obrigatório.")]
    public override string? Nome { get; set; }

    public override string? SobreNome { get; set; }

    [Required(ErrorMessage = "O campo Telefone é obrigatório.")]
    public override string? Telefone { get; set; }

    [EmailAddress(ErrorMessage = "O campo Email é inválido.")]
    [Required(ErrorMessage = "O campo Email é obrigatório.")]
    public override string? Email { get; set; }

    [Required(ErrorMessage = "O campo Senha é obrigatório.")]
    [PasswordPropertyText]
    public override string? Senha { get; set; }

    [Required(ErrorMessage = "O campo Confirma Senha é obrigatório.")]
    [PasswordPropertyText]
    public override string? ConfirmaSenha { get; set; }
    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {

        if (string.IsNullOrEmpty(ConfirmaSenha) || string.IsNullOrWhiteSpace(ConfirmaSenha))
            yield return new ValidationResult("Campo Confirma Senha não pode ser em branco ou nulo");

        if (Senha != ConfirmaSenha)
            yield return new ValidationResult("Senha e Confirma Senha são diferentes!");
    }
}