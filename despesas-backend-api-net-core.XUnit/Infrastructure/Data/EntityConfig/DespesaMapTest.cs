using despesas_backend_api_net_core.Infrastructure.Data.EntityConfig;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using Xunit.Extensions.Ordering;

namespace Infrastructure.EntityConfig
{
    [Order(204)]
    public class DespesaMapTest
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
                var configuration = new DespesaMap();

                configuration.Configure(builder.Entity<Despesa>());

                var model = builder.Model;
                var entityType = model.FindEntityType(typeof(Despesa));

                // Act

                var idProperty = entityType.FindProperty("Id");

                var descricaoProperty = entityType.FindProperty("Descricao");
                var usuarioIdProperty = entityType.FindProperty("UsuarioId");
                var categoriaIdProperty = entityType.FindProperty("CategoriaId");
                var dataProperty = entityType.FindProperty("Data");
                var dataVencimentoProperty = entityType.FindProperty("DataVencimento");
                var valorProperty = entityType.FindProperty("Valor");

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
                Assert.Equal("timestamp", dataProperty.GetColumnType());
                //Assert.Equal(DateTime.Now, dataProperty.GetDefaultValue());
                Assert.True(dataVencimentoProperty.IsNullable);
                Assert.Null(dataVencimentoProperty.GetDefaultValue());
                Assert.Equal(typeof(decimal), valorProperty.ClrType);
                Assert.Equal("decimal(10, 2)", valorProperty.GetColumnType());

                Assert.Equal(0, (decimal)valorProperty.GetDefaultValue());
            }
        }

        [Fact]
        public void Should_Parse_DespesaVM_To_Despesa()
        {
            // Arrange
            var despesaMap = new DespesaMap();
            var despesaVM = DespesaFaker.GetNewFakerVM(1, 1);

            // Act
            var despesa = despesaMap.Parse(despesaVM);

            // Assert
            Assert.Equal(despesaVM.Id, despesa.Id);
            Assert.Equal(despesaVM.Descricao, despesa.Descricao);
            Assert.Equal(despesaVM.IdUsuario, despesa.UsuarioId);
        }

        [Fact]
        public void Should_Parse_Despesa_To_DespesaVM()
        {
            // Arrange
            var despesaMap = new DespesaMap();
            var usuario = UsuarioFaker.GetNewFaker(1);
            var despesa = DespesaFaker.GetNewFaker(
                usuario,
                CategoriaFaker.GetNewFaker(usuario, TipoCategoria.Despesa, usuario.Id)
            ); 
            // Act
            var despesaVM = despesaMap.Parse(despesa);

            // Assert
            Assert.Equal(despesa.Id, despesaVM.Id);
            Assert.Equal(despesa.Descricao, despesaVM.Descricao);
            Assert.Equal(despesa.UsuarioId, despesaVM.IdUsuario);
        }

        [Fact]
        public void Should_Parse_List_DespesaVM_To_List_Despesa()
        {
            // Arrange
            var despesaMap = new DespesaMap();
            var usuario = UsuarioFaker.GetNewFaker(1);
            var despesaVMs = DespesaFaker.Despesas(usuario, usuario.Id);

            // Act
            var despesas = despesaMap.ParseList(despesaVMs);

            // Assert
            Assert.Equal(despesaVMs.Count, despesas.Count);
            for (int i = 0; i < despesaVMs.Count; i++)
            {
                Assert.Equal(despesaVMs[i].Id, despesas[i].Id);
                Assert.Equal(despesaVMs[i].Descricao, despesas[i].Descricao);
                Assert.Equal(despesaVMs[i].UsuarioId, despesas[i].IdUsuario);
            }
        }

        [Fact]
        public void Should_Parse_List_Despesa_To_List_DespesaVM()
        {
            // Arrange
            var despesaMap = new DespesaMap();
            var usuarioVM = UsuarioFaker.GetNewFakerVM(1);
            var despesas = DespesaFaker.DespesasVMs(usuarioVM, usuarioVM.Id);

            // Act
            var despesaVMs = despesaMap.ParseList(despesas);

            // Assert
            Assert.Equal(despesas.Count, despesaVMs.Count);
            for (int i = 0; i < despesas.Count; i++)
            {
                Assert.Equal(despesas[i].Id, despesaVMs[i].Id);
                Assert.Equal(despesas[i].Descricao, despesaVMs[i].Descricao);
                Assert.Equal(despesas[i].IdUsuario, despesaVMs[i].UsuarioId);
            }
        }
    }
}
