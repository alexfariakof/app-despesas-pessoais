namespace despesas_backend_api_net_core.Domain.Entities
{
    public class Usuario : BaseModel
    {
        public string Nome { get; set; }
        public string sobreNome { get; set; }
        public string telefone { get; set; }
        public string Email { get; set; }
    }
}
