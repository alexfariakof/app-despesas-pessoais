using despesas_backend_api_net_core.Infrastructure.Data.EntityConfig;

namespace Domain.ViewModel
{
    public class ReceitaVMTest
    {
        [Fact]
        public void ReceitaVM_Should_Set_Properties_Correctly()
        {
            // Arrange and Act
            var receita = ReceitaFaker.Receitas().First();
            var receitaVM = new ReceitaMap().Parse(receita);

            // Assert
            Assert.Equal(receita.Id, receitaVM .Id);
            Assert.Equal(receita.Data, receitaVM .Data);
            Assert.Equal(receita.Descricao, receitaVM .Descricao);
            Assert.Equal(receita.Valor, receitaVM .Valor);
            Assert.Equal(receita.UsuarioId, receitaVM .IdUsuario);
            Assert.Equal(receita.CategoriaId, receitaVM .IdCategoria);
        }
    }
}