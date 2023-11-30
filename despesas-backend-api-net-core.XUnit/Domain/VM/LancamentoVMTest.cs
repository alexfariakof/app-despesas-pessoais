namespace Test.XUnit.Domain.VM
{
    public class LancamentoVMTest
    {
        [Theory]
        [InlineData(1, 1, 2, 0, "R$ 200.00", "Teste Descripition Despesa ", "Despesa")]
        [InlineData(2, 1, 0, 1, "R$ 500.55", "Teste Descripition Receita ", "Receita")]
        public void LancamentoVM_Should_Set_Properties_Correctly(int id, int idUsuario, int idDespesa, int idReceita, string valor, string descricao, string tipoCategoria)
        {
            // Arrange and Act
            var data = DateTime.Now.ToString("yyyy-MM-dd");
            var mockCategoria = Mock.Of<Categoria>();

            var lancamentoVM = new LancamentoVM
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
            Assert.Equal(id, lancamentoVM.Id);
            Assert.Equal(idUsuario, lancamentoVM.IdUsuario);
            Assert.Equal(idDespesa, lancamentoVM.IdDespesa);
            Assert.Equal(idReceita, lancamentoVM.IdReceita);
            Assert.Equal(valor, lancamentoVM.Valor);
            Assert.Equal(data, lancamentoVM.Data);
            Assert.Equal(descricao, lancamentoVM.Descricao);
            Assert.Equal(tipoCategoria, lancamentoVM.TipoCategoria);
            Assert.Equal(mockCategoria.Descricao, lancamentoVM.Categoria);
        }
    }
}