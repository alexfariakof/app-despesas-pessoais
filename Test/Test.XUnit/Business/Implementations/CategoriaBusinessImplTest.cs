using despesas_backend_api_net_core.Business.Implementations;
using despesas_backend_api_net_core.Domain.Entities;
using despesas_backend_api_net_core.Domain.VM;
using despesas_backend_api_net_core.Infrastructure.Data.EntityConfig;
using despesas_backend_api_net_core.Infrastructure.Data.Repositories.Generic;
using Moq;

namespace Test.XUnit.Business.Implementations
{
    public class CategoriaBusinessImplTest
    {
        private readonly Mock<IRepositorio<Categoria>> _repositorioMock;
        private readonly CategoriaBusinessImpl _categoriaBusiness;
        
       
        private List<Categoria> categorias = new List<Categoria>
        {
            new Categoria { Id = 1, Descricao = "Alimentação", UsuarioId = 1, TipoCategoria = TipoCategoria.Despesa, Usuario = new Mock<Usuario>().Object  },
            new Categoria { Id = 2, Descricao = "Transporte", UsuarioId= 1, TipoCategoria = TipoCategoria.Despesa, Usuario = new Mock<Usuario>().Object },
            new Categoria { Id = 3, Descricao = "Salário", UsuarioId= 1, TipoCategoria = TipoCategoria.Receita, Usuario = new Mock<Usuario>().Object },
            new Categoria { Id = 4, Descricao = "Lazer", UsuarioId= 1, TipoCategoria = TipoCategoria.Despesa, Usuario = new Mock<Usuario>().Object },
            new Categoria { Id = 5, Descricao = "Moradia", UsuarioId= 1, TipoCategoria = TipoCategoria.Despesa , Usuario = new Mock < Usuario >().Object},
            new Categoria { Id = 6, Descricao = "Investimentos", UsuarioId= 1, TipoCategoria = TipoCategoria.Receita , Usuario = new Mock < Usuario >().Object},
            new Categoria { Id = 7, Descricao = "Presentes", UsuarioId= 1, TipoCategoria = TipoCategoria.Despesa , Usuario = new Mock < Usuario >().Object},
            new Categoria { Id = 8, Descricao = "Educação", UsuarioId= 1, TipoCategoria = TipoCategoria.Despesa , Usuario = new Mock < Usuario >().Object},
            new Categoria { Id = 9, Descricao = "Prêmios", UsuarioId= 1, TipoCategoria = TipoCategoria.Receita , Usuario = new Mock < Usuario >().Object},
            new Categoria { Id = 10, Descricao = "Saúde", UsuarioId= 1, TipoCategoria = TipoCategoria.Despesa , Usuario = new Mock < Usuario >().Object}
        };

        public CategoriaBusinessImplTest()
        {
            _repositorioMock = new Mock<IRepositorio<Categoria>>();
            _categoriaBusiness = new CategoriaBusinessImpl(_repositorioMock.Object);
        }

        [Fact]
        public void Create_ReturnsParsedCategoriaVM()
        {
            // Arrange
            var categoria = new CategoriaVM
            { 
                Id = 1,
                Descricao = "Alimentação",
                IdUsuario = 1,
                IdTipoCategoria = (int)TipoCategoria.Despesa
            };

            _repositorioMock.Setup(repo => repo.Insert(It.IsAny<Categoria>())).Returns(new CategoriaMap().Parse(categoria));

            // Act
            var result = _categoriaBusiness.Create(categoria);

            // Assert
            Assert.Equal(categoria.Id, result.Id);            
        }

        [Fact]
        public void FindAll_ReturnsListOfCategoriaVM()
        {
            // Arrange         
            int idUsuario  = 1;
            _repositorioMock.Setup(repo => repo.GetAll()).Returns(categorias);

            // Act
            var result = _categoriaBusiness.FindAll(idUsuario);

            // Assert
            Assert.Equal(categorias.Count, result.Count);
            // Assert other properties as needed
        }

        [Fact]
        public void FindById_ReturnsParsedCategoriaVM()
        {
            // Arrange
            var id = 1;
            var categoria = new Mock<Categoria>().Object;
            categoria.Id = 1;
            _repositorioMock.Setup(repo => repo.Get(id)).Returns(categoria);

            // Act
            var result = _categoriaBusiness.FindById(id, categoria.UsuarioId);

            // Assert
            Assert.Equal(categoria.Id, result.Id);
            // Assert other properties as needed
        }

        [Fact]
        public void Update_ReturnsParsedCategoriaVM()
        {
            // Arrange
            var categoriaVM = new CategoriaVM();
            var categoria = new Categoria();
            _repositorioMock.Setup(repo => repo.Update(It.IsAny<Categoria>())).Returns(categoria);

            // Act
            var result = _categoriaBusiness.Update(categoriaVM);

            // Assert
            Assert.Equal(categoria.Id, result.Id);
            // Assert other properties as needed
        }

        [Fact]
        public void Delete_ReturnsTrue()
        {
            // Arrange
            var id = 1;
            _repositorioMock.Setup(repo => repo.Delete(id)).Returns(true);

            // Act
            var result = _categoriaBusiness.Delete(id);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void FindByIdUsuario_ReturnsListOfCategoriaVM()
        {
            // Arrange
            var idUsuario = 1;
            var lstCategoria = new List<Categoria>();
            _repositorioMock.Setup(repo => repo.GetAll())
                .Returns(lstCategoria.FindAll(p => p.UsuarioId.Equals(idUsuario)));

            // Act
            var result = _categoriaBusiness.FindAll(idUsuario);

            // Assert
            Assert.Equal(lstCategoria.Count, result.Count);
            // Assert other properties as needed
        }
    }
}
