using Domain.Entities;
using Repository.Persistency.Generic;
using Repository.Persistency.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace Repository.Persistency.Implementations;
public class DespesaRepositorioImpl : RepositorioBase<Despesa>, IRepositorio<Despesa>
{
    private readonly RegisterContext _context;    
    public DespesaRepositorioImpl(RegisterContext context) : base(context)
    {
        _context = context;
    }

    public override Despesa Get(int id)
    {
        return _context.Despesa
                .Include(d => d.Categoria)
                .Include(d => d.Usuario).First(d => d.Id.Equals(id)) ?? new();
    }

    public override List<Despesa> GetAll()
    {
        return _context.Despesa
                .Include(d => d.Categoria)
                .Include(d => d.Usuario).ToList() ?? new();
    }

    public override void Insert(ref Despesa entity)
    {
        var categoriaId = entity.CategoriaId;
        entity.Categoria = _context.Categoria.First(c => c.Id.Equals(categoriaId));
        _context.Add(entity);
        _context.SaveChanges();
    }

    public override void Update(ref Despesa entity)
    {
        var categoriaId = entity.CategoriaId;
        entity.Categoria = _context.Categoria.First(c => c.Id.Equals(categoriaId));
        var existingEntity = _context.Despesa.Find(entity.Id);
        _context.Entry(existingEntity).CurrentValues.SetValues(entity);
        _context.SaveChanges();
    }
}