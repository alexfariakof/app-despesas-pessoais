using despesas_backend_api_net_core.Domain.Entities;
using despesas_backend_api_net_core.Infrastructure.Data.Common;

namespace despesas_backend_api_net_core.Database_In_Memory.Implementations
{
    public class DataSeederControleAcesso : IDataSeeder
    {
        private readonly RegisterContext _context;
        public DataSeederControleAcesso(RegisterContext context)
        {
            _context = context;
        }
        public void SeedData()
        {
            if (!_context.ControleAcesso.Any())
            {
                _context.ControleAcesso.AddRange(
                    new ControleAcesso
                    {
                        Id = 1,
                        Login = "alexfariakof@gmail.com",
                        Senha = "G5BtHrnqJJGlg/QRi2hnOmt3EeLMB4M/38fy2lIG4mg=",
                        UsuarioId = 1 
                    },
                    new ControleAcesso
                    {
                        Id = 2,
                        Login = "teste@teste.com",
                        Senha = "5bgAMsdJdv08DMKj4Eco8/COtUdfxTfrQjhU1jftVy4==",
                        UsuarioId = 2 
                    },
                    new ControleAcesso
                    {
                        Id = 3,
                        Login = "dns@dns.com",
                        Senha = "5bgAMsdJdv08DMKj4Eco8/COtUdfxTfrQjhU1jftVy4==",
                        UsuarioId = 3 
                    },
                    new ControleAcesso
                    {
                        Id = 4,
                        Login = "joao.silva5@gmail.com",
                        Senha = "5bgAMsdJdv08DMKj4Eco8/COtUdfxTfrQjhU1jftVy4==",
                        UsuarioId = 4 
                    },
                    new ControleAcesso
                    {
                        Id = 5,
                        Login = "lequinho.mumu@gmail.com",
                        Senha = "5bgAMsdJdv08DMKj4Eco8/COtUdfxTfrQjhU1jftVy4==",
                        UsuarioId = 5 
                    });
                _context.SaveChanges();
            }
        }
    }
}