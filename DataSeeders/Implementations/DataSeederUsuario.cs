using Repository;
using Domain.Entities;

namespace DataSeeders.Implementations;
public class DataSeederUsuario : IDataSeeder
{
    private readonly RegisterContext _context;
    public DataSeederUsuario(RegisterContext context)
    {
        _context = context;
    }
    public void SeedData()
    {
        if (!_context.Usuario.Any())
        {
            _context.Usuario.AddRange(
                new Usuario
                {
                    Id = 1,
                    Nome = "Alex",
                    SobreNome = "Ribeiro de Faria",
                    Telefone = "(21) 99287-9319",
                    Email = "alexfariakof@gmail.com",
                    StatusUsuario = StatusUsuario.Ativo,
                    PerfilUsuario = PerfilUsuario.Administrador
                },
                new Usuario
                {
                    Id = 2,
                    Nome = "Teste",
                    SobreNome = "Teste",
                    Telefone = "(21) 9999-9999",
                    Email = "teste@teste.com",
                    StatusUsuario = StatusUsuario.Ativo,
                    PerfilUsuario = PerfilUsuario.Usuario
                },
                new Usuario
                {
                    Id = 3,
                    Nome = "dns",
                    SobreNome = "AWS dns",
                    Telefone = "(21) 9999-9999",
                    Email = "dns@dns.com",
                    StatusUsuario = StatusUsuario.Ativo,
                    PerfilUsuario = PerfilUsuario.Usuario
                },
                new Usuario
                {
                    Id = 4,
                    Nome = "João",
                    SobreNome = "da Silva",
                    Telefone = "(21) 8888-8888",
                    Email = "joao.silva5@gmail.com",
                    StatusUsuario = StatusUsuario.Ativo,
                    PerfilUsuario = PerfilUsuario.Usuario
                },
                new Usuario
                {
                    Id = 5,
                    Nome = "Alexsandro Clóvis",
                    SobreNome = "Sacramento",
                    Telefone = "21992320175",
                    Email = "lequinho.mumu@gmail.com",
                    StatusUsuario = StatusUsuario.Ativo,
                    PerfilUsuario = PerfilUsuario.Usuario
                });
            _context.SaveChanges();
        }
    }
}