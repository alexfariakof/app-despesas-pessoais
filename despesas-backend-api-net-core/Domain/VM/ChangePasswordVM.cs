using System.ComponentModel.DataAnnotations;

namespace despesas_backend_api_net_core.Domain.VM
{
    public class ChangePasswordVM
    {
        [Required]
        public string Senha { get; set; }
        
        [Required]
        public string ConfirmaSenha { get; set; }
    }
}
