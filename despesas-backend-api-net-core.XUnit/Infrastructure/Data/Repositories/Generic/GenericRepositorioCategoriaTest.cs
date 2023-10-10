using despesas_backend_api_net_core.Infrastructure.Data.Repositories.Generic;

namespace Test.XUnit.Infrastructure.Data.Repositories.Generic
{
    public class GenericRepositorioCategoriaTest
    {
        private Mock<RegisterContext> _dbContextMock;
        private Mock<GenericRepositorio<Categoria>> _repository;

        public GenericRepositorioCategoriaTest()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<RegisterContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;

            _dbContextMock = new Mock<RegisterContext>(options);
            _dbContextMock.Setup(c => c.Set<List<Categoria>>());
            _repository = new Mock<GenericRepositorio<Categoria>>(_dbContextMock);
        }

        [Fact]
        public void Insert_ShouldAddItemToDataSetAndSaveChanges()
        {
            // Arrange
            var item = new Categoria();
            var dataSet = new List<Categoria>();
            var dbSetMock = Usings.MockDbSet(dataSet);
            _dbContextMock.Setup(c => c.Set<Categoria>()).Returns(dbSetMock.Object);
            var repository = new GenericRepositorio<Categoria>(_dbContextMock.Object);

            // Act
            var result = repository.Insert(item);

            // Assert
            Assert.Equal(1, dataSet.Count);
            Assert.Contains(item, dataSet);
            _dbContextMock.Verify(c => c.SaveChanges(), Times.Once);
            Assert.Equal(item, result);
        }

        [Fact]
        public void GetAll_ShouldReturnAllItemsFromDataSet()
        {
            // Arrange
            var items = CategoriaFaker.Categorias();
            var dataSet = items;
            var dbSetMock = Usings.MockDbSet(dataSet);
            _dbContextMock.Setup(c => c.Set<Categoria>()).Returns(dbSetMock.Object);
            var repository = new GenericRepositorio<Categoria>(_dbContextMock.Object);

            // Act
            var result = repository.GetAll();

            // Assert
            Assert.Equal(items.Count, result.Count);
            Assert.Equal(items, result);
        }

        [Fact]
        public void Get_ShouldReturnItemWithMatchingId()
        {
            // Arrange
            var itens = UsuarioFaker.Usuarios();
            var item = itens.First();
            var itemId = item.Id;
            
            var dataSet = itens;
            var dbSetMock = Usings.MockDbSet(dataSet);
            _dbContextMock.Setup(c => c.Set<Usuario>()).Returns(dbSetMock.Object);
            var repository = new GenericRepositorio<Usuario>(_dbContextMock.Object);

            // Act
            var result = repository.Get(itemId);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<Usuario>(result);
            Assert.Equal(item, result);
        }

        [Fact]
        public void Update_ShouldUpdateItemAndSaveChanges()
        {
            // Arrange
            var dataSet = CategoriaFaker.Categorias();
            var dbSetMock = Usings.MockDbSet(dataSet);
            var existingItem = dataSet.First();
            var updatedItem = dataSet.First();
            updatedItem.Descricao = "Teste Update Item";

            _dbContextMock.Setup(c => c.Set<Categoria>()).Returns(dbSetMock.Object);

            
            var repository = new GenericRepositorio<Categoria>(_dbContextMock.Object);

            // Act
            var result = repository.Update(updatedItem);

            // Assert
            Assert.NotNull(result);
            Assert.NotEqual(existingItem, result);
            Assert.Equal(updatedItem.Descricao, result.Descricao);
            Assert.Equal(updatedItem, result);            
        }


        [Fact]
        public void Delete_WithExistingItem_ShouldRemoveItemAndSaveChanges()
        {
            // Arrange
            var lstCategorias = CategoriaFaker.Categorias();
            var itemId = lstCategorias.First().Id;
            var dataSet = lstCategorias;

            var dbSetMock = Usings.MockDbSet(dataSet);
            _dbContextMock.Setup(c => c.Set<Categoria>()).Returns(dbSetMock.Object);
            var repository = new GenericRepositorio<Categoria>(_dbContextMock.Object);

            // Act
            var result = repository.Delete(new BaseModel { Id = itemId });

            // Assert
            Assert.True(result);
            _dbContextMock.Verify(c => c.SaveChanges(), Times.Once);
        }

        [Fact]
        public void Delete_WithNonExistingItem_ShouldNotRemoveItemAndReturnFalse()
        {
            // Arrange
            
            var dataSet = CategoriaFaker.Categorias();
            var itemId = 0;
            var dbSetMock = Usings.MockDbSet(dataSet);
            _dbContextMock.Setup(c => c.Set<Categoria>()).Returns(dbSetMock.Object);
            var repository = new GenericRepositorio<Categoria>(_dbContextMock.Object);

            // Act
            var result = repository.Delete(new BaseModel { Id = itemId });

            // Assert            
            Assert.False(result);
            _dbContextMock.Verify(c => c.SaveChanges(), Times.Never);
        }
    }
}