namespace despesas_backend_api_net_core.Domain.VM
{
    public class LoginVM 
    {
        internal int? IdUsuario { get; set; }
        public string? Email { get; set; }
        public string Senha { get; set; }
        internal string? ConfirmaSenha { get; set; }

    }
}