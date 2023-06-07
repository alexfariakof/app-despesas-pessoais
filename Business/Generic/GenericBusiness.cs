
using despesas_backend_api_net_core.Domain.Entities;
using despesas_backend_api_net_core.Domain.VM;
using despesas_backend_api_net_core.Infrastructure.Data.Repositories.Generic;

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

        public bool Delete(int id)
        {
            return _repositorio.Delete(id);
        }

        public List<T> FindByIdUsuario(int idUsuario)
        {
            return new List<T>();
        }
    }
}
