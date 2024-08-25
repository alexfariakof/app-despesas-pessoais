using Business.Dtos.Core;
using System.ComponentModel.DataAnnotations;

namespace Business.Dtos.v1;
public class DespesaDto : DespesaDtoBase
{
    [Required]
    public override DateTime? Data { get; set; }

    [Required]
    public override string? Descricao { get; set; }

    [Required]
    public override decimal Valor { get; set; }
    public override DateTime? DataVencimento { get; set; }

    [Required]
    public override Guid? IdCategoria { get; set; }
    
}