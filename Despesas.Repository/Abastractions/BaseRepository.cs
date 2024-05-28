using Domain.Core;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Repository.Abastractions;
public abstract class BaseRepository<TEntity> where TEntity : BaseModel, new()
{
    private DbContext Context { get; set; }

    protected BaseRepository(DbContext context)
    {
        Context = context;
    }

    public virtual void Insert(ref TEntity entity)
    {
        Context.Add(entity);
        Context.SaveChanges();
    }

    public virtual void Update(ref TEntity entity)
    {
        var existingEntity = this.Context.Set<TEntity>().Find(entity.Id);
        this.Context?.Entry(existingEntity).CurrentValues.SetValues(entity);
        this.Context?.SaveChanges();
    }

    public virtual bool Delete(TEntity entity)
    {
        try
        {
            var existingEntity = this.Context.Set<TEntity>().Find(entity.Id);
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

    public virtual IEnumerable<TEntity> GetAll()
    {
        return Context.Set<TEntity>().ToList();
    }

    public virtual TEntity? Get(int id)
    {
        return Context.Set<TEntity>().Find(id) ?? new();
    }

    public virtual IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> expression)
    {
        return Context.Set<TEntity>().Where(expression);
    }

    public virtual bool Exists(int? id)
    {
        return this.Get(id.Value) != null;
    }

    public virtual bool Exists(Expression<Func<TEntity, bool>> expression)
    {
        return Find(expression).Any();
    }
}