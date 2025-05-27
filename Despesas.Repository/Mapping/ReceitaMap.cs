using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Repository.Mapping;
public class ReceitaMap : IEntityTypeConfiguration<Receita>
{
    public void Configure(EntityTypeBuilder<Receita> builder)
    {
        builder.ToTable(nameof(Receita));
        builder.Property(r => r.Id).HasColumnType("binary(16)")
            .HasConversion(v => v.ToByteArray(), v => new Guid(v))
            .ValueGeneratedOnAdd().IsRequired();
        builder.HasKey(r => r.Id);
        builder.Property(r => r.UsuarioId).HasColumnType("binary(16)")
            .HasConversion(v => v.ToByteArray(), v => new Guid(v))
            .ValueGeneratedOnAdd().IsRequired();
        builder.Property(r => r.Descricao).IsRequired(false).HasMaxLength(100);
        builder.Property(r => r.CategoriaId).HasColumnType("binary(16)")
            .HasConversion(v => v.ToByteArray(), v => new Guid(v)).ValueGeneratedOnAdd().IsRequired();

        // MySqlServer
        builder.Property(m => m.Data).HasColumnType("datetime").HasDefaultValueSql<DateTime>("CURRENT_TIMESTAMP").IsRequired();

        // MsSqlServer
        //builder.Property(r => r.Data).HasColumnType("datetime").HasDefaultValueSql<DateTime>("GetDate()").IsRequired();        

        builder.Property(r => r.Valor).HasColumnType("decimal(10, 2)").HasDefaultValue(0);
        builder.HasOne(r => r.Usuario).WithMany().HasForeignKey(r => r.UsuarioId).OnDelete(DeleteBehavior.NoAction);
    }
}