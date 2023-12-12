using despesas_backend_api_net_core.Domain.Entities;
using System.Text.Json.Serialization;

namespace despesas_backend_api_net_core.Domain.VM
{
    public class BaseModelVM: BaseModel
    {
        [JsonIgnore]
        public int IdUsuario { get; set; }
    }
}
