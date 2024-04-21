using Domain.Core;
using System.Text.RegularExpressions;

namespace Domain.Entities;
public class ControleAcesso : BaseModel
{
    private string _login = string.Empty;
    public string Login
    {
        get => _login;
        set => _login = IsValidEmail(value);
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
    private string IsValidEmail(string email)
    {
        if (email.Length > 256)
            throw new ArgumentException("Email inválido!");

        string pattern = @"^[a-zA-Z0-9_.+-]+@[a-zA-Z0-9-]+\.[a-zA-Z0-9-.]+$";
        Regex regex = new Regex(pattern);

        if (!regex.IsMatch(email))
            throw new ArgumentException("Email inválido!");

        return email;
    }
}