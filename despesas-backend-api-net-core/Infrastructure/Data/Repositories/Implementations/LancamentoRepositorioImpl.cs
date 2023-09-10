using AutoMapper;
using despesas_backend_api_net_core.Domain.Entities;
using despesas_backend_api_net_core.Infrastructure.Data.Common;
using despesas_backend_api_net_core.Infrastructure.ExtensionMethods;
using Microsoft.EntityFrameworkCore;
using MySql.Data.MySqlClient;
using System.Data;

namespace despesas_backend_api_net_core.Infrastructure.Data.Repositories.Implementations
{
    public class LancamentoRepositorioImpl : ILancamentoRepositorio
    {
        private readonly RegisterContext _context;

        public LancamentoRepositorioImpl(RegisterContext context)
        {
            _context = context;
        }

        public List<Lancamento> FindByMesAno(DateTime data, int idUsuario)
        {
            int mes = data.Month;
            int ano = data.Year;

            string sql = $"CALL LancamentosPorMesAno({idUsuario},{mes}, {ano})";
            using (_context)
            {
                try
                {
                    var list = _context.Lancamento.FromSqlRaw(sql).ToList();
                    return list.OrderBy(item => item.Data).ThenBy(item => item.Categoria).ToList();
                }
                catch
                {
                    throw new Exception("Erro ao gerar registros de lançamentos!");
                }
            }
        }

        public decimal GetSaldo(int idUsuario)
        {
            using (var command = _context.Database.GetDbConnection().CreateCommand())
            {
                decimal saldo = 0;                
                _context.Database.OpenConnection();
                try
                {
                    command.CommandText = $"CALL GetSaldoByIdUsuario({idUsuario})";
                    command.CommandType = CommandType.Text;

                    using (var result = command.ExecuteReader())
                    {
                        if (result.Read())
                        {
                            decimal.TryParse(result.GetString(0), out saldo);
                        }
                    }
                }
                catch
                {
                    throw new Exception("Erro ao consultar saldo!");
                }
                finally
                {
                    _context.Database.CloseConnection();
                }
                
                return saldo;
            }
        }

        public Grafico GetDadosGraficoByAno(int idUsuario, DateTime data)
        {
            int ano = data.Year;
            Dictionary<string, decimal> somatorioDespesaPorAno = new Dictionary<string, decimal>
            {
                { "Janeiro", 0 },
                { "Fevereiro", 0 },
                { "Março", 0 },
                { "Abril", 0 },
                { "Maio", 0 },
                { "Junho", 0 },
                { "Julho", 0 },
                { "Agosto", 0 },
                { "Setembro", 0 },
                { "Outubro", 0 },
                { "Novembro", 0 },
                { "Dezembro", 0 }
            };
            Dictionary<string, decimal> somatorioReceitaProAno = new Dictionary<string, decimal>
            {
                { "Janeiro", 0 },
                { "Fevereiro", 0 },
                { "Março", 0 },
                { "Abril", 0 },
                { "Maio", 0 },
                { "Junho", 0 },
                { "Julho", 0 },
                { "Agosto", 0 },
                { "Setembro", 0 },
                { "Outubro", 0 },
                { "Novembro", 0 },
                { "Dezembro", 0 }
            };
            Grafico grafico = new Grafico();

            using (var command = _context.Database.GetDbConnection().CreateCommand())
            {
                string sqlDespesa = $"CALL SomatorioDespesasPorAno({idUsuario}, {ano})";
                string sqlReceita = $"CALL SomatorioReceitasPorAno({idUsuario}, {ano})";

                _context.Database.OpenConnection();
                try
                {
                    command.CommandText = sqlDespesa;
                    command.CommandType = CommandType.Text;


                    using (var result = command.ExecuteReader())
                    {
                        if (result.Read())
                        {
                            somatorioDespesaPorAno = new Dictionary<string, decimal>
                            {
                                { "Janeiro", result.GetDecimal("Janeiro") },
                                { "Fevereiro", result.GetDecimal("Fevereiro") },
                                { "Marco", result.GetDecimal("Marco") },
                                { "Abril", result.GetDecimal("Abril")},
                                { "Maio", result.GetDecimal("Maio") },
                                { "Junho", result.GetDecimal("Junho") },
                                { "Julho", result.GetDecimal("Julho") },
                                { "Agosto", result.GetDecimal("Agosto") },
                                { "Setembro", result.GetDecimal("Setembro") },
                                { "Outubro", result.GetDecimal("Outubro") },
                                { "Novembro", result.GetDecimal("Novembro") },
                                { "Dezembro", result.GetDecimal("Dezembro") }
                            };
                            grafico.SomatorioDespesasPorAno = somatorioDespesaPorAno;
                        }
                        
                    }
                    
                    command.CommandText = sqlReceita;
                    using (var result = command.ExecuteReader())
                    {
                        if (result.Read())
                        {
                            somatorioReceitaProAno = new Dictionary<string, decimal>
                            {
                                { "Janeiro", result.GetDecimal("Janeiro") },
                                { "Fevereiro", result.GetDecimal("Fevereiro") },
                                { "Marco", result.GetDecimal("Marco") },
                                { "Abril", result.GetDecimal("Abril")},
                                { "Maio", result.GetDecimal("Maio") },
                                { "Junho", result.GetDecimal("Junho") },
                                { "Julho", result.GetDecimal("Julho") },
                                { "Agosto", result.GetDecimal("Agosto") },
                                { "Setembro", result.GetDecimal("Setembro") },
                                { "Outubro", result.GetDecimal("Outubro") },
                                { "Novembro", result.GetDecimal("Novembro") },
                                { "Dezembro", result.GetDecimal("Dezembro") }
                            };
                            grafico.SomatorioReceitasPorAno = somatorioReceitaProAno;
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("Erro ao consultar saldo!", ex);
                }
                finally
                {
                    _context.Database.CloseConnection();
                }                

                return grafico;
            }        
            
        }
    }
}

