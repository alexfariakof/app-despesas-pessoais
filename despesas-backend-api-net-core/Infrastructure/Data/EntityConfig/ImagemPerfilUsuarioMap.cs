using despesas_backend_api_net_core.Domain.Entities;
using despesas_backend_api_net_core.Domain.VM;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace despesas_backend_api_net_core.Infrastructure.Data.EntityConfig
{
    public class ImagemPerfilUsuarioMap : IParser<ImagemPerfilVM, ImagemPerfilUsuario>, IParser<ImagemPerfilUsuario, ImagemPerfilVM>, IEntityTypeConfiguration<ImagemPerfilUsuario>
    {
        public void Configure(EntityTypeBuilder<ImagemPerfilUsuario> builder)
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
        public ImagemPerfilUsuario Parse(ImagemPerfilVM origin)
        {
            if (origin == null) return new ImagemPerfilUsuario();
            return new ImagemPerfilUsuario
            {
                Id = origin.Id,
                Name = origin.Name,
                Type = origin.Type,          
                Url = origin.Url,
                UsuarioId = origin.IdUsuario,
            };
        }
        public ImagemPerfilVM Parse(ImagemPerfilUsuario origin)
        {
            if (origin == null) return new ImagemPerfilVM();
            return new ImagemPerfilVM
            {
                Id = origin.Id,
                Name = origin.Name,
                Type = origin.Type,
                Url = origin.Url,
                IdUsuario = origin.UsuarioId,
            };
        }

        public List<ImagemPerfilUsuario> ParseList(List<ImagemPerfilVM> origin)
        {
            if (origin == null) return new List<ImagemPerfilUsuario>();
            return origin.Select(item => Parse(item)).ToList();
        }

        public List<ImagemPerfilVM> ParseList(List<ImagemPerfilUsuario> origin)
        {
            if (origin == null) return new List<ImagemPerfilVM>();
            return origin.Select(item => Parse(item)).ToList();
        }
    }
}