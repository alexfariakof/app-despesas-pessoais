using despesas_backend_api_net_core.Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace despesas_backend_api_net_core.Domain.VM
{
    public class CategoriaVM : BaseModelVM
    {
        [Required]
        public String Descricao { get; set; }
        
        [Required]
        public int IdTipoCategoria { get; set; }
        internal virtual TipoCategoria TipoCategoria { get { return (TipoCategoria)IdTipoCategoria; } set   {  IdTipoCategoria = (int)value;  } }
    }
}