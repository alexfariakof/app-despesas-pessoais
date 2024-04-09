namespace despesas_backend_api_net_core.XUnit.Fakers
{
    public class ReceitaFaker
    {
        static int counter = 1;
        static int counterVM = 1;

        public static Receita GetNewFaker(Usuario usuario, Categoria categoria)
        {
            var receitaFaker = new Faker<Receita>()
                .RuleFor(r => r.Id, f => counter++)
                .RuleFor(r => r.Data, new DateTime(DateTime.Now.Year, new Random().Next(1, 13), 1))
                .RuleFor(r => r.Descricao, f => f.Commerce.ProductName())
                .RuleFor(r => r.Valor, f => f.Random.Decimal(1, 900000))
                .RuleFor(r => r.UsuarioId, usuario.Id)
                .RuleFor(r => r.Usuario, usuario)                
                .RuleFor(r => r.Categoria, CategoriaFaker.GetNewFaker(usuario, TipoCategoria.Receita, usuario.Id));

            return receitaFaker.Generate();
        }

        public static ReceitaVM GetNewFakerVM(int idUsuario, int idCategoria)
        {
            var receitaFaker = new Faker<ReceitaVM>()
                .RuleFor(r => r.Id, f => counterVM++)
                .RuleFor(r => r.Data, new DateTime(DateTime.Now.Year, new Random().Next(1, 13), 1))
                .RuleFor(r => r.Descricao, f => f.Commerce.ProductName())
                .RuleFor(r => r.Valor, f => f.Random.Decimal(1, 900000))
                .RuleFor(r => r.IdUsuario, idUsuario)
                .RuleFor(r => r.Categoria, CategoriaFaker.GetNewFakerVM(UsuarioFaker.GetNewFakerVM(idUsuario),  TipoCategoria.Receita, idUsuario)
            );

            return receitaFaker.Generate();
        }

        public static List<ReceitaVM> ReceitasVMs(
            UsuarioVM? usuarioVM = null,
            int? idUsaurio = null
        )
        {
            var listReceitaVM = new List<ReceitaVM>();
            for (int i = 0; i < 10; i++)
            {
                if (idUsaurio == null)
                    usuarioVM = UsuarioFaker.GetNewFakerVM(new Random().Next(1, 10));

                var categoriaVM = CategoriaFaker.GetNewFakerVM(usuarioVM);

                var receitaVM = GetNewFakerVM(usuarioVM.Id, categoriaVM.Id);
                listReceitaVM.Add(receitaVM);
            }
            return listReceitaVM;
        }

        public static List<Receita> Receitas(Usuario? usuario = null, int? idUsuario = null)
        {
            var listReceita = new List<Receita>();
            for (int i = 0; i < 10; i++)
            {
                if (idUsuario == null)
                    usuario = UsuarioFaker.GetNewFaker(new Random().Next(1, 10));

                var categoria = CategoriaFaker.GetNewFaker(usuario, TipoCategoria.Receita, usuario.Id);

                var receita = GetNewFaker(usuario, categoria);
                listReceita.Add(receita);
            }
            return listReceita;
        }
    }
}
