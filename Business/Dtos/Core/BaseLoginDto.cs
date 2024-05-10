namespace Business.Dtos.Core;
public abstract class BaseLoginDto
{
    public string? Email { get; set; }
    public string? Senha { get; set; }
}