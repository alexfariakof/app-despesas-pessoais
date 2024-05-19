using AutoMapper;
using Business.Abstractions;
using Business.Dtos.Core;
using Domain.Entities;
using Domain.Entities.ValueObjects;
using Repository.Persistency.Generic;

namespace Business.Implementations;
public class UsuarioBusinessImpl<Dto> : IUsuarioBusiness<Dto> where Dto : UsuarioDtoBase, new()
{
    private readonly IRepositorio<Usuario> _repositorio;
    private readonly IMapper _mapper;

    public UsuarioBusinessImpl(IMapper mapper, IRepositorio<Usuario> repositorio)
    {
        _mapper = mapper;
        _repositorio = repositorio;        
    }

    public Dto Create(Dto usuarioDto)
    {
        var isValidUsuario = _repositorio.Get(usuarioDto.UsuarioId);
        if (isValidUsuario.PerfilUsuario is null || isValidUsuario.PerfilUsuario != PerfilUsuario.PerfilType.Administrador)
            throw new ArgumentException("Usuário não permitido a realizar operação!");
        
        var usuario = _mapper.Map<Usuario>(usuarioDto);
        usuario = usuario.CreateUsuario(usuario);
        _repositorio.Insert(ref usuario);
        return _mapper.Map<Dto>(usuario);
    }

    public List<Dto> FindAll(int idUsuario)
    {
        var usuario = _repositorio.Find(u => u.Id == idUsuario).FirstOrDefault();
        if (usuario.PerfilUsuario == PerfilUsuario.PerfilType.Administrador)
            return _mapper.Map<List<Dto>>(_repositorio.GetAll());

        throw new ArgumentException("Usuário não permitido a realizar operação!");
    }      

    public Dto FindById(int id)
    {
        var usuario = _repositorio.Get(id);
        return _mapper.Map<Dto>(usuario);
    }

    public Dto Update(Dto usuarioDto)
    {
        var usuario = _mapper.Map<Usuario>(usuarioDto);
        _repositorio.Update(ref usuario);
        return _mapper.Map<Dto>(usuario);
    }

    public bool Delete(Dto usuarioDto)
    {
        var usuario = _mapper.Map<Usuario>(usuarioDto);
        return _repositorio.Delete(usuario);
    }
}