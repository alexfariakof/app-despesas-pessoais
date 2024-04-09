using Domain.Core;

namespace Domain.Entities;
public class Categoria : BaseModel
{        
    public string? Descricao { get; set; }
    public int UsuarioId { get; set; }        
    public virtual Usuario? Usuario { get; set; }
    public virtual TipoCategoria TipoCategoria { get; set; }             
}