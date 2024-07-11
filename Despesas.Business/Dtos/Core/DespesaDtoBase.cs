namespace Business.Dtos.Core;
public abstract class DespesaDtoBase : ModelDtoBase
{
    public virtual DateTime? Data { get; set; }
    public virtual string? Descricao { get; set; }
    public virtual decimal Valor { get; set; }
    public virtual DateTime? DataVencimento { get; set; }
    public virtual Guid? IdCategoria { get; set; }
}