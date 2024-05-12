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
        if (isValidUsuario.PerfilUsuario != PerfilUsuario.PerfilType.Administrador)
            throw new ArgumentException("Usuário não permitido a realizar operação!");
        
        var usuario = new Usuario().CreateUsuario(
            usuarioDto.Nome,
            usuarioDto.SobreNome,
            usuarioDto.Email,
            usuarioDto.Telefone,
            StatusUsuario.Ativo,
            PerfilUsuario.PerfilType.Usuario);

        _repositorio.Insert(ref usuario);
        return _mapper.Map<Dto>(usuario);
    }

    public List<Dto> FindAll(int idUsuario)
    {
        var usuario = FindById(idUsuario);
        if (usuario.PerfilUsuario == PerfilUsuario.PerfilType.Administrador)
            return _mapper.Map<List<Dto>>(_repositorio.GetAll());
        return null;
    }      

    public Dto FindById(int id)
    {
        var usuario = _repositorio.Get(id);
        return _mapper.Map<Dto>(usuario);
    }

    public Dto Update(Dto usuarioDto)
    {
        var usuario = new Usuario
        {
            Id = usuarioDto.Id,
            Nome = usuarioDto.Nome,
            SobreNome = usuarioDto.SobreNome,
            Email = usuarioDto.Email,
            Telefone = usuarioDto.Telefone,
            StatusUsuario = StatusUsuario.Ativo
        };
        
        _repositorio.Update(ref usuario);
        return _mapper.Map<Dto>(usuario);
    }

    public bool Delete(Dto usuarioDto)
    {
        return _repositorio.Delete(new Usuario{ Id = usuarioDto.Id });
    }
}