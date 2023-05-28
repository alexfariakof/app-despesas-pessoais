using despesas_backend_api_net_core.Domain.Entities;
using despesas_backend_api_net_core.Domain.VM;

namespace despesas_backend_api_net_core.Infrastructure.Data.EntityConfig
{
    public class DespesaMap : IParser<DespesaVM, Despesa>, IParser<Despesa, DespesaVM>
    {

        public Despesa Parse(DespesaVM origin)
        {
            if (origin == null) return new Despesa();
            return new Despesa
            {
                Id  = origin.Id,
                Data = origin.Data,
                Descricao = origin.Descricao,                
                Valor = origin.Valor,
                DataVencimento = origin.DataVencimento,
                IdCategoria =origin.IdCategoria,
                Categoria = new CategoriaMap().Parse(origin.Categoria),
                IdUsuario = origin.IdUsuario,
                Usuario = new UsuarioMap().Parse(origin.Usuario)
            };
        }

        public DespesaVM Parse(Despesa origin)
        {
            if (origin == null) return new DespesaVM();
            return new DespesaVM
            {
                Id = origin.Id,
                Data = origin.Data,
                Descricao = origin.Descricao,
                Valor = origin.Valor,
                DataVencimento = origin.DataVencimento,
                IdCategoria = origin.IdCategoria,
                Categoria = new CategoriaMap().Parse(origin.Categoria),
                IdUsuario = origin.IdUsuario,
                Usuario = new UsuarioMap().Parse(origin.Usuario)
            };
        }

        public List<Despesa> ParseList(List<DespesaVM> origin)
        {
            if (origin == null) return new List<Despesa>();
            return origin.Select(item => Parse(item)).ToList();
        }

        public List<DespesaVM> ParseList(List<Despesa> origin)
        {
            if (origin == null) return new List<DespesaVM>();
            return origin.Select(item => Parse(item)).ToList();
        }
    }
}
