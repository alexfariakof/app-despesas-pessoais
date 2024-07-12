using Domain.Core;

namespace Domain.Entities;
public class Lancamento : BaseModel
{
    public decimal Valor { get; set; }
    public DateTime Data { get; set; }
    public string? Descricao { get; set; }
    public Guid UsuarioId { get; set; }
    public virtual Usuario? Usuario { get; set; }
    public Guid DespesaId { get; set; }
    public virtual Despesa? Despesa { get; set; }
    public Guid ReceitaId { get; set; }
    public virtual Receita? Receita { get; set; }
    public Guid CategoriaId { get; set; }
    public virtual Categoria? Categoria { get; set; }
    public DateTime DataCriacao { get; set; }
}