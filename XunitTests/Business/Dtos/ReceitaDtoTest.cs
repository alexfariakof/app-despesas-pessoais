using Business.Dtos.Parser;
using __mock__.v1;

namespace Business.Dtos;
public sealed class ReceitaDtoTest
{
    [Fact]
    public void ReceitaDto_Should_Set_Properties_Correctly()
    {
        // Arrange and Act
        var receita = ReceitaFaker.Instance.Receitas().First();
        var receitaDto = new ReceitaParser().Parse(receita);

        // Assert
        Assert.Equal(receita.Id, receitaDto .Id);
        Assert.Equal(receita.Data, receitaDto .Data);
        Assert.Equal(receita.Descricao, receitaDto .Descricao);
        Assert.Equal(receita.Valor, receitaDto .Valor);
        Assert.Equal(receita.UsuarioId, receitaDto .UsuarioId);
        Assert.Equal(receita?.Categoria?.Id, receitaDto.IdCategoria);
    }
}