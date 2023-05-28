
namespace despesas_backend_api_net_core.Domain.VM
{
    public class LancamentoVM
    {
        public int Id { get; set; }
        public int IdUsuario { get; set; }
        public int IdDespesa { get; set; }
        public int IdReceita { get; set; }
        public String Valor { get; set; }
        public String Data { get; set; }
        public String Descricao { get; set; }
        public String Categoria { get; set; }
    }
}
