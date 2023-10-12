using despesas_backend_api_net_core.Business.Generic;
using despesas_backend_api_net_core.Domain.Entities;
using despesas_backend_api_net_core.Domain.VM;
using despesas_backend_api_net_core.Infrastructure.Data.EntityConfig;
using despesas_backend_api_net_core.Infrastructure.Data.Repositories.Generic;

namespace despesas_backend_api_net_core.Business.Implementations
{
    public class UsuarioBusinessImpl : IUsuarioBusiness
    {
        private IRepositorio<Usuario> _repositorio;
        private readonly UsuarioMap _converter;

        public UsuarioBusinessImpl(IRepositorio<Usuario> repositorio)
        {
            _repositorio = repositorio;
            _converter = new UsuarioMap();

        }
        public UsuarioVM Create(UsuarioVM usuarioVM)
        {
            var usuario = new Usuario
            {
                Id = usuarioVM.Id,
                Nome = usuarioVM.Nome,
                SobreNome = usuarioVM.SobreNome,
                Email = usuarioVM.Email,
                Telefone = usuarioVM.Telefone,
                StatusUsuario = StatusUsuario.Ativo
            };

            return _converter.Parse(_repositorio.Insert(usuario));
        }

        public List<UsuarioVM> FindAll(int idUsuario)
        {
            var usuario = FindById(idUsuario);
            if (usuario.PerfilUsuario == PerfilUsuario.Administrador)
                return _converter.ParseList(_repositorio.GetAll());
            return null;
        }      

        public UsuarioVM FindById(int id)
        {
            var usuario = _repositorio.Get(id);
            return _converter.Parse(usuario);
        }

        public UsuarioVM Update(UsuarioVM usuarioVM)
        {
            var usuario = new Usuario
            {
                Id = usuarioVM.Id,
                Nome = usuarioVM.Nome,
                SobreNome = usuarioVM.SobreNome,
                Email = usuarioVM.Email,
                Telefone = usuarioVM.Telefone,
                StatusUsuario = StatusUsuario.Ativo
            };

            usuarioVM = _converter.Parse(_repositorio.Update(usuario));

            return usuarioVM;
        }

        public bool Delete(UsuarioVM usuarioVM)
        {
            return _repositorio.Delete(new Usuario{ Id = usuarioVM.Id });
        }
    }
}