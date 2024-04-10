using System.ComponentModel.DataAnnotations;

namespace Domain.VM;
public class DespesaVM : BaseModelVM
{
    [Required]
    public DateTime Data { get; set; }    
    [Required]
    public string? Descricao { get; set; }    
    [Required]
    public decimal Valor { get; set; }
    public DateTime? DataVencimento { get; set; }           
    [Required]
    public CategoriaVM? Categoria { get; set; }
    public UsuarioVM? Usuario { get; set; }
}