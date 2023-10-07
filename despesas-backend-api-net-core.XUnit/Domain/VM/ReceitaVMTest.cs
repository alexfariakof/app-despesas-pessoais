namespace Test.XUnit.Domain.VM
{
    public class ReceitaVMTest
    {
        [Theory]
        [InlineData(1, "Test Refeita Description 1", 0.99, 1, 11 )]
        [InlineData(2, "Test Receita Description 2", 0, 1, 3 )]
        [InlineData(3, "Test Receita Description 3", 2000, 1, 22)]
        public void ReceitaVM_ShouldSetPropertiesCorrectly(int id, string descricao, Decimal valor, int idUsuario, int idCategoria)
        {
            // Arrange and Act
            var data = DateTime.Now;

            var receitaVM = new ReceitaVM
            {
                Id = id,
                Data = data,
                Descricao = descricao,
                Valor = valor,
                IdUsuario = idUsuario,
                IdCategoria = idCategoria
                
            };

            // Assert
            Assert.Equal(id, receitaVM .Id);
            Assert.Equal(data, receitaVM .Data);
            Assert.Equal(descricao, receitaVM .Descricao);
            Assert.Equal(valor, receitaVM .Valor);
            Assert.Equal(idUsuario, receitaVM .IdUsuario);
            Assert.Equal(idCategoria, receitaVM .IdCategoria);
        }
    }
}