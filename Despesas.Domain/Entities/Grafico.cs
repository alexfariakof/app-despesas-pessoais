namespace Domain.Entities;
public class Grafico
{
    public virtual Dictionary<string, decimal>?  SomatorioDespesasPorAno { get; set; }
    public virtual Dictionary<string, decimal>? SomatorioReceitasPorAno { get; set; }
}