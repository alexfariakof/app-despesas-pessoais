namespace Business.Dtos.Parser;
public class CategoriaParserTest
{
    [Fact]
    public void Should_Parse_CategoriaDto_To_Categoria()
    {
        // Arrange
        var categoriaParser = new CategoriaParser();
        var categoriaDto = new CategoriaDto
        {
            Id = 1,
            Descricao = "Categoria Teste",
            IdTipoCategoria = 1,
            IdUsuario = 1
        };

        // Act
        var categoria = categoriaParser.Parse(categoriaDto);

        // Assert
        Assert.Equal(categoriaDto.Id, categoria.Id);
        Assert.Equal(categoriaDto.Descricao, categoria.Descricao);
        Assert.Equal(TipoCategoria.Despesa, categoria.TipoCategoria);
        Assert.Equal(categoriaDto.IdUsuario, categoria.UsuarioId);
    }

    [Fact]
    public void Should_Parse_Categoria_To_CategoriaDto()
    {
        // Arrange
        var categoriaParser = new CategoriaParser();
        var categoria = new Categoria
        {
            Id = 1,
            Descricao = "Categoria Teste",
            TipoCategoria = TipoCategoria.Receita,
            UsuarioId = 1
        };

        // Act
        var categoriaDto = categoriaParser.Parse(categoria);

        // Assert
        Assert.Equal(categoria.Id, categoriaDto.Id);
        Assert.Equal(categoria.Descricao, categoriaDto.Descricao);
        Assert.Equal((int)categoria.TipoCategoria, categoriaDto.IdTipoCategoria);
        Assert.Equal(categoria.UsuarioId, categoriaDto.UsuarioId);
    }

    [Fact]
    public void Should_Parse_List_CategoriaDtos_To_List_Categorias()
    {
        // Arrange
        var categoriaParser = new CategoriaParser();
        var categoriaDtos = new List<CategoriaDto>
        {
            new CategoriaDto
            {
                Id = 1,
                Descricao = "Categoria 1",
                IdTipoCategoria = 1,
                IdUsuario = 1
            },
            new CategoriaDto
            {
                Id = 2,
                Descricao = "Categoria 2",
                IdTipoCategoria = 2,
                IdUsuario = 2
            },
            new CategoriaDto
            {
                Id = 3,
                Descricao = "Categoria 3",
                IdTipoCategoria = 1,
                IdUsuario = 1
            }
        };

        // Act
        var categorias = categoriaParser.ParseList(categoriaDtos);

        // Assert
        Assert.Equal(categoriaDtos.Count, categorias.Count);
        for (int i = 0; i < categoriaDtos.Count; i++)
        {
            Assert.Equal(categoriaDtos[i].Id, categorias[i].Id);
            Assert.Equal(categoriaDtos[i].Descricao, categorias[i].Descricao);
            Assert.Equal(categoriaDtos[i].IdUsuario, categorias[i].UsuarioId);
            Assert.Equal(
                categoriaDtos[i].IdTipoCategoria == 1
                    ? TipoCategoria.Despesa
                    : TipoCategoria.Receita,
                categorias[i].TipoCategoria
            );
        }
    }

    [Fact]
    public void Should_Parse_List_Categorias_To_List_CategoriaDtos()
    {
        // Arrange
        var categoriaParser = new CategoriaParser();
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
        var categoriaDtos = categoriaParser.ParseList(categorias);

        // Assert
        Assert.Equal(categorias.Count, categoriaDtos.Count);
        for (int i = 0; i < categorias.Count; i++)
        {
            Assert.Equal(categorias[i].Id, categoriaDtos[i].Id);
            Assert.Equal(categorias[i].Descricao, categoriaDtos[i].Descricao);
            Assert.Equal((int)categorias[i].TipoCategoria, categoriaDtos[i].IdTipoCategoria);
            Assert.Equal(categorias[i].UsuarioId, categoriaDtos[i].UsuarioId);
        }
    }
}
