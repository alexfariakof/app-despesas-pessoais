
namespace despesas_backend_api_net_core.Domain.Entities
{
    public class Lancamento : BaseModel
    {
        public Decimal Valor { get; set; }
        public DateTime Data { get; set; }
        public string Descricao { get; set; }
        public int UsuarioId { get; set; }
        internal virtual Usuario? Usuario { get; set; }
        public int DespesaId { get; set; }
        internal virtual Despesa? Despesa { get; set; }
        public int ReceitaId { get; set; }
        internal virtual Receita? Receita { get; set; }
        public int CategoriaId { get; set; }
        internal virtual Categoria? Categoria { get; set; }

    }
}
