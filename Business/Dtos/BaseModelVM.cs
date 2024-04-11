using Domain.Core;
using System.Text.Json.Serialization;

namespace Business.Dtos;
public class BaseModelVM: BaseModel
{
    [JsonIgnore]
    public int IdUsuario { get; set; }
}
