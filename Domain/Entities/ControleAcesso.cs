using Domain.Core;

namespace Domain.Entities;
public class ControleAcesso : BaseModel
{
    private string _login = string.Empty;
    public string Login
    {
        get => _login;
        set => _login = value;
    }

    private string _senha = string.Empty;
    public string Senha
    {
        get => _senha;
        set => _senha = Crypto.GetInstance.Encrypt(value);
    }
    public int UsuarioId { get; set; }

    public string? RefreshToken { get; set; }

    public DateTime? RefreshTokenExpiry { get; set; }

    public virtual Usuario? Usuario { get; set; }
    public void CreateAccount(Usuario usuario, string email, string senha)
    {
        Login = email;
        Senha = senha;
        Usuario = usuario;        
    }
}