using Repository;
using Domain.Entities;

namespace DataSeeders.Implementations;
public class DataSeederReceita : IDataSeeder
{
    private readonly RegisterContext _context;

    public DataSeederReceita(RegisterContext context)
    {
        _context = context;
    }

    public void SeedData()
    {
        if (!_context.Receita.Any())
        {
            var receitas = new List<Receita>
            {
                new Receita
                {

                    Data = new DateTime(2023, 01, 05),
                    Descricao = "Salário mês de Janeiro",
                    Valor = 2000.36m,
                    UsuarioId = 2,
                    CategoriaId = 22
                },
                new Receita
                {

                    Data = new DateTime(2023, 05, 31, 01, 45, 04),
                    Descricao = "Teste Alteração Receita",
                    Valor = 500.50m,
                    UsuarioId = 2,
                    CategoriaId = 26
                },
                new Receita
                {

                    Data = new DateTime(2023, 01, 20),
                    Descricao = "Investimento bitcoin",
                    Valor = 1000.99m,
                    UsuarioId = 2,
                    CategoriaId = 24
                },
                new Receita
                {

                    Data = new DateTime(2023, 01, 25),
                    Descricao = "Benefício casa alugada",
                    Valor = 300.35m,
                    UsuarioId = 2,
                    CategoriaId = 25
                },
                new Receita
                {

                    Data = new DateTime(2023, 01, 30),
                    Descricao = "Outros ganhos",
                    Valor = 120.25m,
                    UsuarioId = 2,
                    CategoriaId = 26
                },
                new Receita
                {

                    Data = new DateTime(2023, 02, 05),
                    Descricao = "Salário mês de Fevereiro",
                    Valor = 2000.36m,
                    UsuarioId = 2,
                    CategoriaId = 22
                },
                new Receita
                {

                    Data = new DateTime(2023, 02, 15),
                    Descricao = "Prêmio recebido raspadinha",
                    Valor = 500.36m,
                    UsuarioId = 2,
                    CategoriaId = 23
                },
                new Receita
                {

                    Data = new DateTime(2023, 02, 20),
                    Descricao = "Investimento na poupança",
                    Valor = 1000.99m,
                    UsuarioId = 2,
                    CategoriaId = 24
                },
                new Receita
                {

                    Data = new DateTime(2023, 02, 25),
                    Descricao = "Restituiação do Imposto de Renda",
                    Valor = 300.35m,
                    UsuarioId = 2,
                    CategoriaId = 25
                },
                new Receita
                {

                    Data = new DateTime(2023, 02, 28),
                    Descricao = "Outros ganhos",
                    Valor = 120.25m,
                    UsuarioId = 2,
                    CategoriaId = 26
                },
                new Receita
                {

                    Data = new DateTime(2023, 03, 05),
                    Descricao = "Salário mês de Março",
                    Valor = 2000.36m,
                    UsuarioId = 2,
                    CategoriaId = 22
                },
                new Receita
                {

                    Data = new DateTime(2023, 03, 15),
                    Descricao = "Prêmio Loteria Esportiva",
                    Valor = 500.36m,
                    UsuarioId = 2,
                    CategoriaId = 23
                },
                new Receita
                {

                    Data = new DateTime(2023, 03, 20),
                    Descricao = "Investimento Bitcoin",
                    Valor = 1000.99m,
                    UsuarioId = 2,
                    CategoriaId = 24
                },
                new Receita
                {

                    Data = new DateTime(2023, 03, 25),
                    Descricao = "Benefício recebido do INSS",
                    Valor = 300.35m,
                    UsuarioId = 2,
                    CategoriaId = 25
                },
                new Receita
                {

                    Data = new DateTime(2023, 03, 31),
                    Descricao = "Outros ganhos",
                    Valor = 120.25m,
                    UsuarioId = 2,
                    CategoriaId = 26
                },
                new Receita
                {

                    Data = new DateTime(2023, 04, 05),
                    Descricao = "Salário mês de Abril",
                    Valor = 2000.36m,
                    UsuarioId = 2,
                    CategoriaId = 22
                },
                new Receita
                {

                    Data = new DateTime(2023, 04, 15),
                    Descricao = "Prêmio Jogo do Bicho",
                    Valor = 500.36m,
                    UsuarioId = 2,
                    CategoriaId = 23
                },
                new Receita
                {

                    Data = new DateTime(2023, 04, 20),
                    Descricao = "Investimento Bitcoin",
                    Valor = 1000.99m,
                    UsuarioId = 2,
                    CategoriaId = 24
                },
                new Receita
                {

                    Data = new DateTime(2023, 04, 25),
                    Descricao = "Benefício recebido em Abril INSS",
                    Valor = 300.35m,
                    UsuarioId = 2,
                    CategoriaId = 25
                },
                new Receita
                {

                    Data = new DateTime(2023, 04, 30),
                    Descricao = "Outros ganhos",
                    Valor = 120.25m,
                    UsuarioId = 2,
                    CategoriaId = 26
                },
                new Receita
                {

                    Data = new DateTime(2023, 05, 05),
                    Descricao = "Salário mês de Maio",
                    Valor = 2000.36m,
                    UsuarioId = 2,
                    CategoriaId = 22
                },
                new Receita
                {

                    Data = new DateTime(2023, 05, 15),
                    Descricao = "Prêmio Loteria",
                    Valor = 500.36m,
                    UsuarioId = 2,
                    CategoriaId = 23
                },
                new Receita
                {

                    Data = new DateTime(2023, 05, 20),
                    Descricao = "Investimento na poupança",
                    Valor = 1000.99m,
                    UsuarioId = 2,
                    CategoriaId = 24
                },
                new Receita
                {

                    Data = new DateTime(2023, 05, 25),
                    Descricao = "Benefício recebido Cashback Nubank",
                    Valor = 300.35m,
                    UsuarioId = 2,
                    CategoriaId = 25
                },
                new Receita
                {

                    Data = new DateTime(2023, 05, 31),
                    Descricao = "Outros ganhos",
                    Valor = 120.25m,
                    UsuarioId = 2,
                    CategoriaId = 26
                },
                new Receita
                {

                    Data = new DateTime(2023, 06, 05),
                    Descricao = "Salário mês de Junho",
                    Valor = 2000.36m,
                    UsuarioId = 2,
                    CategoriaId = 22
                },
                new Receita
                {

                    Data = new DateTime(2023, 06, 15),
                    Descricao = "Prêmio loteria Esportiva",
                    Valor = 500.36m,
                    UsuarioId = 2,
                    CategoriaId = 23
                },
                new Receita
                {

                    Data = new DateTime(2023, 06, 20),
                    Descricao = "Investimento em Bitcoin",
                    Valor = 1000.99m,
                    UsuarioId = 2,
                    CategoriaId = 24
                },
                new Receita
                {

                    Data = new DateTime(2023, 06, 25),
                    Descricao = "Benefício recebido Junho INSS",
                    Valor = 300.35m,
                    UsuarioId = 2,
                    CategoriaId = 25
                },
                new Receita
                {

                    Data = new DateTime(2023, 06, 30),
                    Descricao = "Outros ganhos",
                    Valor = 120.25m,
                    UsuarioId = 2,
                    CategoriaId = 26
                },
                new Receita
                {

                    Data = new DateTime(2023, 07, 05),
                    Descricao = "Salário mês de Julho",
                    Valor = 2000.36m,
                    UsuarioId = 2,
                    CategoriaId = 22
                },
                new Receita
                {

                    Data = new DateTime(2023, 07, 15),
                    Descricao = "Prêmio Jogo do Bicho",
                    Valor = 500.36m,
                    UsuarioId = 2,
                    CategoriaId = 23
                },
                new Receita
                {

                    Data = new DateTime(2023, 07, 20),
                    Descricao = "Investimento em Bitcoin",
                    Valor = 1000.99m,
                    UsuarioId = 2,
                    CategoriaId = 24
                },
                new Receita
                {

                    Data = new DateTime(2023, 07, 25),
                    Descricao = "Benefício recebido Julho INSS",
                    Valor = 300.35m,
                    UsuarioId = 2,
                    CategoriaId = 25
                },
                new Receita
                {

                    Data = new DateTime(2023, 07, 31),
                    Descricao = "Outros ganhos",
                    Valor = 120.25m,
                    UsuarioId = 2,
                    CategoriaId = 26
                },
                new Receita
                {

                    Data = new DateTime(2023, 08, 05),
                    Descricao = "Salário mês de Agosto",
                    Valor = 2000.36m,
                    UsuarioId = 2,
                    CategoriaId = 22
                },
                new Receita
                {

                    Data = new DateTime(2023, 08, 15),
                    Descricao = "Prêmio Loteria",
                    Valor = 500.36m,
                    UsuarioId = 2,
                    CategoriaId = 23
                },
                new Receita
                {

                    Data = new DateTime(2023, 08, 20),
                    Descricao = "Investimento na Poupança",
                    Valor = 1000.99m,
                    UsuarioId = 2,
                    CategoriaId = 24
                },
                new Receita
                {

                    Data = new DateTime(2023, 08, 25),
                    Descricao = "Benefício recebido em Agosto INSS",
                    Valor = 300.35m,
                    UsuarioId = 2,
                    CategoriaId = 25
                },
                new Receita
                {

                    Data = new DateTime(2023, 08, 31),
                    Descricao = "Outros ganhos",
                    Valor = 120.25m,
                    UsuarioId = 2,
                    CategoriaId = 26
                },
                new Receita
                {

                    Data = new DateTime(2023, 09, 05),
                    Descricao = "Salário mês de Setembro",
                    Valor = 2000.36m,
                    UsuarioId = 2,
                    CategoriaId = 22
                },
                new Receita
                {

                    Data = new DateTime(2023, 09, 15),
                    Descricao = "Prêmio Loteria Esportiva",
                    Valor = 500.36m,
                    UsuarioId = 2,
                    CategoriaId = 23
                },
                new Receita
                {

                    Data = new DateTime(2023, 09, 20),
                    Descricao = "Investimento em BitCoin",
                    Valor = 1000.99m,
                    UsuarioId = 2,
                    CategoriaId = 24
                },
                new Receita
                {

                    Data = new DateTime(2023, 09, 25),
                    Descricao = "Benefício",
                    Valor = 300.35m,
                    UsuarioId = 2,
                    CategoriaId = 25
                },
                new Receita
                {

                    Data = new DateTime(2023, 09, 30),
                    Descricao = "Outros ganhos",
                    Valor = 120.25m,
                    UsuarioId = 2,
                    CategoriaId = 26
                },
                new Receita
                {

                    Data = new DateTime(2023, 10, 05),
                    Descricao = "Salário mês de Outubro",
                    Valor = 2000.36m,
                    UsuarioId = 2,
                    CategoriaId = 22
                },
                new Receita
                {

                    Data = new DateTime(2023, 10, 15),
                    Descricao = "Prêmio Loteria ",
                    Valor = 500.36m,
                    UsuarioId = 2,
                    CategoriaId = 23
                },
                new Receita
                {

                    Data = new DateTime(2023, 10, 20),
                    Descricao = "Investimento na Poupança",
                    Valor = 1000.99m,
                    UsuarioId = 2,
                    CategoriaId = 24
                },
                new Receita
                {

                    Data = new DateTime(2023, 10, 25),
                    Descricao = "Benefício recebido Outubro INSS",
                    Valor = 300.35m,
                    UsuarioId = 2,
                    CategoriaId = 25
                },
                new Receita
                {

                    Data = new DateTime(2023, 10, 31),
                    Descricao = "Outros ganhos",
                    Valor = 120.25m,
                    UsuarioId = 2,
                    CategoriaId = 26
                },
                new Receita
                {

                    Data = new DateTime(2023, 11, 05),
                    Descricao = "Salário mês de Novembro",
                    Valor = 2000.36m,
                    UsuarioId = 2,
                    CategoriaId = 22
                },
                new Receita
                {

                    Data = new DateTime(2023, 11, 15),
                    Descricao = "Prêmio Raspadinha",
                    Valor = 500.36m,
                    UsuarioId = 2,
                    CategoriaId = 23
                },
                new Receita
                {

                    Data = new DateTime(2023, 11, 20),
                    Descricao = "Investimento em Bitcoin",
                    Valor = 1000.99m,
                    UsuarioId = 2,
                    CategoriaId = 24
                },
                new Receita
                {

                    Data = new DateTime(2023, 11, 25),
                    Descricao = "Benefício recebido Novembro INSS",
                    Valor = 300.35m,
                    UsuarioId = 2,
                    CategoriaId = 25
                },
                new Receita
                {

                    Data = new DateTime(2023, 11, 30),
                    Descricao = "Outros ganhos",
                    Valor = 120.25m,
                    UsuarioId = 2,
                    CategoriaId = 26
                },
                new Receita
                {

                    Data = new DateTime(2023, 12, 05),
                    Descricao = "Salário mês de Dezembro",
                    Valor = 2000.36m,
                    UsuarioId = 2,
                    CategoriaId = 22
                },
                new Receita
                {

                    Data = new DateTime(2023, 12, 15),
                    Descricao = "Prêmio Mega Senna",
                    Valor = 500.36m,
                    UsuarioId = 2,
                    CategoriaId = 23
                },
                new Receita
                {

                    Data = new DateTime(2023, 12, 20),
                    Descricao = "Investimento em Bitcoin",
                    Valor = 1000.99m,
                    UsuarioId = 2,
                    CategoriaId = 24
                },
                new Receita
                {

                    Data = new DateTime(2023, 12, 25),
                    Descricao = "Benefício recebido Dezembro INSS",
                    Valor = 300.35m,
                    UsuarioId = 2,
                    CategoriaId = 25
                },
                new Receita
                {

                    Data = new DateTime(2023, 12, 31),
                    Descricao = "Outros ganhos",
                    Valor = 120.25m,
                    UsuarioId = 2,
                    CategoriaId = 26
                },
                new Receita
                {

                    Data = new DateTime(2023, 05, 10),
                    Descricao = "Salário mês de maio",
                    Valor = 8500.98m,
                    UsuarioId = 1,
                    CategoriaId = 9
                },
                new Receita
                {

                    Data = new DateTime(2023, 05, 10),
                    Descricao = "Salário mês de maio",
                    Valor = 8500.98m,
                    UsuarioId = 2,
                    CategoriaId = 9
                },
                new Receita
                {

                    Data = new DateTime(2023, 06, 10),
                    Descricao = "Salário mês de Junho",
                    Valor = 8500.98m,
                    UsuarioId = 1,
                    CategoriaId = 9
                },
                new Receita
                {

                    Data = new DateTime(2023, 06, 10),
                    Descricao = "Salário mês de Julho",
                    Valor = 8500.98m,
                    UsuarioId = 1,
                    CategoriaId = 9
                },
                                new Receita
                {

                    Data = new DateTime(2024, 01, 05),
                    Descricao = "Salário mês de Janeiro",
                    Valor = 2000.36m,
                    UsuarioId = 2,
                    CategoriaId = 22
                },
                new Receita
                {

                    Data = new DateTime(2024, 05, 31, 01, 45, 04),
                    Descricao = "Teste Alteração Receita",
                    Valor = 500.50m,
                    UsuarioId = 2,
                    CategoriaId = 26
                },
                new Receita
                {

                    Data = new DateTime(2024, 01, 20),
                    Descricao = "Investimento bitcoin",
                    Valor = 1000.99m,
                    UsuarioId = 2,
                    CategoriaId = 24
                },
                new Receita
                {

                    Data = new DateTime(2024, 01, 25),
                    Descricao = "Benefício casa alugada",
                    Valor = 300.35m,
                    UsuarioId = 2,
                    CategoriaId = 25
                },
                new Receita
                {

                    Data = new DateTime(2024, 01, 30),
                    Descricao = "Outros ganhos",
                    Valor = 120.25m,
                    UsuarioId = 2,
                    CategoriaId = 26
                },
                new Receita
                {

                    Data = new DateTime(2024, 02, 05),
                    Descricao = "Salário mês de Fevereiro",
                    Valor = 2000.36m,
                    UsuarioId = 2,
                    CategoriaId = 22
                },
                new Receita
                {

                    Data = new DateTime(2024, 02, 15),
                    Descricao = "Prêmio recebido raspadinha",
                    Valor = 500.36m,
                    UsuarioId = 2,
                    CategoriaId = 23
                },
                new Receita
                {

                    Data = new DateTime(2024, 02, 20),
                    Descricao = "Investimento na poupança",
                    Valor = 1000.99m,
                    UsuarioId = 2,
                    CategoriaId = 24
                },
                new Receita
                {

                    Data = new DateTime(2024, 02, 25),
                    Descricao = "Restituiação do Imposto de Renda",
                    Valor = 300.35m,
                    UsuarioId = 2,
                    CategoriaId = 25
                },
                new Receita
                {

                    Data = new DateTime(2024, 02, 28),
                    Descricao = "Outros ganhos",
                    Valor = 120.25m,
                    UsuarioId = 2,
                    CategoriaId = 26
                },
                new Receita
                {

                    Data = new DateTime(2024, 03, 05),
                    Descricao = "Salário mês de Março",
                    Valor = 2000.36m,
                    UsuarioId = 2,
                    CategoriaId = 22
                },
                new Receita
                {

                    Data = new DateTime(2024, 03, 15),
                    Descricao = "Prêmio Loteria Esportiva",
                    Valor = 500.36m,
                    UsuarioId = 2,
                    CategoriaId = 23
                },
                new Receita
                {

                    Data = new DateTime(2024, 03, 20),
                    Descricao = "Investimento Bitcoin",
                    Valor = 1000.99m,
                    UsuarioId = 2,
                    CategoriaId = 24
                },
                new Receita
                {

                    Data = new DateTime(2024, 03, 25),
                    Descricao = "Benefício recebido do INSS",
                    Valor = 300.35m,
                    UsuarioId = 2,
                    CategoriaId = 25
                },
                new Receita
                {

                    Data = new DateTime(2024, 03, 31),
                    Descricao = "Outros ganhos",
                    Valor = 120.25m,
                    UsuarioId = 2,
                    CategoriaId = 26
                },
                new Receita
                {

                    Data = new DateTime(2024, 04, 05),
                    Descricao = "Salário mês de Abril",
                    Valor = 2000.36m,
                    UsuarioId = 2,
                    CategoriaId = 22
                },
                new Receita
                {

                    Data = new DateTime(2024, 04, 15),
                    Descricao = "Prêmio Jogo do Bicho",
                    Valor = 500.36m,
                    UsuarioId = 2,
                    CategoriaId = 23
                },
                new Receita
                {

                    Data = new DateTime(2024, 04, 20),
                    Descricao = "Investimento Bitcoin",
                    Valor = 1000.99m,
                    UsuarioId = 2,
                    CategoriaId = 24
                },
                new Receita
                {

                    Data = new DateTime(2024, 04, 25),
                    Descricao = "Benefício recebido em Abril INSS",
                    Valor = 300.35m,
                    UsuarioId = 2,
                    CategoriaId = 25
                },
                new Receita
                {

                    Data = new DateTime(2024, 04, 30),
                    Descricao = "Outros ganhos",
                    Valor = 120.25m,
                    UsuarioId = 2,
                    CategoriaId = 26
                },
                new Receita
                {

                    Data = new DateTime(2024, 05, 05),
                    Descricao = "Salário mês de Maio",
                    Valor = 2000.36m,
                    UsuarioId = 2,
                    CategoriaId = 22
                },
                new Receita
                {

                    Data = new DateTime(2024, 05, 15),
                    Descricao = "Prêmio Loteria",
                    Valor = 500.36m,
                    UsuarioId = 2,
                    CategoriaId = 23
                },
                new Receita
                {

                    Data = new DateTime(2024, 05, 20),
                    Descricao = "Investimento na poupança",
                    Valor = 1000.99m,
                    UsuarioId = 2,
                    CategoriaId = 24
                },
                new Receita
                {

                    Data = new DateTime(2024, 05, 25),
                    Descricao = "Benefício recebido Cashback Nubank",
                    Valor = 300.35m,
                    UsuarioId = 2,
                    CategoriaId = 25
                },
                new Receita
                {

                    Data = new DateTime(2024, 05, 31),
                    Descricao = "Outros ganhos",
                    Valor = 120.25m,
                    UsuarioId = 2,
                    CategoriaId = 26
                },
                new Receita
                {

                    Data = new DateTime(2024, 06, 05),
                    Descricao = "Salário mês de Junho",
                    Valor = 2000.36m,
                    UsuarioId = 2,
                    CategoriaId = 22
                },
                new Receita
                {

                    Data = new DateTime(2024, 06, 15),
                    Descricao = "Prêmio loteria Esportiva",
                    Valor = 500.36m,
                    UsuarioId = 2,
                    CategoriaId = 23
                },
                new Receita
                {

                    Data = new DateTime(2024, 06, 20),
                    Descricao = "Investimento em Bitcoin",
                    Valor = 1000.99m,
                    UsuarioId = 2,
                    CategoriaId = 24
                },
                new Receita
                {

                    Data = new DateTime(2024, 06, 25),
                    Descricao = "Benefício recebido Junho INSS",
                    Valor = 300.35m,
                    UsuarioId = 2,
                    CategoriaId = 25
                },
                new Receita
                {

                    Data = new DateTime(2024, 06, 30),
                    Descricao = "Outros ganhos",
                    Valor = 120.25m,
                    UsuarioId = 2,
                    CategoriaId = 26
                },
                new Receita
                {

                    Data = new DateTime(2024, 07, 05),
                    Descricao = "Salário mês de Julho",
                    Valor = 2000.36m,
                    UsuarioId = 2,
                    CategoriaId = 22
                },
                new Receita
                {

                    Data = new DateTime(2024, 07, 15),
                    Descricao = "Prêmio Jogo do Bicho",
                    Valor = 500.36m,
                    UsuarioId = 2,
                    CategoriaId = 23
                },
                new Receita
                {

                    Data = new DateTime(2024, 07, 20),
                    Descricao = "Investimento em Bitcoin",
                    Valor = 1000.99m,
                    UsuarioId = 2,
                    CategoriaId = 24
                },
                new Receita
                {

                    Data = new DateTime(2024, 07, 25),
                    Descricao = "Benefício recebido Julho INSS",
                    Valor = 300.35m,
                    UsuarioId = 2,
                    CategoriaId = 25
                },
                new Receita
                {

                    Data = new DateTime(2024, 07, 31),
                    Descricao = "Outros ganhos",
                    Valor = 120.25m,
                    UsuarioId = 2,
                    CategoriaId = 26
                },
                new Receita
                {

                    Data = new DateTime(2024, 08, 05),
                    Descricao = "Salário mês de Agosto",
                    Valor = 2000.36m,
                    UsuarioId = 2,
                    CategoriaId = 22
                },
                new Receita
                {

                    Data = new DateTime(2024, 08, 15),
                    Descricao = "Prêmio Loteria",
                    Valor = 500.36m,
                    UsuarioId = 2,
                    CategoriaId = 23
                },
                new Receita
                {

                    Data = new DateTime(2024, 08, 20),
                    Descricao = "Investimento na Poupança",
                    Valor = 1000.99m,
                    UsuarioId = 2,
                    CategoriaId = 24
                },
                new Receita
                {

                    Data = new DateTime(2024, 08, 25),
                    Descricao = "Benefício recebido em Agosto INSS",
                    Valor = 300.35m,
                    UsuarioId = 2,
                    CategoriaId = 25
                },
                new Receita
                {

                    Data = new DateTime(2024, 08, 31),
                    Descricao = "Outros ganhos",
                    Valor = 120.25m,
                    UsuarioId = 2,
                    CategoriaId = 26
                },
                new Receita
                {

                    Data = new DateTime(2024, 09, 05),
                    Descricao = "Salário mês de Setembro",
                    Valor = 2000.36m,
                    UsuarioId = 2,
                    CategoriaId = 22
                },
                new Receita
                {

                    Data = new DateTime(2024, 09, 15),
                    Descricao = "Prêmio Loteria Esportiva",
                    Valor = 500.36m,
                    UsuarioId = 2,
                    CategoriaId = 23
                },
                new Receita
                {

                    Data = new DateTime(2024, 09, 20),
                    Descricao = "Investimento em BitCoin",
                    Valor = 1000.99m,
                    UsuarioId = 2,
                    CategoriaId = 24
                },
                new Receita
                {

                    Data = new DateTime(2024, 09, 25),
                    Descricao = "Benefício",
                    Valor = 300.35m,
                    UsuarioId = 2,
                    CategoriaId = 25
                },
                new Receita
                {

                    Data = new DateTime(2024, 09, 30),
                    Descricao = "Outros ganhos",
                    Valor = 120.25m,
                    UsuarioId = 2,
                    CategoriaId = 26
                },
                new Receita
                {

                    Data = new DateTime(2024, 10, 05),
                    Descricao = "Salário mês de Outubro",
                    Valor = 2000.36m,
                    UsuarioId = 2,
                    CategoriaId = 22
                },
                new Receita
                {

                    Data = new DateTime(2024, 10, 15),
                    Descricao = "Prêmio Loteria ",
                    Valor = 500.36m,
                    UsuarioId = 2,
                    CategoriaId = 23
                },
                new Receita
                {

                    Data = new DateTime(2024, 10, 20),
                    Descricao = "Investimento na Poupança",
                    Valor = 1000.99m,
                    UsuarioId = 2,
                    CategoriaId = 24
                },
                new Receita
                {

                    Data = new DateTime(2024, 10, 25),
                    Descricao = "Benefício recebido Outubro INSS",
                    Valor = 300.35m,
                    UsuarioId = 2,
                    CategoriaId = 25
                },
                new Receita
                {

                    Data = new DateTime(2024, 10, 31),
                    Descricao = "Outros ganhos",
                    Valor = 120.25m,
                    UsuarioId = 2,
                    CategoriaId = 26
                },
                new Receita
                {

                    Data = new DateTime(2024, 11, 05),
                    Descricao = "Salário mês de Novembro",
                    Valor = 2000.36m,
                    UsuarioId = 2,
                    CategoriaId = 22
                },
                new Receita
                {

                    Data = new DateTime(2024, 11, 15),
                    Descricao = "Prêmio Raspadinha",
                    Valor = 500.36m,
                    UsuarioId = 2,
                    CategoriaId = 23
                },
                new Receita
                {

                    Data = new DateTime(2024, 11, 20),
                    Descricao = "Investimento em Bitcoin",
                    Valor = 1000.99m,
                    UsuarioId = 2,
                    CategoriaId = 24
                },
                new Receita
                {

                    Data = new DateTime(2024, 11, 25),
                    Descricao = "Benefício recebido Novembro INSS",
                    Valor = 300.35m,
                    UsuarioId = 2,
                    CategoriaId = 25
                },
                new Receita
                {

                    Data = new DateTime(2024, 11, 30),
                    Descricao = "Outros ganhos",
                    Valor = 120.25m,
                    UsuarioId = 2,
                    CategoriaId = 26
                },
                new Receita
                {

                    Data = new DateTime(2024, 12, 05),
                    Descricao = "Salário mês de Dezembro",
                    Valor = 2000.36m,
                    UsuarioId = 2,
                    CategoriaId = 22
                },
                new Receita
                {

                    Data = new DateTime(2024, 12, 15),
                    Descricao = "Prêmio Mega Senna",
                    Valor = 500.36m,
                    UsuarioId = 2,
                    CategoriaId = 23
                },
                new Receita
                {

                    Data = new DateTime(2024, 12, 20),
                    Descricao = "Investimento em Bitcoin",
                    Valor = 1000.99m,
                    UsuarioId = 2,
                    CategoriaId = 24
                },
                new Receita
                {

                    Data = new DateTime(2024, 12, 25),
                    Descricao = "Benefício recebido Dezembro INSS",
                    Valor = 300.35m,
                    UsuarioId = 2,
                    CategoriaId = 25
                },
                new Receita
                {

                    Data = new DateTime(2024, 12, 31),
                    Descricao = "Outros ganhos",
                    Valor = 120.25m,
                    UsuarioId = 2,
                    CategoriaId = 26
                },
                new Receita
                {

                    Data = new DateTime(2024, 05, 10),
                    Descricao = "Salário mês de maio",
                    Valor = 8500.98m,
                    UsuarioId = 1,
                    CategoriaId = 9
                },
                new Receita
                {

                    Data = new DateTime(2024, 05, 10),
                    Descricao = "Salário mês de maio",
                    Valor = 8500.98m,
                    UsuarioId = 2,
                    CategoriaId = 9
                },
                new Receita
                {

                    Data = new DateTime(2024, 06, 10),
                    Descricao = "Salário mês de Junho",
                    Valor = 8500.98m,
                    UsuarioId = 1,
                    CategoriaId = 9
                },
                new Receita
                {

                    Data = new DateTime(2024, 06, 10),
                    Descricao = "Salário mês de Julho",
                    Valor = 8500.98m,
                    UsuarioId = 1,
                    CategoriaId = 9
                }
            };
            _context.Receita.AddRange(receitas);
            _context.SaveChanges();
        }
    }
}
