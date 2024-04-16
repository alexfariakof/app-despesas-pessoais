using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Domain.Entities;

namespace Repository.Mapping;
public class LancamentoMap: IEntityTypeConfiguration<Lancamento>
{
    public void Configure(EntityTypeBuilder<Lancamento> builder)
    {
        builder.ToTable(nameof(Lancamento));
        builder.HasKey(m => m.Id);
        builder.Property(x => x.Id).ValueGeneratedOnAdd().IsRequired();        
        builder.Property(m => m.UsuarioId).IsRequired();
        builder.Property(m => m.DespesaId).IsRequired(false).HasDefaultValue(null);
        builder.Property(m => m.ReceitaId).IsRequired(false).HasDefaultValue(null);
        builder.Property(m => m.UsuarioId).IsRequired();
        builder.Property(m => m.Data).HasColumnType("timestamp").IsRequired();
        builder.Property(m => m.DataCriacao).HasColumnType("timestamp").HasDefaultValueSql<DateTime>("NOW()");            
        builder.Property(m => m.Valor).HasColumnType("decimal(10, 2)");
        builder.Property(m => m.Descricao).HasMaxLength(100);            
    }
}