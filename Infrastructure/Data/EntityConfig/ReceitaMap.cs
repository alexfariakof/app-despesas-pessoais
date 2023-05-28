using despesas_backend_api_net_core.Domain.Entities;
using despesas_backend_api_net_core.Domain.VM;

namespace despesas_backend_api_net_core.Infrastructure.Data.EntityConfig
{
    public class ReceitaMap : IParser<ReceitaVM, Receita>, IParser<Receita, ReceitaVM>
    {

        public Receita Parse(ReceitaVM origin)
        {
            if (origin == null) return new Receita();
            return new Receita
            {
                Id  = origin.Id,
                Data = origin.Data,
                Descricao = origin.Descricao,                
                Valor = origin.Valor,
                IdCategoria =origin.IdCategoria,
                Categoria = new CategoriaMap().Parse(origin.Categoria),
                IdUsuario = origin.IdUsuario,
                Usuario = new UsuarioMap().Parse(origin.Usuario)
            };
        }

        public ReceitaVM Parse(Receita origin)
        {
            if (origin == null) return new ReceitaVM();
            return new ReceitaVM
            {
                Id = origin.Id,
                Data = origin.Data,
                Descricao = origin.Descricao,
                Valor = origin.Valor,
                IdCategoria = origin.IdCategoria,
                Categoria = new CategoriaMap().Parse(origin.Categoria),
                IdUsuario = origin.IdUsuario,
                Usuario = new UsuarioMap().Parse(origin.Usuario)

            };
        }

        public List<Receita> ParseList(List<ReceitaVM> origin)
        {
            if (origin == null) return new List<Receita>();
            return origin.Select(item => Parse(item)).ToList();
        }

        public List<ReceitaVM> ParseList(List<Receita> origin)
        {
            if (origin == null) return new List<ReceitaVM>();
            return origin.Select(item => Parse(item)).ToList();
        }
    }
}
