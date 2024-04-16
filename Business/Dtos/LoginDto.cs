using System.ComponentModel.DataAnnotations;

namespace Business.Dtos;
public class LoginDto
{
    [Required]
    public string? Email { get; set; }
   
    [Required]
    public string? Senha { get; set; }
}