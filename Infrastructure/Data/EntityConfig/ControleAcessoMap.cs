using despesas_backend_api_net_core.Domain.Entities;
using despesas_backend_api_net_core.Domain.VM;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Reflection.Metadata;

namespace despesas_backend_api_net_core.Infrastructure.Data.EntityConfig
{
    public class ControleAcessoMap : IEntityTypeConfiguration<ControleAcesso>
    {
        public void Configure(EntityTypeBuilder<ControleAcesso> builder)
        {
            builder.HasKey(m => m.Id);
            builder.HasIndex(m => m.Login)
                .HasDatabaseName("UX_IDTypes_Code")
                .IsUnique(true);
            builder.Property(m => m.Login)
            .IsRequired()
            .HasMaxLength(100) ;
        }
  

    }
}
