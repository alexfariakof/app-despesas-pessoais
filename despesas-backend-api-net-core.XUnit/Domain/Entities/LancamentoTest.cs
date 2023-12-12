namespace Domain.Entities
{
    public class LancamentoTest
    {
        [Theory]
        [InlineData(1, 100.6, "Descrição 1", 1, 1, 1, 1)]
        [InlineData(2, 58.98, "Descrição 2", 2, 3, 2, 2)]
        [InlineData(3, 5000, "Descrição 2", 3, 4, 6, 54)]
        public void Lancamento_Should_Set_Properties_Correctly(int id, Decimal valor, string descricao, int usuarioId, int despesaId, int receitaId, int categoriaId)
        {
            // Arrange
            var mockUsuario = Mock.Of<Usuario>();
            var mockDespesa = Mock.Of<Despesa>();
            var mockReceita = Mock.Of<Receita>();
            var mockCategoria= Mock.Of<Categoria>();
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
}