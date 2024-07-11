namespace Domain.Entities;
public sealed class LancamentoTest
{
    [Theory]
    [InlineData(100, "Descrição 1")]
    [InlineData(58.98, "Descrição 2")]
    [InlineData(5003, "Descrição 2")]
    public void Lancamento_Should_Set_Properties_Correctly(decimal valor, string descricao)
    {
        // Arrange
        var mockUsuario = Mock.Of<Usuario>();
        var mockDespesa = Mock.Of<Despesa>();
        var mockReceita = Mock.Of<Receita>();
        var mockCategoria= Mock.Of<Categoria>();
        var id = Guid.NewGuid();
        var usuarioId = Guid.NewGuid();
        var despesaId = valor % 2 == 0 ? Guid.NewGuid() : Guid.Empty;
        var receitaId = valor % 2 == 0 ? Guid.NewGuid() : Guid.Empty;
        var categoriaId = Guid.NewGuid();
        DateTime data = DateTime.Now;
        DateTime dataCriacao = DateTime.Now;

        //Act
        var lancamento = new Lancamento
        { 
            Id = id,
            Valor = valor,
            Data = data,
            Descricao = descricao,                
            UsuarioId = usuarioId,                
            Usuario  = mockUsuario,
            DespesaId = despesaId,
            Despesa = mockDespesa,
            ReceitaId   = receitaId,
            Receita = mockReceita,
            CategoriaId = categoriaId,
            Categoria = mockCategoria,
            DataCriacao = dataCriacao                
        };

        // Assert
        Assert.Equal(id, lancamento.Id);
        Assert.Equal(valor, lancamento.Valor);
        Assert.Equal(data, lancamento.Data);
        Assert.Equal(descricao, lancamento.Descricao);            
        Assert.Equal(usuarioId, lancamento.UsuarioId);
        Assert.Equal(mockUsuario, lancamento.Usuario);
        Assert.Equal(despesaId, lancamento.DespesaId);
        Assert.Equal(mockDespesa, lancamento.Despesa);
        Assert.Equal(receitaId, lancamento.ReceitaId);
        Assert.Equal(mockReceita, lancamento.Receita);
        Assert.Equal(categoriaId, lancamento.CategoriaId);
        Assert.Equal(mockCategoria, lancamento.Categoria);
        Assert.Equal(dataCriacao, lancamento.DataCriacao);
    }
}