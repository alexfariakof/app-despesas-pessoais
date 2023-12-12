using Xunit.Extensions.Ordering;

namespace Infrastructure.Common
{
    [Order(201)]
    public class RegisterContextTest
    {
        [Fact]
        public void RegisterContext_Should_Have_DbSets()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<RegisterContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;

            // Act
            using (var context = new RegisterContext(options))
            {
                // Assert
                Assert.NotNull(context.ControleAcesso);
                Assert.NotNull(context.Usuario);
                Assert.NotNull(context.ImagemPerfilUsuario);
                Assert.NotNull(context.Despesa);
                Assert.NotNull(context.Receita);
                Assert.NotNull(context.Categoria);
                Assert.NotNull(context.Lancamento);
            }
        }

        [Fact]
        public void RegisterContext_Should_Apply_Configurations()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<RegisterContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;

            // Act
            using (var context = new RegisterContext(options))
            {
                // Assert
                var model = context.Model;
                Assert.True(model.FindEntityType(typeof(Categoria)) != null);
                Assert.True(model.FindEntityType(typeof(Usuario)) != null);
                Assert.True(model.FindEntityType(typeof(ImagemPerfilUsuario)) != null);
                Assert.True(model.FindEntityType(typeof(ControleAcesso)) != null);
                Assert.True(model.FindEntityType(typeof(Despesa)) != null);
                Assert.True(model.FindEntityType(typeof(Receita)) != null);
                Assert.True(model.FindEntityType(typeof(Lancamento)) != null);
            }
        }
    }
}
