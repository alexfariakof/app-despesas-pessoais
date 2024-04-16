using Domain.Entities;
using Repository;

namespace DataSeeders.Implementations;
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
                    Senha = "12345T!",
                    UsuarioId = 1 
                },
                new ControleAcesso
                {
                    Id = 2,
                    Login = "teste@teste.com",
                    Senha = "12345T!",
                    UsuarioId = 2 
                },
                new ControleAcesso
                {
                    Id = 3,
                    Login = "dns@dns.com",
                    Senha = "12345T!",
                    UsuarioId = 3 
                },
                new ControleAcesso
                {
                    Id = 4,
                    Login = "joao.silva5@gmail.com",
                    Senha = "12345T!",
                    UsuarioId = 4 
                },
                new ControleAcesso
                {
                    Id = 5,
                    Login = "lequinho.mumu@gmail.com",
                    Senha = "618/OUKTTzRLXOWNpU7+QWwpM8UWJG+LlA/a7C6RKcY=",
                    UsuarioId = 5 
                });
            _context.SaveChanges();
        }
    }
}