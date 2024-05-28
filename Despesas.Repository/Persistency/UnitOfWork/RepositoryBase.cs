using Domain.Core;
using Microsoft.EntityFrameworkCore;
using Repository.Persistency.UnitOfWork.Abstractions;
using System.Linq.Expressions;

namespace Repository.UnitOfWork;
public sealed class UnitOfWork<T>: IRepositoy<T> where T : BaseModel
{
    private RegisterContext Context { get; set; }

    public UnitOfWork(RegisterContext context)
    {
        Context = context;
    }

    public async Task<T?> GetById(int entityId)
    {
        return await Context.Set<T>().FindAsync(entityId);
    }

    public async Task<IEnumerable<T>> GetAll()
    {
        return await Context.Set<T>().ToListAsync();
    }

    public Task Insert(T entity)
    {
        if (entity == null) 
            throw new ArgumentNullException(nameof(entity));

        return Context.AddAsync(entity).AsTask(); 
    }

    public Task? Update(T entity)
    {
        if (entity == null)
            throw new ArgumentNullException(nameof(entity));

        return Context.Update(entity) as Task;
    }

    public async void Delete(int entityId)
    {
        var entity = await GetById(entityId);
        
        if (entity == null)
            throw new ArgumentNullException(nameof(entity));

        this.Context.Remove(entity);
    }

    public bool Exists(Expression<Func<T, bool>> expression)
    {
        return this.Find(expression).Result.Any();
    }

    public async Task<IEnumerable<T>> Find(Expression<Func<T, bool>> expression)
    {
        try
        {
            return await this.Context.Set<T>().Where(expression).ToListAsync();
        }
        catch
        {
            throw;
        }        
    }
}