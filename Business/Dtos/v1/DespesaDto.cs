using Business.Dtos.Core;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Business.Dtos.v1;
public class DespesaDto : BaseDespesaDto
{
    [Required]
    public DateTime Data { get; set; }

    [Required]
    public string? Descricao { get; set; }

    [Required]
    public decimal Valor { get; set; }
    public DateTime? DataVencimento { get; set; }

    [Required]
    public int? IdCategoria { get; set; }

    [JsonIgnore]
    public UsuarioDto? Usuario { get; set; }
    
}