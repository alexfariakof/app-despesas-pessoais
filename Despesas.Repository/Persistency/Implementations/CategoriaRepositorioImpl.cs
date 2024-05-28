using Domain.Entities;
using Repository.Persistency.Generic;
using Microsoft.EntityFrameworkCore;
using Repository.Abastractions;
using Domain.Entities.ValueObjects;
using System.Linq.Expressions;

namespace Repository.Persistency.Implementations;
public class CategoriaRepositorioImpl : BaseRepository<Categoria>, IRepositorio<Categoria>
{
    public RegisterContext Context { get; }
    public CategoriaRepositorioImpl(RegisterContext context) : base(context)
    {
        Context = context;
    }

    public override Categoria? Get(int id)
    {
        return Context.Categoria.Include(d => d.TipoCategoria).Include(d => d.Usuario).FirstOrDefault(d => d.Id.Equals(id));
    }

    public override List<Categoria> GetAll()
    {
        return Context.Categoria.Include(d => d.TipoCategoria).Include(d => d.Usuario).ToList();
    }

    public override void Insert(ref Categoria entity)
    {
        var tipoCategoriaId = entity.TipoCategoria.Id;
        entity.TipoCategoria = Context.Set<TipoCategoria>().First(tc => tc.Id.Equals(tipoCategoriaId));
        Context.Categoria.Add(entity);
        Context.SaveChanges();
    }

    public override void Update(ref Categoria entity)
    {
        var tipoCategoriaId = entity.TipoCategoria.Id;
        entity.TipoCategoria = Context.Set<TipoCategoria>().First(tc => tc.Id.Equals(tipoCategoriaId));
        var existingEntity = Context.Categoria.Find(entity.Id);
        Context?.Entry(existingEntity).CurrentValues.SetValues(entity);
        Context?.SaveChanges();
    }

    public override IEnumerable<Categoria> Find(Expression<Func<Categoria, bool>> expression)
    {
        return Context.Categoria.Include(d => d.TipoCategoria).Include(d => d.Usuario).Where(expression);
    }

}