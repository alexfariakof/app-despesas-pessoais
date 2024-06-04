using EasyCryptoSalt;
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
        set => _senha = Crypto.Instance.Encrypt(value);
    }
    public int UsuarioId { get; set; }

    public string RefreshToken { get; set; } = String.Empty;

    public DateTime? RefreshTokenExpiry { get; set; }

    public virtual Usuario? Usuario { get; set; }
    public void CreateAccount(Usuario usuario, string senha)
    {
        this.Login = usuario.Email;
        this.Senha = senha;
        this.Usuario = usuario;        
    }
}