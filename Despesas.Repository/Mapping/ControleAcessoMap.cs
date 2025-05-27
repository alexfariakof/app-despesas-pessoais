using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Repository.Mapping;
public class ControleAcessoMap : IEntityTypeConfiguration<ControleAcesso>
{
    public void Configure(EntityTypeBuilder<ControleAcesso> builder)
    {
        builder.ToTable(nameof(ControleAcesso));
        builder.Property(ca => ca.Id).HasColumnType("binary(16)")
            .HasConversion(v => v.ToByteArray(), v => new Guid(v))
            .ValueGeneratedOnAdd().IsRequired();
        builder.HasKey(ca => ca.Id);
        builder.HasIndex(ca => ca.Login).IsUnique(true);
        builder.Property(ca => ca.UsuarioId).HasColumnType("binary(16)")
            .HasConversion(v => v.ToByteArray(), v => new Guid(v)).IsRequired();
        builder.Property(ca => ca.Login).IsRequired().HasMaxLength(100);
        //builder.Property(ca => ca.Senha).IsRequired().HasColumnType("TEXT").HasDefaultValueSql("''");
        builder.Property(ca => ca.RefreshToken).HasDefaultValue(null).IsRequired(false);
        builder.Property(ca => ca.RefreshTokenExpiry).HasDefaultValue(null).IsRequired(false);
    }
}
