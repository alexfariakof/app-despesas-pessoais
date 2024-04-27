using System.ComponentModel.DataAnnotations;

namespace Business.Dtos;
public class LoginDto
{
    [EmailAddress(ErrorMessage = "O campo Email é inválido.")]
    [Required(ErrorMessage = "O campo Email é obrigatório.")]    
    public string? Email { get; set; }
   
    [Required(ErrorMessage = "O campo Senha é obrigatório.")]
    public string? Senha { get; set; }
}