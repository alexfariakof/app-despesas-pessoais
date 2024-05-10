using Business.Dtos.Core;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Business.Dtos.v1;
public class DespesaDto : BaseDespesaDto
{
    [Required]
    public override DateTime? Data { get; set; }

    [Required]
    public override string? Descricao { get; set; }

    [Required]
    public override decimal Valor { get; set; }
    public override DateTime? DataVencimento { get; set; }

    [Required]
    public override int? CategoriaId { get; set; }
    
}