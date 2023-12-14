using System.ComponentModel.DataAnnotations;

namespace despesas_backend_api_net_core.Domain.VM
{
    public class DespesaVM : BaseModelVM
    {
        [Required]
        public DateTime Data { get; set; }
        
        [Required]
        public string Descricao { get; set; }
        
        [Required]
        public Decimal Valor { get; set; }
        public DateTime? DataVencimento { get; set; }
               
        [Required]
        public  CategoriaVM Categoria { get; set; }
        internal virtual UsuarioVM Usuario { get; set; }
    }
}