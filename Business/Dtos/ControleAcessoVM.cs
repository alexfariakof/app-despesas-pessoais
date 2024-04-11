using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Business.Dtos;
public class ControleAcessoVM : UsuarioVM
{
    [JsonIgnore]
    public override int Id { get; set; }
    [Required]
    public string? Senha { get; set; }
    [Required]
    public string? ConfirmaSenha { get; set; }
}