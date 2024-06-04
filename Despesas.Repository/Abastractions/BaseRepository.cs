using Domain.Core;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Repository.Abastractions;
public abstract class BaseRepository<T> where T : BaseModel, new()
{
    private DbContext Context { get; set; }

    protected BaseRepository(DbContext context)
    {
        Context = context;
    }

    public virtual void Insert(ref T entity)
    {
        Context.Add(entity);
        Context.SaveChanges();
    }

    public virtual void Update(ref T entity)
    {
        var existingEntity = this.Context.Set<T>().Find(entity.Id);
        this.Context?.Entry(existingEntity).CurrentValues.SetValues(entity);
        this.Context?.SaveChanges();
    }

    public virtual bool Delete(T entity)
    {
        try
        {
            var existingEntity = this.Context.Set<T>().Find(entity.Id);
            if (existingEntity != null)
            {
                this.Context.Remove(existingEntity);
                this.Context.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }
        }
        catch
        {
            return false;
        }
    }

    public virtual IEnumerable<T> GetAll()
    {
        return Context.Set<T>().ToList();
    }

    public virtual T? Get(int id)
    {
        return Context.Set<T>().Find(id) ?? new();
    }

    public virtual IEnumerable<T> Find(Expression<Func<T, bool>> expression)
    {
        return Context.Set<T>().Where(expression);
    }

    public virtual bool Exists(int? id)
    {
        return this.Get(id.Value) != null;
    }

    public virtual bool Exists(Expression<Func<T, bool>> expression)
    {
        return Find(expression).Any();
    }
}