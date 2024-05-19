using Domain.Entities;
using Repository.Persistency.Generic;
using Microsoft.EntityFrameworkCore;
using Repository.Abastractions;
using Domain.Entities.ValueObjects;
using System.Linq.Expressions;

namespace Repository.Persistency.Implementations;
public class CategoriaRepositorioImpl : BaseRepository<Categoria>, IRepositorio<Categoria>
{
    private readonly RegisterContext _context;    
    public CategoriaRepositorioImpl(RegisterContext context) : base(context)
    {
        _context = context;
    }

    public override Categoria Get(int id)
    {
        return _context.Categoria.Include(d => d.TipoCategoria).Include(d => d.Usuario).FirstOrDefault(d => d.Id.Equals(id));
    }

    public override List<Categoria> GetAll()
    {
        return _context.Categoria.Include(d => d.TipoCategoria).Include(d => d.Usuario).ToList() ?? new();
    }

    public override void Insert(ref Categoria entity)
    {
        var tipoCategoriaId = entity.TipoCategoria.Id;
        entity.TipoCategoria = this._context.Set<TipoCategoria>().First(tc => tc.Id.Equals(tipoCategoriaId));
        _context.Categoria.Add(entity);
        _context.SaveChanges();
    }

    public override void Update(ref Categoria entity)
    {
        var tipoCategoriaId = entity.TipoCategoria.Id;
        entity.TipoCategoria = this._context.Set<TipoCategoria>().First(tc => tc.Id.Equals(tipoCategoriaId));
        var existingEntity = _context.Categoria.Find(entity.Id);
        _context.Entry(existingEntity).CurrentValues.SetValues(entity);
        _context.SaveChanges();
    }

    public override IEnumerable<Categoria>? Find(Expression<Func<Categoria, bool>> expression)
    {
        return this._context.Categoria.Include(d => d.TipoCategoria).Include(d => d.Usuario).Where(expression);
    }

}