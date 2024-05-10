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
                    Descricao = "Alimentação",
                    UsuarioId = 1,
                    TipoCategoria = TipoCategoria.Despesa
                },
                new Categoria
                {
                    Descricao = "Casa",
                    UsuarioId = 1,
                    TipoCategoria = TipoCategoria.Despesa
                },
                new Categoria
                {
                    Descricao = "Serviços",
                    UsuarioId = 1,
                    TipoCategoria = TipoCategoria.Despesa
                },
                new Categoria
                {
                    Descricao = "Saúde",
                    UsuarioId = 1,
                    TipoCategoria = TipoCategoria.Despesa
                },
                new Categoria
                {
                    Descricao = "Imposto",
                    UsuarioId = 1,
                    TipoCategoria = TipoCategoria.Despesa
                },
                new Categoria
                {
                    Descricao = "Transporte",
                    UsuarioId = 1,
                    TipoCategoria = TipoCategoria.Despesa
                },
                new Categoria
                {
                    Descricao = "Lazer",
                    UsuarioId = 1,
                    TipoCategoria = TipoCategoria.Despesa
                },
                new Categoria
                {
                    Descricao = "Outros",
                    UsuarioId = 1,
                    TipoCategoria = TipoCategoria.Despesa
                },
                new Categoria
                {
                    Descricao = "Salário",
                    UsuarioId = 1,
                    TipoCategoria = TipoCategoria.Receita
                },
                new Categoria
                {
                    Descricao = "Prêmio",
                    UsuarioId = 1,
                    TipoCategoria = TipoCategoria.Receita
                },
                new Categoria
                {
                    Descricao = "Investimento",
                    UsuarioId = 1,
                    TipoCategoria = TipoCategoria.Receita
                },
                new Categoria
                {
                    Descricao = "Benefício",
                    UsuarioId = 1,
                    TipoCategoria = TipoCategoria.Receita
                },
                new Categoria
                {
                    Descricao = "Outros",
                    UsuarioId = 1,
                    TipoCategoria = TipoCategoria.Receita
                },
                new Categoria
                {
                    Descricao = "Alimentação",
                    UsuarioId = 2,
                    TipoCategoria = TipoCategoria.Despesa
                },
                new Categoria
                {
                    Descricao = "Casa",
                    UsuarioId = 2,
                    TipoCategoria = TipoCategoria.Despesa
                },
                new Categoria
                {
                    Descricao = "Serviços",
                    UsuarioId = 2,
                    TipoCategoria = TipoCategoria.Despesa
                },
                new Categoria
                {
                    Descricao = "Saúde",
                    UsuarioId = 2,
                    TipoCategoria = TipoCategoria.Despesa
                },
                new Categoria
                {
                    Descricao = "Imposto",
                    UsuarioId = 2,
                    TipoCategoria = TipoCategoria.Despesa
                },
                new Categoria
                {
                    Descricao = "Transporte",
                    UsuarioId = 2,
                    TipoCategoria = TipoCategoria.Despesa
                },
                new Categoria
                {
                    Descricao = "Lazer",
                    UsuarioId = 2,
                    TipoCategoria = TipoCategoria.Despesa
                },
                new Categoria
                {

                    Descricao = "Outros",
                    UsuarioId = 2,
                    TipoCategoria = TipoCategoria.Despesa
                },
                new Categoria
                {
                    Descricao = "Salário",
                    UsuarioId = 2,
                    TipoCategoria = TipoCategoria.Receita
                },
                new Categoria
                {
                    Descricao = "Prêmio",
                    UsuarioId = 2,
                    TipoCategoria = TipoCategoria.Receita
                },
                new Categoria
                {
                    Descricao = "Investimento",
                    UsuarioId = 2,
                    TipoCategoria = TipoCategoria.Receita
                },
                new Categoria
                {
                    Descricao = "Benefício",
                    UsuarioId = 2,
                    TipoCategoria = TipoCategoria.Receita
                },
                new Categoria
                {
                    Descricao = "Outros",
                    UsuarioId = 2,
                    TipoCategoria = TipoCategoria.Receita
                }
            );
            _context.SaveChanges();
        }
    }
}
