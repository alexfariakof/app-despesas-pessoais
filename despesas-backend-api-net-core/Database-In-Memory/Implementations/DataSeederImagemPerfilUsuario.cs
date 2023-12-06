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
                var imagens = new List<ImagemPerfilUsuario> { };
                _context.ImagemPerfilUsuario.AddRange(imagens);
                _context.SaveChanges();
            }
        }
    }
}
