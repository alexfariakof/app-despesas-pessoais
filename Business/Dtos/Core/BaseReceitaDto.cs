namespace Business.Dtos.Core;
public abstract class BaseReceitaDto : BaseModelDto
{
    public DateTime? Data { get; set; }
    public string? Descricao { get; set; }
    public decimal Valor { get; set; }
    public int? IdCategoria { get; set; }

}