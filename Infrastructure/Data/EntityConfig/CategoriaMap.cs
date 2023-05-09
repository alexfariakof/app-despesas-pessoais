using despesas_backend_api_net_core.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace despesas_backend_api_net_core.Infrastructure.Data.EntityConfig
{
    public class CategoriaMap : IEntityTypeConfiguration<Categoria>
    {
        public void Configure(EntityTypeBuilder<Categoria> builder)
        {
            builder.HasKey(m => m.Id);

            builder.Property(m => m.Descricao)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(m => m.IdUsuario)
                .IsRequired();


            builder.Property(m => m.IdTipoCategoria)
            .IsRequired();


        }
    }
}
