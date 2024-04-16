using Domain.Core;

namespace Repository.Persistency.Generic;
public interface IRepositorio<T> where T : BaseModel
{
    T Get(int id);
    List<T> GetAll();
    void Insert(ref T item);
    void Update(ref T item);
    bool Delete(T item);
    bool Exists(int? id);    
}