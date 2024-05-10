using Business.Dtos.Core;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Business.Dtos.v1;
public class ReceitaDto : BaseReceitaDto
{
    [Required]
    public override DateTime? Data { get; set; }

    [Required]
    public override string? Descricao { get; set; }

    [Required]
    public override decimal Valor { get; set; }

    [Required]
    public override int? IdCategoria { get; set; }

    [JsonIgnore]
    public UsuarioDto? Usuario { get; set; }
}