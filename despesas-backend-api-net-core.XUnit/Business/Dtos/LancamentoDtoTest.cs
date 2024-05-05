namespace Business.Dtos;
public class LancamentoDtoTest
{
    [Theory]
    [InlineData(1, 1, 2, 0, 200.00, "Teste Descripition Despesa ", "Despesa")]
    [InlineData(2, 1, 0, 1, 500.55, "Teste Descripition Receita ", "Receita")]
    public void LancamentoDto_Should_Set_Properties_Correctly(int id, int idUsuario, int idDespesa, int idReceita, decimal valor, string descricao, string tipoCategoria)
    {
        // Arrange and Act
        var data = DateTime.Now.ToString("yyyy-MM-dd");
        var mockCategoria = Mock.Of<Categoria>();

        var lancamentoDto = new LancamentoDto
        {
            Id = id,
            IdUsuario = idUsuario,
            IdDespesa = idDespesa,
            IdReceita = idReceita,
            Valor = valor,
            Data = data,
            Descricao = descricao,
            TipoCategoria = tipoCategoria,
            Categoria = mockCategoria.Descricao
        };

        // Assert
        Assert.Equal(id, lancamentoDto.Id);
        Assert.Equal(idUsuario, lancamentoDto.IdUsuario);
        Assert.Equal(idDespesa, lancamentoDto.IdDespesa);
        Assert.Equal(idReceita, lancamentoDto.IdReceita);
        Assert.Equal(valor, lancamentoDto.Valor);
        Assert.Equal(data, lancamentoDto.Data);
        Assert.Equal(descricao, lancamentoDto.Descricao);
        Assert.Equal(tipoCategoria, lancamentoDto.TipoCategoria);
        Assert.Equal(mockCategoria.Descricao, lancamentoDto.Categoria);
    }
}