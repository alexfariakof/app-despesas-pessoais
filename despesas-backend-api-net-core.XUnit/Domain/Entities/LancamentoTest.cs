namespace Test.XUnit.Domain.Entities
{
    public class LancamentoTest
    {
        [Theory]
        [InlineData(1, 100.6, "Descrição 1", 1, typeof(Usuario), 1, typeof(Despesa), 1 , typeof(Receita), 1, typeof(Categoria))]
        [InlineData(2, 58.98, "Descrição 2", 2, typeof(Usuario), 3, typeof(Despesa), 2, typeof(Receita), 2, typeof(Categoria))]
        [InlineData(3, 5000, "Descrição 2", 3, typeof(Usuario), 4, typeof(Despesa), 6, typeof(Receita), 54, typeof(Categoria))]
        public void Lancamento_ShouldSetPropertiesCorrectly(int id, Decimal valor, string descricao, int usuarioId, Type typeUsuario, int despesaId, Type typeDespesa, int receitaId, Type typeReceita,  int categoriaId, Type typeCategoria)
        {
            var mockUsuario = Mock.Of<Usuario>();
            var mockDespesa = Mock.Of<Despesa>();
            var mockReceita = Mock.Of<Receita>();
            var mockCategoria= Mock.Of<Categoria>();
            DateTime data = DateTime.Now;
            DateTime dataCriacao = DateTime.Now;

            // Arrange and Act
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