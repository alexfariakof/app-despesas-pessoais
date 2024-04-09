using Domain.Entities;
using Repository.Mapping;
using System.Data;

namespace Repository.Persistency.Implementations;
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
        try
        {
            var despesas = _context.Despesa.Where(d => d.Data.Month == mes && d.Data.Year == ano && d.UsuarioId == idUsuario).ToList();
            foreach(var despesa in despesas)
                despesa.Categoria = _context.Categoria.Where(c => c.Id == despesa.CategoriaId).First();
            var receitas = _context.Receita.Where(r => r.Data.Month == mes && r.Data.Year == ano && r.UsuarioId == idUsuario).ToList();
            foreach (var receita in receitas)
                receita.Categoria = _context.Categoria.Where(c => c.Id == receita.CategoriaId).First();

            var lancamentos = new LancamentoMap().ParseList(despesas)
                .Union(new LancamentoMap().ParseList(receitas))
                .ToList();
            return lancamentos.OrderBy(l => l.Data).ToList();
        }
        catch
        {
            throw new Exception("LancamentoRepositorioImpl_FindByMesAno_Erro");
        }
    }

}

