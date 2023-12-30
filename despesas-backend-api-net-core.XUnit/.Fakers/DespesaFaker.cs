namespace despesas_backend_api_net_core.XUnit.Fakers
{
    public class DespesaFaker
    {
        static int counter = 1;
        static int counterVM = 1;

        public static Despesa GetNewFaker(Usuario usuario, Categoria categoria)
        {
            var despesaFaker = new Faker<Despesa>()
                .RuleFor(r => r.Id, f => counter++)
                .RuleFor(r => r.Data, new DateTime(DateTime.Now.Year, new Random().Next(1, 13), 1))
                .RuleFor(
                    r => r.DataVencimento,
                    new DateTime(DateTime.Now.Year, new Random().Next(1, 13), 1)
                )
                .RuleFor(r => r.Descricao, f => f.Commerce.ProductName())
                .RuleFor(r => r.Valor, f => f.Random.Decimal(1, 900000))
                .RuleFor(r => r.UsuarioId, usuario.Id)
                .RuleFor(r => r.Usuario, usuario)
                .RuleFor(r => r.CategoriaId, categoria.Id)
                .RuleFor(r => r.Categoria, CategoriaFaker.GetNewFaker(usuario, TipoCategoria.Despesa, usuario.Id));

            return despesaFaker.Generate();
        }

        public static DespesaVM GetNewFakerVM(int idUsuario, int idCategoria)
        {
            var despesaFaker = new Faker<DespesaVM>()
                .RuleFor(r => r.Id, f => counterVM++)
                .RuleFor(r => r.Data, new DateTime(DateTime.Now.Year, new Random().Next(1, 13), 1))
                .RuleFor(
                    r => r.DataVencimento,
                    new DateTime(DateTime.Now.Year, new Random().Next(1, 13), 1)
                )
                .RuleFor(r => r.Descricao, f => f.Commerce.ProductName())
                .RuleFor(r => r.Valor, f => f.Random.Decimal(1, 900000))
                .RuleFor(r => r.IdUsuario, idUsuario)
                .RuleFor(r => r.Categoria, CategoriaFaker.GetNewFakerVM(UsuarioFaker.GetNewFakerVM(idUsuario), TipoCategoria.Despesa, idUsuario));

            return despesaFaker.Generate();
        }

        public static List<DespesaVM> DespesasVMs(UsuarioVM? usuarioVM = null, int? idUsaurio = null)
        {
            var listDespesaVM = new List<DespesaVM>();
            for (int i = 0; i < 10; i++)
            {
                if (idUsaurio == null)
                    usuarioVM = UsuarioFaker.GetNewFakerVM(new Random().Next(1, 10));

                var categoriaVM = CategoriaFaker.GetNewFakerVM(usuarioVM, TipoCategoria.Despesa);

                var despesaVM = GetNewFakerVM(usuarioVM.Id, categoriaVM.Id);
                listDespesaVM.Add(despesaVM);
            }

            return listDespesaVM;
        }

        public static List<Despesa> Despesas(Usuario? usuario = null, int? idUsurio = null)
        {
            var listDespesa = new List<Despesa>();
            for (int i = 0; i < 10; i++)
            {
                if (idUsurio == null)
                    usuario = UsuarioFaker.GetNewFaker(new Random().Next(1, 10));

                var categoria = CategoriaFaker.GetNewFaker(usuario, TipoCategoria.Despesa, usuario.Id);

                var despesa = GetNewFaker(usuario, categoria);
                listDespesa.Add(despesa);
            }
            return listDespesa;
        }
    }
}
