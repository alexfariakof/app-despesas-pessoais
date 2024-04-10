using System.ComponentModel.DataAnnotations;

namespace Domain.VM;
public class ReceitaVM : BaseModelVM
{     
    [Required]
    public DateTime Data { get; set; }

    [Required]
    public string? Descricao { get; set; }
    [Required]
    public decimal Valor { get; set; }
    
    [Required]
    public CategoriaVM? Categoria { get; set; }
    public UsuarioVM? Usuario { get; set; }
}