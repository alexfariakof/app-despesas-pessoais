using despesas_backend_api_net_core.Business.Generic;
using despesas_backend_api_net_core.Domain.Entities;
using despesas_backend_api_net_core.Domain.VO;
using despesas_backend_api_net_core.Infrastructure.Data.EntityConfig;
using despesas_backend_api_net_core.Infrastructure.Data.Repositories.Generic;

namespace despesas_backend_api_net_core.Business.Implementations
{
    public class UsuarioBusinessImpl : IBusiness<UsuarioVO>
    {
        private IRepositorio<Usuario> _repositorio;

        public UsuarioBusinessImpl(IRepositorio<Usuario> repositorio)
        {
            _repositorio = repositorio;

        }
        public Usuario Create(UsuarioVO usuarioVO)
        {

            var usuario = new Usuario
            {
                Id = usuarioVO.Id,
                Nome = usuarioVO.Nome,
                SobreNome = usuarioVO.SobreNome,
                Email = usuarioVO.Email,
                Telefone = usuarioVO.Telefone,
                StatusUsuario = StatusUsuario.Ativo
            };
            return _repositorio.Insert(usuario);
        }

        public List<Usuario> FindAll()
        {
            return _repositorio.GetAll();
        }      

        public Usuario FindById(int idUsuario)
        {
            return _repositorio.Get(idUsuario);
        }

        public Usuario Update(UsuarioVO usuarioVO)
        {
            var usuario = new Usuario
            {
                Id = usuarioVO.Id,
                Nome = usuarioVO.Nome,
                SobreNome = usuarioVO.SobreNome,
                Email = usuarioVO.Email,
                Telefone = usuarioVO.Telefone,
                StatusUsuario = StatusUsuario.Ativo
            };


            return _repositorio.Update(usuario);
        }

        public void Delete(int idUsuario)
        {
            _repositorio.Delete(idUsuario);
        }
    }
}
