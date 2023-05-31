using despesas_backend_api_net_core.Domain.Entities;
using despesas_backend_api_net_core.Infrastructure.Data.Common;
using Google.Protobuf.WellKnownTypes;
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
            //Mysql  CONV(SUBSTRING(uuid(), 4, 4), 16, 10) as id
            //SqlServer ABS(Checksum(NewId()) %10000) as id
            string sql = "Select cast(CONV(SUBSTRING(uuid(), 4, 4), 16, 10) as UNSIGNED) as id, lancamentos.* From ( " +
                         "Select d.UsuarioId, data, d.CategoriaId, valor*-1 as valor, 'Despesas' as Tipo, d.id as DespesaId, 0 as ReceitaId, d.descricao, c.descricao as categoria " +
                         "  FROM Despesa d " +
                         " Inner Join Categoria c on d.CategoriaId = c.id " +
                         " where d.UsuarioId = @UsuarioId " +
                         "   and Month(data) = @mes " +
                         "   and  Year(data) = @ano " +
                         " union " +
                         "Select r.UsuarioId, data, r.CategoriaId, valor, 'Receitas' as Tipo, 0 as DespesaId, r.id as ReceitaId, r.descricao, cr.descricao as categoria " +
                         "  FROM Receita r " +
                         " Inner Join Categoria cr on r.CategoriaId = cr.id " +
                         " where r.UsuarioId = @UsuarioId " +
                         "   and Month(data) = @mes " +
                         "   and  Year(data) = @ano " +
                         ") lancamentos ";

            using (_context)
            {
                try
                {
                    var list = _context.Lancamento.FromSqlRaw<Lancamento>(sql, new MySqlParameter("@UsuarioId", idUsuario), new MySqlParameter("@mes", data.Month), new MySqlParameter("@ano", data.Year)).ToList();
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
                decimal saldoReceita = 0;
                decimal saldoDespesa = 0;
                _context.Database.OpenConnection();
                try
                {
                    command.CommandText = @"SELECT CASE WHEN sum(valor) >= 0 THEN sum(valor) ELSE 0 END as Saldo FROM Receita Where UsuarioId = @UsuarioId";
                    command.CommandType = CommandType.Text;
                    command.Parameters.Add(new MySqlParameter("@UsuarioId", idUsuario));

                    using (var result = command.ExecuteReader())
                    {
                        if (result.Read())
                        {
                            saldoReceita = result.GetDecimal(0);
                        }
                    }
                    command.CommandText = @"SELECT CASE WHEN sum(valor) >= 0 THEN sum(valor) ELSE 0 END as Saldo FROM Despesa Where UsuarioId = @UsuarioId";
                    using (var result = command.ExecuteReader())
                    {
                        if (result.Read())
                        {
                            saldoDespesa = result.GetDecimal(0);
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
                
                return saldoReceita - saldoDespesa;
            }
        }

    }
}

