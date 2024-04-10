using Domain.Core;
using System.Text.Json.Serialization;

namespace Domain.VM;
public class BaseModelVM: BaseModel
{
    [JsonIgnore]
    public int IdUsuario { get; set; }
}
