using System.ComponentModel.DataAnnotations;

namespace Business.Dtos;
public class DespesaDto : BaseModelDto
{
    [Required]
    public DateTime Data { get; set; }    
    [Required]
    public string? Descricao { get; set; }    
    [Required]
    public decimal Valor { get; set; }
    public DateTime? DataVencimento { get; set; }           
    [Required]
    public CategoriaDto? Categoria { get; set; }
    public UsuarioDto? Usuario { get; set; }
}