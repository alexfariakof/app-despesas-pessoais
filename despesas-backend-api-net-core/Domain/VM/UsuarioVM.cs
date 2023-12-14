using despesas_backend_api_net_core.Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace despesas_backend_api_net_core.Domain.VM
{
    public class UsuarioVM : BaseModel
    {
        [Required]
        public string Nome { get; set; }
        public string SobreNome { get; set; }
        
        [Required]
        public string Telefone { get; set; }
        
        [Required]
        public string Email { get; set; }
        internal PerfilUsuario PerfilUsuario  {get; set;} 
    }
}