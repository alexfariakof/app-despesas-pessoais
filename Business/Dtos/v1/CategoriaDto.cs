using Business.Dtos.Core;
using Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace Business.Dtos.v1;
public class CategoriaDto : BaseCategoriaDto
{
    [Required]
    public override string? Descricao { get; set; }
    [Required]
    public override int IdTipoCategoria { get; set; }
    public override TipoCategoria TipoCategoria { get { return (TipoCategoria)IdTipoCategoria; } set { IdTipoCategoria = (int)value; } }    
}