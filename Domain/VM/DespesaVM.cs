using despesas_backend_api_net_core.Domain.Entities;

namespace despesas_backend_api_net_core.Domain.VM
{
    public class DespesaVM : BaseModel
    {
        public DateTime Data { get; set; }
        public string Descricao { get; set; }
        public Decimal Valor { get; set; }
        public DateTime DataVencimento { get; set; }
        public int IdUsuario { get; set; }
        public virtual UsuarioVM Usuario { get; set; }
        public int IdCategoria { get; set; }
        public virtual CategoriaVM Categoria { get; set; }
    }
}
