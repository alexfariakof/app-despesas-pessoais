using despesas_backend_api_net_core.Domain.VO;
using despesas_backend_api_net_core.Domain.Entities;

namespace despesas_backend_api_net_core.Infrastructure.Data.EntityConfig
{
    public class LancamentoMap : IParser<LancamentoVO, Lancamento>, IParser<Lancamento, LancamentoVO>
    {
        public Lancamento Parse(LancamentoVO origin)
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

        public LancamentoVO Parse(Lancamento origin)
        {
            if (origin == null) return new LancamentoVO();
            return new LancamentoVO
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

        public List<Lancamento> ParseList(List<LancamentoVO> origin)
        {
            if (origin == null) return new List<Lancamento>();
            return origin.Select(item => Parse(item)).ToList();
        }

        public List<LancamentoVO> ParseList(List<Lancamento> origin)
        {
            if (origin == null) return new List<LancamentoVO>();
            return origin.Select(item => Parse(item)).ToList();
        }
    }
}
