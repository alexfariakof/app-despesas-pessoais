using Business.HyperMedia;
using Business.HyperMedia.Abstractions;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Business.Dtos;
public class DespesaDto : BaseModelDto, ISupportHyperMedia
{
    [Required(ErrorMessage = "O campo Data é obrigatório.")]
    public DateTime Data { get; set; }    
    
    [Required(ErrorMessage = "O campo Descrição é obrigatório.")]
    public string? Descricao { get; set; }

    [Required(ErrorMessage = "O campo Valor é obrigatório.")]
    public decimal Valor { get; set; }
    public DateTime? DataVencimento { get; set; }

    [Required(ErrorMessage = "A Categoria é obrigatória.")]
    public CategoriaDto? Categoria { get; set; }
    
    [JsonIgnore]
    public UsuarioDto? Usuario { get; set; }

    public IList<HyperMediaLink> Links { get; set; } = new List<HyperMediaLink>();
}