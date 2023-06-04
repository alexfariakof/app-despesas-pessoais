using despesas_backend_api_net_core.Domain.Entities;

namespace despesas_backend_api_net_core.Domain.VM
{
    public class UsuarioVM : BaseModel
    {
        public string Nome { get; set; }
        public string SobreNome { get; set; }
        public string Telefone { get; set; }
        public string Email { get; set; }
        internal PerfilUsuario PerfilUsuario  {get; set;} 
    }
}