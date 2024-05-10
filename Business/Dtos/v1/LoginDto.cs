using Business.Dtos.Core;
using System.ComponentModel.DataAnnotations;

namespace Business.Dtos.v1;
public class LoginDto: BaseLoginDto
{
    [EmailAddress]
    [Required]
    public string? Email { get; set; }

    [Required]
    public string? Senha { get; set; }
}