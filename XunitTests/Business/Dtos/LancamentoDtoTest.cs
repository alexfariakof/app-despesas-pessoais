using Business.Dtos.v1;

namespace Business.Dtos;
public sealed class LancamentoDtoTest
{
    [Theory]
    [InlineData(200.00, "Teste Descripition Despesa ", "Despesa")]
    [InlineData(500.55, "Teste Descripition Receita ", "Receita")]
    public void LancamentoDto_Should_Set_Properties_Correctly(decimal valor, string descricao, string tipoCategoria)
    {
        // Arrange and Act
        var id = Guid.NewGuid();
        Guid idUsuario = Guid.NewGuid();
        Guid idDespesa = Guid.NewGuid();
        Guid idReceita = Guid.NewGuid();

        var data = DateTime.Now.ToString("yyyy-MM-dd");
        var mockCategoria = Mock.Of<Categoria>();

        var lancamentoDto = new LancamentoDto
        {
            Id = id,
            UsuarioId = idUsuario,
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
        Assert.Equal(idUsuario, lancamentoDto.UsuarioId);
        Assert.Equal(idDespesa, lancamentoDto.IdDespesa);
        Assert.Equal(idReceita, lancamentoDto.IdReceita);
        Assert.Equal(valor, lancamentoDto.Valor);
        Assert.Equal(data, lancamentoDto.Data);
        Assert.Equal(descricao, lancamentoDto.Descricao);
        Assert.Equal(tipoCategoria, lancamentoDto.TipoCategoria);
        Assert.Equal(mockCategoria.Descricao, lancamentoDto.Categoria);
    }
}