using System.Text.Json.Serialization;

namespace despesas_backend_api_net_core.Domain.VM
{
    public class LoginVM
    {   
        public string Email { get; set; }
        public string Senha { get; set; }
    }

}