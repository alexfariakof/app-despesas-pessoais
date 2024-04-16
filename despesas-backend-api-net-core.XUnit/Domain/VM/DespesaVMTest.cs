using Business.Dtos.Parser;

namespace Domain.ViewModel;
public class DespesaVMTest
{
    [Fact]
    public void DespesaVM_Should_Set_Properties_Correctly()
    {
        // Arrange and Act
        var despesa = DespesaFaker.Instance.Despesas().First();
        var despesaVM = new DespesaParser().Parse(despesa);

        // Assert
        Assert.Equal(despesa.Id, despesaVM.Id);
        Assert.Equal(despesa.Data, despesaVM.Data);
        Assert.Equal(despesa.Descricao, despesaVM.Descricao);
        Assert.Equal(despesa.Valor, despesaVM.Valor);
        Assert.Equal(despesa.DataVencimento, despesaVM.DataVencimento);
        Assert.Equal(despesa.UsuarioId, despesaVM.IdUsuario);
        Assert.Equal(despesa.Categoria.Id, despesaVM.Categoria.Id);
    }
    
}