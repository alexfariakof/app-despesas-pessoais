namespace despesas_backend_api_net_core.Domain.VM
{
    public class LancamentoVM : BaseModelVM
    {
        public int IdDespesa { get; set; }
        public int IdReceita { get; set; }
        public Decimal Valor { get; set; }
        public String Data { get; set; }
        public String Descricao { get; set; }
        public String TipoCategoria { get; set; }
        public String Categoria { get; set; }
    }
}