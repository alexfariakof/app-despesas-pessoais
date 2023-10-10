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
        private List<UsuarioVM> _usuarioVMs;

        public UsuarioBusinessImplTest()
        {
            _repositorioMock = new Mock<IRepositorio<Usuario>>();
            _usuarioBusiness = new UsuarioBusinessImpl(_repositorioMock.Object);
            _usuarios = UsuarioFaker.Usuarios();
            _usuarioVMs = UsuarioFaker.UsuariosVMs();
        }

        [Fact]
        public void Create_ReturnsParsedUsuarioVM()
        {
            // Arrange
            var usuario = _usuarios.First();

            _repositorioMock.Setup(repo => repo.Insert(It.IsAny<Usuario>())).Returns(usuario);

            // Act
            var result = _usuarioBusiness.Create(new UsuarioMap().Parse(usuario));

            // Assert
            Assert.Equal(usuario.Id, result.Id);
        }

        [Fact]
        public void FindAll_ReturnsListOfUsuarioVM()
        {
            var usuario = _usuarios.First();
            usuario.PerfilUsuario = PerfilUsuario.Administrador;
            usuario.StatusUsuario= StatusUsuario.Ativo;
            var idUsuario = usuario.Id;
            
            // Arrange         
            _repositorioMock.Setup(repo => repo.GetAll()).Returns(_usuarios);
            _repositorioMock.Setup(repo => repo.Get(idUsuario)).Returns(usuario);

            // Act
            var result = _usuarioBusiness.FindAll(idUsuario);

            // Assert
            Assert.NotNull(result);            
            Assert.IsType<List<UsuarioVM>>(result);
            Assert.Equal(_usuarios.Count, result.Count);           
        }

        [Fact]
        public void FindAll_ReturnsNull()
        {
            var usuario = _usuarios.First();
            usuario.PerfilUsuario = PerfilUsuario.Usuario;
            usuario.StatusUsuario = StatusUsuario.Ativo;
            var idUsuario = usuario.Id;

            // Arrange         
            _repositorioMock.Setup(repo => repo.GetAll()).Returns(_usuarios);
            _repositorioMock.Setup(repo => repo.Get(idUsuario)).Returns(usuario);

            // Act
            var result = _usuarioBusiness.FindAll(idUsuario);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public void FindById_ReturnsParsedUsuarioVM()
        {
            // Arrange
            var usuario = _usuarios.First();
            var id = usuario.Id;
            

            _repositorioMock.Setup(repo => repo.Get(id)).Returns(usuario);

            // Act
            var result = _usuarioBusiness.FindById(id);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<UsuarioVM>(result);
            Assert.Equal(usuario.Id, result.Id);            
        }

        [Fact]
        public void Update_ReturnsParsedUsuarioVM()
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
        }

        [Fact]
        public void Delete_ReturnsTrue()
        {
            // Arrange
            var id = 1;
            _repositorioMock.Setup(repo => repo.Delete(new BaseModel { Id = id })).Returns(true);

            // Act
            // Act
            var result = _usuarioBusiness.Delete(id);

            // Assert
            Assert.True(result);
        }
    }
}