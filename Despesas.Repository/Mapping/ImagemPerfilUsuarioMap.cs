using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Repository.Mapping;
public class ImagemPerfilUsuarioMap:  IEntityTypeConfiguration<ImagemPerfilUsuario>
{
    public void Configure(EntityTypeBuilder<ImagemPerfilUsuario> builder)
    {
        builder.ToTable(nameof(ImagemPerfilUsuario));
        builder.HasKey(i => i.Id);
        builder.Property(i => i.Id).ValueGeneratedOnAdd().IsRequired();
        builder.Property(i => i.UsuarioId).IsRequired();
        builder.HasIndex(i => i.Name).IsUnique(true);
        builder.Property(i => i.Name).IsRequired().HasMaxLength(50);
        builder.HasIndex(i => i.Url).IsUnique(true);
        builder.Property(i => i.Url).IsRequired();        
        builder.Property(i => i.ContentType).IsRequired().HasMaxLength(20);
        builder.HasIndex(i => i.UsuarioId).IsUnique(true);
        builder.Property(i => i.UsuarioId).IsRequired();

    }
}