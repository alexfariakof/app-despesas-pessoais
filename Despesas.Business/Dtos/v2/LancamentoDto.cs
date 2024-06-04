using Business.Dtos.Core;

namespace Business.Dtos.v2;
public class LancamentoDto : LancamentoDtoBase
{
    public override int IdDespesa { get; set; }
    public override int IdReceita { get; set; }
    public override decimal Valor { get; set; }
    public override string? Data { get; set; }
    public override string? Descricao { get; set; }
    public override string? TipoCategoria { get; set; }
    public override string? Categoria { get; set; }
}