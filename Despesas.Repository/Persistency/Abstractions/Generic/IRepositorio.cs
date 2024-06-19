using System.Linq.Expressions;

namespace Repository.Persistency.Generic;
public interface IRepositorio<T> where T : class
{
    public T Get(int id);
    public List<T> GetAll();
    public void Insert(ref T entity);
    public void Update(ref T entity);
    public bool Delete(T entity);
    public bool Exists(int id);
    IEnumerable<T>? Find(Expression<Func<T, bool>> expression);
}