using despesas_backend_api_net_core.Domain.Entities;
using despesas_backend_api_net_core.Domain.VM;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace despesas_backend_api_net_core.Infrastructure.Data.EntityConfig
{
    public class ReceitaMap : IParser<ReceitaVM, Receita>, IParser<Receita, ReceitaVM>, IEntityTypeConfiguration<Receita>
    {
        public void Configure(EntityTypeBuilder<Receita> builder)
        {
            builder.HasKey(m => m.Id);

            builder.Property(m => m.UsuarioId)
           .IsRequired();

            builder.Property(m => m.Descricao)
            .IsRequired(false)
            .HasMaxLength(100);

            builder.Property(m => m.CategoriaId)
            .IsRequired();

            builder.Property(m => m.Data)
            .HasColumnType("timestamp")
            .HasDefaultValueSql<DateTime>("NOW()")
            .IsRequired();
            
            builder.Property(m => m.Valor)
                .HasColumnType("decimal(10, 2)")
                .HasDefaultValue(0);

        }
        public Receita Parse(ReceitaVM origin)
        {
            if (origin == null) return new Receita();
            return new Receita
            {
                Id  = origin.Id,
                Data = origin.Data,
                Descricao = origin.Descricao,                
                Valor = origin.Valor,
                CategoriaId = origin.IdCategoria,
                UsuarioId = origin.IdUsuario
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
                IdCategoria = origin.CategoriaId,
                Categoria = new CategoriaMap().Parse(origin.Categoria),
                IdUsuario = origin.UsuarioId,
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