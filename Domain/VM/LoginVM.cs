using System.ComponentModel.DataAnnotations;

namespace Domain.VM;
public class LoginVM
{
    [Required]
    public string? Email { get; set; }
   
    [Required]
    public string? Senha { get; set; }
}