using System.ComponentModel.DataAnnotations;

namespace Domain.VM;
public class ChangePasswordVM
{
    [Required]
    public string? Senha { get; set; }    
    [Required]
    public string? ConfirmaSenha { get; set; }
}
