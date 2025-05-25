﻿using Business.Dtos.Parser;
using __mock__.v1;

namespace Business.Dtos;
public sealed class DespesaDtoTest
{
    [Fact]
    public void DespesaDto_Should_Set_Properties_Correctly()
    {
        // Arrange and Act
        var despesa = DespesaFaker.Instance.Despesas().First();
        var despesaDto = new DespesaParser().Parse(despesa);

        // Assert
        Assert.Equal(despesa.Id, despesaDto.Id);
        Assert.Equal(despesa.Data, despesaDto.Data);
        Assert.Equal(despesa.Descricao, despesaDto.Descricao);
        Assert.Equal(despesa.Valor, despesaDto.Valor);
        Assert.Equal(despesa.DataVencimento, despesaDto.DataVencimento);
        Assert.Equal(despesa.UsuarioId, despesaDto.UsuarioId);
        Assert.Equal(despesa?.Categoria?.Id, despesaDto.IdCategoria);
    }

}