using Domain.Entities.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Repository.Mapping;

public class PerfilUsuarioMap: IEntityTypeConfiguration<PerfilUsuario>
{
    public void Configure(EntityTypeBuilder<PerfilUsuario> builder)
    {
        builder.ToTable("PerfilUsuario");
        builder.HasKey(pu => pu.Id);
        builder.Property(pu => pu.Id).IsRequired().HasConversion<int>();
        builder.Property(pu => pu.Name).IsRequired();

        builder.HasData
        (
            new PerfilUsuario(PerfilUsuario.Perfil.Admin),
            new PerfilUsuario(PerfilUsuario.Perfil.User)
        );
    }
}
