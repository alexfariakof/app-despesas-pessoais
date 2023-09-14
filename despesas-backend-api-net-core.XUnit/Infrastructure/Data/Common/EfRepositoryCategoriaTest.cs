using despesas_backend_api_net_core.Domain.Entities;
using despesas_backend_api_net_core.Domain.VM;
using despesas_backend_api_net_core.Infrastructure.Data.Common;
using Microsoft.EntityFrameworkCore;
using Moq;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Test.XUnit.Infrastructure.Data.Common
{
    public class EfRepositoryCategoriaTest 
    {
        private readonly Mock<RegisterContext> _dbContextMock;
        private readonly EfRepository<Categoria> _repository;
        private readonly List<Categoria> _categorias;

        public EfRepositoryCategoriaTest()
        {
            _dbContextMock = new Mock<RegisterContext>();
            _repository = new EfRepository<Categoria>(_dbContextMock.Object);

            // Cria uma lista de categorias para ser usada nos testes
            _categorias = new List<Categoria>
            {
                new Categoria { Id = 1, Descricao = "Aluguel", TipoCategoria = TipoCategoria.Despesa },
                new Categoria { Id = 2, Descricao = "Salário", TipoCategoria = TipoCategoria.Receita },
                new Categoria { Id = 3, Descricao = "Lanche", TipoCategoria = TipoCategoria.Despesa },
            };
        }

        [Fact]
        public void GetAll_ShouldReturnAllCategories()
        {
            // Arrange
            var dbSetMock = CreateDbSetMock(_categorias);
            _dbContextMock.Setup(db => db.Set<Categoria>()).Returns(dbSetMock.Object);

            // Act
            var result = _repository.GetAll();

            // Assert
            Assert.Equal(_categorias.Count, result.Count());
        }

        [Fact]
        public void Get_WithValidId_ShouldReturnCategory()
        {
            // Arrange
            var categoriaId = 2;
            var categoria = _categorias.First(c => c.Id == categoriaId);
            var dbSetMock = CreateDbSetMock(_categorias);
            _dbContextMock.Setup(db => db.Set<Categoria>()).Returns(dbSetMock.Object);

            // Act
            var result = _repository.Get(categoriaId);

            // Assert
            Assert.Equal(categoria, result);
        }

        [Fact]
        public void Get_WithInvalidId_ShouldReturnNull()
        {
            // Arrange
            var invalidCategoriaId = 10;
            var dbSetMock = CreateDbSetMock(_categorias);
            _dbContextMock.Setup(db => db.Set<Categoria>()).Returns(dbSetMock.Object);

            // Act
            var result = _repository.Get(invalidCategoriaId);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public void Insert_ShouldAddCategoryToDbContext()
        {
            // Arrange
            var categoria = new Categoria { Id = 4, Descricao = "Nova Categoria", TipoCategoria = TipoCategoria.Despesa };
            var dbSetMock = CreateDbSetMock(_categorias);
            _dbContextMock.Setup(db => db.Set<Categoria>()).Returns(dbSetMock.Object);

            // Act
            _repository.Insert(categoria);

            // Assert
            dbSetMock.Verify(dbSet => dbSet.Add(categoria), Times.Once);
            _dbContextMock.Verify(db => db.SaveChanges(), Times.Once);
        }

        [Fact]
        public void Update_ShouldUpdateCategoryInDbContext()
        {
            // Arrange
            var categoriaId = 3;
            var categoria = _categorias.First(c => c.Id == categoriaId);
            var dbSetMock = CreateDbSetMock(_categorias);
            _dbContextMock.Setup(db => db.Set<Categoria>()).Returns(dbSetMock.Object);

            // Act
            _repository.Update(categoria);

            // Assert
            _dbContextMock.Verify(db => db.SaveChanges(), Times.Once);
        }

        [Fact]
        public void Delete_ShouldRemoveCategoryFromDbContext()
        {
            // Arrange
            var categoriaId = 1;
            var categoria = _categorias.First(c => c.Id == categoriaId);
            var dbSetMock = CreateDbSetMock(_categorias);
            _dbContextMock.Setup(db => db.Set<Categoria>()).Returns(dbSetMock.Object);

            // Act
            _repository.Delete(categoriaId);

            // Assert
            dbSetMock.Verify(dbSet => dbSet.Remove(categoria), Times.Once);
            _dbContextMock.Verify(db => db.SaveChanges(), Times.Once);
        }

        private Mock<DbSet<Categoria>> CreateDbSetMock<Categoria>(List<Categoria> data) where Categoria : class
        {
            var queryableData = data.AsQueryable();
            var dbSetMock = new Mock<DbSet<Categoria>>();

            dbSetMock.As<IQueryable<Categoria>>().Setup(m => m.Provider).Returns(queryableData.Provider);
            dbSetMock.As<IQueryable<Categoria>>().Setup(m => m.Expression).Returns(queryableData.Expression);
            dbSetMock.As<IQueryable<Categoria>>().Setup(m => m.ElementType).Returns(queryableData.ElementType);
            dbSetMock.As<IQueryable<Categoria>>().Setup(m => m.GetEnumerator()).Returns(() => queryableData.GetEnumerator());

            return dbSetMock;
        }
    }
}
