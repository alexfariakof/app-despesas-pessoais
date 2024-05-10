using Business.Dtos.Core;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Business.Dtos.v1;
public class CategoriaDto : BaseCategoriaDto
{
    [Required]
    public override string? Descricao { get; set; }
    [Required]
    public override int IdTipoCategoria { get; set; }
    
    [JsonIgnore]
    public override TipoCategoriaDto TipoCategoria { get { return (TipoCategoriaDto)IdTipoCategoria; } set { IdTipoCategoria = (int)value; } }    
}