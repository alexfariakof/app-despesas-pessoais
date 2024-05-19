namespace Domain.Entities;
public sealed class DespesaTest
{
    [Theory]
    [InlineData(1, "Descrição 1", 10.0, 1, 1)]
    [InlineData(2, "Descrição 2", 0.0, 2, 2)]
    [InlineData(3, "Descrição 3", 9.5, 3, 3)]
    public void Despesa_Should_Set_Properties_Correctly(int id, string descricao, Decimal valor, int usuarioId,int categoriaId)
    {
        var mockUsuario = Mock.Of<Usuario>();
        var mockCategoria= Mock.Of<Categoria>();
        DateTime data = DateTime.Now;
        DateTime? dataVencimento = DateTime.Now;

        // Arrange and Act
        var despesa = new Despesa
        { 
            Id = id,
            Data = data,
            Descricao = descricao,
            Valor = valor,            
            DataVencimento = dataVencimento,
            UsuarioId = usuarioId,                
            Usuario  = mockUsuario,
            CategoriaId = categoriaId,
            Categoria = mockCategoria,
            
        };
        
        // Assert
        Assert.Equal(id, despesa.Id);
        Assert.Equal(data, despesa.Data);
        Assert.Equal(descricao, despesa.Descricao);
        Assert.Equal(valor, despesa.Valor);
        Assert.Equal(dataVencimento, despesa.DataVencimento);
        Assert.Equal(usuarioId, despesa.UsuarioId);
        Assert.Equal(mockUsuario, despesa.Usuario);
        Assert.Equal(categoriaId, despesa.CategoriaId);
        Assert.Equal(mockCategoria, despesa.Categoria);
    }
}