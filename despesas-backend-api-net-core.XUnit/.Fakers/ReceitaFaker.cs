using Bogus;

namespace despesas_backend_api_net_core.XUnit.Fakers
{
    public static class ReceitaFaker
    {
        public static Receita GetNewFaker(Usuario usuario, Categoria categoria)
        {
            var receitaFaker = new Faker<Receita>()
                .RuleFor(r => r.Id, f => f.Random.Number(1, 100))
                .RuleFor(r => r.Data, DateTime.Now.AddDays(new Random().Next(99)))
                .RuleFor(r => r.Descricao, f => f.Commerce.ProductName())
                .RuleFor(r => r.Valor, f => f.Random.Decimal(1, 900000))
                .RuleFor(r => r.UsuarioId, usuario.Id)
                .RuleFor(r => r.Usuario, usuario)
                .RuleFor(r => r.CategoriaId, categoria.Id)
                .RuleFor(r => r.Categoria, categoria);

            return receitaFaker.Generate();
        }

        public static ReceitaVM GetNewFakerVM(int idUsuario, int idCategoria)
        {
            var receitaFaker = new Faker<ReceitaVM>()
                .RuleFor(r => r.Id, f => f.Random.Number(1, 100))
                .RuleFor(r => r.Data, DateTime.Now.AddDays(new Random().Next(99)))
                .RuleFor(r => r.Descricao, f => f.Commerce.ProductName())
                .RuleFor(r => r.Valor, f => f.Random.Decimal(1, 900000))
                .RuleFor(r => r.IdUsuario, idUsuario)
                .RuleFor(r => r.IdCategoria, idCategoria);


            return receitaFaker.Generate();
        }

        public static List<ReceitaVM> ReceitasVMs()
        {
            var listReceitaVM = new List<ReceitaVM>();
            var usuario = UsuarioFaker.GetNewFaker();
            
            for (int i = 0; i < 10; i++)
            {
                var categoriaVM = CategoriaFaker.GetNewFakerVM(usuario.Id);
                var receitaVM = GetNewFakerVM(usuario.Id, categoriaVM.Id);
                listReceitaVM.Add(receitaVM);
                usuario = UsuarioFaker.GetNewFaker();                
            }
            return listReceitaVM;
        }
        public static List<Receita> Receitas()
        {
            var listReceita = new List<Receita>();
            var usuario = UsuarioFaker.GetNewFaker();
            
            for (int i = 0; i < 10; i++)
            {
                var categoria = CategoriaFaker.GetNewFaker(usuario);
                var receita = GetNewFaker(usuario, categoria);
                listReceita.Add(receita);
                usuario = UsuarioFaker.GetNewFaker();
            }
            return listReceita;
        }
    }
}