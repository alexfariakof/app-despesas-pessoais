using Microsoft.EntityFrameworkCore.Metadata.Conventions;

namespace Repository.Mapping;
public sealed class ImagemPerfilUsuarioMapTest
{
    [Fact]
    public void EntityConfiguration_IsValid()
    {
        // Arrange
        var options = new DbContextOptionsBuilder<RegisterContext>().UseInMemoryDatabase(databaseName: "ImagemPerfilUsuarioMapTest").Options;

        using (var context = new RegisterContext(options))
        {
            var builder = new ModelBuilder(new ConventionSet());
            var configuration = new ImagemPerfilUsuarioMap();
            var model = builder.Model;
            var entityType = model.FindEntityType(typeof(ImagemPerfilUsuario));
            configuration.Configure(builder.Entity<ImagemPerfilUsuario>());

            var entity = builder.Model.FindEntityType(typeof(ImagemPerfilUsuario));

            // Act

            var idProperty = entity?.FindProperty("Id");
            var nameProperty = entity?.FindProperty("Name");
            var urlProperty = entity?.FindProperty("Url");
            var typeProperty = entity?.FindProperty("ContentType");
            var usuarioIdProperty = entity?.FindProperty("UsuarioId");
            var nameIndex = entity?.GetIndexes().FirstOrDefault(index => index.Properties.Any(p => p.Name == "Name"));
            var urlIndex = entity?.GetIndexes().FirstOrDefault(index => index.Properties.Any(p => p.Name == "Url"));
            var usuarioIdIndex = entity?.GetIndexes().FirstOrDefault(index => index.Properties.Any(p => p.Name == "UsuarioId"));

            // Assert
            Assert.NotNull(idProperty);
            Assert.NotNull(nameProperty);
            Assert.NotNull(urlProperty);
            Assert.NotNull(typeProperty);
            Assert.NotNull(usuarioIdProperty);
            Assert.NotNull(nameIndex);
            Assert.NotNull(urlIndex);
            Assert.NotNull(usuarioIdIndex);
            Assert.True(idProperty.IsPrimaryKey());
            Assert.False(nameProperty.IsNullable);
            Assert.Equal(50, nameProperty.GetMaxLength());
            Assert.True(nameIndex.IsUnique);
            Assert.False(urlProperty.IsNullable);
            Assert.False(typeProperty.IsNullable);
            Assert.Equal(20, typeProperty.GetMaxLength());
            Assert.False(usuarioIdProperty.IsNullable);
            Assert.True(usuarioIdIndex.IsUnique);
        }
    }
}