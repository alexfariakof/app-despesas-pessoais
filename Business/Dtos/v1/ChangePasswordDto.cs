using Business.Dtos.Core;
using System.ComponentModel.DataAnnotations;

namespace Business.Dtos.v1;
public class ChangePasswordDto : ChangePasswordDtoBase
{
    [Required]
    public string? Senha { get; set; }

    [Required]
    public string? ConfirmaSenha { get; set; }
}
