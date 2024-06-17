using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;

namespace Repository.Mapping;
public sealed class UsuarioMapTest
{
    [Fact]
    public void EntityConfiguration_IsValid()
    {
        // Arrange
        var options = new DbContextOptionsBuilder<RegisterContext>().UseInMemoryDatabase(databaseName: "UsuarioMapTest").Options;

        using (var context = new RegisterContext(options))
        {
            var builder = new ModelBuilder(new ConventionSet());
            var configuration = new UsuarioMap();

            // Act
            configuration.Configure(builder.Entity<Usuario>());

            var model = builder.Model;
            var entityType = model.FindEntityType(typeof(Usuario));

            // Act
            var idProperty = entityType?.FindProperty("Id");
            var emailProperty = entityType?.FindProperty("Email");
            var nomeProperty = entityType?.FindProperty("Nome");
            var sobrenomeProperty = entityType?.FindProperty("SobreNome");
            var telefoneProperty = entityType?.FindProperty("Telefone");
            var perfilUsuarioProperty = entityType?.FindNavigation("PerfilUsuario");
            var emailIndex = entityType?.GetIndexes().FirstOrDefault(index => index.Properties.Any(p => p.Name == "Email"));

            // Assert
            Assert.NotNull(idProperty);
            Assert.NotNull(emailProperty);
            Assert.NotNull(nomeProperty);
            Assert.NotNull(sobrenomeProperty);
            Assert.NotNull(telefoneProperty);
            Assert.NotNull(perfilUsuarioProperty);
            Assert.NotNull(emailIndex);
            Assert.True(idProperty.IsPrimaryKey());
            Assert.False(emailProperty.IsNullable);
            Assert.Equal(50, emailProperty.GetMaxLength());
            Assert.True(emailIndex.IsUnique);
            Assert.Equal(50, nomeProperty.GetMaxLength());
            Assert.Equal(50, sobrenomeProperty.GetMaxLength());
            Assert.Equal(15, telefoneProperty.GetMaxLength());
            Assert.True(telefoneProperty.IsNullable);
            var defaultPerfilValue = perfilUsuarioProperty.Sentinel;
            Assert.Null(defaultPerfilValue);
        }
    }
}