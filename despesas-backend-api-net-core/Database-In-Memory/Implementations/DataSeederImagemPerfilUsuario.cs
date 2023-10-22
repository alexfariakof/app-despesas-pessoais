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
                        Id = 1,
                        Url ="https://bucket-usuario-perfil.s3.amazonaws.com/1-imagem-perfil-usuario",
                        Name = "1-imagem-perfil-usuario",
                        Type = "jpg",
                        UsuarioId = 1
                    },
                    new ImagemPerfilUsuario
                    {
                        Id = 2,
                        Url =
                            "https://bucket-usuario-perfil.s3.sa-east-1.amazonaws.com/2-imagem-perfil-usuario",
                        Name = "2-imagem-perfil-usuario",
                        Type = "jpg",
                        UsuarioId = 2
                    },
                };
                _context.ImagemPerfilUsuario.AddRange(imagens);
                _context.SaveChanges();
            }
        }
    }
}