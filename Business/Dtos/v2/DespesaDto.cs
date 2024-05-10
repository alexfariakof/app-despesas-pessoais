using Business.Dtos.Core;
using Business.HyperMedia;
using Business.HyperMedia.Abstractions;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Business.Dtos.v2;
public class DespesaDto : BaseDespesaDto, ISupportHyperMedia
{
    [Required(ErrorMessage = "O campo Data é obrigatório.")]
    public override DateTime? Data { get; set; }

    [Required(ErrorMessage = "O campo Descrição é obrigatório.")]
    public override string? Descricao { get; set; }

    [Required(ErrorMessage = "O campo Valor é obrigatório.")]
    public override decimal Valor { get; set; }
    public override DateTime? DataVencimento { get; set; }

    [JsonIgnore]
    public override int? CategoriaId
    {
        get
        {
            return this.Categoria?.Id;
        }
        set
        {
            if (value != null)
            {
                this.Categoria.Id = value.Value; 
            }
        }
    }

    [Required(ErrorMessage = "A Categoria é obrigatória.")]
    public CategoriaDto? Categoria { get; set; } = new();
    public IList<HyperMediaLink> Links { get; set; } = new List<HyperMediaLink>();
}