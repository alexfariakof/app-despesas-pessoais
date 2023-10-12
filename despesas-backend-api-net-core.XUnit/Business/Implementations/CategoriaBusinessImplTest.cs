using despesas_backend_api_net_core.Business.Implementations;
using despesas_backend_api_net_core.Infrastructure.Data.EntityConfig;
using despesas_backend_api_net_core.Infrastructure.Data.Repositories.Generic;

namespace Test.XUnit.Business.Implementations
{
    public class CategoriaBusinessImplTest
    {
        private readonly Mock<IRepositorio<Categoria>> _repositorioMock;
        private readonly CategoriaBusinessImpl _categoriaBusiness;
        private readonly List<Categoria> _categorias;
        public CategoriaBusinessImplTest()
        {
            _categorias = CategoriaFaker.Categorias();
            _repositorioMock = Usings.MockRepositorio(_categorias);
            _categoriaBusiness = new CategoriaBusinessImpl(_repositorioMock.Object);            
        }

        [Fact]
        public void Create_Returns_Parsed_CategoriaVM()
        {
            // Arrange
            var categoriaVM = CategoriaFaker.CategoriasVMs().First();

            _repositorioMock.Setup(repo => repo.Insert(It.IsAny<Categoria>())).Returns(new CategoriaMap().Parse(categoriaVM));

            // Act
            var result = _categoriaBusiness.Create(categoriaVM);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<CategoriaVM>(result);
            Assert.Equal(categoriaVM.Id, result.Id);
        }

        [Fact]
        public void FindAll_Returns_List_Of_CategoriaVM()
        {
            // Arrange         
            var categoria = _categorias.First();
            var mockCategorias = _categorias.FindAll(obj => obj.UsuarioId == categoria.UsuarioId);
            _repositorioMock.Setup(repo => repo.GetAll()).Returns(mockCategorias);

            // Act
            var result = _categoriaBusiness.FindAll(categoria.UsuarioId);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<List<CategoriaVM>>(result);
            Assert.Equal(mockCategorias.Count, result.Count);
        }

        [Fact]
        public void FindById_Returns_Parsed_CategoriaVM()
        {
            // Arrange
            var categoria = _categorias.First();
            var id = categoria.Id;
            var idUsuario = categoria.UsuarioId;
            

            _repositorioMock.Setup(repo => repo.Get(id)).Returns(categoria);

            // Act
            var result = _categoriaBusiness.FindById(id, idUsuario);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<CategoriaVM>(result);
            Assert.Equal(categoria.Id, result.Id);            
        }

        [Fact]
        public void FindById_Returns_Null()
        {
            // Arrange
            var categoria = _categorias.First();
            var id = categoria.Id;
            var idUsuario = categoria.UsuarioId;

            _repositorioMock.Setup(repo => repo.Get(id)).Returns((Categoria)null);

            // Act
            var result = _categoriaBusiness.FindById(0, idUsuario);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public void Update_Returns_Parsed_CategoriaVM()
        {
            // Arrange
            var categoriaVM = new CategoriaVM();
            var categoria = new Categoria();
            _repositorioMock.Setup(repo => repo.Update(It.IsAny<Categoria>())).Returns(categoria);

            // Act
            var result = _categoriaBusiness.Update(categoriaVM);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<CategoriaVM>(result);
            Assert.Equal(categoria.Id, result.Id);
        }

        [Fact]
        public void Delete_ReturnsTrue()
        {
            // Arrange
            var obj = CategoriaFaker.CategoriasVMs().First();
            var categoria = new CategoriaMap().Parse(obj);
            _repositorioMock.Setup(repo => repo.Delete(It.IsAny<Categoria>())).Returns(true);
            
            // Act
            var result = _categoriaBusiness.Delete(obj);

            // Assert
            Assert.True(result);
        }
    }
}