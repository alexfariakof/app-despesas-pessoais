using despesas_backend_api_net_core.Domain.Entities;
using despesas_backend_api_net_core.Infrastructure.Data.Common;
using Microsoft.EntityFrameworkCore;
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

            string sql = "Select ABS(Checksum(NewId()) %10000) as id, lancamentos.* From ( " + 
                         "Select d.idUsuario, data, idCategoria, valor*-1 as valor, d.id as idDespesa, 0 as idReceita, d.descricao, c.descricao as categoria " +
                         "  FROM Despesa d " +
                         " Inner Join Categoria c on d.idCategoria = c.id " +
                         " where d.idUsuario = {0} " +
                         "   and Month(data) = {1} " +
                         "   and  Year(data) = {2}" +
                         " union " +
                         "Select r.idUsuario, data, idCategoria, valor,0 as idDespesa, r.id as idReceita, r.descricao, c.descricao as categoria " +
                         "  FROM Receita r " +
                         " Inner Join Categoria c on r.idCategoria = c.id " +
                         " where r.idUsuario = {0}" +
                         "   and Month(data) = {1} " +
                         "   and  Year(data) = {2} " +
                         ") lancamentos ";

            using (_context)
            {
                try
                {
                    var list = _context.Lancamento.FromSqlRaw<Lancamento>(sql, idUsuario, data.Month, data.Year).ToList();
                    return list.OrderBy(item => item.Data).ThenBy(item => item.Categoria).ToList();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public decimal GetSaldo(int idUsuario)
        {
            using (var command = _context.Database.GetDbConnection().CreateCommand())
            {
                decimal value = 0;
                try
                {
                    command.CommandText = @"Select (SELECT sum(valor) FROM Receita Where idUsuario = {0}) - (SELECT sum(valor) FROM Despesa Where idUsuario = {0})"; ;
                    command.CommandType = CommandType.Text;
                    command.Parameters.Add(idUsuario);
                    _context.Database.OpenConnection();
                    using (var result = command.ExecuteReader())
                    {
                        if (result.Read())
                        {
                            value  = result.GetDecimal(0);
                        }
                    }
                }
                catch 
                {
                    return 0;
                }
                return value;
            }            
        }
    }
}

