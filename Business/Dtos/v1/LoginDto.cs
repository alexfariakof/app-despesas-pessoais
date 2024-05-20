using Business.Dtos.Core;
using System.ComponentModel.DataAnnotations;

namespace Business.Dtos.v1;
public class LoginDto: LoginDtoBase
{
    [EmailAddress]
    [Required]
    public override string? Email { get; set; }

    [Required]
    public override string? Senha { get; set; }
}