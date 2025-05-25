using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Repository.Mapping;
public class CategoriaMap : IEntityTypeConfiguration<Categoria>
{
    public void Configure(EntityTypeBuilder<Categoria> builder)
    {
        builder.ToTable(nameof(Categoria));
        builder.Property(c => c.Id).HasColumnType("binary(16)")
            .HasConversion(v => v.ToByteArray(), v => new Guid(v))
            .ValueGeneratedOnAdd().IsRequired();
        builder.HasKey(c => c.Id);
        builder.Property(c => c.Descricao).IsRequired(false).HasMaxLength(100);
        builder.Property(c => c.UsuarioId).HasColumnType("binary(16)")
            .HasConversion(v => v.ToByteArray(), v => new Guid(v)).IsRequired();
        builder.HasOne(c => c.TipoCategoria).WithMany();
    }
}