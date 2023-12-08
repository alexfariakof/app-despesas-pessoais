using despesas_backend_api_net_core.Domain.Entities;

namespace despesas_backend_api_net_core.Domain.VM
{
    public class ImagemPerfilVM : BaseModel
    {
        public string Url { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public string ContentType { get; set; }
        public int IdUsuario { get; set; }
        public virtual byte[] Arquivo { get; set; }    
    }
}