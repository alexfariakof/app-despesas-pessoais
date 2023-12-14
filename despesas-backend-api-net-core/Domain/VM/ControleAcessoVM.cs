using System.ComponentModel.DataAnnotations;

namespace despesas_backend_api_net_core.Domain.VM
{
    public class ControleAcessoVM
    {
        [Required]
        public string Nome { get; set; }
        public string SobreNome { get; set; }

        [Required]
        public string Telefone { get; set; }

        [Required]
        public string Email { get; set; }
        
        [Required]
        public string Senha { get; set; }

        [Required]
        public string ConfirmaSenha { get; set; }
    }
}