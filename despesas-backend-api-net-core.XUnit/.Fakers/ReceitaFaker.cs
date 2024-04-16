namespace XUnit.Fakers;
public class ReceitaFaker
{
    static int counter = 1;
    static int counterVM = 1;

    private static ReceitaFaker? _instance;
    private static readonly object LockObject = new object();
    public static ReceitaFaker Instance
    {
        get
        {
            lock (LockObject)
            {
                return _instance ??= new ReceitaFaker();
            }
        }
    }


    public Receita GetNewFaker(Usuario usuario, Categoria categoria)
    {
        var receitaFaker = new Faker<Receita>()
            .RuleFor(r => r.Id, f => counter++)
            .RuleFor(r => r.Data, new DateTime(DateTime.Now.Year, new Random().Next(1, 13), 1))
            .RuleFor(r => r.Descricao, f => f.Commerce.ProductName())
            .RuleFor(r => r.Valor, f => f.Random.Decimal(1, 900000))
            .RuleFor(r => r.UsuarioId, usuario.Id)
            .RuleFor(r => r.Usuario, usuario)                
            .RuleFor(r => r.Categoria, CategoriaFaker.Instance.GetNewFaker(usuario, TipoCategoria.Receita, usuario.Id));

        return receitaFaker.Generate();
    }

    public ReceitaDto GetNewFakerVM(int idUsuario, int idCategoria)
    {
        var receitaFaker = new Faker<ReceitaDto>()
            .RuleFor(r => r.Id, f => counterVM++)
            .RuleFor(r => r.Data, new DateTime(DateTime.Now.Year, new Random().Next(1, 13), 1))
            .RuleFor(r => r.Descricao, f => f.Commerce.ProductName())
            .RuleFor(r => r.Valor, f => f.Random.Decimal(1, 900000))
            .RuleFor(r => r.IdUsuario, idUsuario)
            .RuleFor(r => r.Categoria, CategoriaFaker.Instance.GetNewFakerVM(UsuarioFaker.Instance.GetNewFakerVM(idUsuario),  TipoCategoria.Receita, idUsuario)
        );

        return receitaFaker.Generate();
    }

    public List<ReceitaDto> ReceitasVMs(
        UsuarioDto? usuarioVM = null,
        int? idUsuario = null
    )
    {
        var listReceitaVM = new List<ReceitaDto>();
        for (int i = 0; i < 10; i++)
        {
            if (idUsuario == null)
                usuarioVM = UsuarioFaker.Instance.GetNewFakerVM(new Random().Next(1, 10));

            var categoriaVM = CategoriaFaker.Instance.GetNewFakerVM(usuarioVM);

            var receitaVM = GetNewFakerVM(usuarioVM.Id, categoriaVM.Id);
            listReceitaVM.Add(receitaVM);
        }
        return listReceitaVM;
    }

    public List<Receita> Receitas(Usuario? usuario = null, int? idUsuario = null, int count = 10 )
    {
        var listReceita = new List<Receita>();
        for (int i = 0; i < count; i++)
        {
            if (idUsuario == null)
                usuario = UsuarioFaker.Instance.GetNewFaker(new Random().Next(1, 10));

            var categoria = CategoriaFaker.Instance.GetNewFaker(usuario, TipoCategoria.Receita, usuario.Id);

            var receita = GetNewFaker(usuario, categoria);
            listReceita.Add(receita);
        }
        return listReceita;
    }
}
