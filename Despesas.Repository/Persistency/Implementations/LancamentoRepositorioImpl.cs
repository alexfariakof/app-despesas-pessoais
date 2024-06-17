using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Repository.Persistency.Abstractions;
using System.Data;

namespace Repository.Persistency.Implementations;
public class LancamentoRepositorioImpl : ILancamentoRepositorio
{
    public RegisterContext Context { get; }
    public LancamentoRepositorioImpl(RegisterContext context)
    {
        Context = context;
    }

    public List<Lancamento> FindByMesAno(DateTime data, int idUsuario)
    {
        int mes = data.Month;
        int ano = data.Year;
        try
        {
            var despesas = Context.Despesa
                .Include(d => d.Categoria)
                .Include(tc => tc.Categoria.TipoCategoria)
                .Where(d => d.Data.Month == mes && d.Data.Year == ano && d.UsuarioId == idUsuario)
                .Select(d => new Lancamento
                {
                    Id = d.Id,
                    Categoria = d.Categoria,
                    CategoriaId = d.CategoriaId,
                    DespesaId = d.Id,
                    UsuarioId = d.UsuarioId,
                    Data = d.Data,
                    DataCriacao = DateTime.Now,
                    Valor = d.Valor,
                    Despesa = new Despesa { Id = d.Id, Descricao = d.Descricao },
                    Receita = null,
                    ReceitaId = 0
                })
                .ToList();

            var receitas = Context.Receita
                .Include(r => r.Categoria)
                .Include(tc => tc.Categoria.TipoCategoria)
                .Where(r => r.Data.Month == mes && r.Data.Year == ano && r.UsuarioId == idUsuario)
                .Select(r => new Lancamento
                {
                    Id = r.Id,
                    Categoria = r.Categoria,
                    CategoriaId = r.CategoriaId,
                    ReceitaId = r.Id,
                    UsuarioId = r.UsuarioId,
                    Data = r.Data,
                    DataCriacao = DateTime.Now,
                    Valor = r.Valor,
                    Despesa = null, 
                    Receita = new Receita { Id = r.Id, Descricao = r.Descricao },
                    DespesaId = 0
                })
                .ToList();

            var lancamentos = despesas.Concat(receitas).OrderBy(l => l.Data).ToList();
            return lancamentos;
        }
        catch
        {
            throw new Exception("LancamentoRepositorioImpl_FindByMesAno_Erro");
        }
    }
}