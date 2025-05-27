namespace Business.Dtos.Core;
public abstract class LoginDtoBase
{
    public virtual string? Email { get; set; }
    public virtual string? Senha { get; set; }
}