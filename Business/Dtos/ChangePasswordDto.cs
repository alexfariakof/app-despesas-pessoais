using System.ComponentModel.DataAnnotations;

namespace Business.Dtos;
public class ChangePasswordDto
{
    [Required]
    public string? Senha { get; set; }    
    [Required]
    public string? ConfirmaSenha { get; set; }
}
