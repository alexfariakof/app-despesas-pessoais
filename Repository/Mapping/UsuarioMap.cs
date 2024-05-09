using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Repository.Mapping;
public class UsuarioMap: IEntityTypeConfiguration<Usuario>
{
    public void Configure(EntityTypeBuilder<Usuario> builder)
    {
        builder.ToTable(nameof(Usuario));
        builder.HasKey(u => u.Id);
        builder.Property(u => u.Id).ValueGeneratedOnAdd();
        builder.HasIndex(u => u.Email).IsUnique(true);
        builder.Property(u => u.Email).IsRequired().HasMaxLength(50);
        builder.Property(u => u.Nome).HasMaxLength(50).IsRequired();
        builder.Property(u => u.SobreNome).HasMaxLength(50).IsRequired();
        builder.Property(u => u.Telefone).HasMaxLength(15).IsRequired(false);        
        builder.Property(u => u.PerfilUsuario).IsRequired().HasSentinel(PerfilUsuario.Usuario);        
    }
}