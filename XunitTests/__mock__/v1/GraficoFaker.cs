using Bogus;

namespace __mock__.v1;
public sealed class GraficoFaker
{
    public static Grafico GetNewFaker()
    {
        return new Faker<Grafico>()
        .RuleFor(g => g.SomatorioDespesasPorAno, f => new Dictionary<string, decimal>
        {
            { "Janeiro", f.Random.Decimal(1, 5000) },
            { "Fevereiro", f.Random.Decimal(1, 5000) },
            { "Março", f.Random.Decimal(1, 5000) },
            { "Abril", f.Random.Decimal(1, 5000) },
            { "Maio", f.Random.Decimal(1, 5000) },
            { "Junho", f.Random.Decimal(1, 5000) },
            { "Julho", f.Random.Decimal(1, 5000) },
            { "Agosto", f.Random.Decimal(1, 5000) },
            { "Setembro", f.Random.Decimal(1, 5000) },
            { "Outubro", f.Random.Decimal(1, 5000) },
            { "Novembro", f.Random.Decimal(1, 5000) },
            { "Dezembro", f.Random.Decimal(1, 5000) },
        })
        .RuleFor(g => g.SomatorioReceitasPorAno, f => new Dictionary<string, decimal>
        {
            { "Janeiro", f.Random.Decimal(1, 5000) },
            { "Fevereiro", f.Random.Decimal(1, 5000) },
            { "Março", f.Random.Decimal(1, 5000) },
            { "Abril", f.Random.Decimal(1, 5000) },
            { "Maio", f.Random.Decimal(1, 5000) },
            { "Junho", f.Random.Decimal(1, 5000) },
            { "Julho", f.Random.Decimal(1, 5000) },
            { "Agosto", f.Random.Decimal(1, 5000) },
            { "Setembro", f.Random.Decimal(1, 5000) },
            { "Outubro", f.Random.Decimal(1, 5000) },
            { "Novembro", f.Random.Decimal(1, 5000) },
            { "Dezembro", f.Random.Decimal(1, 5000) },
        });

    }
}
