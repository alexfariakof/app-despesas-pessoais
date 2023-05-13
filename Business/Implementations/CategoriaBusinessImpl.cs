using despesas_backend_api_net_core.Business.Generic;
using despesas_backend_api_net_core.Domain.Entities;
using despesas_backend_api_net_core.Infrastructure.Data.Repositories.Generic;

namespace despesas_backend_api_net_core.Business.Implementations
{
    public class CategoriaBusinessImpl : IBusiness<Categoria>
    {
        private readonly IRepositorio<Categoria> _repositorio;

        public CategoriaBusinessImpl(IRepositorio<Categoria> repositorio)
        {
            _repositorio = repositorio;
        }
        public Categoria Create(Categoria obj)
        {
            return _repositorio.Insert(obj);
        }

        public List<Categoria> FindAll()
        {
            return _repositorio.GetAll();
        }      

        public Categoria FindById(int id)
        {
            return _repositorio.Get(id);
        }

        public Categoria Update(Categoria obj)
        {           

            return _repositorio.Update(obj);
        }

        public void Delete(int id)
        {
            _repositorio.Delete(id);
        }

    }
}
