using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace despesas_backend_api_net_core.Domain.VM
{
    public class ControleAcessoVM : UsuarioVM
    {
        [JsonIgnore]
        public override int Id { get; set; }

        [Required]
        public string Senha { get; set; }

        [Required]
        public string ConfirmaSenha { get; set; }
    }
}