using despesas_backend_api_net_core.Infrastructure.ExtensionMethods;
using System.Globalization;

namespace despesas_backend_api_net_core.XUnit.Fakers
{
    public class LancamentoFaker
    {
        static int counter = 1;
        static int counterVM = 1;
        public static Lancamento GetNewFaker(Usuario usuario, Despesa despesa, Receita receita, Categoria categoria)
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

        public static LancamentoVM GetNewFakerVM(int idusuario, int idDespesa, int idReceita, Categoria categoria)
        {
            var lancamentoVMFaker = new Faker<LancamentoVM>()
                .RuleFor(l => l.Id, f => counterVM++)
                .RuleFor(l => l.Valor, f => f.Random.Decimal(1, 90000).ToString("N2"))
                .RuleFor(l => l.Data, f => new DateTime(DateTime.Now.Year, new Random().Next(1, 13), 1).ToString())
                .RuleFor(l => l.Descricao, f => f.Commerce.ProductName())
                .RuleFor(l => l.IdUsuario, idusuario)
                .RuleFor(l => l.IdDespesa, idDespesa)                    
                .RuleFor(l => l.IdReceita, idReceita)
                .RuleFor(c => c.TipoCategoria, f => f.PickRandom<TipoCategoria>().ToString())
                .RuleFor(l => l.Categoria, categoria);

            return lancamentoVMFaker.Generate();
        }

        public static List<LancamentoVM> LancamentoVMs(Usuario? usuario = null, int? idUsuario = null)
        {
            var listLancamentoVM = new List<LancamentoVM>();            
            for (int i = 0; i < 10; i++)
            {
                if (idUsuario == null)
                    usuario = UsuarioFaker.GetNewFaker(new Random(1).Next(1, 10));
                                
                var categoria = CategoriaFaker.GetNewFaker(usuario);
                var despesa = DespesaFaker.GetNewFaker(usuario, categoria);
                var receita = ReceitaFaker.GetNewFaker(usuario, categoria);
                var lancamentoVM = GetNewFakerVM(usuario.Id, despesa.Id, receita.Id, categoria);
                listLancamentoVM.Add(lancamentoVM);
            }

            return listLancamentoVM;
        }
        public static List<Lancamento> Lancamentos(Usuario? usuario = null, int? idUsuario = null)
        {
            var listLancamento = new List<Lancamento>();            
            for (int i = 0; i < 10; i++)
            {
                if (idUsuario == null)
                    usuario = UsuarioFaker.GetNewFaker(new Random(1).Next(1, 10));

                var categoria = CategoriaFaker.GetNewFaker(usuario);
                var despesa = DespesaFaker.GetNewFaker(usuario, categoria);
                var receita = ReceitaFaker.GetNewFaker(usuario, categoria);
                var lancamento = GetNewFaker(usuario, despesa, receita, categoria);
                listLancamento.Add(lancamento);
            }
            return listLancamento;
        }
    }
}