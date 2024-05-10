using Business.Dtos.Core;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Business.Dtos.v1;
public class ControleAcessoDto : BaseControleAcessoDto
{
    [JsonIgnore]
    public override int Id { get; set; }

    [Required]
    public override string? Senha { get; set; }

    [Required]
    public override string? ConfirmaSenha { get; set; }

    [JsonIgnore]
    public override string? RefreshToken { get; set; }

}