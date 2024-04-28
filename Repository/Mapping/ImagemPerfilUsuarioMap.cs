using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Repository.Mapping;
public class ImagemPerfilUsuarioMap:  IEntityTypeConfiguration<ImagemPerfilUsuario>
{
    public void Configure(EntityTypeBuilder<ImagemPerfilUsuario> builder)
    {
        builder.ToTable(nameof(ImagemPerfilUsuario));
        builder.HasKey(m => m.Id);
        builder.Property(x => x.Id).ValueGeneratedOnAdd().IsRequired();
        builder.Property(m => m.UsuarioId).IsRequired();
        builder.HasIndex(m => m.Name).IsUnique(true);
        builder.Property(m => m.Name).IsRequired().HasMaxLength(50);
        builder.HasIndex(m => m.Url).IsUnique(true);
        builder.Property(m => m.Url).IsRequired();        
        builder.Property(m => m.ContentType).IsRequired().HasMaxLength(20);
        builder.HasIndex(m => m.UsuarioId).IsUnique(true);
        builder.Property(m => m.UsuarioId).IsRequired();
    }
}