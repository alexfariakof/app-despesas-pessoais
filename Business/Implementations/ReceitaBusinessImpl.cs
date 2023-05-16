using despesas_backend_api_net_core.Business.Generic;
using despesas_backend_api_net_core.Domain.Entities;
using despesas_backend_api_net_core.Infrastructure.Data.Repositories.Generic;

namespace despesas_backend_api_net_core.Business.Implementations
{
    public class ReceitaBusinessImpl : IBusiness<Receita>
    {
        private readonly IRepositorio<Receita> _repositorio;

        public ReceitaBusinessImpl(IRepositorio<Receita> repositorio)
        {
            _repositorio = repositorio;
        }
        public Receita Create(Receita obj)
        {
            return _repositorio.Insert(obj);
        }

        public List<Receita> FindAll()
        {
            return _repositorio.GetAll();
        }      

        public Receita FindById(int id)
        {
            return _repositorio.Get(id);
        }

        public Receita Update(Receita obj)
        {           

            return _repositorio.Update(obj);
        }

        public void Delete(int id)
        {
            _repositorio.Delete(id);
        }
    }
}
