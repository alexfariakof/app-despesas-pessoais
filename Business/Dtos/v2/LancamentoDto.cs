using Business.Dtos.Core;

namespace Business.Dtos.v2;
public class LancamentoDto : BaseLancamentoDto
{
    public override int DespesaId { get; set; }
    public override int ReceitaId { get; set; }
    public override decimal Valor { get; set; }
    public override string? Data { get; set; }
    public override string? Descricao { get; set; }
    public override string? TipoCategoria { get; set; }
    public override string? Categoria { get; set; }
}