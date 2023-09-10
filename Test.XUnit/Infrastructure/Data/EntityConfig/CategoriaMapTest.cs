using System.Linq;
using despesas_backend_api_net_core.Domain.Entities;
using despesas_backend_api_net_core.Domain.VM;
using despesas_backend_api_net_core.Infrastructure.Data.Common;
using despesas_backend_api_net_core.Infrastructure.Data.EntityConfig;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;

namespace Test.XUnit.Infrastructure.Data.EntityConfig
{
    public class CategoriaMapTest
    {
        [Fact]
        public void CategoriaMap_Should_Configure_EntityTypeBuilder()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<RegisterContext>()
                .UseInMemoryDatabase(databaseName: "test_database")
                .Options;

            using (var context = new RegisterContext(options))
            {
                var modelBuilder = new ModelBuilder(new ConventionSet());
                var builder = modelBuilder.Entity<Categoria>();

                var categoriaMap = new CategoriaMap();

                // Act
                categoriaMap.Configure(builder);

                // Assert
                var entity = builder.Metadata;
                var idProperty = entity?.FindPrimaryKey()?.Properties.First();
                var descricaoProperty = entity?.FindProperty(nameof(Categoria.Descricao));
                var usuarioIdProperty = entity?.FindProperty(nameof(Categoria.UsuarioId));
                var tipoCategoriaProperty = entity?.FindProperty(nameof(Categoria.TipoCategoria));
                var usuarioProperty = entity?.FindNavigation(nameof(Categoria.Usuario));

                Assert.Equal(nameof(Categoria), entity?.Name);
                Assert.False(entity?.IsOwned());
                Assert.NotNull(entity?.FindPrimaryKey());
                Assert.Equal(nameof(Categoria.Id), idProperty?.Name);
                Assert.Equal(nameof(Categoria.Descricao), descricaoProperty?.Name);
                Assert.Equal(nameof(Categoria.UsuarioId), usuarioIdProperty?.Name);
                Assert.Equal(nameof(Categoria.TipoCategoria), tipoCategoriaProperty?.Name);
                Assert.False(descricaoProperty?.IsNullable);
                Assert.False(usuarioIdProperty?.IsNullable);
                Assert.False(tipoCategoriaProperty?.IsNullable);
                Assert.Equal(100, descricaoProperty?.GetMaxLength());


                Assert.NotNull(usuarioProperty);
                Assert.NotNull(tipoCategoriaProperty);
                Assert.Equal(nameof(Categoria.Usuario), usuarioProperty.Name);
                Assert.Equal(nameof(Categoria.TipoCategoria), tipoCategoriaProperty.Name);
                Assert.Equal(typeof(Usuario), usuarioProperty.TargetEntityType.ClrType);
                Assert.True(usuarioProperty.ForeignKey.IsRequired);
            }
        }

        [Fact]
        public void CategoriaMap_Should_Parse_CategoriaVM_To_Categoria()
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
        public void CategoriaMap_Should_Parse_Categoria_To_CategoriaVM()
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
        public void CategoriaMap_Should_ParseList_CategoriaVM_To_Categoria()
        {
            // Arrange
            var categoriaMap = new CategoriaMap();
            var categoriaVMs = new List<CategoriaVM>
            {
                new CategoriaVM { Id = 1, Descricao = "Categoria 1", IdTipoCategoria = 1, IdUsuario = 1 },
                new CategoriaVM { Id = 2, Descricao = "Categoria 2", IdTipoCategoria = 2, IdUsuario = 2 },
                new CategoriaVM { Id = 3, Descricao = "Categoria 3", IdTipoCategoria = 1, IdUsuario = 1 }
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
                Assert.Equal(categoriaVMs[i].IdTipoCategoria == 1 ? TipoCategoria.Despesa : TipoCategoria.Receita, categorias[i].TipoCategoria);
            }
        }

        [Fact]
        public void CategoriaMap_Should_ParseList_Categoria_To_CategoriaVM()
        {
            // Arrange
            var categoriaMap = new CategoriaMap();
            var categorias = new List<Categoria>
            {
                new Categoria { Id = 1, Descricao = "Categoria 1", TipoCategoria = TipoCategoria.Despesa, UsuarioId = 1 },
                new Categoria { Id = 2, Descricao = "Categoria 2", TipoCategoria = TipoCategoria.Receita, UsuarioId = 2 },
                new Categoria { Id = 3, Descricao = "Categoria 3", TipoCategoria = TipoCategoria.Despesa, UsuarioId = 1 }
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