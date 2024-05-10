namespace Business.Dtos.Core;
public abstract class BaseReceitaDto : BaseModelDto
{
    public virtual DateTime? Data { get; set; }
    public virtual string? Descricao { get; set; }
    public virtual decimal Valor { get; set; }
    public virtual int? CategoriaId { get; set; }
}