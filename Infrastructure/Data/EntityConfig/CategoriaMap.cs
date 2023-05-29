using despesas_backend_api_net_core.Domain.Entities;
using despesas_backend_api_net_core.Domain.VM;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace despesas_backend_api_net_core.Infrastructure.Data.EntityConfig
{
    public class CategoriaMap : IParser<CategoriaVM, Categoria>, IParser<Categoria, CategoriaVM>, IEntityTypeConfiguration<Categoria>
    {

        public void Configure(EntityTypeBuilder<Categoria> builder)
        {
            builder.HasKey(m => m.Id);
        }

        public Categoria Parse(CategoriaVM origin)
        {
            if (origin == null) return new Categoria();
            return new Categoria
            {
                Id = origin.Id,
                Descricao = origin.Descricao,
                IdTipoCategoria = origin.IdTipoCategoria,
                TipoCategoria = origin.TipoCategoria,
                IdUsuario = origin.IdUsuario,
                Usuario = new UsuarioMap().Parse(origin.Usuario)
            };
        }

        public CategoriaVM Parse(Categoria origin)
        {
            if (origin == null) return new CategoriaVM();
            return new CategoriaVM
            {
                Id = origin.Id,
                Descricao = origin.Descricao,
                IdTipoCategoria = origin.IdTipoCategoria,
                TipoCategoria = origin.TipoCategoria,
                IdUsuario = origin.IdUsuario,
                Usuario = new UsuarioMap().Parse(origin.Usuario)
                
            };
        }

        public List<Categoria> ParseList(List<CategoriaVM> origin)
        {
            if (origin == null) return new List<Categoria>();
            return origin.Select(item => Parse(item)).ToList();
        }

        public List<CategoriaVM> ParseList(List<Categoria> origin)
        {
            if (origin == null) return new List<CategoriaVM>();
            return origin.Select(item => Parse(item)).ToList();
        }
    }
}
