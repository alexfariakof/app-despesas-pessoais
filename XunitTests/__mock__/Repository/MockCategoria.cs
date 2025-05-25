using Bogus;
using Domain.Entities.ValueObjects;

namespace __mock__.Repository;
public sealed class MockCategoria
{
    private static MockCategoria? _instance;
    private static readonly object LockObject = new object();
    public static MockCategoria Instance
    {
        get
        {
            lock (LockObject)
            {
                return _instance ??= new MockCategoria();
            }
        }
    }

    public Categoria GetCategoria()
    {
        var mockCategoria = new Faker<Categoria>()
            .RuleFor(c => c.Descricao, f => f.Commerce.Categories(10)[1])
            .Generate();
        mockCategoria.TipoCategoria = new Random().Next(1, 2) % 2 == 0 ? new TipoCategoria(TipoCategoria.CategoriaType.Receita) : new TipoCategoria(TipoCategoria.CategoriaType.Despesa);
        return mockCategoria;
    }

    public List<Categoria> GetCategorias(int count = 3)
    {
        var listCategoria = new List<Categoria>();
        for (int i = 0; i < count; i++)
        {
            Categoria categoria = MockCategoria.Instance.GetCategoria();
            listCategoria.Add(categoria);

        }
        return listCategoria;
    }
}
