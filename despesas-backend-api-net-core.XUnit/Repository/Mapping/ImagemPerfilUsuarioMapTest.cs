using Microsoft.EntityFrameworkCore.Metadata.Conventions;

namespace Repository.Mapping;
public class ImagemPerfilUsuarioMapTest
{
    [Fact]
    public void EntityConfiguration_IsValid()
    {
        // Arrange
        var options = new DbContextOptionsBuilder<RegisterContext>()
            .UseInMemoryDatabase(databaseName: "InMemoryDatabase")
            .Options;

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
            var typeProperty = entity?.FindProperty("Type");
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
            Assert.Equal(4, typeProperty.GetMaxLength());
            Assert.False(usuarioIdProperty.IsNullable);
            Assert.True(usuarioIdIndex.IsUnique);
        }
    }

    [Fact]
    public void Should_Parse_ImagemPerfilUsuarioVM_To_ImagemPerfilUsuario()
    {
        // Arrange
        var map = new ImagemPerfilUsuarioMap();
        var origin = new ImagemPerfilVM
        {
            Id = 1,
            Name = "example.jpg",
            Type = "jpg",
            Url = "https://example.com/image.jpg",
            IdUsuario = 42
        };

        // Act
        var result = map.Parse(origin);

        // Assert
        Assert.Equal(origin.Id, result.Id);
        Assert.Equal(origin.Name, result.Name);
        Assert.Equal(origin.Type, result.Type);
        Assert.Equal(origin.Url, result.Url);
        Assert.Equal(origin.IdUsuario, result.UsuarioId);
    }

    [Fact]
    public void Should_Parse_ImagemPerfilUsuario_To_ImagemPerfilUsuarioVM()
    {
        // Arrange
        var map = new ImagemPerfilUsuarioMap();
        var origin = new ImagemPerfilUsuario
        {
            Id = 1,
            Name = "example.jpg",
            Type = "jpg",
            Url = "https://example.com/image.jpg",
            UsuarioId = 42
        };

        // Act
        var result = map.Parse(origin);

        // Assert
        Assert.Equal(origin.Id, result.Id);
        Assert.Equal(origin.Name, result.Name);
        Assert.Equal(origin.Type, result.Type);
        Assert.Equal(origin.Url, result.Url);
        Assert.Equal(origin.UsuarioId, result.IdUsuario);
    }

    [Fact]
    public void Should_Parse_List_ImagemPerfilUsuarioVM_To_ImagemPerfilUsuario_List()
    {
        // Arrange
        var map = new ImagemPerfilUsuarioMap();
        var usuarioVM = UsuarioFaker.Instance.GetNewFakerVM();
        var originList = ImagemPerfilUsuarioFaker.ImagensPerfilUsuarioVMs(
            usuarioVM,
            usuarioVM.Id
        );
        // Act
        var resultList = map.ParseList(originList);

        // Assert
        Assert.Equal(originList.Count, resultList.Count);
        for (int i = 0; i < originList.Count; i++)
        {
            Assert.Equal(originList[i].Id, resultList[i].Id);
            Assert.Equal(originList[i].Name, resultList[i].Name);
            Assert.Equal(originList[i].Type, resultList[i].Type);
            Assert.Equal(originList[i].Url, resultList[i].Url);
            Assert.Equal(originList[i].IdUsuario, resultList[i].UsuarioId);
        }
    }

    [Fact]
    public void Shhould_Parse_List_ImagemPerfilUsuario_To_ImagemPerfilUsuarioVM_List()
    {
        // Arrange
        var map = new ImagemPerfilUsuarioMap();
        var usuario = UsuarioFaker.Instance.GetNewFaker();
        var originList = ImagemPerfilUsuarioFaker.ImagensPerfilUsuarios(usuario, usuario.Id);

        // Act
        var resultList = map.ParseList(originList);

        // Assert
        Assert.Equal(originList.Count, resultList.Count);
        for (int i = 0; i < originList.Count; i++)
        {
            Assert.Equal(originList[i].Id, resultList[i].Id);
            Assert.Equal(originList[i].Name, resultList[i].Name);
            Assert.Equal(originList[i].Type, resultList[i].Type);
            Assert.Equal(originList[i].Url, resultList[i].Url);
            Assert.Equal(originList[i].UsuarioId, resultList[i].IdUsuario);
        }
    }
}
