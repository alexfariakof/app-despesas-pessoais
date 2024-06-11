using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Domain.Entities;

namespace Repository.Mapping;
public class LancamentoMap: IEntityTypeConfiguration<Lancamento>
{
    public void Configure(EntityTypeBuilder<Lancamento> builder)
    {
        builder.ToTable(nameof(Lancamento));
        builder.HasKey(l => l.Id);
        builder.Property(l => l.Id).ValueGeneratedOnAdd().IsRequired();        
        builder.Property(l => l.UsuarioId).IsRequired();
        builder.Property(l => l.DespesaId).IsRequired(false).HasDefaultValue(null);
        builder.Property(l => l.ReceitaId).IsRequired(false).HasDefaultValue(null);
        builder.Property(l => l.UsuarioId).IsRequired();

        //MySqlServer
        builder.Property(m => m.Data).HasColumnType("datetime").IsRequired();
        builder.Property(m => m.DataCriacao).HasColumnType("datetime").HasDefaultValueSql<DateTime>("CURRENT_TIMESTAMP");

        // MsSqlServer
        //builder.Property(l => l.Data).HasColumnType("datetime").IsRequired();
        //builder.Property(l => l.DataCriacao).HasColumnType("datetime").HasDefaultValueSql<DateTime>("GetDate()");            

        builder.Property(l => l.Valor).HasColumnType("decimal(10, 2)");
        builder.Property(l => l.Descricao).HasMaxLength(100);
        builder.HasOne(l => l.Usuario).WithMany().HasForeignKey(l => l.UsuarioId).OnDelete(DeleteBehavior.NoAction);
        builder.HasOne(l => l.Despesa).WithMany().HasForeignKey(l => l.DespesaId).OnDelete(DeleteBehavior.NoAction);
        builder.HasOne(l => l.Receita).WithMany().HasForeignKey(l => l.ReceitaId).OnDelete(DeleteBehavior.NoAction);
    }
}