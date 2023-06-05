using despesas_backend_api_net_core.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace despesas_backend_api_net_core.Infrastructure.Data.EntityConfig
{
    public class ControleAcessoMap : IEntityTypeConfiguration<ControleAcesso>
    {
        public void Configure(EntityTypeBuilder<ControleAcesso> builder)
        {
            builder.HasKey(m => m.Id);
                
            builder.HasIndex(m => m.Login)                
                .IsUnique(true);

            builder.Property(x => x.UsuarioId).IsRequired();

            builder.Property(m => m.Login)
            .IsRequired()
            .HasMaxLength(100) ;
        }
    }
}