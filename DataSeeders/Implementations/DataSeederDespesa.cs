using Repository;
using Domain.Entities;

namespace DataSeeders.Implementations;
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
            var id = 1;
            var despesas = new List<Despesa>
            {
                new Despesa
                {
                    Id = id++,
                    Data = new DateTime(2023, 1, 1),
                    Descricao = "Conta de Luz",
                    Valor = 150.95m,
                    DataVencimento = new DateTime(2023, 1, 10),
                    UsuarioId = 2,
                    CategoriaId = 15
                },
                new Despesa
                {
                    Id = id++,
                    Data = new DateTime(2023, 1, 2),
                    Descricao = "Compra de mantimentos",
                    Valor = 50.95m,
                    DataVencimento = new DateTime(2023, 1, 15),
                    UsuarioId = 2,
                    CategoriaId = 15
                },
                new Despesa
                {
                    Id = id++,
                    Data = new DateTime(2023, 1, 3),
                    Descricao = "Serviço de Limpeza",
                    Valor = 80.95m,
                    DataVencimento = new DateTime(2023, 1, 20),
                    UsuarioId = 2,
                    CategoriaId = 16
                },
                new Despesa
                {
                    Id = id++,
                    Data = new DateTime(2023, 1, 4),
                    Descricao = "Consulta Médica",
                    Valor = 200.95m,
                    DataVencimento = new DateTime(2023, 1, 25),
                    UsuarioId = 2,
                    CategoriaId = 17
                },
                new Despesa
                {
                    Id = id++,
                    Data = new DateTime(2023, 1, 5),
                    Descricao = "Imposto de Renda",
                    Valor = 300.95m,
                    DataVencimento = new DateTime(2023, 1, 5),
                    UsuarioId = 2,
                    CategoriaId = 18
                },
                new Despesa
                {
                    Id = id++,
                    Data = new DateTime(2023, 1, 6),
                    Descricao = "Passagem de Ônibus",
                    Valor = 10.95m,
                    DataVencimento = new DateTime(2023, 1, 10),
                    UsuarioId = 2,
                    CategoriaId = 19
                },
                new Despesa
                {
                    Id = id++,
                    Data = new DateTime(2023, 1, 7),
                    Descricao = "Cinema",
                    Valor = 20.508m,
                    DataVencimento = new DateTime(2023, 1, 15),
                    UsuarioId = 2,
                    CategoriaId = 20
                },
                new Despesa
                {
                    Id = id++,
                    Data = new DateTime(2023, 1, 8),
                    Descricao = "Outros gastos",
                    Valor = 15.20m,
                    DataVencimento = new DateTime(2023, 1, 20),
                    UsuarioId = 2,
                    CategoriaId = 21
                },
                new Despesa
                {
                    Id = id++,
                    Data = new DateTime(2023, 1, 9),
                    Descricao = "Gastos Diversos",
                    Valor = 1000.95m,
                    DataVencimento = new DateTime(2023, 1, 25),
                    UsuarioId = 2,
                    CategoriaId = 21
                },
                new Despesa
                {
                    Id = id++,
                    Data = new DateTime(2023, 1, 10),
                    Descricao = "Gastos Diversos",
                    Valor = 500.95m,
                    DataVencimento = new DateTime(2023, 1, 5),
                    UsuarioId = 2,
                    CategoriaId = 21
                },
                new Despesa
                {
                    Id = id++,
                    Data = new DateTime(2023, 2, 1),
                    Descricao = "Aluguel",
                    Valor = 800.95m,
                    DataVencimento = new DateTime(2023, 2, 10),
                    UsuarioId = 2,
                    CategoriaId = 14
                },
                new Despesa
                {
                    Id = id++,
                    Data = new DateTime(2023, 2, 2),
                    Descricao = "Compra de roupas",
                    Valor = 100.95m,
                    DataVencimento = new DateTime(2023, 2, 15),
                    UsuarioId = 2,
                    CategoriaId = 15
                },
                new Despesa
                {
                    Id = id++,
                    Data = new DateTime(2023, 2, 3),
                    Descricao = "Serviço de manutenção",
                    Valor = 150.95m,
                    DataVencimento = new DateTime(2023, 2, 20),
                    UsuarioId = 2,
                    CategoriaId = 16
                },
                new Despesa
                {
                    Id = id++,
                    Data = new DateTime(2023, 2, 4),
                    Descricao = "Consulta Médica",
                    Valor = 250.95m,
                    DataVencimento = new DateTime(2023, 2, 25),
                    UsuarioId = 2,
                    CategoriaId = 17
                },
                new Despesa
                {
                    Id = id++,
                    Data = new DateTime(2023, 2, 5),
                    Descricao = "Imposto de Renda",
                    Valor = 350.95m,
                    DataVencimento = new DateTime(2023, 2, 5),
                    UsuarioId = 2,
                    CategoriaId = 18
                },
                new Despesa
                {
                    Id = id++,
                    Data = new DateTime(2023, 2, 6),
                    Descricao = "Passagem de Ônibus",
                    Valor = 15.95m,
                    DataVencimento = new DateTime(2023, 2, 10),
                    UsuarioId = 2,
                    CategoriaId = 19
                },
                new Despesa
                {
                    Id = id++,
                    Data = new DateTime(2023, 2, 7),
                    Descricao = "Cinema",
                    Valor = 25.508m,
                    DataVencimento = new DateTime(2023, 2, 15),
                    UsuarioId = 2,
                    CategoriaId = 20
                },
                new Despesa
                {
                    Id = id++,
                    Data = new DateTime(2023, 2, 8),
                    Descricao = "Outros gastos",
                    Valor = 20.20m,
                    DataVencimento = new DateTime(2023, 2, 20),
                    UsuarioId = 2,
                    CategoriaId = 21
                },
                new Despesa
                {
                    Id = id++,
                    Data = new DateTime(2023, 2, 9),
                    Descricao = "Gastos Diversos",
                    Valor = 1000.95m,
                    DataVencimento = new DateTime(2023, 2, 25),
                    UsuarioId = 2,
                    CategoriaId = 21
                },
                new Despesa
                {
                    Id = id++,
                    Data = new DateTime(2023, 2, 10),
                    Descricao = "Gastos Diversos",
                    Valor = 500.95m,
                    DataVencimento = new DateTime(2023, 2, 5),
                    UsuarioId = 2,
                    CategoriaId = 21
                },
                new Despesa
                {
                    Id = id++,
                    Data = new DateTime(2023, 3, 01),
                    Descricao = "Conta de Água",
                    Valor = 100.95m,
                    DataVencimento = new DateTime(2023, 3, 10),
                    UsuarioId = 2,
                    CategoriaId = 15
                },
                new Despesa
                {
                    Id = id++,
                    Data = new DateTime(2023, 3, 02),
                    Descricao = "Compra de Eletrônicos",
                    Valor = 500.95m,
                    DataVencimento = new DateTime(2023, 3, 15),
                    UsuarioId = 2,
                    CategoriaId = 15
                },
                new Despesa
                {
                    Id = id++,
                    Data = new DateTime(2023, 3, 03),
                    Descricao = "Serviço de Jardinagem",
                    Valor = 120.95m,
                    DataVencimento = new DateTime(2023, 3, 20),
                    UsuarioId = 2,
                    CategoriaId = 16
                },
                new Despesa
                {
                    Id = id++,
                    Data = new DateTime(2023, 3, 04),
                    Descricao = "Consulta Médica",
                    Valor = 150.95m,
                    DataVencimento = new DateTime(2023, 3, 25),
                    UsuarioId = 2,
                    CategoriaId = 17
                },
                new Despesa
                {
                    Id = id++,
                    Data = new DateTime(2023, 3, 05),
                    Descricao = "Imposto de Renda",
                    Valor = 400.95m,
                    DataVencimento = new DateTime(2023, 3, 05),
                    UsuarioId = 2,
                    CategoriaId = 18
                },
                new Despesa
                {
                    Id = id++,
                    Data = new DateTime(2023, 3, 06),
                    Descricao = "Passagem de Trem",
                    Valor = 25.95m,
                    DataVencimento = new DateTime(2023, 3, 10),
                    UsuarioId = 2,
                    CategoriaId = 19
                },
                new Despesa
                {
                    Id = id++,
                    Data = new DateTime(2023, 3, 07),
                    Descricao = "Teatro",
                    Valor = 30.95m,
                    DataVencimento = new DateTime(2023, 3, 15),
                    UsuarioId = 2,
                    CategoriaId = 20
                },
                new Despesa
                {
                    Id = id++,
                    Data = new DateTime(2023, 3, 08),
                    Descricao = "Outros gastos",
                    Valor = 18.20m,
                    DataVencimento = new DateTime(2023, 3, 20),
                    UsuarioId = 2,
                    CategoriaId = 21
                },
                new Despesa
                {
                    Id = id++,
                    Data = new DateTime(2023, 3, 09),
                    Descricao = "Gastos Diversos",
                    Valor = 1000.95m,
                    DataVencimento = new DateTime(2023, 3, 25),
                    UsuarioId = 2,
                    CategoriaId = 21
                },
                new Despesa
                {
                    Id = id++,
                    Data = new DateTime(2023, 3, 10),
                    Descricao = "Gastos Diversos",
                    Valor = 500.95m,
                    DataVencimento = new DateTime(2023, 3, 05),
                    UsuarioId = 2,
                    CategoriaId = 21
                },
                new Despesa
                {
                    Id = id++,
                    Data = new DateTime(2023, 4, 01),
                    Descricao = "Refeição Ifood",
                    Valor = 80.95m,
                    DataVencimento = new DateTime(2023, 4, 10),
                    UsuarioId = 2,
                    CategoriaId = 14
                },
                new Despesa
                {
                    Id = id++,
                    Data = new DateTime(2023, 4, 02),
                    Descricao = "Compra de Móveis",
                    Valor = 700.95m,
                    DataVencimento = new DateTime(2023, 4, 15),
                    UsuarioId = 2,
                    CategoriaId = 15
                },
                new Despesa
                {
                    Id = id++,
                    Data = new DateTime(2023, 4, 03),
                    Descricao = "Serviço de Encanamento",
                    Valor = 90.95m,
                    DataVencimento = new DateTime(2023, 4, 20),
                    UsuarioId = 2,
                    CategoriaId = 16
                },
                new Despesa
                {
                    Id = id++,
                    Data = new DateTime(2023, 4, 04),
                    Descricao = "Consulta Médica",
                    Valor = 180.95m,
                    DataVencimento = new DateTime(2023, 4, 25),
                    UsuarioId = 2,
                    CategoriaId = 17
                },
                new Despesa
                {
                    Id = id++,
                    Data = new DateTime(2023, 4, 05),
                    Descricao = "Imposto de Renda",
                    Valor = 200.95m,
                    DataVencimento = new DateTime(2023, 4, 05),
                    UsuarioId = 2,
                    CategoriaId = 18
                },
                new Despesa
                {
                    Id = id++,
                    Data = new DateTime(2023, 4, 06),
                    Descricao = "Passagem de Metrô",
                    Valor = 5.95m,
                    DataVencimento = new DateTime(2023, 4, 10),
                    UsuarioId = 2,
                    CategoriaId = 19
                },
                new Despesa
                {
                    Id = id++,
                    Data = new DateTime(2023, 4, 07),
                    Descricao = "Shows",
                    Valor = 50.95m,
                    DataVencimento = new DateTime(2023, 4, 15),
                    UsuarioId = 2,
                    CategoriaId = 20
                },
                new Despesa
                {
                    Id = id++,
                    Data = new DateTime(2023, 4, 08),
                    Descricao = "Outros gastos",
                    Valor = 22.508m,
                    DataVencimento = new DateTime(2023, 4, 20),
                    UsuarioId = 2,
                    CategoriaId = 21
                },
                new Despesa
                {
                    Id = id++,
                    Data = new DateTime(2023, 4, 09),
                    Descricao = "Gastos Diversos",
                    Valor = 1000.95m,
                    DataVencimento = new DateTime(2023, 4, 25),
                    UsuarioId = 2,
                    CategoriaId = 21
                },
                new Despesa
                {
                    Id = id++,
                    Data = new DateTime(2023, 4, 10),
                    Descricao = "Gastos Diversos",
                    Valor = 500.95m,
                    DataVencimento = new DateTime(2023, 4, 05),
                    UsuarioId = 2,
                    CategoriaId = 21
                },
                new Despesa
                {
                    Id = id++,
                    Data = new DateTime(2023, 5, 01),
                    Descricao = "Almoço Restaurante",
                    Valor = 120.95m,
                    DataVencimento = new DateTime(2023, 5, 10),
                    UsuarioId = 2,
                    CategoriaId = 14
                },
                new Despesa
                {
                    Id = id++,
                    Data = new DateTime(2023, 5, 02),
                    Descricao = "Compra de Eletrodomésticos",
                    Valor = 800.95m,
                    DataVencimento = new DateTime(2023, 5, 15),
                    UsuarioId = 2,
                    CategoriaId = 15
                },
                new Despesa
                {
                    Id = id++,
                    Data = new DateTime(2023, 5, 03),
                    Descricao = "Serviço de Pintura",
                    Valor = 200.95m,
                    DataVencimento = new DateTime(2023, 5, 20),
                    UsuarioId = 2,
                    CategoriaId = 16
                },
                new Despesa
                {
                    Id = id++,
                    Data = new DateTime(2023, 5, 04),
                    Descricao = "Consulta Médica",
                    Valor = 150.95m,
                    DataVencimento = new DateTime(2023, 5, 25),
                    UsuarioId = 2,
                    CategoriaId = 17
                },
                new Despesa
                {
                    Id = id++,
                    Data = new DateTime(2023, 5, 05),
                    Descricao = "Imposto de Renda",
                    Valor = 400.95m,
                    DataVencimento = new DateTime(2023, 5, 05),
                    UsuarioId = 2,
                    CategoriaId = 18
                },
                new Despesa
                {
                    Id = id++,
                    Data = new DateTime(2023, 5, 06),
                    Descricao = "Passagem de Barco",
                    Valor = 30.95m,
                    DataVencimento = new DateTime(2023, 5, 10),
                    UsuarioId = 2,
                    CategoriaId = 19
                },
                new Despesa
                {
                    Id = id++,
                    Data = new DateTime(2023, 5, 07),
                    Descricao = "Exposição de Arte",
                    Valor = 40.95m,
                    DataVencimento = new DateTime(2023, 5, 15),
                    UsuarioId = 2,
                    CategoriaId = 20
                },
                new Despesa
                {
                    Id = id++,
                    Data = new DateTime(2023, 5, 08),
                    Descricao = "Outros gastos",
                    Valor = 25.80m,
                    DataVencimento = new DateTime(2023, 5, 20),
                    UsuarioId = 2,
                    CategoriaId = 21
                },
                new Despesa
                {
                    Id = id++,
                    Data = new DateTime(2023, 5, 09),
                    Descricao = "Gastos Diversos",
                    Valor = 1000.95m,
                    DataVencimento = new DateTime(2023, 5, 25),
                    UsuarioId = 2,
                    CategoriaId = 21
                },
                new Despesa
                {
                    Id = id++,
                    Data = new DateTime(2023, 5, 10),
                    Descricao = "Gastos Diversos",
                    Valor = 500.95m,
                    DataVencimento = new DateTime(2023, 5, 05),
                    UsuarioId = 2,
                    CategoriaId = 21
                },
                new Despesa
                {
                    Id = id++,
                    Data = new DateTime(2023, 6, 01),
                    Descricao = "Pedido Ifood",
                    Valor = 90.95m,
                    DataVencimento = new DateTime(2023, 6, 10),
                    UsuarioId = 2,
                    CategoriaId = 14
                },
                new Despesa
                {
                    Id = id++,
                    Data = new DateTime(2023, 6, 02),
                    Descricao = "Compra de Livros",
                    Valor = 60.95m,
                    DataVencimento = new DateTime(2023, 6, 15),
                    UsuarioId = 2,
                    CategoriaId = 15
                },
                new Despesa
                {
                    Id = id++,
                    Data = new DateTime(2023, 6, 03),
                    Descricao = "Serviço de Manicure",
                    Valor = 40.95m,
                    DataVencimento = new DateTime(2023, 6, 20),
                    UsuarioId = 2,
                    CategoriaId = 16
                },
                new Despesa
                {
                    Id = id++,
                    Data = new DateTime(2023, 6, 04),
                    Descricao = "Consulta Médica",
                    Valor = 120.95m,
                    DataVencimento = new DateTime(2023, 6, 25),
                    UsuarioId = 2,
                    CategoriaId = 17
                },
                new Despesa
                {
                    Id = id++,
                    Data = new DateTime(2023, 6, 05),
                    Descricao = "Imposto de Renda",
                    Valor = 300.95m,
                    DataVencimento = new DateTime(2023, 6, 05),
                    UsuarioId = 2,
                    CategoriaId = 18
                },
                new Despesa
                {
                    Id = id++,
                    Data = new DateTime(2023, 6, 06),
                    Descricao = "Passagem de Avião",
                    Valor = 200.95m,
                    DataVencimento = new DateTime(2023, 6, 10),
                    UsuarioId = 2,
                    CategoriaId = 19
                },
                new Despesa
                {
                    Id = id++,
                    Data = new DateTime(2023, 6, 07),
                    Descricao = "Shows",
                    Valor = 70.95m,
                    DataVencimento = new DateTime(2023, 6, 15),
                    UsuarioId = 2,
                    CategoriaId = 20
                },
                new Despesa
                {
                    Id = id++,
                    Data = new DateTime(2023, 6, 08),
                    Descricao = "Outros gastos",
                    Valor = 35.508m,
                    DataVencimento = new DateTime(2023, 6, 20),
                    UsuarioId = 2,
                    CategoriaId = 21
                },
                new Despesa
                {
                    Id = id++,
                    Data = new DateTime(2023, 6, 09),
                    Descricao = "Gastos Diversos",
                    Valor = 1000.95m,
                    DataVencimento = new DateTime(2023, 6, 25),
                    UsuarioId = 2,
                    CategoriaId = 21
                },
                new Despesa
                {
                    Id = id++,
                    Data = new DateTime(2023, 6, 10),
                    Descricao = "Gastos Diversos",
                    Valor = 500.95m,
                    DataVencimento = new DateTime(2023, 6, 05),
                    UsuarioId = 2,
                    CategoriaId = 21
                },
                new Despesa
                {
                    Id = id++,
                    Data = new DateTime(2023, 7, 01),
                    Descricao = "Refeição Ifood",
                    Valor = 150.95m,
                    DataVencimento = new DateTime(2023, 7, 10),
                    UsuarioId = 2,
                    CategoriaId = 14
                },
                new Despesa
                {
                    Id = id++,
                    Data = new DateTime(2023, 7, 02),
                    Descricao = "Compra de Roupas",
                    Valor = 200.95m,
                    DataVencimento = new DateTime(2023, 7, 15),
                    UsuarioId = 2,
                    CategoriaId = 15
                },
                new Despesa
                {
                    Id = id++,
                    Data = new DateTime(2023, 7, 03),
                    Descricao = "Serviço de Limpeza",
                    Valor = 80.95m,
                    DataVencimento = new DateTime(2023, 7, 20),
                    UsuarioId = 2,
                    CategoriaId = 16
                },
                new Despesa
                {
                    Id = id++,
                    Data = new DateTime(2023, 7, 04),
                    Descricao = "Consulta Médica",
                    Valor = 100.95m,
                    DataVencimento = new DateTime(2023, 7, 25),
                    UsuarioId = 2,
                    CategoriaId = 17
                },
                new Despesa
                {
                    Id = id++,
                    Data = new DateTime(2023, 7, 05),
                    Descricao = "Imposto de Renda",
                    Valor = 250.95m,
                    DataVencimento = new DateTime(2023, 7, 05),
                    UsuarioId = 2,
                    CategoriaId = 18
                },
                new Despesa
                {
                    Id = id++,
                    Data = new DateTime(2023, 7, 06),
                    Descricao = "Passagem de Ônibus",
                    Valor = 10.95m,
                    DataVencimento = new DateTime(2023, 7, 10),
                    UsuarioId = 2,
                    CategoriaId = 19
                },
                new Despesa
                {
                    Id = id++,
                    Data = new DateTime(2023, 7, 07),
                    Descricao = "Cinema",
                    Valor = 25.508m,
                    DataVencimento = new DateTime(2023, 7, 15),
                    UsuarioId = 2,
                    CategoriaId = 20
                },
                new Despesa
                {
                    Id = id++,
                    Data = new DateTime(2023, 7, 08),
                    Descricao = "Outros gastos",
                    Valor = 20.20m,
                    DataVencimento = new DateTime(2023, 7, 20),
                    UsuarioId = 2,
                    CategoriaId = 21
                },
                new Despesa
                {
                    Id = id++,
                    Data = new DateTime(2023, 7, 09),
                    Descricao = "Gastos Diversos",
                    Valor = 1000.95m,
                    DataVencimento = new DateTime(2023, 7, 25),
                    UsuarioId = 2,
                    CategoriaId = 21
                },
                new Despesa
                {
                    Id = id++,
                    Data = new DateTime(2023, 7, 10),
                    Descricao = "Gastos Diversos",
                    Valor = 500.95m,
                    DataVencimento = new DateTime(2023, 7, 05),
                    UsuarioId = 2,
                    CategoriaId = 21
                },
                new Despesa
                {
                    Id = id++,
                    Data = new DateTime(2023, 8, 01),
                    Descricao = "Almoço no Restaurante",
                    Valor = 100.95m,
                    DataVencimento = new DateTime(2023, 8, 10),
                    UsuarioId = 2,
                    CategoriaId = 14
                },
                new Despesa
                {
                    Id = id++,
                    Data = new DateTime(2023, 8, 02),
                    Descricao = "Compra de Eletrônicos",
                    Valor = 500.95m,
                    DataVencimento = new DateTime(2023, 8, 15),
                    UsuarioId = 2,
                    CategoriaId = 15
                },
                new Despesa
                {
                    Id = id++,
                    Data = new DateTime(2023, 8, 03),
                    Descricao = "Serviço de Jardinagem",
                    Valor = 120.95m,
                    DataVencimento = new DateTime(2023, 8, 20),
                    UsuarioId = 2,
                    CategoriaId = 16
                },
                new Despesa
                {
                    Id = id++,
                    Data = new DateTime(2023, 8, 04),
                    Descricao = "Consulta Médica",
                    Valor = 80.95m,
                    DataVencimento = new DateTime(2023, 8, 25),
                    UsuarioId = 2,
                    CategoriaId = 17
                },
                new Despesa
                {
                    Id = id++,
                    Data = new DateTime(2023, 8, 05),
                    Descricao = "Imposto de Renda",
                    Valor = 350.95m,
                    DataVencimento = new DateTime(2023, 8, 05),
                    UsuarioId = 2,
                    CategoriaId = 18
                },
                new Despesa
                {
                    Id = id++,
                    Data = new DateTime(2023, 8, 06),
                    Descricao = "Passagem de Trem",
                    Valor = 15.95m,
                    DataVencimento = new DateTime(2023, 8, 10),
                    UsuarioId = 2,
                    CategoriaId = 19
                },
                new Despesa
                {
                    Id = id++,
                    Data = new DateTime(2023, 8, 07),
                    Descricao = "Shows",
                    Valor = 70.95m,
                    DataVencimento = new DateTime(2023, 8, 15),
                    UsuarioId = 2,
                    CategoriaId = 20
                },
                new Despesa
                {
                    Id = id++,
                    Data = new DateTime(2023, 8, 08),
                    Descricao = "Outros gastos",
                    Valor = 30.95m,
                    DataVencimento = new DateTime(2023, 8, 20),
                    UsuarioId = 2,
                    CategoriaId = 21
                },
                new Despesa
                {
                    Id = id++,
                    Data = new DateTime(2023, 9, 01),
                    Descricao = "Conta de Telefone",
                    Valor = 50.95m,
                    DataVencimento = new DateTime(2023, 9, 10),
                    UsuarioId = 2,
                    CategoriaId = 15
                },
                new Despesa
                {
                    Id = id++,
                    Data = new DateTime(2023, 9, 02),
                    Descricao = "Aluguel",
                    Valor = 800.95m,
                    DataVencimento = new DateTime(2023, 9, 15),
                    UsuarioId = 2,
                    CategoriaId = 15
                },
                new Despesa
                {
                    Id = id++,
                    Data = new DateTime(2023, 9, 03),
                    Descricao = "Manutenção do Carro",
                    Valor = 120.95m,
                    DataVencimento = new DateTime(2023, 9, 20),
                    UsuarioId = 2,
                    CategoriaId = 16
                },
                new Despesa
                {
                    Id = id++,
                    Data = new DateTime(2023, 9, 04),
                    Descricao = "Material de Escritório",
                    Valor = 30.95m,
                    DataVencimento = new DateTime(2023, 9, 25),
                    UsuarioId = 2,
                    CategoriaId = 16
                },
                new Despesa
                {
                    Id = id++,
                    Data = new DateTime(2023, 9, 05),
                    Descricao = "Seguro de Vida",
                    Valor = 40.95m,
                    DataVencimento = new DateTime(2023, 9, 05),
                    UsuarioId = 2,
                    CategoriaId = 17
                },
                new Despesa
                {
                    Id = id++,
                    Data = new DateTime(2023, 9, 06),
                    Descricao = "Assinatura de Jornal",
                    Valor = 15.95m,
                    DataVencimento = new DateTime(2023, 9, 10),
                    UsuarioId = 2,
                    CategoriaId = 16
                },
                new Despesa
                {
                    Id = id++,
                    Data = new DateTime(2023, 9, 07),
                    Descricao = "Presente de Aniversário",
                    Valor = 50.95m,
                    DataVencimento = new DateTime(2023, 9, 15),
                    UsuarioId = 2,
                    CategoriaId = 21
                },
                new Despesa
                {
                    Id = id++,
                    Data = new DateTime(2023, 9, 08),
                    Descricao = "Outros gastos",
                    Valor = 25.95m,
                    DataVencimento = new DateTime(2023, 9, 20),
                    UsuarioId = 2,
                    CategoriaId = 21
                },
                new Despesa
                {
                    Id = id++,
                    Data = new DateTime(2023, 9, 09),
                    Descricao = "Gastos Diversos",
                    Valor = 1000.95m,
                    DataVencimento = new DateTime(2023, 9, 25),
                    UsuarioId = 2,
                    CategoriaId = 21
                },
                new Despesa
                {
                    Id = id++,
                    Data = new DateTime(2023, 10, 01),
                    Descricao = "Conta de Internet",
                    Valor = 60.95m,
                    DataVencimento = new DateTime(2023, 10, 10),
                    UsuarioId = 2,
                    CategoriaId = 15
                },
                new Despesa
                {
                    Id = id++,
                    Data = new DateTime(2023, 10, 02),
                    Descricao = "Aluguel",
                    Valor = 800.95m,
                    DataVencimento = new DateTime(2023, 10, 15),
                    UsuarioId = 2,
                    CategoriaId = 15
                },
                new Despesa
                {
                    Id = id++,
                    Data = new DateTime(2023, 10, 03),
                    Descricao = "Manutenção do Carro",
                    Valor = 100.95m,
                    DataVencimento = new DateTime(2023, 10, 20),
                    UsuarioId = 2,
                    CategoriaId = 21
                },
                new Despesa
                {
                    Id = id++,
                    Data = new DateTime(2023, 10, 04),
                    Descricao = "Material de Escritório",
                    Valor = 40.95m,
                    DataVencimento = new DateTime(2023, 10, 25),
                    UsuarioId = 2,
                    CategoriaId = 16
                },
                new Despesa
                {
                    Id = id++,
                    Data = new DateTime(2023, 10, 05),
                    Descricao = "Seguro de Vida",
                    Valor = 40.95m,
                    DataVencimento = new DateTime(2023, 10, 05),
                    UsuarioId = 2,
                    CategoriaId = 17
                },
                new Despesa
                {
                    Id = id++,
                    Data = new DateTime(2023, 10, 06),
                    Descricao = "Assinatura de Revista",
                    Valor = 20.95m,
                    DataVencimento = new DateTime(2023, 10, 10),
                    UsuarioId = 2,
                    CategoriaId = 16
                },
                new Despesa
                {
                    Id = id++,
                    Data = new DateTime(2023, 10, 07),
                    Descricao = "Presente de Casamento",
                    Valor = 70.95m,
                    DataVencimento = new DateTime(2023, 10, 15),
                    UsuarioId = 2,
                    CategoriaId = 21
                },
                new Despesa
                {
                    Id = id++,
                    Data = new DateTime(2023, 10, 08),
                    Descricao = "Outros gastos",
                    Valor = 35.95m,
                    DataVencimento = new DateTime(2023, 10, 20),
                    UsuarioId = 2,
                    CategoriaId = 21
                },
                new Despesa
                {
                    Id = id++,
                    Data = new DateTime(2023, 10, 09),
                    Descricao = "Gastos Diversos",
                    Valor = 1100.95m,
                    DataVencimento = new DateTime(2023, 10, 25),
                    UsuarioId = 2,
                    CategoriaId = 21
                },
                new Despesa
                {
                    Id = id++,
                    Data = new DateTime(2023, 11, 01),
                    Descricao = "Conta de Água",
                    Valor = 40.95m,
                    DataVencimento = new DateTime(2023, 11, 10),
                    UsuarioId = 2,
                    CategoriaId = 15
                },
                new Despesa
                {
                    Id = id++,
                    Data = new DateTime(2023, 11, 02),
                    Descricao = "Aluguel",
                    Valor = 800.95m,
                    DataVencimento = new DateTime(2023, 11, 15),
                    UsuarioId = 2,
                    CategoriaId = 15
                },
                new Despesa
                {
                    Id = id++,
                    Data = new DateTime(2023, 11, 03),
                    Descricao = "Manutenção do Carro",
                    Valor = 80.95m,
                    DataVencimento = new DateTime(2023, 11, 20),
                    UsuarioId = 2,
                    CategoriaId = 16
                },
                new Despesa
                {
                    Id = id++,
                    Data = new DateTime(2023, 11, 04),
                    Descricao = "Material de Escritório",
                    Valor = 35.95m,
                    DataVencimento = new DateTime(2023, 11, 25),
                    UsuarioId = 2,
                    CategoriaId = 16
                },
                new Despesa
                {
                    Id = id++,
                    Data = new DateTime(2023, 11, 05),
                    Descricao = "Seguro de Vida",
                    Valor = 40.95m,
                    DataVencimento = new DateTime(2023, 11, 05),
                    UsuarioId = 2,
                    CategoriaId = 17
                },
                new Despesa
                {
                    Id = id++,
                    Data = new DateTime(2023, 11, 06),
                    Descricao = "Assinatura de Revista",
                    Valor = 20.95m,
                    DataVencimento = new DateTime(2023, 11, 10),
                    UsuarioId = 2,
                    CategoriaId = 21
                },
                new Despesa
                {
                    Id = id++,
                    Data = new DateTime(2023, 11, 07),
                    Descricao = "Presente de Casamento",
                    Valor = 70.95m,
                    DataVencimento = new DateTime(2023, 11, 15),
                    UsuarioId = 2,
                    CategoriaId = 21
                },
                new Despesa
                {
                    Id = id++,
                    Data = new DateTime(2023, 11, 08),
                    Descricao = "Outros gastos",
                    Valor = 35.95m,
                    DataVencimento = new DateTime(2023, 11, 20),
                    UsuarioId = 2,
                    CategoriaId = 21
                },
                new Despesa
                {
                    Id = id++,
                    Data = new DateTime(2023, 11, 09),
                    Descricao = "Gastos Diversos",
                    Valor = 1100.95m,
                    DataVencimento = new DateTime(2023, 11, 25),
                    UsuarioId = 2,
                    CategoriaId = 21
                },
                new Despesa
                {
                    Id = id++,
                    Data = new DateTime(2023, 12, 01),
                    Descricao = "Conta de Água",
                    Valor = 40.95m,
                    DataVencimento = new DateTime(2023, 12, 10),
                    UsuarioId = 2,
                    CategoriaId = 15
                },
                new Despesa
                {
                    Id = id++,
                    Data = new DateTime(2023, 12, 02),
                    Descricao = "Aluguel",
                    Valor = 800.95m,
                    DataVencimento = new DateTime(2023, 12, 15),
                    UsuarioId = 2,
                    CategoriaId = 15
                },
                new Despesa
                {
                    Id = id++,
                    Data = new DateTime(2023, 12, 03),
                    Descricao = "Manutenção do Carro",
                    Valor = 80.95m,
                    DataVencimento = new DateTime(2023, 12, 20),
                    UsuarioId = 2,
                    CategoriaId = 16
                },
                new Despesa
                {
                    Id = id++,
                    Data = new DateTime(2023, 12, 04),
                    Descricao = "Material de Escritório",
                    Valor = 30.95m,
                    DataVencimento = new DateTime(2023, 12, 25),
                    UsuarioId = 2,
                    CategoriaId = 16
                },
                new Despesa
                {
                    Id = id++,
                    Data = new DateTime(2023, 12, 05),
                    Descricao = "Seguro de Vida",
                    Valor = 40.95m,
                    DataVencimento = new DateTime(2023, 12, 05),
                    UsuarioId = 2,
                    CategoriaId = 17
                },
                new Despesa
                {
                    Id = id++,
                    Data = new DateTime(2023, 12, 06),
                    Descricao = "Assinatura de Revista",
                    Valor = 20.95m,
                    DataVencimento = new DateTime(2023, 12, 10),
                    UsuarioId = 2,
                    CategoriaId = 16
                },
                new Despesa
                {
                    Id = id++,
                    Data = new DateTime(2023, 12, 07),
                    Descricao = "Presente de Casamento",
                    Valor = 70.95m,
                    DataVencimento = new DateTime(2023, 12, 15),
                    UsuarioId = 2,
                    CategoriaId = 21
                },
                new Despesa
                {
                    Id = id++,
                    Data = new DateTime(2023, 12, 08),
                    Descricao = "Outros gastos",
                    Valor = 35.95m,
                    DataVencimento = new DateTime(2023, 12, 20),
                    UsuarioId = 2,
                    CategoriaId = 21
                },
                new Despesa
                {
                    Id = id++,
                    Data = new DateTime(2023, 12, 09),
                    Descricao = "Gastos Diversos",
                    Valor = 1100.95m,
                    DataVencimento = new DateTime(2023, 12, 25),
                    UsuarioId = 2,
                    CategoriaId = 21
                },
                new Despesa
                {
                    Id = id++,
                    Data = new DateTime(2024, 01, 01),
                    Descricao = "Conta de Água",
                    Valor = 40.95m,
                    DataVencimento = new DateTime(2024, 01, 10),
                    UsuarioId = 2,
                    CategoriaId = 15
                },
                new Despesa
                {
                    Id = id++,
                    Data = new DateTime(2024, 01, 02),
                    Descricao = "Aluguel",
                    Valor = 800.95m,
                    DataVencimento = new DateTime(2024, 01, 15),
                    UsuarioId = 2,
                    CategoriaId = 15
                },
                new Despesa
                {
                    Id = id++,
                    Data = new DateTime(2024, 01, 03),
                    Descricao = "Manutenção do Carro",
                    Valor = 80.95m,
                    DataVencimento = new DateTime(2024, 01, 20),
                    UsuarioId = 2,
                    CategoriaId = 16
                },
                new Despesa
                {
                    Id = id++,
                    Data = new DateTime(2024, 01, 04),
                    Descricao = "Material de Escritório",
                    Valor = 30.95m,
                    DataVencimento = new DateTime(2024, 01, 25),
                    UsuarioId = 2,
                    CategoriaId = 16
                },
                                new Despesa
                {
                    Id = id++,
                    Data = new DateTime(2024, 1, 1),
                    Descricao = "Conta de Luz",
                    Valor = 150.95m,
                    DataVencimento = new DateTime(2024, 1, 10),
                    UsuarioId = 2,
                    CategoriaId = 15
                },
                new Despesa
                {
                    Id = id++,
                    Data = new DateTime(2024, 1, 2),
                    Descricao = "Compra de mantimentos",
                    Valor = 50.95m,
                    DataVencimento = new DateTime(2024, 1, 15),
                    UsuarioId = 2,
                    CategoriaId = 15
                },
                new Despesa
                {
                    Id = id++,
                    Data = new DateTime(2024, 1, 3),
                    Descricao = "Serviço de Limpeza",
                    Valor = 80.95m,
                    DataVencimento = new DateTime(2024, 1, 20),
                    UsuarioId = 2,
                    CategoriaId = 16
                },
                new Despesa
                {
                    Id = id++,
                    Data = new DateTime(2024, 1, 4),
                    Descricao = "Consulta Médica",
                    Valor = 200.95m,
                    DataVencimento = new DateTime(2024, 1, 25),
                    UsuarioId = 2,
                    CategoriaId = 17
                },
                new Despesa
                {
                    Id = id++,
                    Data = new DateTime(2024, 1, 5),
                    Descricao = "Imposto de Renda",
                    Valor = 300.95m,
                    DataVencimento = new DateTime(2024, 1, 5),
                    UsuarioId = 2,
                    CategoriaId = 18
                },
                new Despesa
                {
                    Id = id++,
                    Data = new DateTime(2024, 1, 6),
                    Descricao = "Passagem de Ônibus",
                    Valor = 10.95m,
                    DataVencimento = new DateTime(2024, 1, 10),
                    UsuarioId = 2,
                    CategoriaId = 19
                },
                new Despesa
                {
                    Id = id++,
                    Data = new DateTime(2024, 1, 7),
                    Descricao = "Cinema",
                    Valor = 20.508m,
                    DataVencimento = new DateTime(2024, 1, 15),
                    UsuarioId = 2,
                    CategoriaId = 20
                },
                new Despesa
                {
                    Id = id++,
                    Data = new DateTime(2024, 1, 8),
                    Descricao = "Outros gastos",
                    Valor = 15.20m,
                    DataVencimento = new DateTime(2024, 1, 20),
                    UsuarioId = 2,
                    CategoriaId = 21
                },
                new Despesa
                {
                    Id = id++,
                    Data = new DateTime(2024, 1, 9),
                    Descricao = "Gastos Diversos",
                    Valor = 1000.95m,
                    DataVencimento = new DateTime(2024, 1, 25),
                    UsuarioId = 2,
                    CategoriaId = 21
                },
                new Despesa
                {
                    Id = id++,
                    Data = new DateTime(2024, 1, 10),
                    Descricao = "Gastos Diversos",
                    Valor = 500.95m,
                    DataVencimento = new DateTime(2024, 1, 5),
                    UsuarioId = 2,
                    CategoriaId = 21
                },
                new Despesa
                {
                    Id = id++,
                    Data = new DateTime(2024, 2, 1),
                    Descricao = "Aluguel",
                    Valor = 800.95m,
                    DataVencimento = new DateTime(2024, 2, 10),
                    UsuarioId = 2,
                    CategoriaId = 14
                },
                new Despesa
                {
                    Id = id++,
                    Data = new DateTime(2024, 2, 2),
                    Descricao = "Compra de roupas",
                    Valor = 100.95m,
                    DataVencimento = new DateTime(2024, 2, 15),
                    UsuarioId = 2,
                    CategoriaId = 15
                },
                new Despesa
                {
                    Id = id++,
                    Data = new DateTime(2024, 2, 3),
                    Descricao = "Serviço de manutenção",
                    Valor = 150.95m,
                    DataVencimento = new DateTime(2024, 2, 20),
                    UsuarioId = 2,
                    CategoriaId = 16
                },
                new Despesa
                {
                    Id = id++,
                    Data = new DateTime(2024, 2, 4),
                    Descricao = "Consulta Médica",
                    Valor = 250.95m,
                    DataVencimento = new DateTime(2024, 2, 25),
                    UsuarioId = 2,
                    CategoriaId = 17
                },
                new Despesa
                {
                    Id = id++,
                    Data = new DateTime(2024, 2, 5),
                    Descricao = "Imposto de Renda",
                    Valor = 350.95m,
                    DataVencimento = new DateTime(2024, 2, 5),
                    UsuarioId = 2,
                    CategoriaId = 18
                },
                new Despesa
                {
                    Id = id++,
                    Data = new DateTime(2024, 2, 6),
                    Descricao = "Passagem de Ônibus",
                    Valor = 15.95m,
                    DataVencimento = new DateTime(2024, 2, 10),
                    UsuarioId = 2,
                    CategoriaId = 19
                },
                new Despesa
                {
                    Id = id++,
                    Data = new DateTime(2024, 2, 7),
                    Descricao = "Cinema",
                    Valor = 25.508m,
                    DataVencimento = new DateTime(2024, 2, 15),
                    UsuarioId = 2,
                    CategoriaId = 20
                },
                new Despesa
                {
                    Id = id++,
                    Data = new DateTime(2024, 2, 8),
                    Descricao = "Outros gastos",
                    Valor = 20.20m,
                    DataVencimento = new DateTime(2024, 2, 20),
                    UsuarioId = 2,
                    CategoriaId = 21
                },
                new Despesa
                {
                    Id = id++,
                    Data = new DateTime(2024, 2, 9),
                    Descricao = "Gastos Diversos",
                    Valor = 1000.95m,
                    DataVencimento = new DateTime(2024, 2, 25),
                    UsuarioId = 2,
                    CategoriaId = 21
                },
                new Despesa
                {
                    Id = id++,
                    Data = new DateTime(2024, 2, 10),
                    Descricao = "Gastos Diversos",
                    Valor = 500.95m,
                    DataVencimento = new DateTime(2024, 2, 5),
                    UsuarioId = 2,
                    CategoriaId = 21
                },
                new Despesa
                {
                    Id = id++,
                    Data = new DateTime(2024, 3, 01),
                    Descricao = "Conta de Água",
                    Valor = 100.95m,
                    DataVencimento = new DateTime(2024, 3, 10),
                    UsuarioId = 2,
                    CategoriaId = 15
                },
                new Despesa
                {
                    Id = id++,
                    Data = new DateTime(2024, 3, 02),
                    Descricao = "Compra de Eletrônicos",
                    Valor = 500.95m,
                    DataVencimento = new DateTime(2024, 3, 15),
                    UsuarioId = 2,
                    CategoriaId = 15
                },
                new Despesa
                {
                    Id = id++,
                    Data = new DateTime(2024, 3, 03),
                    Descricao = "Serviço de Jardinagem",
                    Valor = 120.95m,
                    DataVencimento = new DateTime(2024, 3, 20),
                    UsuarioId = 2,
                    CategoriaId = 16
                },
                new Despesa
                {
                    Id = id++,
                    Data = new DateTime(2024, 3, 04),
                    Descricao = "Consulta Médica",
                    Valor = 150.95m,
                    DataVencimento = new DateTime(2024, 3, 25),
                    UsuarioId = 2,
                    CategoriaId = 17
                },
                new Despesa
                {
                    Id = id++,
                    Data = new DateTime(2024, 3, 05),
                    Descricao = "Imposto de Renda",
                    Valor = 400.95m,
                    DataVencimento = new DateTime(2024, 3, 05),
                    UsuarioId = 2,
                    CategoriaId = 18
                },
                new Despesa
                {
                    Id = id++,
                    Data = new DateTime(2024, 3, 06),
                    Descricao = "Passagem de Trem",
                    Valor = 25.95m,
                    DataVencimento = new DateTime(2024, 3, 10),
                    UsuarioId = 2,
                    CategoriaId = 19
                },
                new Despesa
                {
                    Id = id++,
                    Data = new DateTime(2024, 3, 07),
                    Descricao = "Teatro",
                    Valor = 30.95m,
                    DataVencimento = new DateTime(2024, 3, 15),
                    UsuarioId = 2,
                    CategoriaId = 20
                },
                new Despesa
                {
                    Id = id++,
                    Data = new DateTime(2024, 3, 08),
                    Descricao = "Outros gastos",
                    Valor = 18.20m,
                    DataVencimento = new DateTime(2024, 3, 20),
                    UsuarioId = 2,
                    CategoriaId = 21
                },
                new Despesa
                {
                    Id = id++,
                    Data = new DateTime(2024, 3, 09),
                    Descricao = "Gastos Diversos",
                    Valor = 1000.95m,
                    DataVencimento = new DateTime(2024, 3, 25),
                    UsuarioId = 2,
                    CategoriaId = 21
                },
                new Despesa
                {
                    Id = id++,
                    Data = new DateTime(2024, 3, 10),
                    Descricao = "Gastos Diversos",
                    Valor = 500.95m,
                    DataVencimento = new DateTime(2024, 3, 05),
                    UsuarioId = 2,
                    CategoriaId = 21
                },
                new Despesa
                {
                    Id = id++,
                    Data = new DateTime(2024, 4, 01),
                    Descricao = "Refeição Ifood",
                    Valor = 80.95m,
                    DataVencimento = new DateTime(2024, 4, 10),
                    UsuarioId = 2,
                    CategoriaId = 14
                },
                new Despesa
                {
                    Id = id++,
                    Data = new DateTime(2024, 4, 02),
                    Descricao = "Compra de Móveis",
                    Valor = 700.95m,
                    DataVencimento = new DateTime(2024, 4, 15),
                    UsuarioId = 2,
                    CategoriaId = 15
                },
                new Despesa
                {
                    Id = id++,
                    Data = new DateTime(2024, 4, 03),
                    Descricao = "Serviço de Encanamento",
                    Valor = 90.95m,
                    DataVencimento = new DateTime(2024, 4, 20),
                    UsuarioId = 2,
                    CategoriaId = 16
                },
                new Despesa
                {
                    Id = id++,
                    Data = new DateTime(2024, 4, 04),
                    Descricao = "Consulta Médica",
                    Valor = 180.95m,
                    DataVencimento = new DateTime(2024, 4, 25),
                    UsuarioId = 2,
                    CategoriaId = 17
                },
                new Despesa
                {
                    Id = id++,
                    Data = new DateTime(2024, 4, 05),
                    Descricao = "Imposto de Renda",
                    Valor = 200.95m,
                    DataVencimento = new DateTime(2024, 4, 05),
                    UsuarioId = 2,
                    CategoriaId = 18
                },
                new Despesa
                {
                    Id = id++,
                    Data = new DateTime(2024, 4, 06),
                    Descricao = "Passagem de Metrô",
                    Valor = 5.95m,
                    DataVencimento = new DateTime(2024, 4, 10),
                    UsuarioId = 2,
                    CategoriaId = 19
                },
                new Despesa
                {
                    Id = id++,
                    Data = new DateTime(2024, 4, 07),
                    Descricao = "Shows",
                    Valor = 50.95m,
                    DataVencimento = new DateTime(2024, 4, 15),
                    UsuarioId = 2,
                    CategoriaId = 20
                },
                new Despesa
                {
                    Id = id++,
                    Data = new DateTime(2024, 4, 08),
                    Descricao = "Outros gastos",
                    Valor = 22.508m,
                    DataVencimento = new DateTime(2024, 4, 20),
                    UsuarioId = 2,
                    CategoriaId = 21
                },
                new Despesa
                {
                    Id = id++,
                    Data = new DateTime(2024, 4, 09),
                    Descricao = "Gastos Diversos",
                    Valor = 1000.95m,
                    DataVencimento = new DateTime(2024, 4, 25),
                    UsuarioId = 2,
                    CategoriaId = 21
                },
                new Despesa
                {
                    Id = id++,
                    Data = new DateTime(2024, 4, 10),
                    Descricao = "Gastos Diversos",
                    Valor = 500.95m,
                    DataVencimento = new DateTime(2024, 4, 05),
                    UsuarioId = 2,
                    CategoriaId = 21
                },
                new Despesa
                {
                    Id = id++,
                    Data = new DateTime(2024, 5, 01),
                    Descricao = "Almoço Restaurante",
                    Valor = 120.95m,
                    DataVencimento = new DateTime(2024, 5, 10),
                    UsuarioId = 2,
                    CategoriaId = 14
                },
                new Despesa
                {
                    Id = id++,
                    Data = new DateTime(2024, 5, 02),
                    Descricao = "Compra de Eletrodomésticos",
                    Valor = 800.95m,
                    DataVencimento = new DateTime(2024, 5, 15),
                    UsuarioId = 2,
                    CategoriaId = 15
                },
                new Despesa
                {
                    Id = id++,
                    Data = new DateTime(2024, 5, 03),
                    Descricao = "Serviço de Pintura",
                    Valor = 200.95m,
                    DataVencimento = new DateTime(2024, 5, 20),
                    UsuarioId = 2,
                    CategoriaId = 16
                },
                new Despesa
                {
                    Id = id++,
                    Data = new DateTime(2024, 5, 04),
                    Descricao = "Consulta Médica",
                    Valor = 150.95m,
                    DataVencimento = new DateTime(2024, 5, 25),
                    UsuarioId = 2,
                    CategoriaId = 17
                },
                new Despesa
                {
                    Id = id++,
                    Data = new DateTime(2024, 5, 05),
                    Descricao = "Imposto de Renda",
                    Valor = 400.95m,
                    DataVencimento = new DateTime(2024, 5, 05),
                    UsuarioId = 2,
                    CategoriaId = 18
                },
                new Despesa
                {
                    Id = id++,
                    Data = new DateTime(2024, 5, 06),
                    Descricao = "Passagem de Barco",
                    Valor = 30.95m,
                    DataVencimento = new DateTime(2024, 5, 10),
                    UsuarioId = 2,
                    CategoriaId = 19
                },
                new Despesa
                {
                    Id = id++,
                    Data = new DateTime(2024, 5, 07),
                    Descricao = "Exposição de Arte",
                    Valor = 40.95m,
                    DataVencimento = new DateTime(2024, 5, 15),
                    UsuarioId = 2,
                    CategoriaId = 20
                },
                new Despesa
                {
                    Id = id++,
                    Data = new DateTime(2024, 5, 08),
                    Descricao = "Outros gastos",
                    Valor = 25.80m,
                    DataVencimento = new DateTime(2024, 5, 20),
                    UsuarioId = 2,
                    CategoriaId = 21
                },
                new Despesa
                {
                    Id = id++,
                    Data = new DateTime(2024, 5, 09),
                    Descricao = "Gastos Diversos",
                    Valor = 1000.95m,
                    DataVencimento = new DateTime(2024, 5, 25),
                    UsuarioId = 2,
                    CategoriaId = 21
                },
                new Despesa
                {
                    Id = id++,
                    Data = new DateTime(2024, 5, 10),
                    Descricao = "Gastos Diversos",
                    Valor = 500.95m,
                    DataVencimento = new DateTime(2024, 5, 05),
                    UsuarioId = 2,
                    CategoriaId = 21
                },
                new Despesa
                {
                    Id = id++,
                    Data = new DateTime(2024, 6, 01),
                    Descricao = "Pedido Ifood",
                    Valor = 90.95m,
                    DataVencimento = new DateTime(2024, 6, 10),
                    UsuarioId = 2,
                    CategoriaId = 14
                },
                new Despesa
                {
                    Id = id++,
                    Data = new DateTime(2024, 6, 02),
                    Descricao = "Compra de Livros",
                    Valor = 60.95m,
                    DataVencimento = new DateTime(2024, 6, 15),
                    UsuarioId = 2,
                    CategoriaId = 15
                },
                new Despesa
                {
                    Id = id++,
                    Data = new DateTime(2024, 6, 03),
                    Descricao = "Serviço de Manicure",
                    Valor = 40.95m,
                    DataVencimento = new DateTime(2024, 6, 20),
                    UsuarioId = 2,
                    CategoriaId = 16
                },
                new Despesa
                {
                    Id = id++,
                    Data = new DateTime(2024, 6, 04),
                    Descricao = "Consulta Médica",
                    Valor = 120.95m,
                    DataVencimento = new DateTime(2024, 6, 25),
                    UsuarioId = 2,
                    CategoriaId = 17
                },
                new Despesa
                {
                    Id = id++,
                    Data = new DateTime(2024, 6, 05),
                    Descricao = "Imposto de Renda",
                    Valor = 300.95m,
                    DataVencimento = new DateTime(2024, 6, 05),
                    UsuarioId = 2,
                    CategoriaId = 18
                },
                new Despesa
                {
                    Id = id++,
                    Data = new DateTime(2024, 6, 06),
                    Descricao = "Passagem de Avião",
                    Valor = 200.95m,
                    DataVencimento = new DateTime(2024, 6, 10),
                    UsuarioId = 2,
                    CategoriaId = 19
                },
                new Despesa
                {
                    Id = id++,
                    Data = new DateTime(2024, 6, 07),
                    Descricao = "Shows",
                    Valor = 70.95m,
                    DataVencimento = new DateTime(2024, 6, 15),
                    UsuarioId = 2,
                    CategoriaId = 20
                },
                new Despesa
                {
                    Id = id++,
                    Data = new DateTime(2024, 6, 08),
                    Descricao = "Outros gastos",
                    Valor = 35.508m,
                    DataVencimento = new DateTime(2024, 6, 20),
                    UsuarioId = 2,
                    CategoriaId = 21
                },
                new Despesa
                {
                    Id = id++,
                    Data = new DateTime(2024, 6, 09),
                    Descricao = "Gastos Diversos",
                    Valor = 1000.95m,
                    DataVencimento = new DateTime(2024, 6, 25),
                    UsuarioId = 2,
                    CategoriaId = 21
                },
                new Despesa
                {
                    Id = id++,
                    Data = new DateTime(2024, 6, 10),
                    Descricao = "Gastos Diversos",
                    Valor = 500.95m,
                    DataVencimento = new DateTime(2024, 6, 05),
                    UsuarioId = 2,
                    CategoriaId = 21
                },
                new Despesa
                {
                    Id = id++,
                    Data = new DateTime(2024, 7, 01),
                    Descricao = "Refeição Ifood",
                    Valor = 150.95m,
                    DataVencimento = new DateTime(2024, 7, 10),
                    UsuarioId = 2,
                    CategoriaId = 14
                },
                new Despesa
                {
                    Id = id++,
                    Data = new DateTime(2024, 7, 02),
                    Descricao = "Compra de Roupas",
                    Valor = 200.95m,
                    DataVencimento = new DateTime(2024, 7, 15),
                    UsuarioId = 2,
                    CategoriaId = 15
                },
                new Despesa
                {
                    Id = id++,
                    Data = new DateTime(2024, 7, 03),
                    Descricao = "Serviço de Limpeza",
                    Valor = 80.95m,
                    DataVencimento = new DateTime(2024, 7, 20),
                    UsuarioId = 2,
                    CategoriaId = 16
                },
                new Despesa
                {
                    Id = id++,
                    Data = new DateTime(2024, 7, 04),
                    Descricao = "Consulta Médica",
                    Valor = 100.95m,
                    DataVencimento = new DateTime(2024, 7, 25),
                    UsuarioId = 2,
                    CategoriaId = 17
                },
                new Despesa
                {
                    Id = id++,
                    Data = new DateTime(2024, 7, 05),
                    Descricao = "Imposto de Renda",
                    Valor = 250.95m,
                    DataVencimento = new DateTime(2024, 7, 05),
                    UsuarioId = 2,
                    CategoriaId = 18
                },
                new Despesa
                {
                    Id = id++,
                    Data = new DateTime(2024, 7, 06),
                    Descricao = "Passagem de Ônibus",
                    Valor = 10.95m,
                    DataVencimento = new DateTime(2024, 7, 10),
                    UsuarioId = 2,
                    CategoriaId = 19
                },
                new Despesa
                {
                    Id = id++,
                    Data = new DateTime(2024, 7, 07),
                    Descricao = "Cinema",
                    Valor = 25.508m,
                    DataVencimento = new DateTime(2024, 7, 15),
                    UsuarioId = 2,
                    CategoriaId = 20
                },
                new Despesa
                {
                    Id = id++,
                    Data = new DateTime(2024, 7, 08),
                    Descricao = "Outros gastos",
                    Valor = 20.20m,
                    DataVencimento = new DateTime(2024, 7, 20),
                    UsuarioId = 2,
                    CategoriaId = 21
                },
                new Despesa
                {
                    Id = id++,
                    Data = new DateTime(2024, 7, 09),
                    Descricao = "Gastos Diversos",
                    Valor = 1000.95m,
                    DataVencimento = new DateTime(2024, 7, 25),
                    UsuarioId = 2,
                    CategoriaId = 21
                },
                new Despesa
                {
                    Id = id++,
                    Data = new DateTime(2024, 7, 10),
                    Descricao = "Gastos Diversos",
                    Valor = 500.95m,
                    DataVencimento = new DateTime(2024, 7, 05),
                    UsuarioId = 2,
                    CategoriaId = 21
                },
                new Despesa
                {
                    Id = id++,
                    Data = new DateTime(2024, 8, 01),
                    Descricao = "Almoço no Restaurante",
                    Valor = 100.95m,
                    DataVencimento = new DateTime(2024, 8, 10),
                    UsuarioId = 2,
                    CategoriaId = 14
                },
                new Despesa
                {
                    Id = id++,
                    Data = new DateTime(2024, 8, 02),
                    Descricao = "Compra de Eletrônicos",
                    Valor = 500.95m,
                    DataVencimento = new DateTime(2024, 8, 15),
                    UsuarioId = 2,
                    CategoriaId = 15
                },
                new Despesa
                {
                    Id = id++,
                    Data = new DateTime(2024, 8, 03),
                    Descricao = "Serviço de Jardinagem",
                    Valor = 120.95m,
                    DataVencimento = new DateTime(2024, 8, 20),
                    UsuarioId = 2,
                    CategoriaId = 16
                },
                new Despesa
                {
                    Id = id++,
                    Data = new DateTime(2024, 8, 04),
                    Descricao = "Consulta Médica",
                    Valor = 80.95m,
                    DataVencimento = new DateTime(2024, 8, 25),
                    UsuarioId = 2,
                    CategoriaId = 17
                },
                new Despesa
                {
                    Id = id++,
                    Data = new DateTime(2024, 8, 05),
                    Descricao = "Imposto de Renda",
                    Valor = 350.95m,
                    DataVencimento = new DateTime(2024, 8, 05),
                    UsuarioId = 2,
                    CategoriaId = 18
                },
                new Despesa
                {
                    Id = id++,
                    Data = new DateTime(2024, 8, 06),
                    Descricao = "Passagem de Trem",
                    Valor = 15.95m,
                    DataVencimento = new DateTime(2024, 8, 10),
                    UsuarioId = 2,
                    CategoriaId = 19
                },
                new Despesa
                {
                    Id = id++,
                    Data = new DateTime(2024, 8, 07),
                    Descricao = "Shows",
                    Valor = 70.95m,
                    DataVencimento = new DateTime(2024, 8, 15),
                    UsuarioId = 2,
                    CategoriaId = 20
                },
                new Despesa
                {
                    Id = id++,
                    Data = new DateTime(2024, 8, 08),
                    Descricao = "Outros gastos",
                    Valor = 30.95m,
                    DataVencimento = new DateTime(2024, 8, 20),
                    UsuarioId = 2,
                    CategoriaId = 21
                },
                new Despesa
                {
                    Id = id++,
                    Data = new DateTime(2024, 9, 01),
                    Descricao = "Conta de Telefone",
                    Valor = 50.95m,
                    DataVencimento = new DateTime(2024, 9, 10),
                    UsuarioId = 2,
                    CategoriaId = 15
                },
                new Despesa
                {
                    Id = id++,
                    Data = new DateTime(2024, 9, 02),
                    Descricao = "Aluguel",
                    Valor = 800.95m,
                    DataVencimento = new DateTime(2024, 9, 15),
                    UsuarioId = 2,
                    CategoriaId = 15
                },
                new Despesa
                {
                    Id = id++,
                    Data = new DateTime(2024, 9, 03),
                    Descricao = "Manutenção do Carro",
                    Valor = 120.95m,
                    DataVencimento = new DateTime(2024, 9, 20),
                    UsuarioId = 2,
                    CategoriaId = 16
                },
                new Despesa
                {
                    Id = id++,
                    Data = new DateTime(2024, 9, 04),
                    Descricao = "Material de Escritório",
                    Valor = 30.95m,
                    DataVencimento = new DateTime(2024, 9, 25),
                    UsuarioId = 2,
                    CategoriaId = 16
                },
                new Despesa
                {
                    Id = id++,
                    Data = new DateTime(2024, 9, 05),
                    Descricao = "Seguro de Vida",
                    Valor = 40.95m,
                    DataVencimento = new DateTime(2024, 9, 05),
                    UsuarioId = 2,
                    CategoriaId = 17
                },
                new Despesa
                {
                    Id = id++,
                    Data = new DateTime(2024, 9, 06),
                    Descricao = "Assinatura de Jornal",
                    Valor = 15.95m,
                    DataVencimento = new DateTime(2024, 9, 10),
                    UsuarioId = 2,
                    CategoriaId = 16
                },
                new Despesa
                {
                    Id = id++,
                    Data = new DateTime(2024, 9, 07),
                    Descricao = "Presente de Aniversário",
                    Valor = 50.95m,
                    DataVencimento = new DateTime(2024, 9, 15),
                    UsuarioId = 2,
                    CategoriaId = 21
                },
                new Despesa
                {
                    Id = id++,
                    Data = new DateTime(2024, 9, 08),
                    Descricao = "Outros gastos",
                    Valor = 25.95m,
                    DataVencimento = new DateTime(2024, 9, 20),
                    UsuarioId = 2,
                    CategoriaId = 21
                },
                new Despesa
                {
                    Id = id++,
                    Data = new DateTime(2024, 9, 09),
                    Descricao = "Gastos Diversos",
                    Valor = 1000.95m,
                    DataVencimento = new DateTime(2024, 9, 25),
                    UsuarioId = 2,
                    CategoriaId = 21
                },
                new Despesa
                {
                    Id = id++,
                    Data = new DateTime(2024, 10, 01),
                    Descricao = "Conta de Internet",
                    Valor = 60.95m,
                    DataVencimento = new DateTime(2024, 10, 10),
                    UsuarioId = 2,
                    CategoriaId = 15
                },
                new Despesa
                {
                    Id = id++,
                    Data = new DateTime(2024, 10, 02),
                    Descricao = "Aluguel",
                    Valor = 800.95m,
                    DataVencimento = new DateTime(2024, 10, 15),
                    UsuarioId = 2,
                    CategoriaId = 15
                },
                new Despesa
                {
                    Id = id++,
                    Data = new DateTime(2024, 10, 03),
                    Descricao = "Manutenção do Carro",
                    Valor = 100.95m,
                    DataVencimento = new DateTime(2024, 10, 20),
                    UsuarioId = 2,
                    CategoriaId = 21
                },
                new Despesa
                {
                    Id = id++,
                    Data = new DateTime(2024, 10, 04),
                    Descricao = "Material de Escritório",
                    Valor = 40.95m,
                    DataVencimento = new DateTime(2024, 10, 25),
                    UsuarioId = 2,
                    CategoriaId = 16
                },
                new Despesa
                {
                    Id = id++,
                    Data = new DateTime(2024, 10, 05),
                    Descricao = "Seguro de Vida",
                    Valor = 40.95m,
                    DataVencimento = new DateTime(2024, 10, 05),
                    UsuarioId = 2,
                    CategoriaId = 17
                },
                new Despesa
                {
                    Id = id++,
                    Data = new DateTime(2024, 10, 06),
                    Descricao = "Assinatura de Revista",
                    Valor = 20.95m,
                    DataVencimento = new DateTime(2024, 10, 10),
                    UsuarioId = 2,
                    CategoriaId = 16
                },
                new Despesa
                {
                    Id = id++,
                    Data = new DateTime(2024, 10, 07),
                    Descricao = "Presente de Casamento",
                    Valor = 70.95m,
                    DataVencimento = new DateTime(2024, 10, 15),
                    UsuarioId = 2,
                    CategoriaId = 21
                },
                new Despesa
                {
                    Id = id++,
                    Data = new DateTime(2024, 10, 08),
                    Descricao = "Outros gastos",
                    Valor = 35.95m,
                    DataVencimento = new DateTime(2024, 10, 20),
                    UsuarioId = 2,
                    CategoriaId = 21
                },
                new Despesa
                {
                    Id = id++,
                    Data = new DateTime(2024, 10, 09),
                    Descricao = "Gastos Diversos",
                    Valor = 1100.95m,
                    DataVencimento = new DateTime(2024, 10, 25),
                    UsuarioId = 2,
                    CategoriaId = 21
                },
                new Despesa
                {
                    Id = id++,
                    Data = new DateTime(2024, 11, 01),
                    Descricao = "Conta de Água",
                    Valor = 40.95m,
                    DataVencimento = new DateTime(2024, 11, 10),
                    UsuarioId = 2,
                    CategoriaId = 15
                },
                new Despesa
                {
                    Id = id++,
                    Data = new DateTime(2024, 11, 02),
                    Descricao = "Aluguel",
                    Valor = 800.95m,
                    DataVencimento = new DateTime(2024, 11, 15),
                    UsuarioId = 2,
                    CategoriaId = 15
                },
                new Despesa
                {
                    Id = id++,
                    Data = new DateTime(2024, 11, 03),
                    Descricao = "Manutenção do Carro",
                    Valor = 80.95m,
                    DataVencimento = new DateTime(2024, 11, 20),
                    UsuarioId = 2,
                    CategoriaId = 16
                },
                new Despesa
                {
                    Id = id++,
                    Data = new DateTime(2024, 11, 04),
                    Descricao = "Material de Escritório",
                    Valor = 35.95m,
                    DataVencimento = new DateTime(2024, 11, 25),
                    UsuarioId = 2,
                    CategoriaId = 16
                },
                new Despesa
                {
                    Id = id++,
                    Data = new DateTime(2024, 11, 05),
                    Descricao = "Seguro de Vida",
                    Valor = 40.95m,
                    DataVencimento = new DateTime(2024, 11, 05),
                    UsuarioId = 2,
                    CategoriaId = 17
                },
                new Despesa
                {
                    Id = id++,
                    Data = new DateTime(2024, 11, 06),
                    Descricao = "Assinatura de Revista",
                    Valor = 20.95m,
                    DataVencimento = new DateTime(2024, 11, 10),
                    UsuarioId = 2,
                    CategoriaId = 21
                },
                new Despesa
                {
                    Id = id++,
                    Data = new DateTime(2024, 11, 07),
                    Descricao = "Presente de Casamento",
                    Valor = 70.95m,
                    DataVencimento = new DateTime(2024, 11, 15),
                    UsuarioId = 2,
                    CategoriaId = 21
                },
                new Despesa
                {
                    Id = id++,
                    Data = new DateTime(2024, 11, 08),
                    Descricao = "Outros gastos",
                    Valor = 35.95m,
                    DataVencimento = new DateTime(2024, 11, 20),
                    UsuarioId = 2,
                    CategoriaId = 21
                },
                new Despesa
                {
                    Id = id++,
                    Data = new DateTime(2024, 11, 09),
                    Descricao = "Gastos Diversos",
                    Valor = 1100.95m,
                    DataVencimento = new DateTime(2024, 11, 25),
                    UsuarioId = 2,
                    CategoriaId = 21
                },
                new Despesa
                {
                    Id = id++,
                    Data = new DateTime(2024, 12, 01),
                    Descricao = "Conta de Água",
                    Valor = 40.95m,
                    DataVencimento = new DateTime(2024, 12, 10),
                    UsuarioId = 2,
                    CategoriaId = 15
                },
                new Despesa
                {
                    Id = id++,
                    Data = new DateTime(2024, 12, 02),
                    Descricao = "Aluguel",
                    Valor = 800.95m,
                    DataVencimento = new DateTime(2024, 12, 15),
                    UsuarioId = 2,
                    CategoriaId = 15
                },
                new Despesa
                {
                    Id = id++,
                    Data = new DateTime(2024, 12, 03),
                    Descricao = "Manutenção do Carro",
                    Valor = 80.95m,
                    DataVencimento = new DateTime(2024, 12, 20),
                    UsuarioId = 2,
                    CategoriaId = 16
                },
                new Despesa
                {
                    Id = id++,
                    Data = new DateTime(2024, 12, 04),
                    Descricao = "Material de Escritório",
                    Valor = 30.95m,
                    DataVencimento = new DateTime(2024, 12, 25),
                    UsuarioId = 2,
                    CategoriaId = 16
                },
                new Despesa
                {
                    Id = id++,
                    Data = new DateTime(2024, 12, 05),
                    Descricao = "Seguro de Vida",
                    Valor = 40.95m,
                    DataVencimento = new DateTime(2024, 12, 05),
                    UsuarioId = 2,
                    CategoriaId = 17
                },
                new Despesa
                {
                    Id = id++,
                    Data = new DateTime(2024, 12, 06),
                    Descricao = "Assinatura de Revista",
                    Valor = 20.95m,
                    DataVencimento = new DateTime(2024, 12, 10),
                    UsuarioId = 2,
                    CategoriaId = 16
                },
                new Despesa
                {
                    Id = id++,
                    Data = new DateTime(2024, 12, 07),
                    Descricao = "Presente de Casamento",
                    Valor = 70.95m,
                    DataVencimento = new DateTime(2024, 12, 15),
                    UsuarioId = 2,
                    CategoriaId = 21
                },
                new Despesa
                {
                    Id = id++,
                    Data = new DateTime(2024, 12, 08),
                    Descricao = "Outros gastos",
                    Valor = 35.95m,
                    DataVencimento = new DateTime(2024, 12, 20),
                    UsuarioId = 2,
                    CategoriaId = 21
                },
                new Despesa
                {
                    Id = id++,
                    Data = new DateTime(2024, 12, 09),
                    Descricao = "Gastos Diversos",
                    Valor = 1100.95m,
                    DataVencimento = new DateTime(2024, 12, 25),
                    UsuarioId = 2,
                    CategoriaId = 21
                },
                new Despesa
                {
                    Id = id++,
                    Data = new DateTime(2024, 01, 01),
                    Descricao = "Conta de Água",
                    Valor = 40.95m,
                    DataVencimento = new DateTime(2024, 01, 10),
                    UsuarioId = 2,
                    CategoriaId = 15
                },
                new Despesa
                {
                    Id = id++,
                    Data = new DateTime(2024, 01, 02),
                    Descricao = "Aluguel",
                    Valor = 800.95m,
                    DataVencimento = new DateTime(2024, 01, 15),
                    UsuarioId = 2,
                    CategoriaId = 15
                },
                new Despesa
                {
                    Id = id++,
                    Data = new DateTime(2024, 01, 03),
                    Descricao = "Manutenção do Carro",
                    Valor = 80.95m,
                    DataVencimento = new DateTime(2024, 01, 20),
                    UsuarioId = 2,
                    CategoriaId = 16
                },
                new Despesa
                {
                    Id = id++,
                    Data = new DateTime(2024, 01, 04),
                    Descricao = "Material de Escritório",
                    Valor = 30.95m,
                    DataVencimento = new DateTime(2024, 01, 25),
                    UsuarioId = 2,
                    CategoriaId = 16
                }
            };
            _context.Despesa.AddRange(despesas);
            _context.SaveChanges();
        }
    }
}
