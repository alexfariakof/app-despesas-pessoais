using Domain.Core;

namespace Domain.Entities;
public class Receita : BaseModel
{
    public DateTime Data { get; set; }
    public string? Descricao { get; set; }
    public decimal  Valor { get; set; }
    public virtual int UsuarioId { get; set; }
    public virtual Usuario?  Usuario { get; set; }
    public virtual int CategoriaId { get; set; }
    public virtual Categoria? Categoria { get; set; }
}