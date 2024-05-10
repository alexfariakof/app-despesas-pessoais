namespace Business.Dtos.Core;
public abstract class BaseDespesaDto : BaseModelDto
{
    public DateTime? Data { get; set; }
    public string? Descricao { get; set; }
    public decimal Valor { get; set; }
    public DateTime? DataVencimento { get; set; }
    public int? IdCategoria { get; set; }
}