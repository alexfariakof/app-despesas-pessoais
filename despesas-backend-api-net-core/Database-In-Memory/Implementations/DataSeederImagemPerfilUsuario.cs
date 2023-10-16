using despesas_backend_api_net_core.Domain.Entities;
using despesas_backend_api_net_core.Infrastructure.Data.Common;

namespace despesas_backend_api_net_core.Database_In_Memory.Implementations
{
    public class DataSeederImagemPerfilUsuario : IDataSeeder
    {
        private readonly RegisterContext _context;
        public DataSeederImagemPerfilUsuario(RegisterContext context)
        {
            _context = context;
        }
        public void SeedData()
        {
            if (!_context.ImagemPerfilUsuario.Any())
            {
                var imagens = new List<ImagemPerfilUsuario>
                {
                    new ImagemPerfilUsuario
                    {
                        Id = 2,
                        Url =
                            "https://bucket-usuario-perfil.s3.amazonaws.com/perfil-usuarioId-1-20230703",
                        Name = "perfil-usuarioId-1-20230702",
                        Type = "jpg",
                        UsuarioId = 1
                    },
                    new ImagemPerfilUsuario
                    {
                        Id = 3,
                        Url =
                            "https://bucket-usuario-perfil.s3.amazonaws.com/perfil-usuarioId-2-20230907",
                        Name = "perfil-usuarioId-2-20230719",
                        Type = "png",
                        UsuarioId = 2
                    }
                };
                _context.ImagemPerfilUsuario.AddRange(imagens);
                _context.SaveChanges();
            }
        }
    }
}