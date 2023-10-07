namespace Test.XUnit.Domain.VM
{
    public class DespesaVMTest
    {
        [Theory]
        [InlineData(1, "Test Despesa Description 1", 400.89, 1, 32 )]
        [InlineData(2, "Test Despesa Description 2", 0, 1, 3 )]
        [InlineData(3, "Test Despesa Description 3", 100000, 1, 99)]
        public void DespesaVM_ShouldSetPropertiesCorrectly(int id, string descricao, Decimal valor, int idUsuario, int idCategoria)
        {
            // Arrange and Act
            var data = DateTime.Now;
            var dataVencimento = DateTime.Now;

            var despesaVM = new DespesaVM
            {
                Id = id,
                Data = data,
                Descricao = descricao,
                Valor = valor,
                DataVencimento = dataVencimento,
                IdUsuario = idUsuario,
                IdCategoria = idCategoria
                
            };

            // Assert
            Assert.Equal(id, despesaVM.Id);
            Assert.Equal(data, despesaVM.Data);
            Assert.Equal(descricao, despesaVM.Descricao);
            Assert.Equal(valor, despesaVM.Valor);
            Assert.Equal(dataVencimento, despesaVM.DataVencimento);
            Assert.Equal(idUsuario, despesaVM.IdUsuario);
            Assert.Equal(idCategoria, despesaVM.IdCategoria);
        }
    }
}