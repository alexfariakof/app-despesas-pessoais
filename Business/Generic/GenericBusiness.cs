
using despesas_backend_api_net_core.Domain.Entities;
using despesas_backend_api_net_core.Repositories.Generic;

namespace despesas_backend_api_net_core.Business.Generic
{
    public class GenericBusiness<T> : IBusiness<T> where T : BaseModel
    {
        private readonly IRepositorio<T> _repositorio;

        public GenericBusiness(IRepositorio<T> repositorio)
        {
            _repositorio = repositorio;
        }
        public T Create(T obj)
        {
            return _repositorio.Insert(obj);
        }

        public List<T> FindAll()
        {
            return _repositorio.GetAll();
        }

        public T FindById(int id)
        {
            return _repositorio.Get(id);
        }

        public T Update(T obj)
        {
            return _repositorio.Update(obj);
        }

        public void Delete(int id)
        {
            _repositorio.Delete(id);
        }
    }
}
