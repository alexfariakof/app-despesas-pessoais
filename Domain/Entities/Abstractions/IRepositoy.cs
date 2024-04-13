using System.Linq.Expressions;

namespace Domain.Entities.Abstractions;
public interface IRepositoy<T> where T : class
{
    Task<T> GetById(int entityId);
    Task<IEnumerable<T>> GetAll();
    void Insert(ref T entity);
    void Update(ref T entity);
    void Delete(int entityId);
    bool Exists(Expression<Func<T, bool>> expression);
    Task<IEnumerable<T>> Find(Expression<Func<T, bool>> expression);
}