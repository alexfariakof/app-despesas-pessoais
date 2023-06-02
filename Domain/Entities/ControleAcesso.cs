namespace despesas_backend_api_net_core.Domain.Entities
{
    public class ControleAcesso : BaseModel
    {
        public string Login { get; set; }
        public string Senha { get; set; }
        public int UsuarioId { get; set; }
        public virtual Usuario Usuario { get; set; }
        internal ICollection<Usuario> Usuarios { get; }
    }
}