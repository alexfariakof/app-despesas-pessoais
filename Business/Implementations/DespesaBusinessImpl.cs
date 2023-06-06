using despesas_backend_api_net_core.Business.Generic;
using despesas_backend_api_net_core.Domain.VM;
using despesas_backend_api_net_core.Domain.Entities;
using despesas_backend_api_net_core.Infrastructure.Data.EntityConfig;
using despesas_backend_api_net_core.Infrastructure.Data.Repositories.Generic;

namespace despesas_backend_api_net_core.Business.Implementations
{
    public class DespesaBusinessImpl : IBusiness<DespesaVM>
    {
        private readonly IRepositorio<Despesa> _repositorio;
        private readonly DespesaMap _converter;
        public DespesaBusinessImpl(IRepositorio<Despesa> repositorio)
        {
            _repositorio = repositorio;
            _converter = new DespesaMap();
        }
        public DespesaVM Create(DespesaVM obj)
        {
            Despesa despesa = _repositorio.Insert(_converter.Parse(obj));
            return _converter.Parse(despesa);
        }

        public List<DespesaVM> FindAll()
        {
            return _converter.ParseList(_repositorio.GetAll());
        }

        public DespesaVM FindById(int id)
        {
            return _converter.Parse(_repositorio.Get(id));
        }

        public DespesaVM Update(DespesaVM obj)
        {
            Despesa despesa = _repositorio.Update(_converter.Parse(obj));
            return _converter.Parse(despesa);
        }

        public bool Delete(int id)
        {
            return _repositorio.Delete(id);
        }

        public List<DespesaVM> FindByIdUsuario(int idUsuario)
        {
            return _converter.ParseList(_repositorio.GetAll().FindAll(p => p.UsuarioId.Equals(idUsuario)));
        }
    }
}
