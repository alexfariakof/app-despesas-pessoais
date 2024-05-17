using System.Linq.Expressions;

namespace Repository.Persistency.UnitOfWork.Abstractions;
public interface IRepositoy<T> where T : class
{
    Task<T> GetById(int entityId);
    Task<IEnumerable<T>> GetAll();
    Task Insert(ref T entity);
    Task Update(ref T entity);
    void Delete(int entityId);
    bool Exists(Expression<Func<T, bool>> expression);
    Task<IEnumerable<T>> Find(Expression<Func<T, bool>> expression);
}