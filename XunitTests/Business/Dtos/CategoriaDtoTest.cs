<<<<<<<< HEAD:XunitTests/Business/Dtos/Parsers/CategoriaDtoTest.cs
﻿namespace Business.Dtos;
========
﻿using Business.Dtos.Core;
using Business.Dtos.v1;

namespace Business.Dtos;
>>>>>>>> feature/Create-Migrations-AZURE_SQL_SERVER:XunitTests/Business/Dtos/CategoriaDtoTest.cs
public class CategoriaDtoTest
{

    [Theory]
    [InlineData(1, "Test Categoria Description 1", 1, TipoCategoriaDto.Todas)]
    [InlineData(2, "Test Categoria Description 2", 2, TipoCategoriaDto.Despesa)]
    [InlineData(3, "Test Categoria Description 3", 3, TipoCategoriaDto.Receita)]
    public void CategoriaDto_Should_Set_Properties_Correctly(int id, string descricao, int idUsuario, TipoCategoriaDto tipoCategoria)
    {
        // Arrange
        var categoriaDto = new CategoriaDto();

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