using despesas_backend_api_net_core.Domain.Entities;
using System.Collections.Generic;

namespace despesas_backend_api_net_core.Infrastructure.Data.Repositories.Generic
{
    public interface IRepositorio<T> where T : BaseModel
    {
        T Get(int id);
        List<T> GetAll();
        T Insert(T item);
        T Update(T item);
        bool Delete(T item);
        bool Exists(int? id);
        
    }
}
