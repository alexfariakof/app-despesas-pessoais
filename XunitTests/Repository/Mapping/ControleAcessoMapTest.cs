using Microsoft.EntityFrameworkCore.Metadata.Conventions;

namespace Repository.Mapping;
public sealed class ControleAcessoMapTest
{
    [Fact]
    public void EntityConfiguration_IsValid()
    {
        // Arrange
        var options = new DbContextOptionsBuilder<RegisterContext>().UseInMemoryDatabase(databaseName: "ControleAcessoMapTest").Options;

        using (var context = new RegisterContext(options))
        {
            var builder = new ModelBuilder(new ConventionSet());
            var configuration = new ControleAcessoMap();

            // Act
            configuration.Configure(builder.Entity<ControleAcesso>());

            var model = builder.Model;
            var entityType = model.FindEntityType(typeof(ControleAcesso));

            var idProperty = entityType?.FindProperty("Id");

            var loginProperty = entityType?.FindProperty("Login");
            var usuarioIdProperty = entityType?.FindProperty("UsuarioId");
            var index = new[] { loginProperty  };
            var loginIndex = entityType?.FindIndex(index);

            // Assert
            Assert.NotNull(idProperty);
            Assert.NotNull(loginProperty);
            Assert.NotNull(usuarioIdProperty);
            Assert.NotNull(loginIndex);
            Assert.True(idProperty.IsPrimaryKey());
            Assert.False(idProperty.IsNullable);
            Assert.False(loginProperty.IsNullable);
            Assert.Equal(100, loginProperty.GetMaxLength());
            Assert.True(loginIndex.IsUnique);
        }
    }
}
