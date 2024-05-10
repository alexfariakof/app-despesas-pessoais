namespace Business.Dtos.Core;
public abstract class BaseChangePasswordDto
{
    public string? Senha { get; set; }
    public string? ConfirmaSenha { get; set; }
}
