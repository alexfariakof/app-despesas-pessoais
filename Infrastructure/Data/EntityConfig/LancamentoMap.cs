using despesas_backend_api_net_core.Domain.VM;
using despesas_backend_api_net_core.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace despesas_backend_api_net_core.Infrastructure.Data.EntityConfig
{
    public class LancamentoMap : IParser<LancamentoVM, Lancamento>, IParser<Lancamento, LancamentoVM>, IEntityTypeConfiguration<Lancamento>
    {

        public void Configure(EntityTypeBuilder<Lancamento> builder)
        {
            builder.HasKey(m => m.Id);

            builder.Property(m => m.Data)
            .HasColumnType("timestamp")
            .HasDefaultValue(null);

            builder.Property(m => m.Valor)
            .HasColumnType("decimal(10, 2)");

            builder.Property(m => m.DespesaId)
            .IsRequired(false)
            .HasDefaultValue(null);

            builder.Property(m => m.ReceitaId)
            .IsRequired(false)
            .HasDefaultValue(null);
        }

        public Lancamento Parse(LancamentoVM origin)
        {
            if (origin == null) return new Lancamento();
            return new Lancamento
            {
                Id = origin.Id,
                DespesaId = origin.IdDespesa,
                ReceitaId = origin.IdReceita,
                UsuarioId = origin.IdUsuario,
                Data = origin.Data.ToDateTime(),
                Valor = origin.Valor.ToDecimal(),
                Despesa = new Despesa { Id = origin.IdDespesa, Descricao = origin.Descricao },
                Receita = new Receita { Id = origin.IdReceita, Descricao = origin.Descricao },
                Categoria = new Categoria { Descricao = origin.Categoria }
            };
        }

        public LancamentoVM Parse(Lancamento origin)
        {
            if (origin == null) return new LancamentoVM();
            return new LancamentoVM
            {
                Id = origin.Id,
                IdDespesa = origin.DespesaId.Value,
                IdReceita = origin.ReceitaId.Value,
                IdUsuario = origin.UsuarioId,
                Data = origin.Data.ToDateBr(),
                Valor = origin.Valor.ToString("N2"),
                Descricao = origin.DespesaId == 0 ? origin.Receita.Descricao : origin.Despesa.Descricao,
                Categoria = origin.Categoria.Descricao
            };
        }

        public List<Lancamento> ParseList(List<LancamentoVM> origin)
        {
            if (origin == null) return new List<Lancamento>();
            return origin.Select(item => Parse(item)).ToList();
        }

        public List<LancamentoVM> ParseList(List<Lancamento> origin)
        {
            if (origin == null) return new List<LancamentoVM>();
            return origin.Select(item => Parse(item)).ToList();
        }
    }
}
