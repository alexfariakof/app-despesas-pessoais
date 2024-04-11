using System.ComponentModel.DataAnnotations;

namespace Business.Dtos;
public class ChangePasswordVM
{
    [Required]
    public string? Senha { get; set; }    
    [Required]
    public string? ConfirmaSenha { get; set; }
}
