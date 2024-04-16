using Repository;
using Domain.Entities;

namespace DataSeeders.Implementations;
public class DataSeederCategoria : IDataSeeder
{
    private readonly RegisterContext _context;

    public DataSeederCategoria(RegisterContext context)
    {
        _context = context;
    }

    public void SeedData()
    {
        if (!_context.Categoria.Any())
        {
            _context.Categoria.AddRange(
                new Categoria
                {
                    Id = 1,
                    Descricao = "Alimentação",
                    UsuarioId = 1,
                    TipoCategoria = TipoCategoria.Despesa
                },
                new Categoria
                {
                    Id = 2,
                    Descricao = "Casa",
                    UsuarioId = 1,
                    TipoCategoria = TipoCategoria.Despesa
                },
                new Categoria
                {
                    Id = 3,
                    Descricao = "Serviços",
                    UsuarioId = 1,
                    TipoCategoria = TipoCategoria.Despesa
                },
                new Categoria
                {
                    Id = 4,
                    Descricao = "Saúde",
                    UsuarioId = 1,
                    TipoCategoria = TipoCategoria.Despesa
                },
                new Categoria
                {
                    Id = 5,
                    Descricao = "Imposto",
                    UsuarioId = 1,
                    TipoCategoria = TipoCategoria.Despesa
                },
                new Categoria
                {
                    Id = 6,
                    Descricao = "Transporte",
                    UsuarioId = 1,
                    TipoCategoria = TipoCategoria.Despesa
                },
                new Categoria
                {
                    Id = 7,
                    Descricao = "Lazer",
                    UsuarioId = 1,
                    TipoCategoria = TipoCategoria.Despesa
                },
                new Categoria
                {
                    Id = 8,
                    Descricao = "Outros",
                    UsuarioId = 1,
                    TipoCategoria = TipoCategoria.Despesa
                },
                new Categoria
                {
                    Id = 9,
                    Descricao = "Salário",
                    UsuarioId = 1,
                    TipoCategoria = TipoCategoria.Receita
                },
                new Categoria
                {
                    Id = 10,
                    Descricao = "Prêmio",
                    UsuarioId = 1,
                    TipoCategoria = TipoCategoria.Receita
                },
                new Categoria
                {
                    Id = 11,
                    Descricao = "Investimento",
                    UsuarioId = 1,
                    TipoCategoria = TipoCategoria.Receita
                },
                new Categoria
                {
                    Id = 12,
                    Descricao = "Benefício",
                    UsuarioId = 1,
                    TipoCategoria = TipoCategoria.Receita
                },
                new Categoria
                {
                    Id = 13,
                    Descricao = "Outros",
                    UsuarioId = 1,
                    TipoCategoria = TipoCategoria.Receita
                },
                new Categoria
                {
                    Id = 14,
                    Descricao = "Alimentação",
                    UsuarioId = 2,
                    TipoCategoria = TipoCategoria.Despesa
                },
                new Categoria
                {
                    Id = 15,
                    Descricao = "Casa",
                    UsuarioId = 2,
                    TipoCategoria = TipoCategoria.Despesa
                },
                new Categoria
                {
                    Id = 16,
                    Descricao = "Serviços",
                    UsuarioId = 2,
                    TipoCategoria = TipoCategoria.Despesa
                },
                new Categoria
                {
                    Id = 17,
                    Descricao = "Saúde",
                    UsuarioId = 2,
                    TipoCategoria = TipoCategoria.Despesa
                },
                new Categoria
                {
                    Id = 18,
                    Descricao = "Imposto",
                    UsuarioId = 2,
                    TipoCategoria = TipoCategoria.Despesa
                },
                new Categoria
                {
                    Id = 19,
                    Descricao = "Transporte",
                    UsuarioId = 2,
                    TipoCategoria = TipoCategoria.Despesa
                },
                new Categoria
                {
                    Id = 20,
                    Descricao = "Lazer",
                    UsuarioId = 2,
                    TipoCategoria = TipoCategoria.Despesa
                },
                new Categoria
                {
                    Id = 21,
                    Descricao = "Outros",
                    UsuarioId = 2,
                    TipoCategoria = TipoCategoria.Despesa
                },
                new Categoria
                {
                    Id = 22,
                    Descricao = "Salário",
                    UsuarioId = 2,
                    TipoCategoria = TipoCategoria.Receita
                },
                new Categoria
                {
                    Id = 23,
                    Descricao = "Prêmio",
                    UsuarioId = 2,
                    TipoCategoria = TipoCategoria.Receita
                },
                new Categoria
                {
                    Id = 24,
                    Descricao = "Investimento",
                    UsuarioId = 2,
                    TipoCategoria = TipoCategoria.Receita
                },
                new Categoria
                {
                    Id = 25,
                    Descricao = "Benefício",
                    UsuarioId = 2,
                    TipoCategoria = TipoCategoria.Receita
                },
                new Categoria
                {
                    Id = 26,
                    Descricao = "Outros",
                    UsuarioId = 2,
                    TipoCategoria = TipoCategoria.Receita
                }
            );
            _context.SaveChanges();
        }
    }
}
