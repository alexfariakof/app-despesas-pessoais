namespace Business.Dtos;
public class LancamentoDto : BaseModelDto
{
    public int IdDespesa { get; set; }
    public int IdReceita { get; set; }
    public decimal Valor { get; set; }
    public string? Data { get; set; }
    public string? Descricao { get; set; }
    public string? TipoCategoria { get; set; }
    public string? Categoria { get; set; }
}