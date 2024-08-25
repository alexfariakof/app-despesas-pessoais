using Domain.Entities.ValueObjects;
using __mock__.v1;

namespace Domain.Entities;
public sealed class CategoriaTest
{
    [Theory]
    [InlineData("Test Description 1", TipoCategoria.CategoriaType.Despesa)]
    [InlineData("Test Description 2", TipoCategoria.CategoriaType.Despesa)]
    [InlineData("Test Description 3", TipoCategoria.CategoriaType.Receita)]
    public void Categoria_Should_Set_Properties_Correctly(string descricao, TipoCategoria.CategoriaType tipoCategoria)
    {
        // Arrange
        var idCategoria = Guid.NewGuid();
        var idUsuario = Guid.NewGuid();
        var categoria = new Categoria();

        // Act
        categoria.Id = idCategoria;
        categoria.Descricao = descricao;
        categoria.UsuarioId = idUsuario;
        var usuario = new Usuario { Id = idUsuario };
        categoria.Usuario = usuario;
        categoria.TipoCategoria = new TipoCategoria(tipoCategoria);

        // Assert
        Assert.Equal(idCategoria, categoria.Id);
        Assert.Equal(descricao, categoria.Descricao);
        Assert.Equal(idUsuario, categoria.UsuarioId);
        Assert.Equal(usuario, categoria.Usuario);
        Assert.Equal(tipoCategoria, categoria.TipoCategoria);
    }

    [Fact]
    public void Categoria_First_Constructor_Should_Set_Properties_Correctly()
    {
        // Arrange
        string descricao = "Test Description";
        var usuarioId = Guid.NewGuid();
        Usuario usuario = UsuarioFaker.Instance.GetNewFaker();
        TipoCategoria tipoCategoria = (int)TipoCategoria.CategoriaType.Despesa;

        // Act
        var categoria = new Categoria(descricao, usuarioId, usuario, tipoCategoria);

        // Assert
        Assert.Equal(descricao, categoria.Descricao);
        Assert.Equal(usuarioId, categoria.UsuarioId);
        Assert.Equal(usuario, categoria.Usuario);
        Assert.Equal(tipoCategoria, categoria.TipoCategoria);
    }

    [Fact]
    public void Categoria_Second_Constructor_Should_Set_Properties_Correctly()
    {
        // Arrange
        var id = Guid.NewGuid();
        string descricao = "Test Description";
        var usuarioId = Guid.NewGuid();
        Usuario usuario = UsuarioFaker.Instance.GetNewFaker();
        TipoCategoria tipoCategoria = (int)TipoCategoria.CategoriaType.Despesa;

        // Act
        var categoria = new Categoria(id, descricao, usuarioId, usuario, tipoCategoria);

        // Assert
        Assert.Equal(id, categoria.Id);
        Assert.Equal(descricao, categoria.Descricao);
        Assert.Equal(usuarioId, categoria.UsuarioId);
        Assert.Equal(usuario, categoria.Usuario);
        Assert.Equal(tipoCategoria, categoria.TipoCategoria);
    }
}