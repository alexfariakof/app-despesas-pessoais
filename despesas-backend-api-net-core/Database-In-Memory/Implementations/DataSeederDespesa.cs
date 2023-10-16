using despesas_backend_api_net_core.Domain.Entities;
using despesas_backend_api_net_core.Infrastructure.Data.Common;

namespace despesas_backend_api_net_core.Database_In_Memory.Implementations
{
    public class DataSeederDespesa : IDataSeeder
    {
        private readonly RegisterContext _context;
        public DataSeederDespesa(RegisterContext context)
        {
            _context = context;
        }
        public void SeedData()
        {
            if (!_context.Despesa.Any())
            {
                var despesas = new List<Despesa>
                {
                    new Despesa
                    {
                        Id = 1,
                        Data = new DateTime(2023, 1, 1),
                        Descricao = "Conta de Luz",
                        Valor = 150.00m,
                        DataVencimento = new DateTime(2023, 1, 10),
                        UsuarioId = 2,
                        CategoriaId = 14
                    },
                    new Despesa
                    {
                        Id = 2,
                        Data = new DateTime(2023, 1, 2),
                        Descricao = "Compra de mantimentos",
                        Valor = 50.00m,
                        DataVencimento = new DateTime(2023, 1, 15),
                        UsuarioId = 2,
                        CategoriaId = 15
                    },
                    new Despesa
                    {
                        Id = 3,
                        Data = new DateTime(2023, 1, 3),
                        Descricao = "Serviço de Limpeza",
                        Valor = 80.00m,
                        DataVencimento = new DateTime(2023, 1, 20),
                        UsuarioId = 2,
                        CategoriaId = 16
                    },
                    new Despesa
                    {
                        Id = 4,
                        Data = new DateTime(2023, 1, 4),
                        Descricao = "Consulta Médica",
                        Valor = 200.00m,
                        DataVencimento = new DateTime(2023, 1, 25),
                        UsuarioId = 2,
                        CategoriaId = 17
                    },
                    new Despesa
                    {
                        Id = 5,
                        Data = new DateTime(2023, 1, 5),
                        Descricao = "Imposto de Renda",
                        Valor = 300.00m,
                        DataVencimento = new DateTime(2023, 1, 5),
                        UsuarioId = 2,
                        CategoriaId = 18
                    },
                    new Despesa
                    {
                        Id = 6,
                        Data = new DateTime(2023, 1, 6),
                        Descricao = "Passagem de Ônibus",
                        Valor = 10.00m,
                        DataVencimento = new DateTime(2023, 1, 10),
                        UsuarioId = 2,
                        CategoriaId = 19
                    },
                    new Despesa
                    {
                        Id = 7,
                        Data = new DateTime(2023, 1, 7),
                        Descricao = "Cinema",
                        Valor = 20.50m,
                        DataVencimento = new DateTime(2023, 1, 15),
                        UsuarioId = 2,
                        CategoriaId = 20
                    },
                    new Despesa
                    {
                        Id = 8,
                        Data = new DateTime(2023, 1, 8),
                        Descricao = "Outros gastos",
                        Valor = 15.20m,
                        DataVencimento = new DateTime(2023, 1, 20),
                        UsuarioId = 2,
                        CategoriaId = 21
                    },
                    new Despesa
                    {
                        Id = 9,
                        Data = new DateTime(2023, 1, 9),
                        Descricao = "Salário",
                        Valor = 1000.00m,
                        DataVencimento = new DateTime(2023, 1, 25),
                        UsuarioId = 2,
                        CategoriaId = 22
                    },
                    new Despesa
                    {
                        Id = 10,
                        Data = new DateTime(2023, 1, 10),
                        Descricao = "Prêmio",
                        Valor = 500.00m,
                        DataVencimento = new DateTime(2023, 1, 5),
                        UsuarioId = 2,
                        CategoriaId = 23
                    },
                    new Despesa
                    {
                        Id = 6,
                        Data = new DateTime(2023, 1, 6),
                        Descricao = "Passagem de Ônibus",
                        Valor = 10.00m,
                        DataVencimento = new DateTime(2023, 1, 10),
                        UsuarioId = 2,
                        CategoriaId = 19
                    },
                    new Despesa
                    {
                        Id = 7,
                        Data = new DateTime(2023, 1, 7),
                        Descricao = "Cinema",
                        Valor = 20.50m,
                        DataVencimento = new DateTime(2023, 1, 15),
                        UsuarioId = 2,
                        CategoriaId = 20
                    },
                    new Despesa
                    {
                        Id = 8,
                        Data = new DateTime(2023, 1, 8),
                        Descricao = "Outros gastos",
                        Valor = 15.20m,
                        DataVencimento = new DateTime(2023, 1, 20),
                        UsuarioId = 2,
                        CategoriaId = 21
                    },
                    new Despesa
                    {
                        Id = 9,
                        Data = new DateTime(2023, 1, 9),
                        Descricao = "Salário",
                        Valor = 1000.00m,
                        DataVencimento = new DateTime(2023, 1, 25),
                        UsuarioId = 2,
                        CategoriaId = 22
                    },
                    new Despesa
                    {
                        Id = 10,
                        Data = new DateTime(2023, 1, 10),
                        Descricao = "Prêmio",
                        Valor = 500.00m,
                        DataVencimento = new DateTime(2023, 1, 5),
                        UsuarioId = 2,
                        CategoriaId = 23
                    },
                    new Despesa
                    {
                        Id = 11,
                        Data = new DateTime(2023, 2, 1),
                        Descricao = "Aluguel",
                        Valor = 800.00m,
                        DataVencimento = new DateTime(2023, 2, 10),
                        UsuarioId = 2,
                        CategoriaId = 14
                    },
                    new Despesa
                    {
                        Id = 12,
                        Data = new DateTime(2023, 2, 2),
                        Descricao = "Compra de roupas",
                        Valor = 100.00m,
                        DataVencimento = new DateTime(2023, 2, 15),
                        UsuarioId = 2,
                        CategoriaId = 15
                    },
                    new Despesa
                    {
                        Id = 13,
                        Data = new DateTime(2023, 2, 3),
                        Descricao = "Serviço de manutenção",
                        Valor = 150.00m,
                        DataVencimento = new DateTime(2023, 2, 20),
                        UsuarioId = 2,
                        CategoriaId = 16
                    },
                    new Despesa
                    {
                        Id = 14,
                        Data = new DateTime(2023, 2, 4),
                        Descricao = "Consulta Médica",
                        Valor = 250.00m,
                        DataVencimento = new DateTime(2023, 2, 25),
                        UsuarioId = 2,
                        CategoriaId = 17
                    },
                    new Despesa
                    {
                        Id = 15,
                        Data = new DateTime(2023, 2, 5),
                        Descricao = "Imposto de Renda",
                        Valor = 350.00m,
                        DataVencimento = new DateTime(2023, 2, 5),
                        UsuarioId = 2,
                        CategoriaId = 18
                    },
                    new Despesa
                    {
                        Id = 16,
                        Data = new DateTime(2023, 2, 6),
                        Descricao = "Passagem de Ônibus",
                        Valor = 15.00m,
                        DataVencimento = new DateTime(2023, 2, 10),
                        UsuarioId = 2,
                        CategoriaId = 19
                    },
                    new Despesa
                    {
                        Id = 17,
                        Data = new DateTime(2023, 2, 7),
                        Descricao = "Cinema",
                        Valor = 25.50m,
                        DataVencimento = new DateTime(2023, 2, 15),
                        UsuarioId = 2,
                        CategoriaId = 20
                    },
                    new Despesa
                    {
                        Id = 18,
                        Data = new DateTime(2023, 2, 8),
                        Descricao = "Outros gastos",
                        Valor = 20.20m,
                        DataVencimento = new DateTime(2023, 2, 20),
                        UsuarioId = 2,
                        CategoriaId = 21
                    },
                    new Despesa
                    {
                        Id = 19,
                        Data = new DateTime(2023, 2, 9),
                        Descricao = "Salário",
                        Valor = 1000.00m,
                        DataVencimento = new DateTime(2023, 2, 25),
                        UsuarioId = 2,
                        CategoriaId = 22
                    },
                    new Despesa
                    {
                        Id = 20,
                        Data = new DateTime(2023, 2, 10),
                        Descricao = "Prêmio",
                        Valor = 500.00m,
                        DataVencimento = new DateTime(2023, 2, 5),
                        UsuarioId = 2,
                        CategoriaId = 23
                    },
                    new Despesa
                    {
                        Id = 21,
                        Data = new DateTime(2023, 3, 01),
                        Descricao = "Conta de Água",
                        Valor = 100.00m,
                        DataVencimento = new DateTime(2023, 3, 10),
                        UsuarioId = 2,
                        CategoriaId = 14
                    },
                    new Despesa
                    {
                        Id = 22,
                        Data = new DateTime(2023, 3, 02),
                        Descricao = "Compra de Eletrônicos",
                        Valor = 500.00m,
                        DataVencimento = new DateTime(2023, 3, 15),
                        UsuarioId = 2,
                        CategoriaId = 15
                    },
                    new Despesa
                    {
                        Id = 23,
                        Data = new DateTime(2023, 3, 03),
                        Descricao = "Serviço de Jardinagem",
                        Valor = 120.00m,
                        DataVencimento = new DateTime(2023, 3, 20),
                        UsuarioId = 2,
                        CategoriaId = 16
                    },
                    new Despesa
                    {
                        Id = 24,
                        Data = new DateTime(2023, 3, 04),
                        Descricao = "Consulta Médica",
                        Valor = 150.00m,
                        DataVencimento = new DateTime(2023, 3, 25),
                        UsuarioId = 2,
                        CategoriaId = 17
                    },
                    new Despesa
                    {
                        Id = 25,
                        Data = new DateTime(2023, 3, 05),
                        Descricao = "Imposto de Renda",
                        Valor = 400.00m,
                        DataVencimento = new DateTime(2023, 3, 05),
                        UsuarioId = 2,
                        CategoriaId = 18
                    },
                    new Despesa
                    {
                        Id = 26,
                        Data = new DateTime(2023, 3, 06),
                        Descricao = "Passagem de Trem",
                        Valor = 25.00m,
                        DataVencimento = new DateTime(2023, 3, 10),
                        UsuarioId = 2,
                        CategoriaId = 19
                    },
                    new Despesa
                    {
                        Id = 27,
                        Data = new DateTime(2023, 3, 07),
                        Descricao = "Teatro",
                        Valor = 30.00m,
                        DataVencimento = new DateTime(2023, 3, 15),
                        UsuarioId = 2,
                        CategoriaId = 20
                    },
                    new Despesa
                    {
                        Id = 28,
                        Data = new DateTime(2023, 3, 08),
                        Descricao = "Outros gastos",
                        Valor = 18.20m,
                        DataVencimento = new DateTime(2023, 3, 20),
                        UsuarioId = 2,
                        CategoriaId = 21
                    },
                    new Despesa
                    {
                        Id = 29,
                        Data = new DateTime(2023, 3, 09),
                        Descricao = "Salário",
                        Valor = 1000.00m,
                        DataVencimento = new DateTime(2023, 3, 25),
                        UsuarioId = 2,
                        CategoriaId = 22
                    },
                    new Despesa
                    {
                        Id = 30,
                        Data = new DateTime(2023, 3, 10),
                        Descricao = "Prêmio",
                        Valor = 500.00m,
                        DataVencimento = new DateTime(2023, 3, 05),
                        UsuarioId = 2,
                        CategoriaId = 23
                    },
                    new Despesa
                    {
                        Id = 31,
                        Data = new DateTime(2023, 4, 01),
                        Descricao = "Conta de Telefone",
                        Valor = 80.00m,
                        DataVencimento = new DateTime(2023, 4, 10),
                        UsuarioId = 2,
                        CategoriaId = 14
                    },
                    new Despesa
                    {
                        Id = 32,
                        Data = new DateTime(2023, 4, 02),
                        Descricao = "Compra de Móveis",
                        Valor = 700.00m,
                        DataVencimento = new DateTime(2023, 4, 15),
                        UsuarioId = 2,
                        CategoriaId = 15
                    },
                    new Despesa
                    {
                        Id = 33,
                        Data = new DateTime(2023, 4, 03),
                        Descricao = "Serviço de Encanamento",
                        Valor = 90.00m,
                        DataVencimento = new DateTime(2023, 4, 20),
                        UsuarioId = 2,
                        CategoriaId = 16
                    },
                    new Despesa
                    {
                        Id = 34,
                        Data = new DateTime(2023, 4, 04),
                        Descricao = "Consulta Médica",
                        Valor = 180.00m,
                        DataVencimento = new DateTime(2023, 4, 25),
                        UsuarioId = 2,
                        CategoriaId = 17
                    },
                    new Despesa
                    {
                        Id = 35,
                        Data = new DateTime(2023, 4, 05),
                        Descricao = "Imposto de Renda",
                        Valor = 200.00m,
                        DataVencimento = new DateTime(2023, 4, 05),
                        UsuarioId = 2,
                        CategoriaId = 18
                    },
                    new Despesa
                    {
                        Id = 36,
                        Data = new DateTime(2023, 4, 06),
                        Descricao = "Passagem de Metrô",
                        Valor = 5.00m,
                        DataVencimento = new DateTime(2023, 4, 10),
                        UsuarioId = 2,
                        CategoriaId = 19
                    },
                    new Despesa
                    {
                        Id = 37,
                        Data = new DateTime(2023, 4, 07),
                        Descricao = "Shows",
                        Valor = 50.00m,
                        DataVencimento = new DateTime(2023, 4, 15),
                        UsuarioId = 2,
                        CategoriaId = 20
                    },
                    new Despesa
                    {
                        Id = 38,
                        Data = new DateTime(2023, 4, 08),
                        Descricao = "Outros gastos",
                        Valor = 22.50m,
                        DataVencimento = new DateTime(2023, 4, 20),
                        UsuarioId = 2,
                        CategoriaId = 21
                    },
                    new Despesa
                    {
                        Id = 39,
                        Data = new DateTime(2023, 4, 09),
                        Descricao = "Salário",
                        Valor = 1000.00m,
                        DataVencimento = new DateTime(2023, 4, 25),
                        UsuarioId = 2,
                        CategoriaId = 22
                    },
                    new Despesa
                    {
                        Id = 40,
                        Data = new DateTime(2023, 4, 10),
                        Descricao = "Prêmio",
                        Valor = 500.00m,
                        DataVencimento = new DateTime(2023, 4, 05),
                        UsuarioId = 2,
                        CategoriaId = 23
                    },
                    new Despesa
                    {
                        Id = 41,
                        Data = new DateTime(2023, 5, 01),
                        Descricao = "Conta de Internet",
                        Valor = 120.00m,
                        DataVencimento = new DateTime(2023, 5, 10),
                        UsuarioId = 2,
                        CategoriaId = 14
                    },
                    new Despesa
                    {
                        Id = 42,
                        Data = new DateTime(2023, 5, 02),
                        Descricao = "Compra de Eletrodomésticos",
                        Valor = 800.00m,
                        DataVencimento = new DateTime(2023, 5, 15),
                        UsuarioId = 2,
                        CategoriaId = 15
                    },
                    new Despesa
                    {
                        Id = 43,
                        Data = new DateTime(2023, 5, 03),
                        Descricao = "Serviço de Pintura",
                        Valor = 200.00m,
                        DataVencimento = new DateTime(2023, 5, 20),
                        UsuarioId = 2,
                        CategoriaId = 16
                    },
                    new Despesa
                    {
                        Id = 44,
                        Data = new DateTime(2023, 5, 04),
                        Descricao = "Consulta Médica",
                        Valor = 150.00m,
                        DataVencimento = new DateTime(2023, 5, 25),
                        UsuarioId = 2,
                        CategoriaId = 17
                    },
                    new Despesa
                    {
                        Id = 45,
                        Data = new DateTime(2023, 5, 05),
                        Descricao = "Imposto de Renda",
                        Valor = 400.00m,
                        DataVencimento = new DateTime(2023, 5, 05),
                        UsuarioId = 2,
                        CategoriaId = 18
                    },
                    new Despesa
                    {
                        Id = 46,
                        Data = new DateTime(2023, 5, 06),
                        Descricao = "Passagem de Barco",
                        Valor = 30.00m,
                        DataVencimento = new DateTime(2023, 5, 10),
                        UsuarioId = 2,
                        CategoriaId = 19
                    },
                    new Despesa
                    {
                        Id = 47,
                        Data = new DateTime(2023, 5, 07),
                        Descricao = "Exposição de Arte",
                        Valor = 40.00m,
                        DataVencimento = new DateTime(2023, 5, 15),
                        UsuarioId = 2,
                        CategoriaId = 20
                    },
                    new Despesa
                    {
                        Id = 48,
                        Data = new DateTime(2023, 5, 08),
                        Descricao = "Outros gastos",
                        Valor = 25.80m,
                        DataVencimento = new DateTime(2023, 5, 20),
                        UsuarioId = 2,
                        CategoriaId = 21
                    },
                    new Despesa
                    {
                        Id = 49,
                        Data = new DateTime(2023, 5, 09),
                        Descricao = "Salário",
                        Valor = 1000.00m,
                        DataVencimento = new DateTime(2023, 5, 25),
                        UsuarioId = 2,
                        CategoriaId = 22
                    },
                    new Despesa
                    {
                        Id = 50,
                        Data = new DateTime(2023, 5, 10),
                        Descricao = "Prêmio",
                        Valor = 500.00m,
                        DataVencimento = new DateTime(2023, 5, 05),
                        UsuarioId = 2,
                        CategoriaId = 23
                    },
                    new Despesa
                    {
                        Id = 51,
                        Data = new DateTime(2023, 6, 01),
                        Descricao = "Conta de Gás",
                        Valor = 90.00m,
                        DataVencimento = new DateTime(2023, 6, 10),
                        UsuarioId = 2,
                        CategoriaId = 14
                    },
                    new Despesa
                    {
                        Id = 52,
                        Data = new DateTime(2023, 6, 02),
                        Descricao = "Compra de Livros",
                        Valor = 60.00m,
                        DataVencimento = new DateTime(2023, 6, 15),
                        UsuarioId = 2,
                        CategoriaId = 15
                    },
                    new Despesa
                    {
                        Id = 53,
                        Data = new DateTime(2023, 6, 03),
                        Descricao = "Serviço de Manicure",
                        Valor = 40.00m,
                        DataVencimento = new DateTime(2023, 6, 20),
                        UsuarioId = 2,
                        CategoriaId = 16
                    },
                    new Despesa
                    {
                        Id = 54,
                        Data = new DateTime(2023, 6, 04),
                        Descricao = "Consulta Médica",
                        Valor = 120.00m,
                        DataVencimento = new DateTime(2023, 6, 25),
                        UsuarioId = 2,
                        CategoriaId = 17
                    },
                    new Despesa
                    {
                        Id = 55,
                        Data = new DateTime(2023, 6, 05),
                        Descricao = "Imposto de Renda",
                        Valor = 300.00m,
                        DataVencimento = new DateTime(2023, 6, 05),
                        UsuarioId = 2,
                        CategoriaId = 18
                    },
                    new Despesa
                    {
                        Id = 56,
                        Data = new DateTime(2023, 6, 06),
                        Descricao = "Passagem de Avião",
                        Valor = 200.00m,
                        DataVencimento = new DateTime(2023, 6, 10),
                        UsuarioId = 2,
                        CategoriaId = 19
                    },
                    new Despesa
                    {
                        Id = 57,
                        Data = new DateTime(2023, 6, 07),
                        Descricao = "Shows",
                        Valor = 70.00m,
                        DataVencimento = new DateTime(2023, 6, 15),
                        UsuarioId = 2,
                        CategoriaId = 20
                    },
                    new Despesa
                    {
                        Id = 58,
                        Data = new DateTime(2023, 6, 08),
                        Descricao = "Outros gastos",
                        Valor = 35.50m,
                        DataVencimento = new DateTime(2023, 6, 20),
                        UsuarioId = 2,
                        CategoriaId = 21
                    },
                    new Despesa
                    {
                        Id = 59,
                        Data = new DateTime(2023, 6, 09),
                        Descricao = "Salário",
                        Valor = 1000.00m,
                        DataVencimento = new DateTime(2023, 6, 25),
                        UsuarioId = 2,
                        CategoriaId = 22
                    },
                    new Despesa
                    {
                        Id = 60,
                        Data = new DateTime(2023, 6, 10),
                        Descricao = "Prêmio",
                        Valor = 500.00m,
                        DataVencimento = new DateTime(2023, 6, 05),
                        UsuarioId = 2,
                        CategoriaId = 23
                    },
                    new Despesa
                    {
                        Id = 61,
                        Data = new DateTime(2023, 7, 01),
                        Descricao = "Conta de Energia",
                        Valor = 150.00m,
                        DataVencimento = new DateTime(2023, 7, 10),
                        UsuarioId = 2,
                        CategoriaId = 14
                    },
                    new Despesa
                    {
                        Id = 62,
                        Data = new DateTime(2023, 7, 02),
                        Descricao = "Compra de Roupas",
                        Valor = 200.00m,
                        DataVencimento = new DateTime(2023, 7, 15),
                        UsuarioId = 2,
                        CategoriaId = 15
                    },
                    new Despesa
                    {
                        Id = 63,
                        Data = new DateTime(2023, 7, 03),
                        Descricao = "Serviço de Limpeza",
                        Valor = 80.00m,
                        DataVencimento = new DateTime(2023, 7, 20),
                        UsuarioId = 2,
                        CategoriaId = 16
                    },
                    new Despesa
                    {
                        Id = 64,
                        Data = new DateTime(2023, 7, 04),
                        Descricao = "Consulta Médica",
                        Valor = 100.00m,
                        DataVencimento = new DateTime(2023, 7, 25),
                        UsuarioId = 2,
                        CategoriaId = 17
                    },
                    new Despesa
                    {
                        Id = 65,
                        Data = new DateTime(2023, 7, 05),
                        Descricao = "Imposto de Renda",
                        Valor = 250.00m,
                        DataVencimento = new DateTime(2023, 7, 05),
                        UsuarioId = 2,
                        CategoriaId = 18
                    },
                    new Despesa
                    {
                        Id = 66,
                        Data = new DateTime(2023, 7, 06),
                        Descricao = "Passagem de Ônibus",
                        Valor = 10.00m,
                        DataVencimento = new DateTime(2023, 7, 10),
                        UsuarioId = 2,
                        CategoriaId = 19
                    },
                    new Despesa
                    {
                        Id = 67,
                        Data = new DateTime(2023, 7, 07),
                        Descricao = "Cinema",
                        Valor = 25.50m,
                        DataVencimento = new DateTime(2023, 7, 15),
                        UsuarioId = 2,
                        CategoriaId = 20
                    },
                    new Despesa
                    {
                        Id = 68,
                        Data = new DateTime(2023, 7, 08),
                        Descricao = "Outros gastos",
                        Valor = 20.20m,
                        DataVencimento = new DateTime(2023, 7, 20),
                        UsuarioId = 2,
                        CategoriaId = 21
                    },
                    new Despesa
                    {
                        Id = 69,
                        Data = new DateTime(2023, 7, 09),
                        Descricao = "Salário",
                        Valor = 1000.00m,
                        DataVencimento = new DateTime(2023, 7, 25),
                        UsuarioId = 2,
                        CategoriaId = 22
                    },
                    new Despesa
                    {
                        Id = 70,
                        Data = new DateTime(2023, 7, 10),
                        Descricao = "Prêmio",
                        Valor = 500.00m,
                        DataVencimento = new DateTime(2023, 7, 05),
                        UsuarioId = 2,
                        CategoriaId = 23
                    },
                    new Despesa
                    {
                        Id = 71,
                        Data = new DateTime(2023, 8, 01),
                        Descricao = "Conta de Água",
                        Valor = 100.00m,
                        DataVencimento = new DateTime(2023, 8, 10),
                        UsuarioId = 2,
                        CategoriaId = 14
                    },
                    new Despesa
                    {
                        Id = 72,
                        Data = new DateTime(2023, 8, 02),
                        Descricao = "Compra de Eletrônicos",
                        Valor = 500.00m,
                        DataVencimento = new DateTime(2023, 8, 15),
                        UsuarioId = 2,
                        CategoriaId = 15
                    },
                    new Despesa
                    {
                        Id = 73,
                        Data = new DateTime(2023, 8, 03),
                        Descricao = "Serviço de Jardinagem",
                        Valor = 120.00m,
                        DataVencimento = new DateTime(2023, 8, 20),
                        UsuarioId = 2,
                        CategoriaId = 16
                    },
                    new Despesa
                    {
                        Id = 74,
                        Data = new DateTime(2023, 8, 04),
                        Descricao = "Consulta Médica",
                        Valor = 80.00m,
                        DataVencimento = new DateTime(2023, 8, 25),
                        UsuarioId = 2,
                        CategoriaId = 17
                    },
                    new Despesa
                    {
                        Id = 75,
                        Data = new DateTime(2023, 8, 05),
                        Descricao = "Imposto de Renda",
                        Valor = 350.00m,
                        DataVencimento = new DateTime(2023, 8, 05),
                        UsuarioId = 2,
                        CategoriaId = 18
                    },
                    new Despesa
                    {
                        Id = 76,
                        Data = new DateTime(2023, 8, 06),
                        Descricao = "Passagem de Trem",
                        Valor = 15.00m,
                        DataVencimento = new DateTime(2023, 8, 10),
                        UsuarioId = 2,
                        CategoriaId = 19
                    },
                    new Despesa
                    {
                        Id = 77,
                        Data = new DateTime(2023, 8, 07),
                        Descricao = "Shows",
                        Valor = 70.00m,
                        DataVencimento = new DateTime(2023, 8, 15),
                        UsuarioId = 2,
                        CategoriaId = 20
                    },
                    new Despesa
                    {
                        Id = 78,
                        Data = new DateTime(2023, 8, 08),
                        Descricao = "Outros gastos",
                        Valor = 30.00m,
                        DataVencimento = new DateTime(2023, 8, 20),
                        UsuarioId = 2,
                        CategoriaId = 21
                    },
                    new Despesa
                    {
                        Id = 79,
                        Data = new DateTime(2023, 9, 01),
                        Descricao = "Conta de Telefone",
                        Valor = 50.00m,
                        DataVencimento = new DateTime(2023, 9, 10),
                        UsuarioId = 2,
                        CategoriaId = 24
                    },
                    new Despesa
                    {
                        Id = 80,
                        Data = new DateTime(2023, 9, 02),
                        Descricao = "Aluguel",
                        Valor = 800.00m,
                        DataVencimento = new DateTime(2023, 9, 15),
                        UsuarioId = 2,
                        CategoriaId = 25
                    },
                    new Despesa
                    {
                        Id = 81,
                        Data = new DateTime(2023, 9, 03),
                        Descricao = "Manutenção do Carro",
                        Valor = 120.00m,
                        DataVencimento = new DateTime(2023, 9, 20),
                        UsuarioId = 2,
                        CategoriaId = 26
                    },
                    new Despesa
                    {
                        Id = 82,
                        Data = new DateTime(2023, 9, 04),
                        Descricao = "Material de Escritório",
                        Valor = 30.00m,
                        DataVencimento = new DateTime(2023, 9, 25),
                        UsuarioId = 2,
                        CategoriaId = 27
                    },
                    new Despesa
                    {
                        Id = 83,
                        Data = new DateTime(2023, 9, 05),
                        Descricao = "Seguro de Vida",
                        Valor = 40.00m,
                        DataVencimento = new DateTime(2023, 9, 05),
                        UsuarioId = 2,
                        CategoriaId = 28
                    },
                    new Despesa
                    {
                        Id = 84,
                        Data = new DateTime(2023, 9, 06),
                        Descricao = "Assinatura de Jornal",
                        Valor = 15.00m,
                        DataVencimento = new DateTime(2023, 9, 10),
                        UsuarioId = 2,
                        CategoriaId = 29
                    },
                    new Despesa
                    {
                        Id = 85,
                        Data = new DateTime(2023, 9, 07),
                        Descricao = "Presente de Aniversário",
                        Valor = 50.00m,
                        DataVencimento = new DateTime(2023, 9, 15),
                        UsuarioId = 2,
                        CategoriaId = 30
                    },
                    new Despesa
                    {
                        Id = 86,
                        Data = new DateTime(2023, 9, 08),
                        Descricao = "Outros gastos",
                        Valor = 25.00m,
                        DataVencimento = new DateTime(2023, 9, 20),
                        UsuarioId = 2,
                        CategoriaId = 21
                    },
                    new Despesa
                    {
                        Id = 87,
                        Data = new DateTime(2023, 9, 09),
                        Descricao = "Salário",
                        Valor = 1000.00m,
                        DataVencimento = new DateTime(2023, 9, 25),
                        UsuarioId = 2,
                        CategoriaId = 22
                    },
                    new Despesa
                    {
                        Id = 88,
                        Data = new DateTime(2023, 10, 01),
                        Descricao = "Conta de Internet",
                        Valor = 60.00m,
                        DataVencimento = new DateTime(2023, 10, 10),
                        UsuarioId = 2,
                        CategoriaId = 24
                    },
                    new Despesa
                    {
                        Id = 89,
                        Data = new DateTime(2023, 10, 02),
                        Descricao = "Aluguel",
                        Valor = 800.00m,
                        DataVencimento = new DateTime(2023, 10, 15),
                        UsuarioId = 2,
                        CategoriaId = 25
                    },
                    new Despesa
                    {
                        Id = 90,
                        Data = new DateTime(2023, 10, 03),
                        Descricao = "Manutenção do Carro",
                        Valor = 100.00m,
                        DataVencimento = new DateTime(2023, 10, 20),
                        UsuarioId = 2,
                        CategoriaId = 26
                    },
                    new Despesa
                    {
                        Id = 91,
                        Data = new DateTime(2023, 10, 04),
                        Descricao = "Material de Escritório",
                        Valor = 40.00m,
                        DataVencimento = new DateTime(2023, 10, 25),
                        UsuarioId = 2,
                        CategoriaId = 27
                    },
                    new Despesa
                    {
                        Id = 92,
                        Data = new DateTime(2023, 10, 05),
                        Descricao = "Seguro de Vida",
                        Valor = 40.00m,
                        DataVencimento = new DateTime(2023, 10, 05),
                        UsuarioId = 2,
                        CategoriaId = 28
                    },
                    new Despesa
                    {
                        Id = 93,
                        Data = new DateTime(2023, 10, 06),
                        Descricao = "Assinatura de Revista",
                        Valor = 20.00m,
                        DataVencimento = new DateTime(2023, 10, 10),
                        UsuarioId = 2,
                        CategoriaId = 29
                    },
                    new Despesa
                    {
                        Id = 94,
                        Data = new DateTime(2023, 10, 07),
                        Descricao = "Presente de Casamento",
                        Valor = 70.00m,
                        DataVencimento = new DateTime(2023, 10, 15),
                        UsuarioId = 2,
                        CategoriaId = 30
                    },
                    new Despesa
                    {
                        Id = 95,
                        Data = new DateTime(2023, 10, 08),
                        Descricao = "Outros gastos",
                        Valor = 35.00m,
                        DataVencimento = new DateTime(2023, 10, 20),
                        UsuarioId = 2,
                        CategoriaId = 21
                    },
                    new Despesa
                    {
                        Id = 96,
                        Data = new DateTime(2023, 10, 09),
                        Descricao = "Salário",
                        Valor = 1100.00m,
                        DataVencimento = new DateTime(2023, 10, 25),
                        UsuarioId = 2,
                        CategoriaId = 22
                    },
                    new Despesa
                    {
                        Id = 97,
                        Data = new DateTime(2023, 11, 01),
                        Descricao = "Conta de Água",
                        Valor = 40.00m,
                        DataVencimento = new DateTime(2023, 11, 10),
                        UsuarioId = 2,
                        CategoriaId = 24
                    },
                    new Despesa
                    {
                        Id = 98,
                        Data = new DateTime(2023, 11, 02),
                        Descricao = "Aluguel",
                        Valor = 800.00m,
                        DataVencimento = new DateTime(2023, 11, 15),
                        UsuarioId = 2,
                        CategoriaId = 25
                    },
                    new Despesa
                    {
                        Id = 99,
                        Data = new DateTime(2023, 11, 03),
                        Descricao = "Manutenção do Carro",
                        Valor = 80.00m,
                        DataVencimento = new DateTime(2023, 11, 20),
                        UsuarioId = 2,
                        CategoriaId = 26
                    },
                    new Despesa
                    {
                        Id = 100,
                        Data = new DateTime(2023, 11, 04),
                        Descricao = "Material de Escritório",
                        Valor = 35.00m,
                        DataVencimento = new DateTime(2023, 11, 25),
                        UsuarioId = 2,
                        CategoriaId = 27
                    },
                    new Despesa
                    {
                        Id = 101,
                        Data = new DateTime(2023, 11, 05),
                        Descricao = "Seguro de Vida",
                        Valor = 40.00m,
                        DataVencimento = new DateTime(2023, 11, 05),
                        UsuarioId = 2,
                        CategoriaId = 28
                    },
                    new Despesa
                    {
                        Id = 102,
                        Data = new DateTime(2023, 11, 06),
                        Descricao = "Assinatura de Revista",
                        Valor = 20.00m,
                        DataVencimento = new DateTime(2023, 11, 10),
                        UsuarioId = 2,
                        CategoriaId = 29
                    },
                    new Despesa
                    {
                        Id = 103,
                        Data = new DateTime(2023, 11, 07),
                        Descricao = "Presente de Casamento",
                        Valor = 70.00m,
                        DataVencimento = new DateTime(2023, 11, 15),
                        UsuarioId = 2,
                        CategoriaId = 30
                    },
                    new Despesa
                    {
                        Id = 104,
                        Data = new DateTime(2023, 11, 08),
                        Descricao = "Outros gastos",
                        Valor = 35.00m,
                        DataVencimento = new DateTime(2023, 11, 20),
                        UsuarioId = 2,
                        CategoriaId = 21
                    },
                    new Despesa
                    {
                        Id = 105,
                        Data = new DateTime(2023, 11, 09),
                        Descricao = "Salário",
                        Valor = 1100.00m,
                        DataVencimento = new DateTime(2023, 11, 25),
                        UsuarioId = 2,
                        CategoriaId = 22
                    },
                    new Despesa
                    {
                        Id = 106,
                        Data = new DateTime(2023, 12, 01),
                        Descricao = "Conta de Água",
                        Valor = 40.00m,
                        DataVencimento = new DateTime(2023, 12, 10),
                        UsuarioId = 2,
                        CategoriaId = 24
                    },
                    new Despesa
                    {
                        Id = 107,
                        Data = new DateTime(2023, 12, 02),
                        Descricao = "Aluguel",
                        Valor = 800.00m,
                        DataVencimento = new DateTime(2023, 12, 15),
                        UsuarioId = 2,
                        CategoriaId = 25
                    },
                    new Despesa
                    {
                        Id = 108,
                        Data = new DateTime(2023, 12, 03),
                        Descricao = "Manutenção do Carro",
                        Valor = 80.00m,
                        DataVencimento = new DateTime(2023, 12, 20),
                        UsuarioId = 2,
                        CategoriaId = 26
                    },
                    new Despesa
                    {
                        Id = 109,
                        Data = new DateTime(2023, 12, 04),
                        Descricao = "Material de Escritório",
                        Valor = 30.00m,
                        DataVencimento = new DateTime(2023, 12, 25),
                        UsuarioId = 2,
                        CategoriaId = 27
                    },
                    new Despesa
                    {
                        Id = 110,
                        Data = new DateTime(2023, 12, 05),
                        Descricao = "Seguro de Vida",
                        Valor = 40.00m,
                        DataVencimento = new DateTime(2023, 12, 05),
                        UsuarioId = 2,
                        CategoriaId = 28
                    },
                    new Despesa
                    {
                        Id = 111,
                        Data = new DateTime(2023, 12, 06),
                        Descricao = "Assinatura de Revista",
                        Valor = 20.00m,
                        DataVencimento = new DateTime(2023, 12, 10),
                        UsuarioId = 2,
                        CategoriaId = 29
                    },
                    new Despesa
                    {
                        Id = 112,
                        Data = new DateTime(2023, 12, 07),
                        Descricao = "Presente de Casamento",
                        Valor = 70.00m,
                        DataVencimento = new DateTime(2023, 12, 15),
                        UsuarioId = 2,
                        CategoriaId = 30
                    },
                    new Despesa
                    {
                        Id = 113,
                        Data = new DateTime(2023, 12, 08),
                        Descricao = "Outros gastos",
                        Valor = 35.00m,
                        DataVencimento = new DateTime(2023, 12, 20),
                        UsuarioId = 2,
                        CategoriaId = 21
                    },
                    new Despesa
                    {
                        Id = 114,
                        Data = new DateTime(2023, 12, 09),
                        Descricao = "Salário",
                        Valor = 1100.00m,
                        DataVencimento = new DateTime(2023, 12, 25),
                        UsuarioId = 2,
                        CategoriaId = 22
                    },
                    new Despesa
                    {
                        Id = 115,
                        Data = new DateTime(2024, 01, 01),
                        Descricao = "Conta de Água",
                        Valor = 40.00m,
                        DataVencimento = new DateTime(2024, 01, 10),
                        UsuarioId = 2,
                        CategoriaId = 24
                    },
                    new Despesa
                    {
                        Id = 116,
                        Data = new DateTime(2024, 01, 02),
                        Descricao = "Aluguel",
                        Valor = 800.00m,
                        DataVencimento = new DateTime(2024, 01, 15),
                        UsuarioId = 2,
                        CategoriaId = 25
                    },
                    new Despesa
                    {
                        Id = 117,
                        Data = new DateTime(2024, 01, 03),
                        Descricao = "Manutenção do Carro",
                        Valor = 80.00m,
                        DataVencimento = new DateTime(2024, 01, 20),
                        UsuarioId = 2,
                        CategoriaId = 26
                    },
                    new Despesa
                    {
                        Id = 118,
                        Data = new DateTime(2024, 01, 04),
                        Descricao = "Material de Escritório",
                        Valor = 30.00m,
                        DataVencimento = new DateTime(2024, 01, 25),
                        UsuarioId = 2,
                        CategoriaId = 27
                    },
                    new Despesa
                    {
                        Id = 119,
                        Data = new DateTime(2024, 01, 05),
                        Descricao = "Seguro de Vida",
                        Valor = 40.00m,
                        DataVencimento = new DateTime(2024, 01, 05),
                        UsuarioId = 2,
                        CategoriaId = 28
                    },
                    new Despesa
                    {
                        Id = 120,
                        Data = new DateTime(2024, 01, 06),
                        Descricao = "Assinatura de Revista",
                        Valor = 20.00m,
                        DataVencimento = new DateTime(2024, 01, 10),
                        UsuarioId = 2,
                        CategoriaId = 29
                    },
                    new Despesa
                    {
                        Id = 121,
                        Data = new DateTime(2024, 01, 07),
                        Descricao = "Presente de Casamento",
                        Valor = 70.00m,
                        DataVencimento = new DateTime(2024, 01, 15),
                        UsuarioId = 2,
                        CategoriaId = 30
                    },
                    new Despesa
                    {
                        Id = 122,
                        Data = new DateTime(2024, 01, 08),
                        Descricao = "Outros gastos",
                        Valor = 35.00m,
                        DataVencimento = new DateTime(2024, 01, 20),
                        UsuarioId = 2,
                        CategoriaId = 21
                    }
                };
                _context.Despesa.AddRange(despesas);
                _context.SaveChanges();
            }
        }
    }
}