namespace despesas_backend_api_net_core.Domain.Entities
{
    public class Grafico
    {
        public virtual Dictionary<string, decimal>  SomatorioDespesasPorAno { get; set; }
        public virtual Dictionary<string, decimal> SomatorioReceitasPorAno { get; set; }

    }

}