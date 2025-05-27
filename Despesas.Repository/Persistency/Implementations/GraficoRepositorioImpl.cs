using Domain.Entities;
using Repository.Persistency.Abstractions;
using System.Data;

namespace Repository.Persistency.Implementations;
public class GraficosRepositorioImpl : IGraficosRepositorio
{
    public RegisterContext Context { get; }

    public GraficosRepositorioImpl(RegisterContext context)
    {
        Context = context;
    }

    public Grafico GetDadosGraficoByAno(Guid idUsuario, DateTime data)
    {
        Dictionary<string, decimal> defaultSumDespesa = new Dictionary<string, decimal>
        {
            { "Janeiro", 1},
            { "Fevereiro", 2 },
            { "Março", 3 },
            { "Abril", 4 },
            { "Maio", 5},
            { "Junho", 6 },
            { "Julho", 7 },
            { "Agosto", 8 },
            { "Setembro", 9 },
            { "Outubro", 10 },
            { "Novembro", 11 },
            { "Dezembro", 12 }
        };

        Dictionary<string, decimal> defaultSumReceita = new Dictionary<string, decimal>
        {
            { "Janeiro", 12 },
            { "Fevereiro", 11 },
            { "Março", 10 },
            { "Abril", 9 },
            { "Maio", 8 },
            { "Junho", 7 },
            { "Julho", 6 },
            { "Agosto", 5 },
            { "Setembro", 4 },
            { "Outubro", 3 },
            { "Novembro", 2 },
            { "Dezembro", 1 }
        };

        try
        {
            int ano = data.Year;

            Grafico grafico = new Grafico
            {
                SomatorioDespesasPorAno = new Dictionary<string, decimal>
                {
                    { "Janeiro", Context.Despesa.Where(d => d.Data.Month == 1 && d.Data.Year == ano && d.UsuarioId == idUsuario).AsEnumerable().Sum(d => d.Valor) },
                    { "Fevereiro", Context.Despesa.Where(d => d.Data.Month == 2 && d.Data.Year == ano && d.UsuarioId == idUsuario).AsEnumerable().Sum(d => d.Valor)  },
                    { "Março", Context.Despesa.Where(d => d.Data.Month == 3 && d.Data.Year == ano && d.UsuarioId == idUsuario).AsEnumerable().Sum(d => d.Valor)  },
                    { "Abril", Context.Despesa.Where(d => d.Data.Month == 4 && d.Data.Year == ano && d.UsuarioId == idUsuario).AsEnumerable().Sum(d => d.Valor)  },
                    { "Maio", Context.Despesa.Where(d => d.Data.Month == 5 && d.Data.Year == ano && d.UsuarioId == idUsuario).AsEnumerable().Sum(d => d.Valor) },
                    { "Junho", Context.Despesa.Where(d => d.Data.Month == 6 && d.Data.Year == ano && d.UsuarioId == idUsuario).AsEnumerable().Sum(d => d.Valor)  },
                    { "Julho", Context.Despesa.Where(d => d.Data.Month == 7 && d.Data.Year == ano && d.UsuarioId == idUsuario).AsEnumerable().Sum(d => d.Valor)  },
                    { "Agosto", Context.Despesa.Where(d => d.Data.Month == 8 && d.Data.Year == ano && d.UsuarioId == idUsuario).AsEnumerable().Sum(d => d.Valor)  },
                    { "Setembro", Context.Despesa.Where(d => d.Data.Month == 9 && d.Data.Year == ano && d.UsuarioId == idUsuario).AsEnumerable().Sum(d => d.Valor)  },
                    { "Outubro", Context.Despesa.Where(d => d.Data.Month == 10 && d.Data.Year == ano && d.UsuarioId == idUsuario).AsEnumerable().Sum(d => d.Valor)  },
                    { "Novembro", Context.Despesa.Where(d => d.Data.Month == 11 && d.Data.Year == ano && d.UsuarioId == idUsuario).AsEnumerable().Sum(d => d.Valor)  },
                    { "Dezembro", Context.Despesa.Where(d => d.Data.Month == 12 && d.Data.Year == ano && d.UsuarioId == idUsuario).AsEnumerable().Sum(d => d.Valor)  },
                },
                SomatorioReceitasPorAno = new Dictionary<string, decimal>
                {
                    { "Janeiro", Context.Receita.Where(r => r.Data.Month == 1 && r.Data.Year == ano && r.UsuarioId == idUsuario).AsEnumerable().Sum(r => r.Valor) },
                    { "Fevereiro", Context.Receita.Where(r => r.Data.Month == 2 && r.Data.Year == ano && r.UsuarioId == idUsuario).AsEnumerable().Sum(r => r.Valor) },
                    { "Março", Context.Receita.Where(r => r.Data.Month == 3 && r.Data.Year == ano && r.UsuarioId == idUsuario).AsEnumerable().Sum(r => r.Valor) },
                    { "Abril", Context.Receita.Where(r => r.Data.Month == 4 && r.Data.Year == ano && r.UsuarioId == idUsuario).AsEnumerable().Sum(r => r.Valor) },
                    { "Maio", Context.Receita.Where(r => r.Data.Month == 5 && r.Data.Year == ano && r.UsuarioId == idUsuario).AsEnumerable().Sum(r => r.Valor) },
                    { "Junho", Context.Receita.Where(r => r.Data.Month == 6 && r.Data.Year == ano && r.UsuarioId == idUsuario).AsEnumerable().Sum(r => r.Valor) },
                    { "Julho", Context.Receita.Where(r => r.Data.Month == 7 && r.Data.Year == ano && r.UsuarioId == idUsuario).AsEnumerable().Sum(r => r.Valor) },
                    { "Agosto", Context.Receita.Where(r => r.Data.Month == 8 && r.Data.Year == ano && r.UsuarioId == idUsuario).AsEnumerable().Sum(r => r.Valor) },
                    { "Setembro", Context.Receita.Where(r => r.Data.Month == 9 && r.Data.Year == ano && r.UsuarioId == idUsuario).AsEnumerable().Sum(r => r.Valor) },
                    { "Outubro", Context.Receita.Where(r => r.Data.Month == 10 && r.Data.Year == ano && r.UsuarioId == idUsuario).AsEnumerable().Sum(r => r.Valor) },
                    { "Novembro", Context.Receita.Where(r => r.Data.Month == 11 && r.Data.Year == ano && r.UsuarioId == idUsuario).AsEnumerable().Sum(r => r.Valor) },
                    { "Dezembro", Context.Receita.Where(r => r.Data.Month == 12 && r.Data.Year == ano && r.UsuarioId == idUsuario).AsEnumerable().Sum(r => r.Valor) }
                }
            };

            return grafico;
        }
        catch
        {
            return new Grafico
            {
                SomatorioDespesasPorAno = defaultSumDespesa,
                SomatorioReceitasPorAno = defaultSumReceita
            };
        }
    }
}

