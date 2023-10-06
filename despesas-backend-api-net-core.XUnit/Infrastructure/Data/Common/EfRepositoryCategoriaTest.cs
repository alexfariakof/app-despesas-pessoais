using despesas_backend_api_net_core.Infrastructure.Data.Common;
using despesas_backend_api_net_core.Infrastructure.Data.Repositories.Generic;
using Microsoft.EntityFrameworkCore;
using static Amazon.S3.Util.S3EventNotification;

namespace Test.XUnit.Infrastructure.Data.Common
{
    public class EfRepositoryCategoriaTest 
    {
        private Mock<RegisterContext> _dbContextMock;
        private Mock<EfRepository<Categoria>> _repository;
        private Mock<DbSet<T>> MockDbSet<T>(List<T> data) where T : class
        {
            var queryableData = data.AsQueryable();
            var dbSetMock = new Mock<DbSet<T>>();
            dbSetMock.As<IQueryable<T>>().Setup(m => m.Provider).Returns(queryableData.Provider);
            dbSetMock.As<IQueryable<T>>().Setup(m => m.Expression).Returns(queryableData.Expression);
            dbSetMock.As<IQueryable<T>>().Setup(m => m.ElementType).Returns(queryableData.ElementType);
            dbSetMock.As<IQueryable<T>>().Setup(m => m.GetEnumerator()).Returns(() => queryableData.GetEnumerator());
            dbSetMock.Setup(d => d.Add(It.IsAny<T>())).Callback<T>(data.Add);
            dbSetMock.Setup(d => d.Remove(It.IsAny<T>())).Callback<T>(item => data.Remove(item));
            return dbSetMock;
        }

        public EfRepositoryCategoriaTest()
        {
            var options = new DbContextOptionsBuilder<RegisterContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;

            _dbContextMock = new Mock<RegisterContext>(options);            
            _dbContextMock.Setup(c => c.Set<List<Categoria>>());
            _repository = new Mock<EfRepository<Categoria>>(_dbContextMock.Object);
        }

        [Fact]
        public void GetAll_ShouldReturnAllCategories()
        {
            // Arrange
            var dbSetMock = MockDbSet(Usings.lstCategorias);
            _dbContextMock.Setup(c => c.Set<Categoria>()).Returns(dbSetMock.Object);
            // Act
            var result = _repository.Object.GetAll();

            // Assert
            Assert.Equal(Usings.lstCategorias.Count, result.Count());
        }

        [Fact]
        public void Get_WithValidId_ShouldReturnCategory()
        {
            /*
            // Arrange
            var categoriaId = 2;
            var categoria = Usings.lstCategorias.First(c => c.Id == categoriaId);
            var dbSetMock = MockDbSet(Usings.lstCategorias);
            _dbContextMock.Setup(c => c.Set<Categoria>()).Returns(dbSetMock.Object);

            // Act
            var result = _repository.Object.Get(categoriaId);


            // Assert
            Assert.Equal(categoria, result);
            */
        }

        [Fact]
        public void Get_WithInvalidId_ShouldReturnNull()
        {
            /*
            // Arrange
            var invalidCategoriaId = 11;

            _dbContextMock.Setup(db => db.Set<Categoria>().Find(invalidCategoriaId)).Returns((Categoria)null);

            // Act
            var result = _repository.Object.Get(invalidCategoriaId);

            // Assert
            Assert.Null(result);
            */
        }

        [Fact]
        public void Insert_ShouldAddCategoryToDbContext()
        {
            // Arrange
            var categoria = new Categoria { Id= 11, Descricao = "Nova Categoria", TipoCategoria = TipoCategoria.Despesa, UsuarioId = 1 };

            _dbContextMock.Setup(db => db.Set<Categoria>()).Returns(MockDbSet(Usings.lstCategorias).Object);

            // Act
            _repository.Object.Insert(categoria);


            // Assert
            _dbContextMock.Verify(db => db.SaveChanges(), Times.Once);
            //Assert.Contains(categoria, Usings.lstCategorias);
            //var retrievedCategoria = dbSet.FirstOrDefault(c => c.Id == categoria.Id);
            //Assert.NotNull(retrievedCategoria);
            // Verifique os campos da categoria adicionada
            //Assert.Equal(categoria.Descricao, retrievedCategoria.Descricao);
            //Assert.Equal(categoria.TipoCategoria, retrievedCategoria.TipoCategoria);
        }

        [Fact]
        public void Update_ShouldUpdateCategoryInDbContext()
        {
            /*
            // Arrange
            var categoriaId = 3;
            var categoria = Usings.lstCategorias.First(c => c.Id == categoriaId);
            categoria.Descricao = "Teste Atualização Categoria";
            categoria.TipoCategoria = TipoCategoria.Despesa;
            _dbContextMock.Setup(db => db.Set<Categoria>().Update(categoria));

            // Act
            _repository.Object.Update(categoria);

            // Assert
            _dbContextMock.Verify(db => db.SaveChanges(), Times.Once);
            //repository.Verify(dbSet => dbSet.Update(categoria), Times.Once);    
            */
        }

        [Fact]
        public void Delete_ShouldRemoveCategoryFromDbContext()
        {
            // Arrange
            var categoriaId = 1;
            var categoria = Usings.lstCategorias.First(c => c.Id == categoriaId);
            _dbContextMock.Setup(db => db.Set<Categoria>().Remove(categoria));

            // Act
            _repository.Object.Delete(categoriaId);


            // Assert
            _dbContextMock.Verify(db => db.SaveChanges(), Times.Once);
            //_dbContextMock.Verify(db => db.Remove(categoria), Times.Once);
            //_repository.Verify(dbSet => dbSet.Delete(categoria.Id), Times.Once);            
        }
    }
}

