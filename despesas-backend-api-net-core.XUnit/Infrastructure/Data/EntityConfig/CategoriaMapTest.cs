using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using Xunit.Extensions.Ordering;

namespace Infrastructure.EntityConfig
{
    [Order(202)]
    public class CategoriaMapTest
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
                var configuration = new CategoriaMap();

                configuration.Configure(builder.Entity<Categoria>());

                var model = builder.Model;
                var entityType = model.FindEntityType(typeof(Categoria));

                // Act

                var idProperty = entityType.FindProperty("Id");

                var descricaoProperty = entityType.FindProperty("Descricao");
                var usuarioIdProperty = entityType.FindProperty("UsuarioId");
                var tipoCategoriaProperty = entityType.FindProperty("TipoCategoria");

                // Assert
                Assert.NotNull(idProperty);
                Assert.NotNull(descricaoProperty);
                Assert.NotNull(usuarioIdProperty);
                Assert.NotNull(tipoCategoriaProperty);

                Assert.True(idProperty.IsPrimaryKey());
                Assert.False(usuarioIdProperty.IsNullable);
                Assert.False(tipoCategoriaProperty.IsNullable);
                Assert.True(descricaoProperty.IsNullable);
                Assert.Equal(100, descricaoProperty.GetMaxLength());
            }
        }

        [Fact]
        public void Should_Parse_CategoriaVM_To_Categoria()
        {
            // Arrange
            var categoriaMap = new CategoriaMap();
            var categoriaVM = new CategoriaVM
            {
                Id = 1,
                Descricao = "Categoria Teste",
                IdTipoCategoria = 1,
                IdUsuario = 1
            };

            // Act
            var categoria = categoriaMap.Parse(categoriaVM);

            // Assert
            Assert.Equal(categoriaVM.Id, categoria.Id);
            Assert.Equal(categoriaVM.Descricao, categoria.Descricao);
            Assert.Equal(TipoCategoria.Despesa, categoria.TipoCategoria);
            Assert.Equal(categoriaVM.IdUsuario, categoria.UsuarioId);
        }

        [Fact]
        public void Should_Parse_Categoria_To_CategoriaVM()
        {
            // Arrange
            var categoriaMap = new CategoriaMap();
            var categoria = new Categoria
            {
                Id = 1,
                Descricao = "Categoria Teste",
                TipoCategoria = TipoCategoria.Receita,
                UsuarioId = 1
            };

            // Act
            var categoriaVM = categoriaMap.Parse(categoria);

            // Assert
            Assert.Equal(categoria.Id, categoriaVM.Id);
            Assert.Equal(categoria.Descricao, categoriaVM.Descricao);
            Assert.Equal((int)categoria.TipoCategoria, categoriaVM.IdTipoCategoria);
            Assert.Equal(categoria.UsuarioId, categoriaVM.IdUsuario);
        }

        [Fact]
        public void Should_Parse_List_CategoriaVMs_To_List_Categorias()
        {
            // Arrange
            var categoriaMap = new CategoriaMap();
            var categoriaVMs = new List<CategoriaVM>
            {
                new CategoriaVM
                {
                    Id = 1,
                    Descricao = "Categoria 1",
                    IdTipoCategoria = 1,
                    IdUsuario = 1
                },
                new CategoriaVM
                {
                    Id = 2,
                    Descricao = "Categoria 2",
                    IdTipoCategoria = 2,
                    IdUsuario = 2
                },
                new CategoriaVM
                {
                    Id = 3,
                    Descricao = "Categoria 3",
                    IdTipoCategoria = 1,
                    IdUsuario = 1
                }
            };

            // Act
            var categorias = categoriaMap.ParseList(categoriaVMs);

            // Assert
            Assert.Equal(categoriaVMs.Count, categorias.Count);
            for (int i = 0; i < categoriaVMs.Count; i++)
            {
                Assert.Equal(categoriaVMs[i].Id, categorias[i].Id);
                Assert.Equal(categoriaVMs[i].Descricao, categorias[i].Descricao);
                Assert.Equal(categoriaVMs[i].IdUsuario, categorias[i].UsuarioId);
                Assert.Equal(
                    categoriaVMs[i].IdTipoCategoria == 1
                        ? TipoCategoria.Despesa
                        : TipoCategoria.Receita,
                    categorias[i].TipoCategoria
                );
            }
        }

        [Fact]
        public void Should_Parse_List_Categorias_To_List_CategoriaVMs()
        {
            // Arrange
            var categoriaMap = new CategoriaMap();
            var categorias = new List<Categoria>
            {
                new Categoria
                {
                    Id = 1,
                    Descricao = "Categoria 1",
                    TipoCategoria = TipoCategoria.Despesa,
                    UsuarioId = 1
                },
                new Categoria
                {
                    Id = 2,
                    Descricao = "Categoria 2",
                    TipoCategoria = TipoCategoria.Receita,
                    UsuarioId = 2
                },
                new Categoria
                {
                    Id = 3,
                    Descricao = "Categoria 3",
                    TipoCategoria = TipoCategoria.Despesa,
                    UsuarioId = 1
                }
            };

            // Act
            var categoriaVMs = categoriaMap.ParseList(categorias);

            // Assert
            Assert.Equal(categorias.Count, categoriaVMs.Count);
            for (int i = 0; i < categorias.Count; i++)
            {
                Assert.Equal(categorias[i].Id, categoriaVMs[i].Id);
                Assert.Equal(categorias[i].Descricao, categoriaVMs[i].Descricao);
                Assert.Equal((int)categorias[i].TipoCategoria, categoriaVMs[i].IdTipoCategoria);
                Assert.Equal(categorias[i].UsuarioId, categoriaVMs[i].IdUsuario);
            }
        }
    }
}
