using Domain.Entities;
using Domain.VM;
using Repository.Mapping;
using Repository.Persistency.Generic;

namespace Business.Implementations;
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
        
        _repositorio.Update(ref usuario);
        return _converter.Parse(usuario);
    }

    public bool Delete(UsuarioVM usuarioVM)
    {
        return _repositorio.Delete(new Usuario{ Id = usuarioVM.Id });
    }
}