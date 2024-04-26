using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Business.Dtos;
public class ControleAcessoDto : UsuarioDto, IValidatableObject
{
    [JsonIgnore]
    public override int Id { get; set; }
    
    [Required(ErrorMessage = "A senha é obrigatório.")]
    public string? Senha { get; set; }

    [Required(ErrorMessage = "Confirma Senha é obrigatória.")]
    public string? ConfirmaSenha { get; set; }

    [JsonIgnore]
    public string? RefreshToken { get; set; }

    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {

        if (String.IsNullOrEmpty(ConfirmaSenha) | String.IsNullOrWhiteSpace(ConfirmaSenha))
            yield return new ValidationResult("Campo Confirma Senha não pode ser em branco ou nulo");

        if (Senha != ConfirmaSenha)
            yield return new ValidationResult("Senha e Confirma Senha são diferentes!");
    }
}