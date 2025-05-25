using Business.Dtos.Core;
using Business.HyperMedia;
using Business.HyperMedia.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace Business.Dtos.v2;
public class CategoriaDto : CategoriaDtoBase, ISupportHyperMedia
{
    [Required(ErrorMessage = "O campo Descrição é obrigatório.")]
    public override string? Descricao { get; set; }

    [Required(ErrorMessage = "O campo Tipo de categoria é obrigatório.")]
    public TipoCategoriaDto IdTipoCategoria { get; set; }

    public IList<HyperMediaLink> Links { get; set; } = new List<HyperMediaLink>();
}