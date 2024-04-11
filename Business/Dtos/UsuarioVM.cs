using Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace Business.Dtos;
public class UsuarioVM : BaseModelVM
{
    [Required]
    public string? Nome { get; set; }
    public string? SobreNome { get; set; }    
    [Required]
    public string? Telefone { get; set; }    
    [Required]
    public string? Email { get; set; }
    public PerfilUsuario PerfilUsuario  {get; set;} 
}