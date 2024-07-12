using Business.Dtos.Core;
using Business.Dtos.v1;

namespace Business.Dtos;
public sealed class CategoriaDtoTest
{

    [Theory]
    [InlineData("Test Categoria Description 1", TipoCategoriaDto.Todas)]
    [InlineData("Test Categoria Description 2", TipoCategoriaDto.Despesa)]
    [InlineData("Test Categoria Description 3", TipoCategoriaDto.Receita)]
    public void CategoriaDto_Should_Set_Properties_Correctly(string descricao, TipoCategoriaDto tipoCategoria)
    {
        // Arrange
        var categoriaDto = new CategoriaDto();
        var id = Guid.NewGuid();
        var idUsuario = Guid.NewGuid();

        // Act
        categoriaDto.Id = id;
        categoriaDto.Descricao = descricao;
        categoriaDto.UsuarioId = idUsuario;
        categoriaDto.IdTipoCategoria = (int)tipoCategoria;

        // Assert
        Assert.Equal(id, categoriaDto.Id);
        Assert.Equal(descricao, categoriaDto.Descricao);
        Assert.Equal(idUsuario, categoriaDto.UsuarioId);
        Assert.Equal((int)tipoCategoria, categoriaDto.IdTipoCategoria);
    }
}