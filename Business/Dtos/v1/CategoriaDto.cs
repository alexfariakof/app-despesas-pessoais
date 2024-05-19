using Business.Dtos.Core;
using System.ComponentModel.DataAnnotations;

namespace Business.Dtos.v1;
public class CategoriaDto : CategoriaDtoBase
{
    [Required]
    public override string? Descricao { get; set; }

    [Required]
    public int IdTipoCategoria { get; set; }    
}