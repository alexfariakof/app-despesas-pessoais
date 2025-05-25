using Domain.Entities;
using EasyCryptoSalt;
using Repository;

namespace DataSeeders.Implementations;
public class DataSeederControleAcesso : IDataSeeder
{
    private readonly ICrypto _crypto;

    private readonly RegisterContext _context;
    public DataSeederControleAcesso(RegisterContext context, ICrypto crypto)
    {
        _context = context;
        _crypto = crypto;
    }
    public void SeedData()
    {
        var account = new ControleAcesso();
        var usuario = new Usuario
        {
            Nome = "Alex",
            SobreNome = "Ribeiro de Faria",
            Telefone = "(21) 99287-9319",
            Email = "alexfariakof@gmail.com",
            StatusUsuario = StatusUsuario.Ativo
        };
        var teste = _crypto.Encrypt("toor");
        account.CreateAccount(usuario, teste);
        account.Usuario.PerfilUsuario = _context.PerfilUsuario.First(pu => pu.Id.Equals(1));
        account.Usuario.Categorias.ToList()
            .ForEach(c => c.TipoCategoria = _context.TipoCategoria
            .First(tc => tc.Id == c.TipoCategoria.Id));

        _context.Add(account);
        _context.SaveChanges();

        account = new ControleAcesso();
        usuario = new Usuario
        {
            Nome = "Teste",
            SobreNome = "Teste",
            Telefone = "(21) 9999-9999",
            Email = "teste@teste.com",
            StatusUsuario = StatusUsuario.Ativo,
        };
        account.CreateAccount(usuario, _crypto.Encrypt("toor"));
        account.Usuario.PerfilUsuario = _context.PerfilUsuario.First(pu => pu.Id.Equals(2));
        account.Usuario.Categorias.ToList()
            .ForEach(c => c.TipoCategoria = _context.TipoCategoria
            .First(tc => tc.Id == c.TipoCategoria.Id));

        _context.Add(account);
        _context.SaveChanges();
    }
}