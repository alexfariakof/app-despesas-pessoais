namespace Business.Dtos.Core;
public abstract class BaseLancamentoDto : BaseModelDto
{
    public virtual int DespesaId { get; set; }
    public virtual int ReceitaId { get; set; }
    public virtual decimal Valor { get; set; }
    public virtual string? Data { get; set; }
    public virtual string? Descricao { get; set; }
    public virtual string? TipoCategoria { get; set; }
    public virtual string? Categoria { get; set; }
}