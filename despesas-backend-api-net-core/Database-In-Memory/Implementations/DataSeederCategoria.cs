using despesas_backend_api_net_core.Domain.Entities;
using despesas_backend_api_net_core.Infrastructure.Data.Common;

namespace despesas_backend_api_net_core.Database_In_Memory.Implementations
{
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
                    },
                    new Categoria
                    {
                        Id = 27,
                        Descricao = "Alimentação",
                        UsuarioId = 3,
                        TipoCategoria = TipoCategoria.Despesa
                    },
                    new Categoria
                    {
                        Id = 28,
                        Descricao = "Casa",
                        UsuarioId = 3,
                        TipoCategoria = TipoCategoria.Despesa
                    },
                    new Categoria
                    {
                        Id = 29,
                        Descricao = "Serviços",
                        UsuarioId = 3,
                        TipoCategoria = TipoCategoria.Despesa
                    },
                    new Categoria
                    {
                        Id = 30,
                        Descricao = "Saúde",
                        UsuarioId = 3,
                        TipoCategoria = TipoCategoria.Despesa
                    },
                    new Categoria
                    {
                        Id = 31,
                        Descricao = "Imposto",
                        UsuarioId = 3,
                        TipoCategoria = TipoCategoria.Despesa
                    },
                    new Categoria
                    {
                        Id = 32,
                        Descricao = "Transporte",
                        UsuarioId = 3,
                        TipoCategoria = TipoCategoria.Despesa
                    },
                    new Categoria
                    {
                        Id = 33,
                        Descricao = "Lazer",
                        UsuarioId = 3,
                        TipoCategoria = TipoCategoria.Despesa
                    },
                    new Categoria
                    {
                        Id = 34,
                        Descricao = "Outros",
                        UsuarioId = 3,
                        TipoCategoria = TipoCategoria.Despesa
                    },
                    new Categoria
                    {
                        Id = 35,
                        Descricao = "Salário",
                        UsuarioId = 3,
                        TipoCategoria = TipoCategoria.Receita
                    },
                    new Categoria
                    {
                        Id = 36,
                        Descricao = "Prêmio",
                        UsuarioId = 3,
                        TipoCategoria = TipoCategoria.Receita
                    },
                    new Categoria
                    {
                        Id = 37,
                        Descricao = "Investimento",
                        UsuarioId = 3,
                        TipoCategoria = TipoCategoria.Receita
                    },
                    new Categoria
                    {
                        Id = 38,
                        Descricao = "Benefício",
                        UsuarioId = 3,
                        TipoCategoria = TipoCategoria.Receita
                    },
                    new Categoria
                    {
                        Id = 39,
                        Descricao = "Outros",
                        UsuarioId = 3,
                        TipoCategoria = TipoCategoria.Receita
                    },
                    new Categoria
                    {
                        Id = 41,
                        Descricao = "Alimentação",
                        UsuarioId = 4,
                        TipoCategoria = TipoCategoria.Despesa
                    },
                    new Categoria
                    {
                        Id = 42,
                        Descricao = "Casa",
                        UsuarioId = 4,
                        TipoCategoria = TipoCategoria.Despesa
                    },
                    new Categoria
                    {
                        Id = 43,
                        Descricao = "Serviços",
                        UsuarioId = 4,
                        TipoCategoria = TipoCategoria.Despesa
                    },
                    new Categoria
                    {
                        Id = 44,
                        Descricao = "Saúde",
                        UsuarioId = 4,
                        TipoCategoria = TipoCategoria.Despesa
                    },
                    new Categoria
                    {
                        Id = 45,
                        Descricao = "Imposto",
                        UsuarioId = 4,
                        TipoCategoria = TipoCategoria.Despesa
                    },
                    new Categoria
                    {
                        Id = 46,
                        Descricao = "Transporte",
                        UsuarioId = 4,
                        TipoCategoria = TipoCategoria.Despesa
                    },
                    new Categoria
                    {
                        Id = 47,
                        Descricao = "Lazer",
                        UsuarioId = 4,
                        TipoCategoria = TipoCategoria.Despesa
                    },
                    new Categoria
                    {
                        Id = 48,
                        Descricao = "Outros",
                        UsuarioId = 4,
                        TipoCategoria = TipoCategoria.Despesa
                    },
                    new Categoria
                    {
                        Id = 49,
                        Descricao = "Salário",
                        UsuarioId = 4,
                        TipoCategoria = TipoCategoria.Receita
                    },
                    new Categoria
                    {
                        Id = 50,
                        Descricao = "Prêmio",
                        UsuarioId = 4,
                        TipoCategoria = TipoCategoria.Receita
                    },
                    new Categoria
                    {
                        Id = 51,
                        Descricao = "Investimento",
                        UsuarioId = 4,
                        TipoCategoria = TipoCategoria.Receita
                    },
                    new Categoria
                    {
                        Id = 52,
                        Descricao = "Benefício",
                        UsuarioId = 4,
                        TipoCategoria = TipoCategoria.Receita
                    },
                    new Categoria
                    {
                        Id = 53,
                        Descricao = "Outros",
                        UsuarioId = 4,
                        TipoCategoria = TipoCategoria.Receita
                    },
                    new Categoria
                    {
                        Id = 54,
                        Descricao = "Alimentação",
                        UsuarioId = 5,
                        TipoCategoria = TipoCategoria.Despesa
                    },
                    new Categoria
                    {
                        Id = 55,
                        Descricao = "Casa",
                        UsuarioId = 5,
                        TipoCategoria = TipoCategoria.Despesa
                    },
                    new Categoria
                    {
                        Id = 56,
                        Descricao = "Serviços",
                        UsuarioId = 5,
                        TipoCategoria = TipoCategoria.Despesa
                    },
                    new Categoria
                    {
                        Id = 57,
                        Descricao = "Saúde",
                        UsuarioId = 5,
                        TipoCategoria = TipoCategoria.Despesa
                    },
                    new Categoria
                    {
                        Id = 58,
                        Descricao = "Imposto",
                        UsuarioId = 5,
                        TipoCategoria = TipoCategoria.Despesa
                    },
                    new Categoria
                    {
                        Id = 59,
                        Descricao = "Transporte",
                        UsuarioId = 5,
                        TipoCategoria = TipoCategoria.Despesa
                    },
                    new Categoria
                    {
                        Id = 60,
                        Descricao = "Lazer",
                        UsuarioId = 5,
                        TipoCategoria = TipoCategoria.Despesa
                    },
                    new Categoria
                    {
                        Id = 61,
                        Descricao = "Outros",
                        UsuarioId = 5,
                        TipoCategoria = TipoCategoria.Despesa
                    },
                    new Categoria
                    {
                        Id = 62,
                        Descricao = "Salário",
                        UsuarioId = 5,
                        TipoCategoria = TipoCategoria.Receita
                    },
                    new Categoria
                    {
                        Id = 63,
                        Descricao = "Prêmio",
                        UsuarioId = 5,
                        TipoCategoria = TipoCategoria.Receita
                    },
                    new Categoria
                    {
                        Id = 64,
                        Descricao = "Investimento",
                        UsuarioId = 5,
                        TipoCategoria = TipoCategoria.Receita
                    },
                    new Categoria
                    {
                        Id = 65,
                        Descricao = "Benefício",
                        UsuarioId = 5,
                        TipoCategoria = TipoCategoria.Receita
                    },
                    new Categoria
                    {
                        Id = 66,
                        Descricao = "Outros",
                        UsuarioId = 5,
                        TipoCategoria = TipoCategoria.Receita
                    },
                    new Categoria
                    {
                        Id = 67,
                        Descricao = "teste",
                        UsuarioId = 2,
                        TipoCategoria = TipoCategoria.Despesa
                    }
                );
                _context.SaveChanges();
            }
        }
    }
}
