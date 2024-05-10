using Business.Dtos.Core;
using Business.HyperMedia;
using Business.HyperMedia.Abstractions;
using Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace Business.Dtos.v2;
public class CategoriaDto : BaseCategoriaDto, ISupportHyperMedia
{
    [Required(ErrorMessage = "O campo Descrição é obrigatório.")]
    public string? Descricao { get; set; }
    [Required(ErrorMessage = "O campo Tipo de categoria é obrigatório.")]
    public int IdTipoCategoria { get; set; }
    public TipoCategoria TipoCategoria { get { return (TipoCategoria)IdTipoCategoria; } set { IdTipoCategoria = (int)value; } }
    public IList<HyperMediaLink> Links { get; set; } = new List<HyperMediaLink>();
}