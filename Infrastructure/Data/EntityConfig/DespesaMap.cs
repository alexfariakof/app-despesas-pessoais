using despesas_backend_api_net_core.Domain.Entities;
using despesas_backend_api_net_core.Domain.VM;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace despesas_backend_api_net_core.Infrastructure.Data.EntityConfig
{
    public class DespesaMap : IParser<DespesaVM, Despesa>, IParser<Despesa, DespesaVM>, IEntityTypeConfiguration<Despesa>
    {
        public void Configure(EntityTypeBuilder<Despesa> builder)
        {
            builder.HasKey(m => m.Id);

            builder.Property(m => m.Descricao)
            .IsRequired(false)
            .HasMaxLength(100);

            builder.Property(m => m.Data)
            .HasColumnType("timestamp")
            .HasDefaultValue(DateTime.Now);

            builder.Property(m => m.DataVencimento)
            .HasColumnType("timestamp")
            .HasDefaultValue(null);

            builder.Property(m => m.Valor)
                .HasColumnType("decimal(10, 2)");
                
                
        }
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
                CategoriaId =origin.IdCategoria,
                Categoria = new CategoriaMap().Parse(origin.Categoria),
                UsuarioId = origin.IdUsuario,
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
                IdCategoria = origin.CategoriaId,
                Categoria = new CategoriaMap().Parse(origin.Categoria),
                IdUsuario = origin.UsuarioId,
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
