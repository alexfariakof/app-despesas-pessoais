namespace XUnit.Fakers;
public class LancamentoFaker
{
    static int counter = 1;
    static int counterVM = 1;

    public static Lancamento GetNewFaker(
        Usuario usuario,
        Despesa despesa,
        Receita receita,
        Categoria categoria
    )
    {
        var LancamentoFaker = new Faker<Lancamento>()
            .RuleFor(l => l.Id, f => counter++)
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
            .RuleFor(l => l.Categoria, categoria);

        return LancamentoFaker.Generate();
    }

    public static LancamentoDto GetNewFakerVM(
        int idusuario,
        int idDespesa,
        int idReceita,
        Categoria categoria
    )
    {
        var lancamentoDtoFaker = new Faker<LancamentoDto>()
            .RuleFor(l => l.Id, f => counterVM++)
            .RuleFor(l => l.Valor, f => f.Random.Decimal(1, 90000))
            .RuleFor(
                l => l.Data,
                f => new DateTime(DateTime.Now.Year, new Random().Next(1, 13), 1).ToString()
            )
            .RuleFor(l => l.Descricao, f => f.Commerce.ProductName())
            .RuleFor(l => l.IdUsuario, idusuario)
            .RuleFor(l => l.IdDespesa, idDespesa)
            .RuleFor(l => l.IdReceita, idReceita)
            .RuleFor(c => c.TipoCategoria, f => f.PickRandom<TipoCategoria>().ToString())
            .RuleFor(l => l.Categoria, categoria.Descricao);

        return lancamentoDtoFaker.Generate();
    }

    public static List<LancamentoDto> LancamentoDtos(
        Usuario? usuario = null,
        int? idUsuario = null
    )
    {
        var listLancamentoDto = new List<LancamentoDto>();
        for (int i = 0; i < 10; i++)
        {
            if (idUsuario == null)
                usuario = UsuarioFaker.Instance.GetNewFaker();

            var categoria = CategoriaFaker.Instance.GetNewFaker(usuario);

            var despesa = DespesaFaker.Instance.GetNewFaker(usuario, categoria);
            var receita = ReceitaFaker.Instance.GetNewFaker(usuario, categoria);
            var lancamentoDto = GetNewFakerVM(usuario.Id, despesa.Id, receita.Id, categoria);
            listLancamentoDto.Add(lancamentoDto);
        }

        return listLancamentoDto;
    }

    public static List<Lancamento> Lancamentos(Usuario? usuario = null, int? idUsuario = null)
    {
        var listLancamento = new List<Lancamento>();
        for (int i = 0; i < 10; i++)
        {
            if (idUsuario == null)
                usuario = UsuarioFaker.Instance.GetNewFaker();

            var categoria = CategoriaFaker.Instance.GetNewFaker(usuario, TipoCategoria.Todas, usuario.Id);

            var despesa = DespesaFaker.Instance.GetNewFaker(usuario, categoria);
            var receita = ReceitaFaker.Instance.GetNewFaker(usuario, categoria);
            var lancamento = GetNewFaker(usuario, despesa, receita, categoria);
            listLancamento.Add(lancamento);
        }
        return listLancamento;
    }
}
