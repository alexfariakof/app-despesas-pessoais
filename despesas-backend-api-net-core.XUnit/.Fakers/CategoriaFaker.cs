using despesas_backend_api_net_core.Domain.Entities;

namespace despesas_backend_api_net_core.XUnit.Fakers
{
    public class CategoriaFaker
    {
        static int counter = 1;
        static int counterVM = 1;
        public static Categoria GetNewFaker(Usuario usuario, int? idUsuario = null)
        {
            if (idUsuario == null)
                usuario = UsuarioFaker.GetNewFaker(new Random(1).Next(1, 10));

            var categoriaFaker = new Faker<Categoria>()
                .RuleFor(c => c.Id, counter++)
                .RuleFor(c => c.Descricao, f => f.Commerce.Categories(10)[1])
                .RuleFor(c => c.UsuarioId, usuario.Id)
                .RuleFor(c => c.Usuario, usuario)
                .RuleFor(c => c.TipoCategoria, counter % 2 == 0 ? TipoCategoria.Receita : TipoCategoria.Despesa);

            return categoriaFaker.Generate();
        }

        public static CategoriaVM GetNewFakerVM(UsuarioVM usuarioVM, int? idUsuario = null)
        {
            if (idUsuario == null)
                usuarioVM = UsuarioFaker.GetNewFakerVM(new Random(1).Next(1, 10));

            var categoriaFaker = new Faker<CategoriaVM>()
                .RuleFor(c => c.Id, counterVM++)
                .RuleFor(c => c.Descricao, f => f.Commerce.ProductName())
                .RuleFor(c => c.IdUsuario, f => usuarioVM.Id)
                .RuleFor(c => c.IdTipoCategoria, counter % 2 == 0 ? (int)TipoCategoria.Receita : (int)TipoCategoria.Despesa);

            return categoriaFaker.Generate();
        }
        public static List<CategoriaVM> CategoriasVMs(UsuarioVM? usuarioVM = null, int? idUsaurio = null)
        {
            var listCategoriaVM = new List<CategoriaVM>();
            for (int i = 0; i < 10; i++)
            {
                if (idUsaurio == null)
                    usuarioVM = UsuarioFaker.GetNewFakerVM(new Random(1).Next(1, 10));

                var categoriaVM = GetNewFakerVM(usuarioVM);
                listCategoriaVM.Add(categoriaVM);
            }
            return listCategoriaVM;
        }
        public static List<Categoria> Categorias(Usuario? usuario = null, int? idUsuario = null)
        {
            var listCategoria = new List<Categoria>();
            for (int i = 0; i < 10; i++)
            {
                Categoria categoria = null;
                if (idUsuario == null)
                {
                    usuario = UsuarioFaker.GetNewFaker(new Random(1).Next(1, 10));
                    categoria = GetNewFaker(usuario);
                }
                else
                {
                    categoria = GetNewFaker(usuario, idUsuario);
                }

                
                listCategoria.Add(categoria);
                usuario = UsuarioFaker.GetNewFaker();
            }
            return listCategoria;
        }
    }
}