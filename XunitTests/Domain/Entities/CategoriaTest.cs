using Domain.Entities.ValueObjects;
using Fakers.v1;

namespace Domain.Entities;
public sealed class CategoriaTest
{
    [Theory]
    [InlineData(1, "Test Description 1", 1, TipoCategoria.CategoriaType.Despesa)]
    [InlineData(2, "Test Description 2", 2, TipoCategoria.CategoriaType.Despesa)]
    [InlineData(3, "Test Description 3", 3, TipoCategoria.CategoriaType.Receita)]
    public void Categoria_Should_Set_Properties_Correctly(int idCatgeoria, string descricao, int idUsuario, TipoCategoria.CategoriaType tipoCategoria)
    {
        // Arrange
        var categoria = new Categoria();

        // Act
        categoria.Id = idCatgeoria;
        categoria.Descricao = descricao;
        categoria.UsuarioId = idUsuario;
        var usuario = new Usuario { Id = idUsuario };
        categoria.Usuario = usuario;
        categoria.TipoCategoria = new TipoCategoria(tipoCategoria);

        // Assert
        Assert.Equal(idCatgeoria, categoria.Id);
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
        int usuarioId = 1;
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
        int id = 1;
        string descricao = "Test Description";
        int usuarioId = 1;
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