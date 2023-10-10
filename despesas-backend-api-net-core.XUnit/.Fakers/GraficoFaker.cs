using Bogus;

namespace despesas_backend_api_net_core.XUnit.Fakers
{
    public static class GraficoFaker
    {
        public static Grafico GetNewFaker()
        {
            return new Faker<Grafico>()
            .RuleFor(g => g.SomatorioDespesasPorAno, f => new Dictionary<string, decimal>
            {
                { "2020", f.Random.Decimal(1000, 5000) },
                { "2021", f.Random.Decimal(1000, 5000) },
                { "2022", f.Random.Decimal(1000, 5000) },
            })
            .RuleFor(g => g.SomatorioReceitasPorAno, f => new Dictionary<string, decimal>
            {
                { "2020", f.Random.Decimal(1000, 5000) },
                { "2021", f.Random.Decimal(1000, 5000) },
                { "2022", f.Random.Decimal(1000, 5000) },
            });
        }
    }
}
