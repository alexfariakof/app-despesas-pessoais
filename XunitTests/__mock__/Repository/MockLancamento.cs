using Bogus;

namespace __mock__.Repository;
public sealed class MockLancamento
{
    static int counter = 1;
    public static Lancamento GetLancamento()
    {
        Usuario usuario = MockUsuario.Instance.GetUsuario();
        Despesa despesa = MockDespesa.Instance.GetDespesa();
        Receita receita = MockReceita.Instance.GetReceita();
        Categoria categoria = MockCategoria.Instance.GetCategoria();

        var mockLancamento = new Faker<Lancamento>()
            .RuleFor(l => l.Id, f => Guid.NewGuid())
            .RuleFor(l => l.Valor, f => f.Random.Decimal(1, 90000))
            .RuleFor(l => l.Data, new DateTime(DateTime.Now.Year, new Random().Next(1, 13), 1))
            .RuleFor(l => l.Descricao, f => f.Commerce.ProductName())
            .RuleFor(l => l.UsuarioId, usuario.Id)
            .RuleFor(l => l.Usuario, usuario)
            .RuleFor(l => l.DespesaId, despesa.Id)
            .RuleFor(l => l.Despesa, despesa)
            .RuleFor(l => l.ReceitaId, receita.Id)
            .RuleFor(l => l.Receita, receita)
            .RuleFor(l => l.CategoriaId, categoria.Id)
            .RuleFor(l => l.Categoria, categoria)
            .Generate();
        counter++;
        return mockLancamento;
    }

    public static List<Lancamento> GetLancamentos(int count = 3)
    {
        var listLancamento = new List<Lancamento>();
        for (int i = 0; i < count; i++)
        {
            listLancamento.Add(GetLancamento());
        }
        return listLancamento;
    }
}
