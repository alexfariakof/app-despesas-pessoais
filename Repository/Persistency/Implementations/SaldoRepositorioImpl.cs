using System.Data;

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
            decimal sumDespesa = _context.Despesa.Where(d => d.UsuarioId == idUsuario).ToList().Sum(d => d.Valor);
            decimal sumReceita = _context.Receita.Where(r => r.UsuarioId == idUsuario).ToList().Sum(r => r.Valor);

            return (sumReceita - sumDespesa);
        }
        catch
        {
            throw new Exception("SaldoRepositorioImpl_GetSaldo_Erro");
        }
    }
    public decimal GetSaldoByAno(DateTime data, int idUsuario)
    {
        int ano = data.Year;

        try
        {
            decimal sumDespesa = _context.Despesa.Where(d => d.UsuarioId == idUsuario && d.Data.Year == ano).ToList().Sum(d => d.Valor);
            decimal sumReceita = _context.Receita.Where(r => r.UsuarioId == idUsuario && r.Data.Year ==  ano).ToList().Sum(r => r.Valor);

            return (sumReceita - sumDespesa);
        }
        catch
        {
            throw new Exception("SaldoRepositorioImpl_GetSaldoByAno_Erro");
        }
    }
    public decimal GetSaldoByMesAno(DateTime data, int idUsuario)
    {
        int mes = data.Month;
        int ano = data.Year;

        try
        {
            decimal sumDespesa = _context.Despesa.Where(d => d.UsuarioId == idUsuario && d.Data.Year == ano && d.Data.Month == mes).ToList().Sum(d => d.Valor);
            decimal sumReceita = _context.Receita.Where(r => r.UsuarioId == idUsuario && r.Data.Year == ano && r.Data.Month == mes).ToList().Sum(r => r.Valor);

            return (sumReceita - sumDespesa);
        }
        catch
        {
            throw new Exception("SaldoRepositorioImpl_GetSaldoByMesAno_Erro");
        }
    }
}

