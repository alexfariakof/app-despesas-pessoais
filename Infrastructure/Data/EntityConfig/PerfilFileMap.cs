using despesas_backend_api_net_core.Domain.Entities;
using despesas_backend_api_net_core.Domain.VM;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace despesas_backend_api_net_core.Infrastructure.Data.EntityConfig
{
    public class PerfilFileMap : IParser<PerfilUsuarioFileVM, PerfilFile>, IParser<PerfilFile, PerfilUsuarioFileVM>, IEntityTypeConfiguration<PerfilFile>
    {
        public void Configure(EntityTypeBuilder<PerfilFile> builder)
        {
            builder.HasKey(m => m.Id);

            builder.Property(m => m.UsuarioId)
           .IsRequired();

            builder.HasIndex(m => m.Name)
            .IsUnique(true);

            builder.Property(m => m.Name)
            .IsRequired()
            .HasMaxLength(50);

            builder.HasIndex(m => m.Url)
            .IsUnique(true);

            builder.Property(m => m.Url)
            .IsRequired();

            builder.Property(m => m.Type)
            .IsRequired()
            .HasMaxLength(4);

            builder.HasIndex(m => m.UsuarioId)
                .IsUnique(true);

            builder.Property(m => m.UsuarioId)
            .IsRequired();

        }
        public PerfilFile Parse(PerfilUsuarioFileVM origin)
        {
            if (origin == null) return new PerfilFile();
            return new PerfilFile
            {
                Id = origin.Id,
                Name = origin.Name,
                Type = origin.Type,          
                Url = origin.Url,
                UsuarioId = origin.UsuarioId,
            };
        }
        public PerfilUsuarioFileVM Parse(PerfilFile origin)
        {
            if (origin == null) return new PerfilUsuarioFileVM();
            return new PerfilUsuarioFileVM
            {
                Id = origin.Id,
                Name = origin.Name,
                Type = origin.Type,
                Url = origin.Url,
                UsuarioId = origin.UsuarioId,
            };
        }

        public List<PerfilFile> ParseList(List<PerfilUsuarioFileVM> origin)
        {
            if (origin == null) return new List<PerfilFile>();
            return origin.Select(item => Parse(item)).ToList();
        }

        public List<PerfilUsuarioFileVM> ParseList(List<PerfilFile> origin)
        {
            if (origin == null) return new List<PerfilUsuarioFileVM>();
            return origin.Select(item => Parse(item)).ToList();
        }
    }
}