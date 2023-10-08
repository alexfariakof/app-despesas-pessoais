using Bogus;

namespace despesas_backend_api_net_core.XUnit.Fakers
{
    public static class DespesaFaker
    {
        public static Despesa GetNewFaker(Usuario usuario, Categoria categoria)
        {
            var despesaFaker = new Faker<Despesa>()
                .RuleFor(r => r.Id, f => f.Random.Number(1, 100))
                .RuleFor(r => r.Data, DateTime.Now.AddDays(new Random().Next(99)))
                .RuleFor(r => r.DataVencimento, DateTime.Now.AddDays(new Random().Next(99)))
                .RuleFor(r => r.Descricao, f => f.Commerce.ProductName())
                .RuleFor(r => r.Valor, f => f.Random.Decimal(1, 900000))
                .RuleFor(r => r.UsuarioId, usuario.Id)
                .RuleFor(r => r.Usuario, usuario)
                .RuleFor(r => r.CategoriaId, categoria.Id)
                .RuleFor(r => r.Categoria, categoria);

            return despesaFaker.Generate();
        }

        public static DespesaVM GetNewFakerVM(int idUsuario, int idCategoria)
        {
            var despesaFaker = new Faker<DespesaVM>()
                .RuleFor(r => r.Id, f => f.Random.Number(1, 100))
                .RuleFor(r => r.Data, DateTime.Now.AddDays(new Random().Next(99)))
                .RuleFor(r => r.DataVencimento, DateTime.Now.AddDays(new Random().Next(99)))
                .RuleFor(r => r.Descricao, f => f.Commerce.ProductName())
                .RuleFor(r => r.Valor, f => f.Random.Decimal(1, 900000))
                .RuleFor(r => r.IdUsuario, idUsuario)
                .RuleFor(r => r.IdCategoria, idCategoria);


            return despesaFaker.Generate();
        }

        public static List<DespesaVM> DespesasVM()
        {
            var listDespesaVM = new List<DespesaVM>();
            var usuario = UsuarioFaker.GetNewFaker();
            
            for (int i = 0; i < 10; i++)
            {
                var categoriaVM = CategoriaFaker.GetNewFakerVM(usuario.Id);
                var despesaVM = GetNewFakerVM(usuario.Id, categoriaVM.Id);
                listDespesaVM.Add(despesaVM);
                usuario = UsuarioFaker.GetNewFaker();                
            }
            return listDespesaVM;
        }
        public static List<Despesa> Despesas()
        {
            var listDespesa = new List<Despesa>();
            var usuario = UsuarioFaker.GetNewFaker();
            
            for (int i = 0; i < 10; i++)
            {
                var categoria = CategoriaFaker.GetNewFaker(usuario);
                var despesa = GetNewFaker(usuario, categoria);
                listDespesa.Add(despesa);
                usuario = UsuarioFaker.GetNewFaker();
            }
            return listDespesa;
        }
    }
}