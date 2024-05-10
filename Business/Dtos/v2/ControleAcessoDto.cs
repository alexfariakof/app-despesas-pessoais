using Business.Dtos.Core;
using Domain.Entities;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Business.Dtos.v2;
public class ControleAcessoDto : BaseControleAcessoDto, IValidatableObject
{
    [JsonIgnore]
    public override int Id { get; set; }

    [Required(ErrorMessage = "A senha é obrigatório.")]
    public override string? Senha { get; set; }

    [Required(ErrorMessage = "Confirma Senha é obrigatória.")]
    public override string? ConfirmaSenha { get; set; }

    [JsonIgnore]
    public override string? RefreshToken { get; set; }

    [JsonIgnore]
    public override PerfilUsuario PerfilUsuario { get; set; }

    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {

        if (string.IsNullOrEmpty(ConfirmaSenha) | string.IsNullOrWhiteSpace(ConfirmaSenha))
            yield return new ValidationResult("Campo Confirma Senha não pode ser em branco ou nulo");

        if (Senha != ConfirmaSenha)
            yield return new ValidationResult("Senha e Confirma Senha são diferentes!");
    }
}