using Microsoft.EntityFrameworkCore.Metadata.Conventions;

namespace Repository.Mapping;
public class LancamentoParserTest
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
            var configuration = new LancamentoMap();

            configuration.Configure(builder.Entity<Lancamento>());

            var model = builder.Model;
            var entityType = model.FindEntityType(typeof(Lancamento));

            // Act

            var idProperty = entityType?.FindProperty("Id");

            var usuarioIdProperty = entityType?.FindProperty("UsuarioId");
            var despesaIdProperty = entityType?.FindProperty("DespesaId");
            var receitaIdProperty = entityType?.FindProperty("ReceitaId");
            var dataProperty = entityType?.FindProperty("Data");
            var dataCriacaoProperty = entityType?.FindProperty("DataCriacao");
            var valorProperty = entityType?.FindProperty("Valor");
            var descricaoProperty = entityType?.FindProperty("Descricao");

            // Assert
            Assert.NotNull(idProperty);
            Assert.NotNull(usuarioIdProperty);
            Assert.NotNull(despesaIdProperty);
            Assert.NotNull(receitaIdProperty);
            Assert.NotNull(dataProperty);
            Assert.NotNull(dataCriacaoProperty);
            Assert.NotNull(valorProperty);
            Assert.NotNull(descricaoProperty);

            Assert.True(idProperty.IsPrimaryKey());
            Assert.False(usuarioIdProperty.IsNullable);
            Assert.True(despesaIdProperty.IsNullable);
            Assert.Null(despesaIdProperty.GetDefaultValue());
            Assert.True(receitaIdProperty.IsNullable);
            Assert.Null(receitaIdProperty.GetDefaultValue());
            Assert.True(dataProperty.GetColumnType() == "timestamp");
            Assert.False(dataProperty.IsNullable);
            Assert.True(dataCriacaoProperty.GetColumnType() == "timestamp");
            //Assert.Equal(DateTime.Now, dataCriacaoProperty.GetDefaultValue());
            Assert.True(valorProperty.GetColumnType() == "decimal(10, 2)");
            Assert.Equal(100, descricaoProperty.GetMaxLength());
        }
    }
}