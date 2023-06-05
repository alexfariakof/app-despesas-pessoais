namespace despesas_backend_api_net_core.Domain.Entities
{
    public class Lancamento : BaseModel
    {
        public Decimal Valor { get; set; }
        public DateTime Data { get; set; }
        public string Descricao { get; set; }
        public int UsuarioId { get; set; }
        public virtual Usuario? Usuario { get; set; }
        public int? DespesaId { get; set; }
        public virtual Despesa? Despesa { get; set; }
        public int? ReceitaId { get; set; }
        public virtual Receita? Receita { get; set; }
        public int CategoriaId { get; set; }
        public virtual Categoria? Categoria { get; set; }
        public DateTime? DataCriacao { get; set; }
    }
}