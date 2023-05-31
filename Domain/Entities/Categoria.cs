
namespace despesas_backend_api_net_core.Domain.Entities
{
    public class Categoria : BaseModel
    {        
        public string Descricao { get; set; }
        public int UsuarioId { get; set; }        
        public virtual TipoCategoria TipoCategoria { get; set; }


    }
}
