using despesas_backend_api_net_core.Business.Generic;
using despesas_backend_api_net_core.Domain.Entities;
using despesas_backend_api_net_core.Infrastructure.Data.Repositories.Generic;

namespace Test.XUnit.Business.Generic
{
    public class GenericBusinessTests
    {
        private Mock<IRepositorio<Categoria>> _mockRepositorio;
        private GenericBusiness<Categoria> _genericBusiness;
        private List<Categoria> _categorias;

        public GenericBusinessTests()
        {
            _mockRepositorio = new Mock<IRepositorio<Categoria>>(MockBehavior.Default);
            _genericBusiness = new GenericBusiness<Categoria>(_mockRepositorio.Object);
            _categorias = CategoriaFaker.Categorias();
        }

        [Fact]
        public void Create_Should_Return_Inserted_Object()
        {
            // Arrange
            var obj = _categorias.Last();
            var categoria = new Categoria
            {
                Descricao = obj.Descricao,
                TipoCategoria = obj.TipoCategoria,
                Usuario = obj.Usuario,
                UsuarioId = obj.UsuarioId
            };

            _mockRepositorio.Setup(repo => repo.Insert(It.IsAny<Categoria>())).Returns(categoria);
            
            // Act
            var result = _genericBusiness.Create(categoria);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<Categoria>(result);
            Assert.Equal(categoria.Id, result.Id);
            _mockRepositorio.Verify(repo => repo.Insert(categoria), Times.Once);
        }

        [Fact]
        public void FindAll_Should_Return_All_Objects()
        {
            // Arrange
            var objects = UsuarioFaker.Usuarios();
            var repositoryMock = new Mock<IRepositorio<Usuario>>();
            repositoryMock.Setup(repo => repo.GetAll()).Returns(objects);
            var business = new GenericBusiness<Usuario>(repositoryMock.Object);

            // Act
            var result = business.FindAll(1);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(objects, result);
            Assert.IsType<List<Usuario>>(result);
            repositoryMock.Verify(repo => repo.GetAll(), Times.Once);            
        }

        [Fact]
        public void FindById_Should_Return_Object_With_MatchingId()
        {
            // Arrange
            var id = 1;
            var obj = DespesaFaker.Despesas().First();
            var repositoryMock = new Mock<IRepositorio<Despesa>>();
            repositoryMock.Setup(repo => repo.Get(id)).Returns(obj);
            var business = new GenericBusiness<Despesa>(repositoryMock.Object);

            // Act
            var result = business.FindById(id, 1);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(obj, result);
            Assert.IsType<Despesa>(result);
            repositoryMock.Verify(repo => repo.Get(id), Times.Once);            
        }

        [Fact]
        public void Update_Should_Return_Updated_Object()
        {
            // Arrange
            var obj = ReceitaFaker.Receitas().First() ;
            var repositoryMock = new Mock<IRepositorio<Receita>>();
            repositoryMock.Setup(repo => repo.Update(obj)).Returns(obj);
            var business = new GenericBusiness<Receita>(repositoryMock.Object);

            // Act
            var result = business.Update(obj);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(obj, result);
            Assert.IsType<Receita>(result);
            repositoryMock.Verify(repo => repo.Update(obj), Times.Once);
        }

        [Fact]
        public void Delete_Should_Return_True_If_Deleted_Successfully()
        {
            // Arrange
            var objects = UsuarioFaker.Usuarios();
            var obj = objects.First();
            var repositoryMock = Usings.MockRepositorio(objects);
            repositoryMock.Setup(repo => repo.Delete(obj)).Returns(true);
            var business = new GenericBusiness<Usuario>(repositoryMock.Object);

            // Act
            var result = business.Delete(obj);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<bool>(result);
            Assert.True(result);
            repositoryMock.Verify(repo => repo.Delete(obj), Times.Once);            
        }

        [Fact]
        public void FindByIdUsuario_Should_Return_EmptyList()
        {
            // Arrange
            var idUsuario = 1;
            var business = new GenericBusiness<Lancamento>(Mock.Of<IRepositorio<Lancamento>>());

            // Act
            var result = business.FindByIdUsuario(idUsuario);

            // Assert
            Assert.Empty(result);
            Assert.IsType<List<Lancamento>> (result);
        }
    }
}