using System.ComponentModel.DataAnnotations;

namespace Business.Dtos;
public class ChangePasswordDto : IValidatableObject
{
    [Required(ErrorMessage = "O campo Senha é obrigatório.")]
    public string? Senha { get; set; }    

    [Required(ErrorMessage = "O campo Confirma Senha é obrigatório.")]
    public string? ConfirmaSenha { get; set; }

    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        if (String.IsNullOrEmpty(Senha) || String.IsNullOrWhiteSpace(Senha))
            yield return new ValidationResult("Campo Senha não pode ser em branco ou nulo!");

        if (String.IsNullOrEmpty(ConfirmaSenha) | String.IsNullOrWhiteSpace(ConfirmaSenha))
            yield return new ValidationResult("Campo Confirma Senha não pode ser em branco ou nulo!");        
    }
}
