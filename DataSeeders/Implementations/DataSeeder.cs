using Repository;

namespace DataSeeders.Implementations;
public class DataSeeder : IDataSeeder
{
    private readonly RegisterContext _context;
    public DataSeeder(RegisterContext context)
    {
        _context = context;
    }
    public void SeedData()
    {
        new DataSeederUsuario(_context).SeedData();
        new DataSeederControleAcesso(_context).SeedData();
        new DataSeederCategoria(_context).SeedData();
        new DataSeederDespesa(_context).SeedData();
        new DataSeederReceita(_context).SeedData();
        new DataSeederImagemPerfilUsuario(_context).SeedData();
    }
}
