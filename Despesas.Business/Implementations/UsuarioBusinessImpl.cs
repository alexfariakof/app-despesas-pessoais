using AutoMapper;
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

    public UsuarioBusinessImpl(IMapper mapper, IRepositorio<Usuario> repositorio, IUnitOfWork<Usuario>? unitOfWork = null) : base(mapper, repositorio, unitOfWork)
    {
        _mapper = mapper;
        _repositorio = repositorio;
    }

    public override Dto Create(Dto dto)
    {
        IsValidPrefilAdministratdor(dto);
        var usuario = _mapper.Map<Usuario>(dto);
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

    public override Dto Update(Dto dto)
    {
        var usuario = _mapper.Map<Usuario>(dto);
        _repositorio.Update(ref usuario);
        if (usuario is null) throw new ArgumentException("Usuário não encontrado!");
        return _mapper.Map<Dto>(usuario);
    }

    public override Dto FindById(int id)
    {
        var usuario = _repositorio?.Find(u => u.Id == id)?.FirstOrDefault();
        return this.Mapper.Map<Dto>(usuario);
    }

    public override bool Delete(Dto dto)
    {
        IsValidPrefilAdministratdor(dto);
        var usuario = _mapper.Map<Usuario>(dto);
        return _repositorio.Delete(usuario);
    }

    private void IsValidPrefilAdministratdor(Dto dto)
    {
        var adm = _repositorio.Get(dto.UsuarioId);
        if (adm.PerfilUsuario != PerfilUsuario.Perfil.Admin)
            throw new ArgumentException("Usuário não permitido a realizar operação!");
    }

    private void IsValidPrefilAdministratdor(Usuario usuario)
    {
        var adm = _repositorio.Get(usuario.Id);
        if (adm.PerfilUsuario != PerfilUsuario.Perfil.Admin)
            throw new ArgumentException("Usuário não permitido a realizar operação!");
    }
}