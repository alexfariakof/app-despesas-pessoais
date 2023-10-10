using despesas_backend_api_net_core.Infrastructure.Data.Repositories.Generic;

namespace Test.XUnit.Infrastructure.Data.Repositories.Generic
{
    public class GenericRepositorioTest
    {
        private Dictionary<Type, object> entityInstances;
        private static Dictionary<Type, object> entityListInstances;
        private RegisterContext _context;
        private object GetEntityInstance(Type entityType)
        {
            if (entityInstances.ContainsKey(entityType))
            {
                return entityInstances[entityType];
            }
            else
            {
                throw new InvalidOperationException($"Tipo de Lista da entidade não mapeada: {entityType}");
            }
        }

        private object GetEntityListInstance(Type entityType)
        {
            if (entityInstances.ContainsKey(entityType))
            {
                return entityListInstances[entityType];
            }
            else
            {
                throw new InvalidOperationException($"Tipo de entidade não mapeado: {entityType}");
            }
        }

        public GenericRepositorioTest()
        {
            entityInstances = new Dictionary<Type, object>
            {
                { typeof(Categoria), CategoriaFaker.GetNewFaker(UsuarioFaker.GetNewFaker()) },
                { typeof(Usuario), UsuarioFaker.GetNewFaker() },
            };

            entityListInstances = new Dictionary<Type, object>
{               { typeof(Categoria), CategoriaFaker.Categorias() },
                { typeof(Usuario), UsuarioFaker.Usuarios() },
            };

            var options = new DbContextOptionsBuilder<RegisterContext>()
            .UseInMemoryDatabase(databaseName: "Test DataBase")
            .Options;

            _context = new RegisterContext(options);
        }

        [Theory]
        [InlineData(typeof(Categoria))]
        [InlineData(typeof(Usuario))]
        public void Insert_Should_Add_Item_To_Database(Type entityType)
        {
            // Arrange
            var repositoryType = typeof(GenericRepositorio<>).MakeGenericType(entityType);
            var repository = Activator.CreateInstance(repositoryType, _context);
            var entityInstance = GetEntityInstance(entityType);
            var insertMethod = repository.GetType().GetMethod("Insert");

            // Act
            var insertedItem = insertMethod.Invoke(repository, new object[] { entityInstance });

            // Assert
            Assert.NotNull(insertedItem);
        }

        [Theory]
        [InlineData(typeof(Categoria))]
        [InlineData(typeof(Usuario))]

        public void Update_Should_Update_Item(Type entityType)
        {
            // Arrange
            var repositoryType = typeof(GenericRepositorio<>).MakeGenericType(entityType);
            var repository = Activator.CreateInstance(repositoryType, _context);
            var entityInstance = GetEntityInstance(entityType);
            var insertMethod = repository.GetType().GetMethod("Insert");
            var updateMethod = repository.GetType().GetMethod("Update");

            // Act
            var insertedItem = insertMethod.Invoke(repository, new object[] { entityInstance });            
            var updateItem = updateMethod.Invoke(repository, new object[] { insertedItem });

            // Assert
            Assert.NotNull(updateItem);
        }


        [Theory]
        [InlineData(typeof(Categoria))]
        [InlineData(typeof(Usuario))]
        public void GetAll_Should_Return_All_Items(Type entityType)
        {

            // Arrange
            var repositoryType = typeof(GenericRepositorio<>).MakeGenericType(entityType);
            var repository = Activator.CreateInstance(repositoryType, _context);
            var entityInstance = GetEntityInstance(entityType);
            var getAllMethod = repository.GetType().GetMethod("GetAll");

            // Act
            var items = getAllMethod.Invoke(repository, null);

            // Assert
            Assert.NotNull(items);
        }

        [Theory]
        [InlineData(typeof(Categoria))]
        [InlineData(typeof(Usuario))]
        public void Delete_Should_Delete_Item(Type entityType)
        {
            // Arrange
            var repositoryType = typeof(GenericRepositorio<>).MakeGenericType(entityType);
            var repository = Activator.CreateInstance(repositoryType, _context);
            var entityInstance = GetEntityInstance(entityType);
            var insertMethod = repository.GetType().GetMethod("Insert");
            var deleteMethod = repository.GetType().GetMethod("Delete");

            // Act
            var insertedItem = insertMethod.Invoke(repository, new object[] { entityInstance });
            var deletedItem = deleteMethod.Invoke(repository, new object[] { insertedItem });

            // Assert
            Assert.NotNull(deletedItem);
            // Assert
            Assert.True((bool)deletedItem);
            //Assert.Null(repository.Get(item.Id)); // Ensure the item is deleted
        }


        /*
        [Fact]
        public void Get_ShouldReturnItemById()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<RegisterContext>()
                .UseInMemoryDatabase(databaseName: "Get_ShouldReturnItemById")
                .Options;
            using var context = new RegisterContext(options);
            var repository = new GenericRepositorio<YourEntity>(context);
            var item = new YourEntity();
            context.Add(item);
            context.SaveChanges();

            // Act
            var retrievedItem = repository.Get(item.Id);

            // Assert
            Assert.NotNull(retrievedItem);
            Assert.Equal(item, retrievedItem);
        }

        [Fact]
        public void Update_ShouldUpdateItem()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<RegisterContext>()
                .UseInMemoryDatabase(databaseName: "Update_ShouldUpdateItem")
                .Options;
            using var context = new RegisterContext(options);
            var repository = new GenericRepositorio<YourEntity>(context);
            var item = new YourEntity();
            context.Add(item);
            context.SaveChanges();
            var updatedItem = new YourEntity
            {
                Id = item.Id,
                // Update the properties as needed
            };

            // Act
            var result = repository.Update(updatedItem);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(updatedItem, result);
        }


        */
    }
}