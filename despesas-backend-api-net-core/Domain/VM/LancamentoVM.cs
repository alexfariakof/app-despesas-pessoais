using despesas_backend_api_net_core.Domain.Entities;

namespace despesas_backend_api_net_core.Domain.VM
{
    public class LancamentoVM : BaseModel
    {
        public int IdUsuario { get; set; }
        public int IdDespesa { get; set; }
        public int IdReceita { get; set; }
        public String Valor { get; set; }
        public String Data { get; set; }
        public String Descricao { get; set; }
        public String TipoCategoria { get; set; }
        public virtual Categoria? Categoria { get; set; }
    }
}