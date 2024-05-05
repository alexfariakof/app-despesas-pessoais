namespace Business.Dtos;
public class CategoriaDtoTest
{

    [Theory]
    [InlineData(1, "Test Categoria Description 1", 1, TipoCategoria.Todas)]
    [InlineData(2, "Test Categoria Description 2", 2, TipoCategoria.Despesa)]
    [InlineData(3, "Test Categoria Description 3", 3, TipoCategoria.Receita)]
    public void CategoriaDto_Should_Set_Properties_Correctly(int id, string descricao, int idUsuario, TipoCategoria tipoCategoria)
    {
        // Arrange
        var categoriaDto = new CategoriaDto();

        // Act
        categoriaDto.Id = id;
        categoriaDto.Descricao = descricao;
        categoriaDto.IdUsuario = idUsuario;
        categoriaDto.IdTipoCategoria = (int)tipoCategoria;

        // Assert
        Assert.Equal(id, categoriaDto.Id);
        Assert.Equal(descricao, categoriaDto.Descricao);
        Assert.Equal(idUsuario, categoriaDto.IdUsuario);
        Assert.Equal((int)tipoCategoria, categoriaDto.IdTipoCategoria);
    }
}