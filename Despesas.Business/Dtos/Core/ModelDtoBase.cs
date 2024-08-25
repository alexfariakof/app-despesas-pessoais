using Domain.Core;
using System.Text.Json.Serialization;

namespace Business.Dtos.Core;
public abstract class ModelDtoBase : BaseModel
{
    [JsonIgnore]
    public Guid UsuarioId { get; set; }
}
