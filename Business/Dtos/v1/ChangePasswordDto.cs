using Business.Dtos.Core;
using System.ComponentModel.DataAnnotations;

namespace Business.Dtos.v1;
public class ChangePasswordDto : ChangePasswordDtoBase
{
    [Required]
    public override string? Senha { get; set; }

    [Required]
    public override string? ConfirmaSenha { get; set; }
}
