﻿using AutoMapper;
using Business.Abstractions;
using Business.Dtos.Core;
using Domain.Entities;
using Domain.Entities.ValueObjects;
using Repository.Persistency.Generic;
using Repository.Persistency.UnitOfWork.Abstractions;

namespace Business.Implementations;
public class UsuarioBusinessImpl<Dto> : BusinessBase<Dto, Usuario>, IUsuarioBusiness<Dto> where Dto : UsuarioDtoBase, new()
{
    private readonly IRepositorio<Usuario> _repositorio;
    private readonly IMapper _mapper;

    public UsuarioBusinessImpl(IMapper mapper, IRepositorio<Usuario> repositorio, IUnitOfWork<Usuario> unitOfWork = null) : base(mapper, repositorio, unitOfWork)
    {
        _mapper = mapper;
        _repositorio = repositorio;
    }

    public override Dto Create(Dto usuarioDto)
    {
        IsValidPrefilAdministratdor(usuarioDto);
        var usuario = _mapper.Map<Usuario>(usuarioDto);
        usuario = usuario.CreateUsuario(usuario);
        _repositorio.Insert(ref usuario);
        return _mapper.Map<Dto>(usuario);
    }

    public override List<Dto> FindAll(int idUsuario)
    {
        var usuario = _repositorio?.Find(u => u.Id == idUsuario)?.FirstOrDefault();
        IsValidPrefilAdministratdor(usuario);
        return _mapper.Map<List<Dto>>(_repositorio?.GetAll());
    }

    public override Dto Update(Dto usuarioDto)
    {
        var usuario = _mapper.Map<Usuario>(usuarioDto);
        _repositorio.Update(ref usuario);
        if (usuario is null) throw new ArgumentException("Usuário não encontrado!");
        return _mapper.Map<Dto>(usuario);
    }

    public override Dto FindById(int idUsuario)
    {
        var usuario = _repositorio?.Find(u => u.Id == idUsuario)?.FirstOrDefault();
        return this.Mapper.Map<Dto>(usuario);
    }

    public override bool Delete(Dto usuarioDto)
    {
        IsValidPrefilAdministratdor(usuarioDto);
        var usuario = _mapper.Map<Usuario>(usuarioDto);
        return _repositorio.Delete(usuario);
    }

    private void IsValidPrefilAdministratdor(Dto usuarioDto)
    {
        var adm = _repositorio.Get(usuarioDto.UsuarioId);
        if (adm.PerfilUsuario != PerfilUsuario.PerfilType.Administrador)
            throw new ArgumentException("Usuário não permitido a realizar operação!");
    }

    private void IsValidPrefilAdministratdor(Usuario usuario)
    {
        var adm = _repositorio.Get(usuario.Id);
        if (adm.PerfilUsuario != PerfilUsuario.PerfilType.Administrador)
            throw new ArgumentException("Usuário não permitido a realizar operação!");
    }
}