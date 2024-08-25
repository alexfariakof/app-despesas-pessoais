using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;

namespace Repository.Mapping;
public sealed class LancamentoParserTest
{
    [Fact]
    public void EntityConfiguration_IsValid()
    {
        // Arrange
        var options = new DbContextOptionsBuilder<RegisterContext>().UseInMemoryDatabase(databaseName: "LancamentoParserTest").Options;

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
            Assert.False(despesaIdProperty.IsNullable);
            Assert.Equal(despesaIdProperty.GetDefaultValue(), Guid.Empty);
            Assert.False(receitaIdProperty.IsNullable);
            Assert.Equal(receitaIdProperty.GetDefaultValue(), Guid.Empty);
            //Assert.True(dataProperty.GetColumnType() == "datetime");
            Assert.False(dataProperty.IsNullable);
            //Assert.True(dataCriacaoProperty.GetColumnType() == "datetime");
            Assert.Equal(DateTime.MinValue, dataCriacaoProperty.GetDefaultValue());
            Assert.True(valorProperty.GetColumnType() == "decimal(10, 2)");
            Assert.Equal(100, descricaoProperty.GetMaxLength());
        }
    }
}