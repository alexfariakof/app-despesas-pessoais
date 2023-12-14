using System.ComponentModel.DataAnnotations;

namespace despesas_backend_api_net_core.Domain.VM
{
    public class ReceitaVM : BaseModelVM
    {     
        [Required]
        public DateTime Data { get; set; }

        [Required]
        public String Descricao { get; set; }
        [Required]
        public Decimal Valor { get; set; }
        
        [Required]
        public virtual CategoriaVM Categoria { get; set; }
        internal virtual UsuarioVM Usuario { get; set; }
    }
}