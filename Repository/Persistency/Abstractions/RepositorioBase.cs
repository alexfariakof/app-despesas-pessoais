using Domain.Core;
using System.Linq.Expressions;

namespace Repository.Persistency.Abstractions;
public abstract class RepositorioBase<T> where T : BaseModel, new()
{
    protected RegisterContext Context { get; set; }

    public RepositorioBase(RegisterContext context)
    {
        Context = context;
    }

    public virtual T Get(int id)
    {
        return this.Context.Set<T>().Find(id) ?? new();
    }

    public virtual List<T> GetAll()
    {
        return this.Context.Set<T>().ToList();
    }

    public virtual void Insert(ref T entity)
    {
        this.Context.Add(entity);
        this.Context.SaveChanges();
    }

    public virtual void Update(ref T entity)
    {
        var existingEntity = this.Context.Set<T>().Find(entity.Id);
        this.Context.Entry(existingEntity).CurrentValues.SetValues(entity);
        this.Context.SaveChanges();
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

    public virtual IEnumerable<T> Find(Expression<Func<T, bool>> expression)
    {
        return this.Context.Set<T>().Where(expression);
    }

    public virtual bool Exists(int? id)
    {
        return this.Get(id.Value) != null;
    }

    public virtual bool Exists(Expression<Func<T, bool>> expression)
    {
        return this.Find(expression).Any();
    }
}