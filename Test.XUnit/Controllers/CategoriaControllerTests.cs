using despesas_backend_api_net_core.Business.Generic;
using despesas_backend_api_net_core.Controllers;
using despesas_backend_api_net_core.Domain.Entities;
using despesas_backend_api_net_core.Domain.VM;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace Test.XUnit.Controllers
{
    public class CategoriaControllerTests
    {
        private readonly Mock<IBusiness<CategoriaVM>> _mockCategoriaBusiness;
        private readonly CategoriaController _categoriaController;
        protected enum TipoCategoria : ushort
        {
            Todas = 0,
            Despesa = 1,
            Receita = 2,
            Outro = 3
        }

        private List<CategoriaVM> categorias = new List<CategoriaVM>
        {
            new CategoriaVM { Id = 1, IdUsuario = 1, Descricao = "Alimentação", IdTipoCategoria = 1 },
            new CategoriaVM { Id = 2,  IdUsuario = 1, Descricao = "Transporte", IdTipoCategoria = 1 },
            new CategoriaVM { Id = 3, IdUsuario = 1, Descricao = "Salário", IdTipoCategoria = 2 },
            new CategoriaVM { Id = 4,  IdUsuario = 1, Descricao = "Lazer", IdTipoCategoria = 1 },
            new CategoriaVM { Id = 5, IdUsuario = 1, Descricao = "Moradia", IdTipoCategoria = 1 },
            new CategoriaVM { Id = 6, IdUsuario = 1, Descricao = "Investimentos", IdTipoCategoria = 2 },
            new CategoriaVM { Id = 7, IdUsuario = 1, Descricao = "Presentes", IdTipoCategoria = 1 },
            new CategoriaVM { Id = 8, IdUsuario = 1, Descricao = "Educação", IdTipoCategoria = 1 },
            new CategoriaVM { Id = 9, IdUsuario = 1, Descricao = "Prêmios", IdTipoCategoria = 2 },
            new CategoriaVM { Id = 10, IdUsuario = 1, Descricao = "Saúde", IdTipoCategoria = 1 }
        };
        public CategoriaControllerTests()
        {
            _mockCategoriaBusiness = new Mock<IBusiness<CategoriaVM>>();
            _categoriaController = new CategoriaController(_mockCategoriaBusiness.Object);
        }

        [Fact]
        public void Get_ReturnsOkResult()
        {

            _mockCategoriaBusiness.Setup(b => b.FindAll(1)).Returns(categorias);

            // Act
            var result = _categoriaController.Get();

            // Assert
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public void GetById_WithValidId_ReturnsOkResult()
        {
            // Arrange
            var idCategoria = 1;
            var categoria = new CategoriaVM
            {
                Id  = 1,
                Descricao = "Teste Categoria",
                IdTipoCategoria = idCategoria,
                IdUsuario = 1,
            };
            _mockCategoriaBusiness.Setup(b => b.FindById(idCategoria, categoria.IdUsuario)).Returns(categoria);

            // Act
            var result = _categoriaController.GetById(idCategoria);

            // Assert
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public void GetById_WithInvalidId_ReturnsNotFoundResult()
        {
            // Arrange
            var idCategoria = 1;
            CategoriaVM _categoria = new CategoriaVM { IdUsuario = 1 };
            _mockCategoriaBusiness.Setup(b => b.FindById(idCategoria, _categoria.IdUsuario)).Returns(_categoria);

            // Act
            var result = _categoriaController.GetById(idCategoria);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public void GetByIdUsuario_ReturnsOkResult()
        {
            // Arrange
            var idUsuario = 1;
            _mockCategoriaBusiness.Setup(b => b.FindAll(idUsuario)).Returns(categorias);

            // Act
            var result = _categoriaController.GetByIdUsuario(idUsuario);

            // Assert
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task GetByIdUsuario_WithInvalidId_ReturnsNotFoundResult()
        {
            // Arrange
            var idUsuario = 10;            

            Mock.Get(_mockCategoriaBusiness.Object).Setup(b => b.FindAll(idUsuario)).Returns(categorias);

            // Act
            var result = _categoriaController.GetByIdUsuario(idUsuario);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public void GetByTipoCategoria_WithTodas_ReturnsOkResult()
        {
            // Arrange
            var idUsuario = 1;
            var tipoCategoria = despesas_backend_api_net_core.Domain.Entities.TipoCategoria.Todas;
            _mockCategoriaBusiness.Setup(b => b.FindAll(idUsuario)).Returns(categorias);

            // Act
            var result = _categoriaController.GetByTipoCategoria(idUsuario, tipoCategoria);

            // Assert
            Assert.IsType<OkObjectResult>(result);
        }

                
        [Fact]
        public void Post_WithValidData_ReturnsOkResult()
        {
            // Arrange
            var categoria = new CategoriaVM
            {
                Id = 1,
                Descricao = "Teste Categoria",
                IdTipoCategoria = 1,
                IdUsuario = 1,
            };

            _mockCategoriaBusiness.Setup(b => b.Create(categoria)).Returns(categoria);

            // Act
            var result = _categoriaController.Post(categoria);
            
            // Assert
            Assert.IsType<ObjectResult>(result);
        }

        [Fact]
        public void Post_WithInvalidData_ReturnsBadRequestResult()
        {
            // Arrange
            var categoria = new CategoriaVM { };


            // Act
            var result = _categoriaController.Post(categoria);

            // Assert
            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public void Post_WithData_TipoCategotia_Todas_ReturnsBadRequestResult()
        {
            // Arrange
            var categoria = new CategoriaVM { IdTipoCategoria = 0 };


            // Act
            var result = _categoriaController.Post(categoria);

            // Assert
            Assert.IsType<BadRequestObjectResult>(result);
        }


        [Fact]
        public void Put_WithValidData_ReturnsOkResult()
        {
            // Arrange
            var categoria = new CategoriaVM
            {
                Id = 1,
                Descricao = "Alimentação",
                IdTipoCategoria = 1,
                IdUsuario = 1
            };

            var resultCategoria = new CategoriaVM
            {
                Id = 1,
                Descricao = "Alteração Alimentação",
                IdTipoCategoria = 2,
                IdUsuario = 1
            };

            _mockCategoriaBusiness.Setup(b => b.Update(categoria)).Returns(resultCategoria);

            // Act
            var result = _categoriaController.Put(categoria);

            // Assert
            Assert.Equal(resultCategoria.Id, categoria.Id);
            Assert.NotEqual(resultCategoria.Descricao, categoria.Descricao);
            Assert.NotEqual(resultCategoria.IdTipoCategoria, categoria.IdTipoCategoria);
            Assert.Equal(categoria.IdUsuario, categoria.IdUsuario);
        }

        [Fact]
        public void Put_WithInvalidData_ReturnsBadRequestResult()
        {
            // Arrange
            var categoria = new CategoriaVM
            {
                Id = 1,
                Descricao = "Salário",
                IdTipoCategoria = 1,
                IdUsuario = 1
            };


            // Act
            var result = _categoriaController.Put(categoria);

            // Assert
            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public void Delete_WithValidId_ReturnsNoContentResult()
        {
            // Arrange
            var categoria = new CategoriaVM
            {
                Id = 1,
                Descricao = "Salário",
                IdTipoCategoria = 1,
                IdUsuario = 1
            };
            // Act
            var result = _categoriaController.Delete(categoria);

            // Assert
            Assert.IsType<NoContentResult>(result);
        }
    }
}
