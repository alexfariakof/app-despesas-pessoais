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
        private readonly IRepositorio<Categoria> _repoCategoria;
        private readonly DespesaMap _converter;
        public DespesaBusinessImpl(IRepositorio<Despesa> repositorio, IRepositorio<Categoria> repoCategoria)
        {
            _repositorio = repositorio;
            _repoCategoria = repoCategoria;
            _converter = new DespesaMap();
        }
        public DespesaVM Create(DespesaVM obj)
        {
            if (!IsCategoriaValid(obj))
                throw new Exception("Categoria não existe cadastrada para este usuário!");

            Despesa despesa = _repositorio.Insert(_converter.Parse(obj));
            return _converter.Parse(despesa);
        }

        public List<DespesaVM> FindAll(int idUsuario)
        {
            var despesas = _repositorio.GetAll().FindAll(d => d.UsuarioId == idUsuario);
            foreach( var despesa in despesas)
                despesa.Categoria = _repoCategoria.Get(despesa.CategoriaId);            
            return _converter.ParseList(despesas);
        }

        public DespesaVM FindById(int id, int idUsuario)
        {
            var despesa = _repositorio.Get(id);
            despesa.Categoria = _repoCategoria.Get(despesa.CategoriaId);
            var despesaVM = _converter.Parse(despesa);

            if (despesaVM.IdUsuario == idUsuario)
                return despesaVM;
            return null;
        }

        public DespesaVM Update(DespesaVM obj)
        {
            if (!IsCategoriaValid(obj))
                throw new Exception("Categoria não existe cadastrada para este usuário!");

            Despesa despesa = _repositorio.Update(_converter.Parse(obj));
            despesa.Categoria = _repoCategoria.Get(despesa.CategoriaId);
            return _converter.Parse(despesa);
        }

        public bool Delete(DespesaVM obj)
        {
            Despesa despesa = _repositorio.Update(_converter.Parse(obj));
            return _repositorio.Delete(despesa);
        }


        private bool IsCategoriaValid(DespesaVM obj)
        {
            return _repoCategoria.GetAll().Find(c => c.UsuarioId == obj.IdUsuario) != null ? true : false;
        }
    }
}
