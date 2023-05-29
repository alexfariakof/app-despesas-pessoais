namespace despesas_backend_api_net_core.Domain.Entities
{
    public class Despesa : BaseModel
    {
        public DateTime Data { get; set; }
        public string Descricao { get; set; }
        public Decimal Valor { get; set; }
        public DateTime DataVencimento { get; set; }
        public int UsuarioId { get; set; }
        public virtual Usuario Usuario { get; set; }
        public int CategoriaId { get; set; }
        public virtual Categoria Categoria { get; set; }


    }
}
