using Domain.Core;

namespace Business.Generic;
public interface IBusiness<T> where T : BaseModel
{
    T Create(T obj);
    T FindById(int id, int idUsuario);
    List<T> FindAll(int idUsuario);
    T Update(T obj);
    bool Delete(T obj);
}
