using System.ComponentModel.DataAnnotations;

namespace despesas_backend_api_net_core.Domain.VM
{
    public class LoginVM
    {
        [Required]
        public string Email { get; set; }
       
        [Required]
        public string Senha { get; set; }
    }
}