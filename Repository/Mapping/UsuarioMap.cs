using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Repository.Mapping;
public class UsuarioMap: IEntityTypeConfiguration<Usuario>
{
    public void Configure(EntityTypeBuilder<Usuario> builder)
    {
        builder.ToTable(nameof(Usuario));
        builder.HasKey(m => m.Id);
        builder.Property(x => x.Id).ValueGeneratedOnAdd();
        builder.HasIndex(m => m.Email).IsUnique(true);
        builder.Property(m => m.Email).IsRequired().HasMaxLength(50);
        builder.Property(m => m.Nome).HasMaxLength(50).IsRequired();;
        builder.Property(m => m.SobreNome).HasMaxLength(50).IsRequired();
        builder.Property(m => m.Telefone).HasMaxLength(15).IsRequired(false);        
        builder.Property(m => m.PerfilUsuario).IsRequired().HasDefaultValue(PerfilUsuario.Usuario);
    }
}