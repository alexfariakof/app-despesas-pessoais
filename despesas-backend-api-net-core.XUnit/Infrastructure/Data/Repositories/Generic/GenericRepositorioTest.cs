using despesas_backend_api_net_core.Infrastructure.Data.Repositories.Generic;
using Xunit.Extensions.Ordering;

namespace Test.XUnit.Infrastructure.Data.Repositories.Generic
{
    public class GenericRepositorioTest
    {
        private Mock<RegisterContext> _dbContextMock;
        public GenericRepositorioTest()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<RegisterContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;

            _dbContextMock = new Mock<RegisterContext>(options);
            _dbContextMock.Setup(c => c.Set<List<Categoria>>());
        }

        [Fact]
        public void Insert_Should_Add_Item_And_SaveChanges()
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
            Assert.Single(dataSet);
            Assert.Contains(item, dataSet);
            _dbContextMock.Verify(c => c.SaveChanges(), Times.Once);
            Assert.Equal(item, result);
        }

        [Fact]
        public void GetAll_Should_Return_All_Items()
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
            Assert.Equal(items.Count, result.Count());
            Assert.Equal(items, result);
        }

        [Fact, Order(1)]
        public void Get_Should_Return_Item_With_Matching_Id()
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
        public void Update_Should_Update_Item_And_SaveChanges()
        {
            // Arrange
            var dataSet = CategoriaFaker.Categorias();
            var existingItem = dataSet.First();
            var dbContext = new RegisterContext(new DbContextOptionsBuilder<RegisterContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options);

            var repository = new GenericRepositorio<Categoria>(dbContext);

            // Act
            var result = repository.Insert(existingItem);
            var updatedItem = new Categoria
            {
                Id = result.Id,
                Descricao = "Teste Update Item",
                UsuarioId = result.Id,
                Usuario = result.Usuario,
                TipoCategoria = TipoCategoria.Receita
            };

            result = repository.Update(updatedItem);

            // Assert
            Assert.NotNull(result);
            Assert.NotEqual(existingItem, result);
            Assert.Equal(updatedItem.Descricao, result.Descricao);
            Assert.Equal(updatedItem, result);            
        }

        /// <summary>
        /// Testa se o método Delete é executado quando o tipo do objeto é Usuário e seta como Intativo sem realizar deleção.
        /// </summary>
        [Fact]
        public void Delete_Should_Set_Inativo_And_Return_True_When_Usuario_IsDeleted() 
        {
            // Arrange
            var lstUsuarios = UsuarioFaker.Usuarios();
            var usuario = lstUsuarios.First();
            var options = new DbContextOptionsBuilder<RegisterContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;
            var _dbContextMock = new RegisterContext(options);
            _dbContextMock.Usuario.AddRange(lstUsuarios);
            _dbContextMock.SaveChanges();
            var _repository = new GenericRepositorio<Usuario>(_dbContextMock);

            // Act
            bool result = _repository.Delete(usuario);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void Update_Should_Try_Update_Item_And_Return_Null()
        {
            // Arrange
            var dataSet = CategoriaFaker.Categorias();
            var existingItem = dataSet.First();
                   
            var dbContext = new RegisterContext(new DbContextOptionsBuilder<RegisterContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options);


            var repository = new GenericRepositorio<Categoria>(dbContext);

            // Act
            var result = repository.Update(existingItem);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public void Delete_With_Existing_Item_Should_Remove_Item_And_SaveChanges()
        {
            // Arrange
            var lstCategorias = CategoriaFaker.Categorias();
            var item = lstCategorias.Last();
            var dataSet = lstCategorias;

            var dbSetMock = Usings.MockDbSet(dataSet);
            _dbContextMock.Setup(c => c.Set<Categoria>()).Returns(dbSetMock.Object);
            var repository = new GenericRepositorio<Categoria>(_dbContextMock.Object);

            // Act
            var result = repository.Delete(item);

            // Assert
            Assert.True(result);
            _dbContextMock.Verify(c => c.SaveChanges(), Times.Once);
        }

        [Fact]
        public void Delete_With_Non_Existing_Item_Should_Not_Remove_Item_And_Return_False()
        {
            // Arrange
            var dataSet = CategoriaFaker.Categorias();
            var item = new Categoria { Id = 0 };
            var dbSetMock = Usings.MockDbSet(dataSet);
            _dbContextMock.Setup(c => c.Set<Categoria>()).Returns(dbSetMock.Object);
            var repository = new GenericRepositorio<Categoria>(_dbContextMock.Object);

            // Act
            var result = repository.Delete(item);

            // Assert            
            Assert.False(result);
            _dbContextMock.Verify(c => c.SaveChanges(), Times.Never);
        }

        [Fact]
        public void Delete_Should_Throw_Exception()
        {
            // Arrange
            var lstCategorias = CategoriaFaker.Categorias();
            var item = lstCategorias.Last();
            var dataSet = lstCategorias;

            var dbSetMock = Usings.MockDbSet(dataSet);

            _dbContextMock.Setup(c => c.Set<Categoria>()).Returns(dbSetMock.Object);
            _dbContextMock.Setup(c => c.Remove(It.IsAny<Categoria>())).Throws<Exception>();
            var repository = new GenericRepositorio<Categoria>(_dbContextMock.Object);

            // Act and Assert
            var exception = Assert.Throws<Exception>(() => repository.Delete(item));
            Assert.Equal("GenericRepositorio_Delete", exception.Message);
        }
    }
}