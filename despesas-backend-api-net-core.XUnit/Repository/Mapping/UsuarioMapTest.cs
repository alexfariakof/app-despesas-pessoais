using Microsoft.EntityFrameworkCore.Metadata.Conventions;

namespace Repository.Mapping;
public class UsuarioMapTest
{
    [Fact]
    public void EntityConfiguration_IsValid()
    {
        // Arrange
        var options = new DbContextOptionsBuilder<RegisterContext>().UseInMemoryDatabase(databaseName: "InMemoryDatabase").Options;

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
            var perfilUsuarioProperty = entityType?.FindProperty("PerfilUsuario");
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
            var defaultPerfilValue = perfilUsuarioProperty.GetDefaultValue();
            Assert.Equal(PerfilUsuario.Usuario, defaultPerfilValue);
        }
    }

    [Fact]
    public void Should_Parse_UsuarioVM_To_Usuario()
    {
        // Arrange
        var map = new UsuarioMap();
        var origin = UsuarioFaker.Instance.GetNewFakerVM();

        // Act
        var result = map.Parse(origin);

        // Assert
        Assert.Equal(origin.Id, result.Id);
        Assert.Equal(origin.Email, result.Email);
        Assert.Equal(origin.Nome, result.Nome);
        Assert.Equal(origin.SobreNome, result.SobreNome);
        Assert.Equal(origin.Telefone, result.Telefone);
        Assert.Equal(origin.PerfilUsuario, result.PerfilUsuario);
    }

    [Fact]
    public void Should_Parse_Usuario_To_UsuarioVM()
    {
        // Arrange
        var map = new UsuarioMap();
        var origin = UsuarioFaker.Instance.GetNewFaker();

        // Act
        var result = map.Parse(origin);

        // Assert
        Assert.Equal(origin.Id, result.Id);
        Assert.Equal(origin.Email, result.Email);
        Assert.Equal(origin.Nome, result.Nome);
        Assert.Equal(origin.SobreNome, result.SobreNome);
        Assert.Equal(origin.Telefone, result.Telefone);
    }

    [Fact]
    public void Should_ParseList_UsuarioVM_List_To_Usuario_List()
    {
        // Arrange
        var map = new UsuarioMap();

        var originList = UsuarioFaker.Instance.GetNewFakersUsuarios();

        // Act
        var resultList = map.ParseList(originList);

        // Assert
        Assert.Equal(originList.Count, resultList.Count);
        for (int i = 0; i < originList.Count; i++)
        {
            Assert.Equal(originList[i].Id, resultList[i].Id);
            Assert.Equal(originList[i].Email, resultList[i].Email);
            Assert.Equal(originList[i].Nome, resultList[i].Nome);
            Assert.Equal(originList[i].SobreNome, resultList[i].SobreNome);
            Assert.Equal(originList[i].Telefone, resultList[i].Telefone);
        }
    }

    [Fact]
    public void Should_ParseList_Usuario_List_To_UsuarioVM_List()
    {
        // Arrange
        var map = new UsuarioMap();
        var originList = UsuarioFaker.Instance.GetNewFakersUsuariosVMs();

        // Act
        var resultList = map.ParseList(originList);

        // Assert
        Assert.Equal(originList.Count, resultList.Count);
        for (int i = 0; i < originList.Count; i++)
        {
            Assert.Equal(originList[i].Id, resultList[i].Id);
            Assert.Equal(originList[i].Email, resultList[i].Email);
            Assert.Equal(originList[i].Nome, resultList[i].Nome);
            Assert.Equal(originList[i].SobreNome, resultList[i].SobreNome);
            Assert.Equal(originList[i].Telefone, resultList[i].Telefone);
        }
    }
}
