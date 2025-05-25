using Bogus;

namespace __mock__.Repository;
public sealed class MockReceita
{
    private static MockReceita? _instance;
    private static readonly object LockObject = new object();
    public static MockReceita Instance
    {
        get
        {
            lock (LockObject)
            {
                return _instance ??= new MockReceita();
            }
        }
    }

    public Receita GetReceita()
    {
        var mockReceita = new Faker<Receita>()
            .RuleFor(r => r.Data, new DateTime(DateTime.Now.Year, new Random().Next(1, 13), 1))
            .RuleFor(r => r.Descricao, f => f.Commerce.ProductName())
            .RuleFor(r => r.Valor, f => f.Random.Decimal(1, 900000))
            .RuleFor(r => r.Categoria, MockCategoria.Instance.GetCategoria())
            .Generate();
        return mockReceita;

    }

    public List<Receita> GetReceitas(int count = 10)
    {
        var listReceita = new List<Receita>();
        for (int i = 0; i < count; i++)
        {
            listReceita.Add(GetReceita());
        }
        return listReceita;
    }
}
