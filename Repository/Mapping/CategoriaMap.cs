using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Repository.Mapping;
public class CategoriaMap: IEntityTypeConfiguration<Categoria>
{
    public void Configure(EntityTypeBuilder<Categoria> builder)
    {
        builder.ToTable(nameof(Categoria));
        builder.HasKey(m => m.Id);
        builder.Property(m => m.Id).ValueGeneratedOnAdd().IsRequired();
        builder.Property(m => m.Descricao).IsRequired(false).HasMaxLength(100);
        builder.Property(m => m.UsuarioId).IsRequired();
        builder.Property(m => m.TipoCategoria).IsRequired();
    }    
}