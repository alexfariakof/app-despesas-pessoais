using AutoMapper;
using Business.Dtos.Core.Profile;
using Business.Dtos.v2;
using Fakers.v2;
using Domain.Entities.ValueObjects;
using System.Linq.Expressions;
using Moq;

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
        usuario.PerfilUsuario = new PerfilUsuario(PerfilUsuario.PerfilType.Administrador);
        _repositorioMock.Setup(repo => repo.Insert(ref It.Ref<Usuario>.IsAny));
        _repositorioMock.Setup(repo => repo.Get(It.IsAny<int>())).Returns(usuario);

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
        var usuarios = _usuarios.Where(u => u.PerfilUsuario == PerfilUsuario.PerfilType.Administrador);
        var usuario = usuarios.First();
        var idUsuario = usuario.Id;
        _repositorioMock.Setup(repo => repo.GetAll()).Returns(_usuarios);
        _repositorioMock.Setup(repo => repo.Find(It.IsAny<Expression<Func<Usuario, bool>>>())).Returns(usuarios.AsEnumerable());
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
        var usuarios = _usuarios.Where(u => u.PerfilUsuario == PerfilUsuario.PerfilType.Usuario);
        var usuario = usuarios.First();
        var idUsuario = usuario.Id;
        _repositorioMock.Setup(repo => repo.GetAll()).Returns(_usuarios);
        _repositorioMock.Setup(repo => repo.Find(It.IsAny<Expression<Func<Usuario, bool>>>())).Returns(usuarios.AsEnumerable());

        // Act & Assert 
        Assert.Throws<ArgumentException>(() => _usuarioBusiness.FindAll(idUsuario));
        _repositorioMock.Verify(repo => repo.GetAll(), Times.Never);
        _repositorioMock.Verify(repo => repo.Find(It.IsAny<Expression<Func<Usuario, bool>>>()), Times.Once);
    }

    [Fact]
    public void FindById_Should_Returns_Parsed_UsuarioDto()
    {
        // Arrange
        var usuario = _usuarios.First();
        var idUsuario = usuario.Id;            

        _repositorioMock.Setup(repo => repo.Get(idUsuario)).Returns(usuario);

        // Act
        var result = _usuarioBusiness.FindById(idUsuario);

        // Assert
        Assert.NotNull(result);
        Assert.IsType<UsuarioDto>(result);
        Assert.Equal(usuario.Id, result.Id);
        _repositorioMock.Verify(repo => repo.Get(idUsuario), Times.Once);
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
    public void Delete_Should_Returns_True()
    {
        // Arrange
        var obj = _mapper.Map<UsuarioDto>(_usuarios.First());
        _repositorioMock.Setup(repo => repo.Delete(It.IsAny<Usuario>())).Returns(true);

        // Act
        var result = _usuarioBusiness.Delete(obj);

        // Assert
        Assert.IsType<bool>(result);
        Assert.True(result);
        _repositorioMock.Verify(repo => repo.Delete(It.IsAny<Usuario>()), Times.Once);
    }
}