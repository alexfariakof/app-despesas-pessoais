using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Repository.Mapping;
public class CategoriaMap: IEntityTypeConfiguration<Categoria>
{
    public void Configure(EntityTypeBuilder<Categoria> builder)
    {
        builder.ToTable(nameof(Categoria));
        builder.HasKey(c => c.Id);
        builder.Property(c => c.Id).ValueGeneratedOnAdd().IsRequired();
        builder.Property(c => c.Descricao).IsRequired(false).HasMaxLength(100);
        builder.Property(m => m.UsuarioId).IsRequired();
        builder.HasOne(c => c.TipoCategoria).WithMany();
    }
}