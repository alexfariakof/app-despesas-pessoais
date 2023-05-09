using despesas_backend_api_net_core.Domain.Entities;


namespace despesas_backend_api_net_core.Business.Generic
{
    public interface IBusiness<T> where T : BaseModel
    {
        T Create(T obj);
        T FindById(int id);
        List<T> FindAll();
        T Update(T obj);
        void Delete(int id);
    }
}
