using Repository;
using Domain.Entities;

namespace DataSeeders.Implementations;
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
