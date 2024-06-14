using Microsoft.EntityFrameworkCore.Metadata.Conventions;

namespace Repository.Mapping;
public sealed class DespesaMapTest
{
    [Fact]
    public void EntityConfiguration_IsValid()
    {
        // Arrange
        var options = new DbContextOptionsBuilder<RegisterContext>().UseInMemoryDatabase(databaseName: "DespesaMapTest").Options;

        using (var context = new RegisterContext(options))
        {
            var builder = new ModelBuilder(new ConventionSet());
            var configuration = new DespesaMap();

            configuration.Configure(builder.Entity<Despesa>());

            var model = builder.Model;
            var entityType = model.FindEntityType(typeof(Despesa));

            // Act

            var idProperty = entityType?.FindProperty("Id");

            var descricaoProperty = entityType?.FindProperty("Descricao");
            var usuarioIdProperty = entityType?.FindProperty("UsuarioId");
            var categoriaIdProperty = entityType?.FindProperty("CategoriaId");
            var dataProperty = entityType?.FindProperty("Data");
            var dataVencimentoProperty = entityType?.FindProperty("DataVencimento");
            var valorProperty = entityType?.FindProperty("Valor");

            // Assert
            Assert.NotNull(idProperty);
            Assert.NotNull(descricaoProperty);
            Assert.NotNull(usuarioIdProperty);
            Assert.NotNull(categoriaIdProperty);
            Assert.NotNull(dataProperty);
            Assert.NotNull(dataVencimentoProperty);
            Assert.NotNull(valorProperty);
            Assert.True(idProperty.IsPrimaryKey());
            Assert.True(descricaoProperty.IsNullable);
            Assert.Equal(100, descricaoProperty.GetMaxLength());
            Assert.False(usuarioIdProperty.IsNullable);
            Assert.False(categoriaIdProperty.IsNullable);
            Assert.Equal(typeof(DateTime), dataProperty.ClrType);
            //Assert.Equal("datetime", dataProperty.GetColumnType());
            Assert.Equal(DateTime.MinValue, dataProperty.GetDefaultValue());
            Assert.True(dataVencimentoProperty.IsNullable);
            Assert.Null(dataVencimentoProperty.GetDefaultValue());
            Assert.Equal(typeof(decimal), valorProperty.ClrType);
            Assert.Equal("decimal(10, 2)", valorProperty.GetColumnType());
            Assert.Equal(0, (decimal)(valorProperty?.GetDefaultValue() ?? 0));
        }
    }
}