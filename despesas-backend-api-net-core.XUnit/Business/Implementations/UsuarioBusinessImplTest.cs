using despesas_backend_api_net_core.Business.Implementations;
using despesas_backend_api_net_core.Infrastructure.Data.EntityConfig;
using despesas_backend_api_net_core.Infrastructure.Data.Repositories.Generic;

namespace Test.XUnit.Business.Implementations
{
    public class UsuarioBusinessImplTest
    {
        private readonly Mock<IRepositorio<Usuario>> _repositorioMock;
        private readonly UsuarioBusinessImpl _usuarioBusiness;
        private List<Usuario> _usuarios;

        public UsuarioBusinessImplTest()
        {
            _repositorioMock = new Mock<IRepositorio<Usuario>>();
            _usuarioBusiness = new UsuarioBusinessImpl(_repositorioMock.Object);
            _usuarios = UsuarioFaker.Usuarios();
        }

        [Fact]
        public void Create_Should_Returns_Parsed_UsuarioVM()
        {
            // Arrange
            var usuario = _usuarios.First();

            _repositorioMock.Setup(repo => repo.Insert(It.IsAny<Usuario>())).Returns(usuario);

            // Act
            var result = _usuarioBusiness.Create(new UsuarioMap().Parse(usuario));

            // Assert
            Assert.NotNull(result);
            Assert.IsType<UsuarioVM>(result);
            Assert.Equal(usuario.Id, result.Id);
            _repositorioMock.Verify(repo => repo.Insert(It.IsAny<Usuario>()), Times.Once);
        }

        [Fact]
        public void FindAll_Should_Returns_List_Of_UsuarioVM()
        {
            // Arrange         
            var usuario = _usuarios.First();
            usuario.PerfilUsuario = PerfilUsuario.Administrador;
            usuario.StatusUsuario= StatusUsuario.Ativo;
            var idUsuario = usuario.Id;            
            
            _repositorioMock.Setup(repo => repo.GetAll()).Returns(_usuarios);
            _repositorioMock.Setup(repo => repo.Get(idUsuario)).Returns(usuario);

            // Act
            var result = _usuarioBusiness.FindAll(idUsuario);

            // Assert
            Assert.NotNull(result);            
            Assert.IsType<List<UsuarioVM>>(result);
            Assert.Equal(_usuarios.Count, result.Count);
            _repositorioMock.Verify(repo => repo.GetAll(), Times.Once);
            _repositorioMock.Verify(repo => repo.Get(idUsuario), Times.Once);
        }

        [Fact]
        public void FindAll_Should_Returns_Null()
        {
            // Arrange         
            var usuario = _usuarios.First();
            usuario.PerfilUsuario = PerfilUsuario.Usuario;
            usuario.StatusUsuario = StatusUsuario.Ativo;
            var idUsuario = usuario.Id;
            
            _repositorioMock.Setup(repo => repo.GetAll()).Returns(_usuarios);
            _repositorioMock.Setup(repo => repo.Get(idUsuario)).Returns(usuario);

            // Act
            var result = _usuarioBusiness.FindAll(idUsuario);

            // Assert
            Assert.Null(result);
            _repositorioMock.Verify(repo => repo.GetAll(), Times.Never);
            _repositorioMock.Verify(repo => repo.Get(idUsuario), Times.Once);
        }

        [Fact]
        public void FindById_Should_Returns_Parsed_UsuarioVM()
        {
            // Arrange
            var usuario = _usuarios.First();
            var idUsuario = usuario.Id;            

            _repositorioMock.Setup(repo => repo.Get(idUsuario)).Returns(usuario);

            // Act
            var result = _usuarioBusiness.FindById(idUsuario);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<UsuarioVM>(result);
            Assert.Equal(usuario.Id, result.Id);
            _repositorioMock.Verify(repo => repo.Get(idUsuario), Times.Once);
        }

        [Fact]
        public void Update_Should_Returns_Parsed_UsuarioVM()
        {
            // Arrange            
            var usuario = _usuarios.First();
            var usuarioVM = new UsuarioMap().Parse(usuario);
            usuario.Nome = "Teste Usuario Update";                       

            _repositorioMock.Setup(repo => repo.Update(It.IsAny<Usuario>())).Returns(usuario);

            // Act
            var result = _usuarioBusiness.Update(usuarioVM);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<UsuarioVM>(result);
            Assert.Equal(usuarioVM.Id, result.Id);
            _repositorioMock.Verify(repo => repo.Update(It.IsAny<Usuario>()), Times.Once);
        }

        [Fact]
        public void Delete_Should_Returns_True()
        {
            // Arrange
            var obj = new UsuarioMap().Parse(_usuarios.First());
            _repositorioMock.Setup(repo => repo.Delete(It.IsAny<Usuario>())).Returns(true);

            // Act
            var result = _usuarioBusiness.Delete(obj);

            // Assert
            Assert.IsType<bool>(result);
            Assert.True(result);
            _repositorioMock.Verify(repo => repo.Delete(It.IsAny<Usuario>()), Times.Once);
        }
    }
}