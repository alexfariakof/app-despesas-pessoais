using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Repository.Mapping;
public class ReceitaMap: IEntityTypeConfiguration<Receita>
{
    public void Configure(EntityTypeBuilder<Receita> builder)
    {
        builder.ToTable(nameof(Receita));
        builder.HasKey(r => r.Id);
        builder.Property(r => r.Id).ValueGeneratedOnAdd().IsRequired();
        builder.Property(r => r.UsuarioId).IsRequired();
        builder.Property(r => r.Descricao).IsRequired(false).HasMaxLength(100);
        builder.Property(r => r.CategoriaId).IsRequired();

        // MySqlServer
        //builder.Property(m => m.Data).HasColumnType("timestamp").HasDefaultValueSql<DateTime>("NOW()").IsRequired();

        // MsSqlServer
        builder.Property(r => r.Data).HasColumnType("datetime").HasDefaultValueSql<DateTime>("GetDate()").IsRequired();        

        builder.Property(r => r.Valor).HasColumnType("decimal(10, 2)").HasDefaultValue(0);
        builder.HasOne(r => r.Usuario).WithMany().HasForeignKey(r => r.UsuarioId).OnDelete(DeleteBehavior.NoAction);
    }
}