using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Repository.Mapping;
public class ReceitaMap: IEntityTypeConfiguration<Receita>
{
    public void Configure(EntityTypeBuilder<Receita> builder)
    {
        builder.ToTable(nameof(Receita));
        builder.HasKey(m => m.Id);
        builder.Property(x => x.Id).ValueGeneratedOnAdd().IsRequired();
        builder.Property(m => m.UsuarioId).IsRequired();
        builder.Property(m => m.Descricao).IsRequired(false).HasMaxLength(100);
        builder.Property(m => m.CategoriaId).IsRequired();
        builder.Property(m => m.Data).HasColumnType("timestamp").HasDefaultValueSql<DateTime>("NOW()").IsRequired();        
        builder.Property(m => m.Valor).HasColumnType("decimal(10, 2)").HasDefaultValue(0);
    }
}