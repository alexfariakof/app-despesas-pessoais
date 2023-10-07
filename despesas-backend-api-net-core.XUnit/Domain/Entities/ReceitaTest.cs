namespace Test.XUnit.Domain.Entities
{
    public class ReceitaTest
    {
        [Theory]
        [InlineData(1, "Descrição 1", 10.0, 1, typeof(Usuario), 1, typeof(Categoria))]
        [InlineData(2, "Descrição 2", 0.0, 2, typeof(Usuario), 2, typeof(Categoria))]
        [InlineData(3, "Descrição 3", 9.5, 3, typeof(Usuario), 3, typeof(Categoria))]
        public void Receita_ShouldSetPropertiesCorrectly(int id, string descricao, Decimal valor, int usuarioId, Type typeUsuario, int categoriaId, Type typeCategoria )
        {
            var mockUsuario = Mock.Of<Usuario>();
            var mockCategoria= Mock.Of<Categoria>();
            DateTime data = DateTime.Now;


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
}