using despesas_backend_api_net_core.Infrastructure.Data.EntityConfig;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using Xunit.Extensions.Ordering;

namespace Infrastructure.EntityConfig
{
    [Order(207)]
    public class ReceitaMapTest
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
                var configuration = new ReceitaMap();

                configuration.Configure(builder.Entity<Receita>());

                var model = builder.Model;
                var entityType = model.FindEntityType(typeof(Receita));

                // Act

                var idProperty = entityType.FindProperty("Id");

                var descricaoProperty = entityType.FindProperty("Descricao");
                var usuarioIdProperty = entityType.FindProperty("UsuarioId");
                var categoriaIdProperty = entityType.FindProperty("CategoriaId");
                var dataProperty = entityType.FindProperty("Data");
                var valorProperty = entityType.FindProperty("Valor");

                // Assert
                Assert.NotNull(idProperty);
                Assert.NotNull(descricaoProperty);
                Assert.NotNull(usuarioIdProperty);
                Assert.NotNull(categoriaIdProperty);
                Assert.NotNull(dataProperty);
                Assert.NotNull(valorProperty);
                Assert.True(idProperty.IsPrimaryKey());
                Assert.True(descricaoProperty.IsNullable);
                Assert.Equal(100, descricaoProperty.GetMaxLength());
                Assert.False(usuarioIdProperty.IsNullable);
                Assert.False(categoriaIdProperty.IsNullable);
                Assert.Equal(typeof(DateTime), dataProperty.ClrType);
                Assert.Equal("timestamp", dataProperty.GetColumnType());
                //Assert.Equal(DateTime.Now, dataProperty.GetDefaultValue());
                Assert.Equal(typeof(decimal), valorProperty.ClrType);
                Assert.Equal("decimal(10, 2)", valorProperty.GetColumnType());

                Assert.Equal(0, (decimal)valorProperty.GetDefaultValue());
            }
        }

        [Fact]
        public void Should_Parse_ReceitaVM_To_Receita()
        {
            // Arrange
            var receitaMap = new ReceitaMap();
            var receitaVM = ReceitaFaker.GetNewFakerVM(1, 1);

            // Act
            var receita = receitaMap.Parse(receitaVM);

            // Assert
            Assert.Equal(receitaVM.Id, receita.Id);
            Assert.Equal(receitaVM.Descricao, receita.Descricao);
            Assert.Equal(receitaVM.IdUsuario, receita.UsuarioId);
        }

        [Fact]
        public void Should_Parse_Receita_To_ReceitaVM()
        {
            // Arrange
            var receitaMap = new ReceitaMap();
            var usuario = UsuarioFaker.GetNewFaker(1);
            var receita = ReceitaFaker.GetNewFaker(
                usuario,
                CategoriaFaker.GetNewFaker(usuario, usuario.Id)
            );

            // Act
            var receitaVM = receitaMap.Parse(receita);

            // Assert
            Assert.Equal(receita.Id, receitaVM.Id);
            Assert.Equal(receita.Descricao, receitaVM.Descricao);

            Assert.Equal(receita.UsuarioId, receitaVM.IdUsuario);
        }

        [Fact]
        public void Should_Parse_List_ReceitaVMs_To_List_Receitas()
        {
            // Arrange
            var receitaMap = new ReceitaMap();
            var usuarioVM = UsuarioFaker.GetNewFakerVM(1);
            var receitaVMs = ReceitaFaker.ReceitasVMs(usuarioVM, usuarioVM.Id);

            // Act
            var receitas = receitaMap.ParseList(receitaVMs);

            // Assert
            Assert.Equal(receitaVMs.Count, receitas.Count);
            for (int i = 0; i < receitaVMs.Count; i++)
            {
                Assert.Equal(receitaVMs[i].Id, receitas[i].Id);
                Assert.Equal(receitaVMs[i].Descricao, receitas[i].Descricao);
                Assert.Equal(receitaVMs[i].IdUsuario, receitas[i].UsuarioId);
            }
        }

        [Fact]
        public void Should_Parse_List_Receitas_To_List_ReceitaVMs()
        {
            // Arrange
            var receitaMap = new ReceitaMap();
            var usuario = UsuarioFaker.GetNewFaker(1);
            var receitas = ReceitaFaker.Receitas(usuario, usuario.Id);

            // Act
            var receitaVMs = receitaMap.ParseList(receitas);

            // Assert
            Assert.Equal(receitas.Count, receitaVMs.Count);
            for (int i = 0; i < receitas.Count; i++)
            {
                Assert.Equal(receitas[i].Id, receitaVMs[i].Id);
                Assert.Equal(receitas[i].Descricao, receitaVMs[i].Descricao);
                Assert.Equal(receitas[i].UsuarioId, receitaVMs[i].IdUsuario);
            }
        }
    }
}
