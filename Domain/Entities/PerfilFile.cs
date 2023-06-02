namespace despesas_backend_api_net_core.Domain.Entities
{
    public class PerfilFile :BaseModel
    {
        public string Url { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public int UsuarioId { get; set; }
        public virtual Usuario Usuario { get; set; }        
    }
}