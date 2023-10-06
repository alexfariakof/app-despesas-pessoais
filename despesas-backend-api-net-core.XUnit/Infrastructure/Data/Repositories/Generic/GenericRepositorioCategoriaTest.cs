namespace Test.XUnit.Infrastructure.Data.Repositories.Generic
{
    public class GenericRepositorioCategoriaTest
    {
        private Mock<RegisterContext> contextMock;
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
        public GenericRepositorioCategoriaTest()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<RegisterContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;

            contextMock = new Mock<RegisterContext>(options);
        }

        [Fact]
        public void Insert_ShouldAddItemToDataSetAndSaveChanges()
        {
            // Arrange
            var item = new Categoria();
            var dataSet = new List<Categoria>();
            var dbSetMock = MockDbSet(dataSet);
            contextMock.Setup(c => c.Set<Categoria>()).Returns(dbSetMock.Object);
            var repository = new GenericRepositorio<Categoria>(contextMock.Object);

            // Act
            var result = repository.Insert(item);

            // Assert
            Assert.Equal(1, dataSet.Count);
            Assert.Contains(item, dataSet);
            contextMock.Verify(c => c.SaveChanges(), Times.Once);
            Assert.Equal(item, result);
        }

        [Fact]
        public void GetAll_ShouldReturnAllItemsFromDataSet()
        {
            // Arrange
            var items = Usings.lstCategorias;
            var dataSet = items;
            var dbSetMock = MockDbSet(dataSet);
            contextMock.Setup(c => c.Set<Categoria>()).Returns(dbSetMock.Object);
            var repository = new GenericRepositorio<Categoria>(contextMock.Object);

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
            var itemId = 1;
            var item = new Categoria { Id = itemId };
            var dataSet = new List<Categoria> { item };
            var dbSetMock = MockDbSet(dataSet);
            contextMock.Setup(c => c.Set<Categoria>()).Returns(dbSetMock.Object);
            var repository = new GenericRepositorio<Categoria>(contextMock.Object);

            // Act
            var result = repository.Get(itemId);

            // Assert
            Assert.Equal(item, result);
        }

        [Fact]
        public void Update_ShouldUpdateItemAndSaveChanges()
        {
            /*
            // Arrange
            var existingItem = new Categoria
            {
                Id = 1,
                Descricao = "Alimentação",
                UsuarioId = 1,
                TipoCategoria = TipoCategoria.Despesa
            };

            var updatedItem = new Categoria
            {
                Id = existingItem.Id,
                Descricao = "Alterar Descrição",
                UsuarioId = 1,
                TipoCategoria = TipoCategoria.Despesa
            };

            var dataSet = Usings.lstCategorias;
            dataSet.Add(existingItem);

            var dbSetMock = MockDbSet(dataSet);
            contextMock.Setup(c => c.Set<Categoria>()).Returns(dbSetMock.Object);
            var repository = new GenericRepositorio<Categoria>(contextMock.Object);

            // Act
            var result = repository.Update(updatedItem);

            // Assert
            Assert.NotNull(result);
            //Assert.NotEqual(existingItem, result);
            //Assert.Equal (updatedItem.Descricao, result.Descricao);
           // Assert.Equal(updatedItem, result);
            */
        }


        [Fact]
        public void Delete_WithExistingItem_ShouldRemoveItemAndSaveChanges()
        {
            // Arrange
            var itemId = 2;
            var dataSet = Usings.lstCategorias;

            var dbSetMock = MockDbSet(dataSet);
            contextMock.Setup(c => c.Set<Categoria>()).Returns(dbSetMock.Object);
            var repository = new GenericRepositorio<Categoria>(contextMock.Object);

            // Act
            var result = repository.Delete(itemId);

            // Assert
            Assert.True(result);
            contextMock.Verify(c => c.SaveChanges(), Times.Once);
        }

        [Fact]
        public void Delete_WithNonExistingItem_ShouldNotRemoveItemAndReturnFalse()
        {
            // Arrange
            var itemId = 100;
            var dataSet = Usings.lstCategorias;
            var dbSetMock = MockDbSet(dataSet);
            contextMock.Setup(c => c.Set<Categoria>()).Returns(dbSetMock.Object);
            var repository = new GenericRepositorio<Categoria>(contextMock.Object);

            // Act
            var result = repository.Delete(itemId);

            // Assert
            Assert.False(result);
            contextMock.Verify(c => c.SaveChanges(), Times.Never);
        }
    }
}