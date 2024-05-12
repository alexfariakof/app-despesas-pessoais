namespace Business.Dtos.Core;
public abstract class ChangePasswordDtoBase
{
    public virtual string? Senha { get; set; }
    public virtual string? ConfirmaSenha { get; set; }
}
