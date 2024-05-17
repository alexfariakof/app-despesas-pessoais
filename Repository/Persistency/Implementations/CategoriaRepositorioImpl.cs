using Domain.Entities;
using Repository.Persistency.Generic;
using Repository.Persistency.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace Repository.Persistency.Implementations;
public class CategoriaRepositorioImpl : RepositorioBase<Categoria>, IRepositorio<Categoria>
{
    private readonly RegisterContext _context;    
    public CategoriaRepositorioImpl(RegisterContext context) : base(context)
    {
        _context = context;
    }

    public override Categoria Get(int id)
    {
        return _context.Categoria
                .Include(d => d.TipoCategoria)
                .Include(d => d.Usuario).First(d => d.Id.Equals(id)) ?? new();
    }

    public override List<Categoria> GetAll()
    {
        return _context.Categoria
                .Include(d => d.TipoCategoria)
                .Include(d => d.Usuario).ToList() ?? new();
    }

    public override void Insert(ref Categoria entity)
    {
        var tipoCategoriaId = entity.TipoCategoria.Id;
        entity.TipoCategoria = _context.TipoCategoria.First(tc => tc.Id.Equals(tipoCategoriaId));
        _context.Categoria.Add(entity);
        _context.SaveChanges();
    }

    public override void Update(ref Categoria entity)
    {
        var tipoCategoriaId = entity.TipoCategoria.Id;
        entity.TipoCategoria = _context.TipoCategoria.First(tc => tc.Id.Equals(tipoCategoriaId));
        var existingEntity = _context.Categoria.Find(entity.Id);
        _context.Entry(existingEntity).CurrentValues.SetValues(entity);
        _context.SaveChanges();
    }
}