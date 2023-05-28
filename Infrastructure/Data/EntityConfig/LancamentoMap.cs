using despesas_backend_api_net_core.Domain.VM;
using despesas_backend_api_net_core.Domain.Entities;

namespace despesas_backend_api_net_core.Infrastructure.Data.EntityConfig
{
    public class LancamentoMap : IParser<LancamentoVM, Lancamento>, IParser<Lancamento, LancamentoVM>
    {
        public Lancamento Parse(LancamentoVM origin)
        {
            if (origin == null) return new Lancamento();
            return new Lancamento
            {
                Id = origin.Id,
                IdDespesa = origin.IdDespesa,
                IdReceita = origin.IdReceita,
                IdUsuario = origin.IdUsuario,
                Data = origin.Data.ToDateTime(),
                Valor = origin.Valor.ToDecimal(),
                Despesa = new Despesa { Id = origin.IdDespesa, Descricao = origin.Descricao },                
                Receita = new Receita { Id = origin.IdReceita, Descricao = origin.Descricao },
                Categoria = new Categoria { Descricao = origin.Categoria }
            };
        }

        public LancamentoVM Parse(Lancamento origin)
        {
            if (origin == null) return new LancamentoVM();
            return new LancamentoVM
            {
                Id = origin.Id,
                IdDespesa = origin.IdDespesa,
                IdReceita = origin.IdReceita,
                IdUsuario = origin.IdUsuario,
                Data = origin.Data.ToDateBr(),
                Valor = origin.Valor.ToString("N2"),
                Descricao = origin.IdDespesa == 0 ? origin.Receita.Descricao : origin.Despesa.Descricao ,
                Categoria = origin.Categoria.Descricao
            };
        }

        public List<Lancamento> ParseList(List<LancamentoVM> origin)
        {
            if (origin == null) return new List<Lancamento>();
            return origin.Select(item => Parse(item)).ToList();
        }

        public List<LancamentoVM> ParseList(List<Lancamento> origin)
        {
            if (origin == null) return new List<LancamentoVM>();
            return origin.Select(item => Parse(item)).ToList();
        }
    }
}
