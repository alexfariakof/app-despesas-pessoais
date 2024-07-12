namespace Domain.Entities;
public sealed class ReceitaTest
{
    [Theory]
    [InlineData("Descrição 1", 10.0)]
    [InlineData("Descrição 2", 0.0)]
    [InlineData("Descrição 3", 9.5)]
    public void Receita_Should_Set_Properties_Correctly(string descricao, Decimal valor)
    {
        var mockUsuario = Mock.Of<Usuario>();
        var mockCategoria= Mock.Of<Categoria>();
        DateTime data = DateTime.Now;
        var id = Guid.NewGuid();
        var usuarioId = Guid.NewGuid();
        var categoriaId = Guid.NewGuid();

        // Arrange and Act
        var receita = new Receita
        { 
            Id = id,
            Data = data,
            Descricao = descricao,
            Valor = valor,            
            UsuarioId = usuarioId,                
            Usuario  = mockUsuario,
            CategoriaId = categoriaId,
            Categoria = mockCategoria,
            
        };
        
        // Assert
        Assert.Equal(id, receita.Id);
        Assert.Equal(data, receita.Data);
        Assert.Equal(descricao, receita.Descricao);
        Assert.Equal(valor, receita.Valor);
        Assert.Equal(usuarioId, receita.UsuarioId);
        Assert.Equal(mockUsuario, receita.Usuario);
        Assert.Equal(categoriaId, receita.CategoriaId);
        Assert.Equal(mockCategoria, receita.Categoria);
    }
}