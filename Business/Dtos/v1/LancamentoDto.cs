using Business.Dtos.Core;

namespace Business.Dtos.v1;
public class LancamentoDto : BaseLancamentoDto
{
    public int IdDespesa { get; set; }
    public int IdReceita { get; set; }
    public decimal Valor { get; set; }
    public string? Data { get; set; }
    public string? Descricao { get; set; }
    public string? TipoCategoria { get; set; }
    public string? Categoria { get; set; }
}