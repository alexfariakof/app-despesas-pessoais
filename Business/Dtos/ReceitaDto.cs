using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Business.Dtos;
public class ReceitaDto : BaseModelDto
{     
    [Required]
    public DateTime Data { get; set; }

    [Required]
    public string? Descricao { get; set; }
    
    [Required]
    public decimal Valor { get; set; }
    
    [Required]
    public CategoriaDto? Categoria { get; set; }
    
    [JsonIgnore]
    public UsuarioDto? Usuario { get; set; }
}