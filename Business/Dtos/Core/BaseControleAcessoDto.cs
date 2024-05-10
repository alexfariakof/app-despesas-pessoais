namespace Business.Dtos.Core;
public abstract class BaseControleAcessoDto : BaseUsuarioDto
{
    public string? Senha { get; set; }
    public string? ConfirmaSenha { get; set; }
    public string? RefreshToken { get; set; }
}