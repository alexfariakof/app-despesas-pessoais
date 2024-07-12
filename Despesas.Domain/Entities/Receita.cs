using Domain.Core;

namespace Domain.Entities;
public class Receita : BaseModel
{
    public DateTime Data { get; set; }
    public string Descricao { get; set; } = String.Empty;
    public decimal  Valor { get; set; }
    public virtual Guid UsuarioId { get; set; }
    public virtual Usuario? Usuario { get; set; }
    public virtual Guid CategoriaId { get; set; }
    public virtual Categoria? Categoria { get; set; }
}