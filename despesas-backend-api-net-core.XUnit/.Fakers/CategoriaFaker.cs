using Bogus;

namespace despesas_backend_api_net_core.XUnit.Fakers
{
    public static class CategoriaFaker
    {
        static int counter = 1;
        public static Categoria GetNewFaker(Usuario usuario)
        {
            var categoriaFaker = new Faker<Categoria>()
                .RuleFor(c => c.Id, counter++)
                .RuleFor(c => c.Descricao, f => f.Commerce.Categories(10)[1])
                .RuleFor(c => c.UsuarioId, usuario.Id)
                .RuleFor(c => c.Usuario, usuario)
                .RuleFor(c => c.TipoCategoria, counter % 2 == 0 ? TipoCategoria.Receita : TipoCategoria.Despesa);

            return categoriaFaker.Generate();
        }

        public static CategoriaVM GetNewFakerVM(int Idusuario)
        {
            var categoriaFaker = new Faker<CategoriaVM>()
                .RuleFor(c => c.Id, counter++)
                .RuleFor(c => c.Descricao, f => f.Commerce.ProductName())
                .RuleFor(c => c.IdUsuario, new Random().Next(1, 10))
                .RuleFor(c => c.IdTipoCategoria, counter % 2 == 0 ? (int)TipoCategoria.Receita : (int)TipoCategoria.Despesa);

            return categoriaFaker.Generate();
        }

        public static List<CategoriaVM> CategoriasVMs()
        {
            var listCategoriaVM = new List<CategoriaVM>();
            var usuario = UsuarioFaker.GetNewFaker();            
            for (int i = 0; i < 10; i++)
            {
                var categoriaVM = GetNewFakerVM(usuario.Id);
                listCategoriaVM.Add(categoriaVM);
                usuario = UsuarioFaker.GetNewFaker();
            }
            return listCategoriaVM;
        }
        public static List<Categoria> Categorias()
        {
            var listCategoria = new List<Categoria>();
            var usuario = UsuarioFaker.GetNewFaker();
            for (int i = 0; i < 10; i++)
            {
                var categoria = GetNewFaker(usuario);
                listCategoria.Add(categoria);
                usuario = UsuarioFaker.GetNewFaker();
            }
            return listCategoria;
        }
    }
}