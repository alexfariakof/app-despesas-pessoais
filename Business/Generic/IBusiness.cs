using despesas_backend_api_net_core.Domain.Entities;
using despesas_backend_api_net_core.Domain.VM;


namespace despesas_backend_api_net_core.Business.Generic
{
    public interface IBusiness<T> where T : BaseModel
    {
        T Create(T obj);
        T FindById(int id, int idUsuario);
        List<T> FindByIdUsuario(int idUsuario);
        List<T> FindAll(int idUsuario);
        T Update(T obj);
        bool Delete(int id);
    }
}
