using Business.Abstractions;
using Business.Dtos;
using Business.Dtos.Parser;
using Domain.Entities;
using Repository.Persistency.Generic;

namespace Business.Implementations;
public class UsuarioBusinessImpl : IUsuarioBusiness
{
    private readonly IRepositorio<Usuario> _repositorio;
    private readonly UsuarioParser _converter;

    public UsuarioBusinessImpl(IRepositorio<Usuario> repositorio)
    {
        _repositorio = repositorio;
        _converter = new UsuarioParser();
    }

    public UsuarioDto Create(UsuarioDto usuarioDto)
    {
        var isValidUsuario = _repositorio.Get(usuarioDto.IdUsuario);
        if (isValidUsuario.PerfilUsuario != PerfilUsuario.Administrador)
            throw new ArgumentException("Usuário não permitido a realizar operação!");
        
        var usuario = new Usuario().CreateUsuario(
            usuarioDto.Nome,
            usuarioDto.SobreNome,
            usuarioDto.Email,
            usuarioDto.Telefone,
            StatusUsuario.Ativo,
            PerfilUsuario.Usuario);

        _repositorio.Insert(ref usuario);
        return _converter.Parse(usuario);
    }

    public List<UsuarioDto> FindAll(int idUsuario)
    {
        var usuario = FindById(idUsuario);
        if (usuario.PerfilUsuario == PerfilUsuario.Administrador)
            return _converter.ParseList(_repositorio.GetAll());
        return null;
    }      

    public UsuarioDto FindById(int id)
    {
        var usuario = _repositorio.Get(id);
        return _converter.Parse(usuario);
    }
    public UsuarioDto Update(UsuarioDto usuarioDto)
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
        return _converter.Parse(usuario);
    }

    public bool Delete(UsuarioDto usuarioDto)
    {
        return _repositorio.Delete(new Usuario{ Id = usuarioDto.Id });
    }
}