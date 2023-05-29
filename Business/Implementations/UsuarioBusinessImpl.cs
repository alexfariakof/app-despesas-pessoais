﻿using despesas_backend_api_net_core.Business.Generic;
using despesas_backend_api_net_core.Domain.Entities;
using despesas_backend_api_net_core.Domain.VM;
using despesas_backend_api_net_core.Infrastructure.Data.EntityConfig;
using despesas_backend_api_net_core.Infrastructure.Data.Repositories.Generic;

namespace despesas_backend_api_net_core.Business.Implementations
{
    public class UsuarioBusinessImpl : IBusiness<UsuarioVM>
    {
        private IRepositorio<Usuario> _repositorio;
        private readonly UsuarioMap _converter;

        public UsuarioBusinessImpl(IRepositorio<Usuario> repositorio)
        {
            _repositorio = repositorio;
            _converter = new UsuarioMap();

        }
        public UsuarioVM Create(UsuarioVM usuarioVO)
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

            return _converter.Parse(_repositorio.Insert(usuario));
        }

        public List<UsuarioVM> FindAll()
        {
            List<Usuario> lstUsuario = _repositorio.GetAll();
            return _converter.ParseList(lstUsuario) ;
        }      

        public UsuarioVM FindById(int idUsuario)
        {
            var usuario = _repositorio.Get(idUsuario);
            return _converter.Parse(usuario);
        }

        public UsuarioVM Update(UsuarioVM usuarioVO)
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

            usuarioVO = _converter.Parse(_repositorio.Update(usuario));

            return usuarioVO;
        }

        public void Delete(int idUsuario)
        {
            _repositorio.Delete(idUsuario);
        }
    }
}