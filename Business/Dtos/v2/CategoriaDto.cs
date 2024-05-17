using Business.Dtos.Core;
using Business.HyperMedia;
using Business.HyperMedia.Interfaces;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Business.Dtos.v2;
public class CategoriaDto : CategoriaDtoBase, ISupportHyperMedia
{
    [Required(ErrorMessage = "O campo Descrição é obrigatório.")]
    public override string? Descricao { get; set; }

    [Required(ErrorMessage = "O campo Tipo de categoria é obrigatório.")]
    public override int IdTipoCategoria { get; set; }

    [JsonIgnore]
    public override TipoCategoriaDto TipoCategoria { get { return (TipoCategoriaDto)IdTipoCategoria; } set { IdTipoCategoria = (int)value; } }
    
    public IList<HyperMediaLink> Links { get; set; } = new List<HyperMediaLink>();
}