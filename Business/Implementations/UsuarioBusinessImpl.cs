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

    public UsuarioDto Create(UsuarioDto usuarioVM)
    {
        var usuario = new Usuario().CreateUsuario(
            usuarioVM.Nome,
            usuarioVM.SobreNome,
            usuarioVM.Email,
            usuarioVM.Telefone,
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
    public UsuarioDto Update(UsuarioDto usuarioVM)
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
        
        _repositorio.Update(ref usuario);
        return _converter.Parse(usuario);
    }

    public bool Delete(UsuarioDto usuarioVM)
    {
        return _repositorio.Delete(new Usuario{ Id = usuarioVM.Id });
    }
}