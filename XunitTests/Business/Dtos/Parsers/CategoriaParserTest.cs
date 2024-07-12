using Business.Dtos.v1;
using Domain.Entities.ValueObjects;

namespace Business.Dtos.Parser;
public sealed class CategoriaParserTest
{
    [Fact]
    public void Should_Parse_CategoriaDto_To_Categoria()
    {
        // Arrange
        var categoriaParser = new CategoriaParser();
        var categoriaDto = new CategoriaDto
        {
            Id = Guid.NewGuid(),
            Descricao = "Categoria Teste",
            IdTipoCategoria = 1,
            UsuarioId = Guid.NewGuid()
        };

        // Act
        var categoria = categoriaParser.Parse(categoriaDto);

        // Assert
        Assert.Equal(categoriaDto.Id, categoria.Id);
        Assert.Equal(categoriaDto.Descricao, categoria.Descricao);
        Assert.Equal((int)(int)TipoCategoria.CategoriaType.Despesa, categoria.TipoCategoria);
        Assert.Equal(categoriaDto.UsuarioId, categoria.UsuarioId);
    }

    [Fact]
    public void Should_Parse_Categoria_To_CategoriaDto()
    {
        // Arrange
        var categoriaParser = new CategoriaParser();
        var categoria = new Categoria
        {
            Id = Guid.NewGuid(),
            Descricao = "Categoria Teste",
            TipoCategoria = (int)TipoCategoria.CategoriaType.Receita,
            UsuarioId = Guid.NewGuid()
        };

        // Act
        var categoriaDto = categoriaParser.Parse(categoria);

        // Assert
        Assert.Equal(categoria.Id, categoriaDto.Id);
        Assert.Equal(categoria.Descricao, categoriaDto.Descricao);
        Assert.Equal(categoria.TipoCategoria, categoriaDto.IdTipoCategoria);
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
                Id = Guid.NewGuid(),
                Descricao = "Categoria 1",
                IdTipoCategoria = 1,
                UsuarioId = Guid.NewGuid()
            },
            new CategoriaDto
            {
                Id = Guid.NewGuid(),
                Descricao = "Categoria 2",
                IdTipoCategoria = 2,
                UsuarioId = Guid.NewGuid()
            },
            new CategoriaDto
            {
                Id = Guid.NewGuid(),
                Descricao = "Categoria 3",
                IdTipoCategoria = 1,
                UsuarioId = Guid.NewGuid()
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
            Assert.Equal(categoriaDtos[i].UsuarioId, categorias[i].UsuarioId);
            Assert.Equal(
                categoriaDtos[i].IdTipoCategoria == 1
                    ? (int)(int)TipoCategoria.CategoriaType.Despesa
                    : (int)(int)TipoCategoria.CategoriaType.Receita,
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
                Id = Guid.NewGuid(),
                Descricao = "Categoria 1",
                TipoCategoria = (int)(int)TipoCategoria.CategoriaType.Despesa,
                UsuarioId = Guid.NewGuid()
            },
            new Categoria
            {
                Id = Guid.NewGuid(),
                Descricao = "Categoria 2",
                TipoCategoria = (int)TipoCategoria.CategoriaType.Receita,
                UsuarioId = Guid.NewGuid()
            },
            new Categoria
            {
                Id = Guid.NewGuid(),
                Descricao = "Categoria 3",
                TipoCategoria = (int)TipoCategoria.CategoriaType.Despesa,
                UsuarioId = Guid.NewGuid()
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
            Assert.Equal(categorias[i].TipoCategoria, categoriaDtos[i].IdTipoCategoria);
            Assert.Equal(categorias[i].UsuarioId, categoriaDtos[i].UsuarioId);
        }
    }
}
