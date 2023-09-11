using despesas_backend_api_net_core.Domain.Entities;

namespace despesas_backend_api_net_core.Domain.VM
{
    public class ReceitaVM :BaseModel
    {
        public DateTime Data { get; set; }
        public String Descricao { get; set; }
        public Decimal Valor { get; set; }
        public int IdUsuario { get; set; }
        internal virtual UsuarioVM Usuario { get; set; }
        public int IdCategoria { get; set; }
        internal virtual CategoriaVM Categoria { get; set; }

    }
}