using Business.Dtos.Core;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Business.Dtos.v1;
public class ControleAcessoDto : BaseControleAcessoDto
{
    [JsonIgnore]
    public override int Id { get; set; }

    [Required]
    public string? Senha { get; set; }

    [Required]
    public string? ConfirmaSenha { get; set; }

    [JsonIgnore]
    public string? RefreshToken { get; set; }

}