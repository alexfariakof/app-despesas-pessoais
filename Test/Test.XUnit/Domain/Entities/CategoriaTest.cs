using despesas_backend_api_net_core.Domain.Entities;
using Xunit;

namespace Test.XUnit.Domain.Entities
{
    public class CategoriaTest
    {
        [Fact]
        public void Categoria_ShouldSetPropertiesCorrectly()
        {
            // Arrange
            var categoria = new Categoria();

            // Act
            categoria.Descricao = "Test Description";
            categoria.UsuarioId = 1;
            var usuario = new Usuario();
            categoria.Usuario = usuario;
            var tipoCategoria = new TipoCategoria();
            categoria.TipoCategoria = tipoCategoria;

            // Assert
            Assert.Equal("Test Description", categoria.Descricao);
            Assert.Equal(1, categoria.UsuarioId);
            Assert.Equal(usuario, categoria.Usuario);
            Assert.Equal(tipoCategoria, categoria.TipoCategoria);
        }
    }
}
