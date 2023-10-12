using Bogus;
using despesas_backend_api_net_core.Domain.Entities;
using despesas_backend_api_net_core.Infrastructure.ExtensionMethods;

namespace despesas_backend_api_net_core.XUnit.Fakers
{
    public static class LancamentoFaker
    {
        static int counter = 1;
        public static Lancamento GetNewFaker(Usuario usuario, Despesa despesa, Receita receita, Categoria categoria)
        {
            
            var LancamentoFaker = new Faker<Lancamento>()
                .RuleFor(l => l.Id, f => counter++)
                .RuleFor(l => l.Valor, f => f.Random.Decimal(1, 90000))
                .RuleFor(l => l.Data, DateTime.Now.AddDays(new Random().Next(99)))
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

        public static LancamentoVM GetNewFakerVM(int idusuario, int idDespesa, int idReceita, Categoria categoria)
        {
            var lancamentoVMFaker = new Faker<LancamentoVM>()
                .RuleFor(l => l.Id, f => counter++)
                .RuleFor(l => l.Valor, f => f.Random.Decimal(1, 90000).ToString("N2"))
                .RuleFor(l => l.Data, DateTime.Now.AddDays(new Random().Next(99)).ToDateBr())
                .RuleFor(l => l.Descricao, f => f.Commerce.ProductName())
                .RuleFor(l => l.IdUsuario, idusuario)
                .RuleFor(l => l.IdDespesa, idDespesa)                    
                .RuleFor(l => l.IdReceita, idReceita)
                .RuleFor(c => c.TipoCategoria, f => f.PickRandom<TipoCategoria>().ToString())
                .RuleFor(l => l.Categoria, categoria);

            return lancamentoVMFaker.Generate();
        }

        public static List<LancamentoVM> LancamentoVMs()
        {
            counter = 1;
            var listLancamentoVM = new List<LancamentoVM>();
            var usuario = UsuarioFaker.GetNewFaker();
            var categoria = CategoriaFaker.GetNewFaker(usuario);
            var despesa = DespesaFaker.GetNewFaker(usuario, categoria);
            var receita = ReceitaFaker.GetNewFaker(usuario, categoria);

            for (int i = 0; i < 10; i++)
            {
                var lancamentoVM = GetNewFakerVM(usuario.Id, despesa.Id, receita.Id, categoria);
                listLancamentoVM.Add(lancamentoVM);
                usuario = UsuarioFaker.GetNewFaker();
                categoria = CategoriaFaker.GetNewFaker(usuario);
                despesa = DespesaFaker.GetNewFaker(usuario, categoria);
                receita = ReceitaFaker.GetNewFaker(usuario, categoria);
            }

            return listLancamentoVM;
        }
        public static List<Lancamento> Lancamentos()
        {
            counter = 1;
            var listLancamento = new List<Lancamento>();
            var usuario = UsuarioFaker.GetNewFaker();
            var categoria = CategoriaFaker.GetNewFaker(usuario);
            var despesa = DespesaFaker.GetNewFaker(usuario, categoria);
            var receita = ReceitaFaker.GetNewFaker(usuario, categoria);

            for (int i = 0; i < 10; i++)
            {
                var lancamento = GetNewFaker(usuario, despesa, receita, categoria);
                listLancamento.Add(lancamento);
                usuario = UsuarioFaker.GetNewFaker();
                categoria = CategoriaFaker.GetNewFaker(usuario);
                despesa = DespesaFaker.GetNewFaker(usuario, categoria);
                receita = ReceitaFaker.GetNewFaker(usuario, categoria);
            }
            return listLancamento;
        }
    }
}