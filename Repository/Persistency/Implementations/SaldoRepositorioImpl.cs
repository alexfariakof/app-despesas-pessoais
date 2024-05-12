using System.Data;
using Repository.Persistency.Abstractions;

namespace Repository.Persistency.Implementations;
public class SaldoRepositorioImpl : ISaldoRepositorio
{
    private readonly RegisterContext _context;
    public SaldoRepositorioImpl(RegisterContext context)
    {
        _context = context;
    }
    public decimal GetSaldo(int idUsuario)
    {
        try
        {
            decimal sumDespesa = _context.Despesa.Where(d => d.UsuarioId == idUsuario).AsEnumerable().Sum(d => d.Valor);
            decimal sumReceita = _context.Receita.Where(r => r.UsuarioId == idUsuario).AsEnumerable().Sum(r => r.Valor);

            return (sumReceita - sumDespesa);
        }
        catch
        {
            throw new Exception("SaldoRepositorioImpl_GetSaldo_Erro");
        }
    }
    public decimal GetSaldoByAno(DateTime mesAno, int idUsuario)
    {
        int ano = mesAno.Year;

        try
        {
            decimal sumDespesa = _context.Despesa.Where(d => d.UsuarioId == idUsuario && d.Data.Year == ano).AsEnumerable().Sum(d => d.Valor);
            decimal sumReceita = _context.Receita.Where(r => r.UsuarioId == idUsuario && r.Data.Year ==  ano).AsEnumerable().Sum(r => r.Valor);

            return (sumReceita - sumDespesa);
        }
        catch
        {
            throw new Exception("SaldoRepositorioImpl_GetSaldoByAno_Erro");
        }
    }
    public decimal GetSaldoByMesAno(DateTime mesAno, int idUsuario)
    {
        int mes = mesAno.Month;
        int ano = mesAno.Year;

        try
        {
            decimal sumDespesa = _context.Despesa.Where(d => d.UsuarioId == idUsuario && d.Data.Year == ano && d.Data.Month == mes).AsEnumerable().Sum(d => d.Valor);
            decimal sumReceita = _context.Receita.Where(r => r.UsuarioId == idUsuario && r.Data.Year == ano && r.Data.Month == mes).AsEnumerable().Sum(r => r.Valor);

            return (sumReceita - sumDespesa);
        }
        catch
        {
            throw new Exception("SaldoRepositorioImpl_GetSaldoByMesAno_Erro");
        }
    }
}

