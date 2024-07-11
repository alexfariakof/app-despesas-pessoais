using Business.Dtos.Core;
using System.ComponentModel.DataAnnotations;

namespace Business.Dtos.v1;
public class ReceitaDto : ReceitaDtoBase
{
    [Required]
    public override DateTime? Data { get; set; }

    [Required]
    public override string? Descricao { get; set; }

    [Required]
    public override decimal Valor { get; set; }

    [Required]
    public override Guid? IdCategoria { get; set; }

}