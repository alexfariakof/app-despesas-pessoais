
namespace despesas_backend_api_net_core.Domain.Entities
{
    public class Lancamento : BaseModel
    {
        public Decimal Valor { get; set; }
        public DateTime Data { get; set; }        
        public int IdUsuario { get; set; }
        public virtual Usuario Usuario { get; set; }
        public int IdDespesa { get; set; }
        public virtual Despesa  Despesa { get; set; }
        public int IdReceita { get; set; }
        public virtual Receita Receita { get; set; }
        public int IdCategoria { get; set; }
        public virtual Categoria Categoria { get; set; }

    }
}
