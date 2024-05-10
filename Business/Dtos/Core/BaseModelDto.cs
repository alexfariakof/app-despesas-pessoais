using Domain.Core;
using System.Text.Json.Serialization;

namespace Business.Dtos.Core;
public abstract class BaseModelDto : BaseModel
{
    [JsonIgnore]
    public int UsuarioId { get; set; }
}
