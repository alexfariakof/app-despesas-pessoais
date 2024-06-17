using Bogus;

namespace __mock__.Repository;
public sealed class MockDespesa
{
    private static MockDespesa? _instance;
    private static readonly object LockObject = new object();
    public static MockDespesa Instance
    {
        get
        {
            lock (LockObject)
            {
                return _instance ??= new MockDespesa();
            }
        }
    }

    public Despesa GetDespesa()
    {
        var mockDespesa = new Faker<Despesa>()
        .RuleFor(r => r.Data, new DateTime(DateTime.Now.Year, new Random().Next(1, 13), 1))
        .RuleFor(r => r.DataVencimento, new DateTime(DateTime.Now.Year, new Random().Next(1, 13), 1))
        .RuleFor(r => r.Descricao, f => f.Commerce.ProductName())
        .RuleFor(r => r.Valor, f => f.Random.Decimal(1, 900000))
        .RuleFor(r => r.Categoria, MockCategoria.Instance.GetCategoria())
        .Generate();
        return mockDespesa;

    }

    public List<Despesa> GetDespesas(int count = 10)
    {
        var listDespesa = new List<Despesa>();
        for (int i = 0; i < count; i++)
        {
            listDespesa.Add(GetDespesa());
        }
        return listDespesa;
    }
}
