namespace XUnit.Fakers;
public class CategoriaFaker
{
    static int counter = 1;
    static int counterVM = 1;

    private static CategoriaFaker? _instance;
    private static readonly object LockObject = new object();
    public static CategoriaFaker Instance
    {
        get
        {
            lock (LockObject)
            {
                return _instance ??= new CategoriaFaker();
            }
        }
    }

    public Categoria GetNewFaker(Usuario usuario, TipoCategoria tipoCategoria = TipoCategoria.Todas, int? idUsuario = null)
    {
        if (idUsuario == null)
            usuario = UsuarioFaker.Instance.GetNewFaker();

        var categoriaFaker = new Faker<Categoria>()
            .RuleFor(c => c.Id, counter++)
            .RuleFor(c => c.Descricao, f => f.Commerce.Categories(10)[1])
            .RuleFor(c => c.UsuarioId, usuario.Id)
            .RuleFor(c => c.Usuario, usuario)
            .RuleFor(c => c.TipoCategoria, tipoCategoria == TipoCategoria.Todas ? counter % 2 == 0 ? TipoCategoria.Receita : TipoCategoria.Despesa : tipoCategoria);

        return categoriaFaker.Generate();
    }

    public CategoriaVM GetNewFakerVM(UsuarioVM usuarioVM, TipoCategoria tipoCategoria = TipoCategoria.Todas, int? idUsuario = null)
    {
        if (idUsuario == null)
            usuarioVM = UsuarioFaker.Instance.GetNewFakerVM();

        var categoriaFaker = new Faker<CategoriaVM>()
            .RuleFor(c => c.Id, counterVM++)
            .RuleFor(c => c.Descricao, f => f.Commerce.ProductName())
            .RuleFor(c => c.IdUsuario, f => usuarioVM.Id)
            .RuleFor(c => c.IdTipoCategoria, tipoCategoria.Equals(TipoCategoria.Todas) ? counter % 2 == 0 ? (int)TipoCategoria.Receita : (int)TipoCategoria.Despesa : (int)tipoCategoria);

        return categoriaFaker.Generate();
    }

    public List<CategoriaVM> CategoriasVMs(UsuarioVM? usuarioVM = null, TipoCategoria tipoCategoria = TipoCategoria.Todas, int? idUsuario = null)
    {
        var listCategoriaVM = new List<CategoriaVM>();
        for (int i = 0; i < 10; i++)
        {
            if (idUsuario == null)
                usuarioVM = UsuarioFaker.Instance.GetNewFakerVM(new Random(1).Next(1, 10));

            var categoriaVM = GetNewFakerVM(usuarioVM, tipoCategoria);

            listCategoriaVM.Add(categoriaVM);
        }
        return listCategoriaVM;
    }

    public List<Categoria> Categorias(Usuario? usuario = null, TipoCategoria tipoCategoria = TipoCategoria.Todas, int? idUsuario = null)
    {
        var listCategoria = new List<Categoria>();
        for (int i = 0; i < 10; i++)
        {
            Categoria categoria = null;

            if (idUsuario == null)
            {
                usuario = UsuarioFaker.Instance.GetNewFaker(new Random(1).Next(1, 10));
                categoria = GetNewFaker(usuario, tipoCategoria);
            }
            else
            {
                categoria = GetNewFaker(usuario, tipoCategoria, idUsuario);
            }

            listCategoria.Add(categoria);

        }
        return listCategoria;
    }
}
