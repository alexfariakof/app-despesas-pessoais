using Domain.Core;
using System.Text.Json.Serialization;

namespace Business.Dtos;
public class BaseModelDto: BaseModel
{
    [JsonIgnore]
    public int IdUsuario { get; set; }
}
