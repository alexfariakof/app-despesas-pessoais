using Business.Dtos.Core;
using Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace Business.Dtos.v1;
public class CategoriaDto : BaseCategoriaDto
{
    [Required]
    public string? Descricao { get; set; }
    [Required]
    public int IdTipoCategoria { get; set; }
    public TipoCategoria TipoCategoria { get { return (TipoCategoria)IdTipoCategoria; } set { IdTipoCategoria = (int)value; } }    
}