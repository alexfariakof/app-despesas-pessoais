﻿using AutoMapper;
using Business.Dtos.Core.Profile;
using Business.Dtos.v2;
using __mock__.v2;
using Domain.Entities.ValueObjects;
using System.Linq.Expressions;
using Business.Implementations;
using Repository.Persistency.Generic;

namespace Business;
public class UsuarioBusinessImplTest
{
    private readonly Mock<IRepositorio<Usuario>> _repositorioMock;
    private readonly UsuarioBusinessImpl<UsuarioDto> _usuarioBusiness;
    private List<Usuario> _usuarios;
    private Mapper _mapper;
    public UsuarioBusinessImplTest()
    {
        _repositorioMock = new Mock<IRepositorio<Usuario>>();
        _mapper = new Mapper(new MapperConfiguration(cfg => { cfg.AddProfile<UsuarioProfile>(); }));
        _usuarioBusiness = new UsuarioBusinessImpl<UsuarioDto>(_mapper, _repositorioMock.Object);
        _usuarios = UsuarioFaker.Instance.GetNewFakersUsuarios();
    }

    [Fact]
    public void Create_Should_Returns_Parsed_UsuarioDto()
    {
        // Arrange
        var usuario = _usuarios.First();
        usuario.PerfilUsuario = new PerfilUsuario(PerfilUsuario.Perfil.Admin);
        _repositorioMock.Setup(repo => repo.Insert(ref It.Ref<Usuario>.IsAny));
        _repositorioMock.Setup(repo => repo.Get(It.IsAny<Guid>())).Returns(usuario);

        // Act
        var result = _usuarioBusiness.Create(_mapper.Map<UsuarioDto>(usuario));

        // Assert
        Assert.NotNull(result);
        Assert.IsType<UsuarioDto>(result);
        //Assert.Equal(usuario.Id, result.Id);
        _repositorioMock.Verify(repo => repo.Insert(ref It.Ref<Usuario>.IsAny), Times.Once);
    }

    [Fact]
    public void FindAll_Should_Returns_List_of_UsuarioDto()
    {
        // Arrange         
        var usuarios = _usuarios.Where(u => u.PerfilUsuario == PerfilUsuario.Perfil.Admin);
        var usuario = usuarios.First();
        var idUsuario = usuario.Id;
        _repositorioMock.Setup(repo => repo.GetAll()).Returns(_usuarios);
        _repositorioMock.Setup(repo => repo.Find(It.IsAny<Expression<Func<Usuario, bool>>>())).Returns(usuarios.AsEnumerable());
        _repositorioMock.Setup(repo => repo.Get(It.IsAny<Guid>())).Returns(usuario);

        // Act
        var result = _usuarioBusiness.FindAll(idUsuario);

        // Assert
        Assert.NotNull(result);            
        Assert.IsType<List<UsuarioDto>>(result);
        Assert.Equal(_usuarios.Count, result.Count);
        _repositorioMock.Verify(repo => repo.GetAll(), Times.Once);
        _repositorioMock.Verify(repo => repo.Find(It.IsAny<Expression<Func<Usuario, bool>>>()), Times.Once);
    }

    [Fact]
    public void FindAll_Should_Returns_Thwors_Exception()
    {
        // Arrange         
        var usuarios = _usuarios.Where(u => u.PerfilUsuario == PerfilUsuario.Perfil.User);
        var usuario = usuarios.First();
        var idUsuario = usuario.Id;
        _repositorioMock.Setup(repo => repo.GetAll()).Returns(_usuarios);
        _repositorioMock.Setup(repo => repo.Find(It.IsAny<Expression<Func<Usuario, bool>>>())).Returns(usuarios.AsEnumerable());
        _repositorioMock.Setup(repo => repo.Get(It.IsAny<Guid>())).Returns(usuario);

        // Act & Assert 
        Assert.Throws<ArgumentException>(() => _usuarioBusiness.FindAll(idUsuario));
        _repositorioMock.Verify(repo => repo.GetAll(), Times.Never);
        _repositorioMock.Verify(repo => repo.Find(It.IsAny<Expression<Func<Usuario, bool>>>()), Times.Once);
    }

    [Fact]
    public void FindById_Should_Returns_Parsed_UsuarioDto()
    {
        // Arrange
        var usuarios = _usuarios.Take(3);
        var usuario = usuarios.First();
        var idUsuario = usuario.Id;
        _repositorioMock.Setup(repo => repo.Find(It.IsAny<Expression<Func<Usuario, bool>>>())).Returns(_usuarios.AsEnumerable());
        _repositorioMock.Setup(repo => repo.Get(idUsuario)).Returns(usuario);

        // Act
        var result = _usuarioBusiness.FindById(idUsuario);

        // Assert
        Assert.NotNull(result);
        Assert.IsType<UsuarioDto>(result);
        Assert.Equal(usuario.Id, result.Id);
        _repositorioMock.Verify(repo => repo.Find(It.IsAny<Expression<Func<Usuario, bool>>>()), Times.Once);
    }

    [Fact]
    public void Update_Should_Returns_Parsed_UsuarioDto()
    {
        // Arrange            
        var usuario = _usuarios.First();
        var usuarioDto = _mapper.Map<UsuarioDto>(usuario);
        usuario.Nome = "Teste Usuario Update";                       

        _repositorioMock.Setup(repo => repo.Update(ref It.Ref<Usuario>.IsAny));

        // Act
        var result = _usuarioBusiness.Update(usuarioDto);

        // Assert
        Assert.NotNull(result);
        Assert.IsType<UsuarioDto>(result);
        Assert.Equal(usuarioDto.Id, result.Id);
        _repositorioMock.Verify(repo => repo.Update(ref It.Ref<Usuario>.IsAny), Times.Once);
    }

    [Fact]
    public void Delete_Should_Returns_True_when_Usuario_is_Administrador()
    {
        // Arrange
        var usuario= _usuarios.First(u => u.PerfilUsuario == PerfilUsuario.Perfil.Admin);
        var usuarioDto = _mapper.Map<UsuarioDto>(usuario);
        _repositorioMock.Setup(repo => repo.Delete(It.IsAny<Usuario>())).Returns(true);
        _repositorioMock.Setup(repo => repo.Get(It.IsAny<Guid>())).Returns(usuario);       

        // Act
        var result = _usuarioBusiness.Delete(usuarioDto);

        // Assert
        Assert.IsType<bool>(result);
        Assert.True(result);
        _repositorioMock.Verify(repo => repo.Delete(It.IsAny<Usuario>()), Times.Once);
    }


    [Fact]
    public void Delete_Should_Throws_Errro_When_Usuario_is_Not_Admintrador()
    {
        // Arrange
        var usuario = _usuarios.First(u => u.PerfilUsuario == PerfilUsuario.Perfil.User);
        var usuarioDto = _mapper.Map<UsuarioDto>(usuario);
        _repositorioMock.Setup(repo => repo.Delete(It.IsAny<Usuario>())).Returns(false);
        _repositorioMock.Setup(repo => repo.Get(It.IsAny<Guid>())).Returns(usuario);

        // Act & Assert 
        Assert.Throws<ArgumentException>((() => _usuarioBusiness.Delete(usuarioDto)));
        _repositorioMock.Verify(repo => repo.Delete(It.IsAny<Usuario>()), Times.Never);
    }
}