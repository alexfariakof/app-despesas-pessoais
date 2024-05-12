using Domain.Entities.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Repository.Mapping;
public class TipoCategoriaMap: IEntityTypeConfiguration<TipoCategoria>
{
    public void Configure(EntityTypeBuilder<TipoCategoria> builder)
    {
        builder.ToTable("TipoCategoria");
        builder.HasKey(tc => tc.Id);
        builder.Property(tc => tc.Id).IsRequired().HasConversion<int>();
        builder.Property(tc => tc.Name).IsRequired();

        builder.HasData
        (
            new TipoCategoria((int)TipoCategoria.TipoCategoriaType.Despesa),
            new TipoCategoria((int)TipoCategoria.TipoCategoriaType.Receita)
        );
    }
}
