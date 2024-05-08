using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Repository.Mapping;
public class DespesaMap: IEntityTypeConfiguration<Despesa>
{
    public void Configure(EntityTypeBuilder<Despesa> builder)
    {
        builder.ToTable(nameof(Despesa));
        builder.HasKey(d => d.Id);
        builder.Property(d => d.Id).ValueGeneratedOnAdd().IsRequired();
        builder.Property(d => d.Descricao).IsRequired(false).HasMaxLength(100);        
        builder.Property(d => d.UsuarioId).IsRequired();
        builder.Property(d => d.CategoriaId).IsRequired();

        // MySqlServer 
        //builder.Property(m => m.Data).HasColumnType("timestamp").HasDefaultValueSql<DateTime>("NOW()").IsRequired();
        //builder.Property(m => m.DataVencimento).HasColumnType("timestamp").HasDefaultValueSql(null);

        // MsSqlServer
        builder.Property(d => d.Data).HasColumnType("datetime").HasDefaultValueSql<DateTime>("GetDate()").IsRequired();
        builder.Property(d => d.DataVencimento).HasColumnType("datetime").HasDefaultValueSql(null);

        builder.Property(d => d.Valor).HasColumnType("decimal(10, 2)").HasDefaultValue(0);
        builder.HasOne(d => d.Usuario).WithMany().HasForeignKey(d => d.UsuarioId).OnDelete(DeleteBehavior.NoAction);
    }
}