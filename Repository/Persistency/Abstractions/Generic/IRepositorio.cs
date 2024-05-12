namespace Repository.Persistency.Generic;
public interface IRepositorio<T> where T : class
{
    public T Get(int id);
    public List<T> GetAll();
    public void Insert(ref T item);
    public void Update(ref T item);
    public bool Delete(T item);
    public bool Exists(int? id);    
}