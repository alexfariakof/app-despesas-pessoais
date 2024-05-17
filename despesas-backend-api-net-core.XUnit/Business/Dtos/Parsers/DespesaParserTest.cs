using Domain.Entities.ValueObjects;
using Fakers.v1;

namespace Business.Dtos.Parser;
public class DespesaParserTest
{
    [Fact]
    public void Should_Parse_DespesaDto_To_Despesa()
    {
        // Arrange
        var despesaParser = new DespesaParser();
        var despesaDto = DespesaFaker.Instance.GetNewFakerVM(1, 1);

        // Act
        var despesa = despesaParser.Parse(despesaDto);

        // Assert
        Assert.Equal(despesaDto.Id, despesa.Id);
        Assert.Equal(despesaDto.Descricao, despesa.Descricao);
    }

    [Fact]
    public void Should_Parse_Despesa_To_DespesaDto()
    {
        // Arrange
        var despesaParser = new DespesaParser();
        var usuario = UsuarioFaker.Instance.GetNewFaker();
        var despesa = DespesaFaker.Instance.GetNewFaker(
            usuario,
            CategoriaFaker.Instance.GetNewFaker(usuario, TipoCategoria.TipoCategoriaType.Despesa, usuario.Id)
        );
        // Act
        var despesaDto = despesaParser.Parse(despesa);

        // Assert
        Assert.Equal(despesa.Id, despesaDto.Id);
        Assert.Equal(despesa.Descricao, despesaDto.Descricao);
        Assert.Equal(despesa.UsuarioId, despesaDto.UsuarioId);
    }

    [Fact]
    public void Should_Parse_List_DespesaDto_To_List_Despesa()
    {
        // Arrange
        var despesaParser = new DespesaParser();
        var usuario = UsuarioFaker.Instance.GetNewFaker();
        var despesaDtos = DespesaFaker.Instance.Despesas(usuario, usuario.Id);

        // Act
        var despesas = despesaParser.ParseList(despesaDtos);

        // Assert
        Assert.Equal(despesaDtos.Count, despesas.Count);
        for (int i = 0; i < despesaDtos.Count; i++)
        {
            Assert.Equal(despesaDtos[i].Id, despesas[i].Id);
            Assert.Equal(despesaDtos[i].Descricao, despesas[i].Descricao);
            Assert.Equal(despesaDtos[i].UsuarioId, despesas[i].UsuarioId);
        }
    }

    [Fact]
    public void Should_Parse_List_Despesa_To_List_DespesaDto()
    {
        // Arrange
        var despesaParser = new DespesaParser();
        var usuarioDto = UsuarioFaker.Instance.GetNewFakerVM();
        var despesas = DespesaFaker.Instance.DespesasVMs(usuarioDto, usuarioDto.Id);

        // Act
        var despesaDtos = despesaParser.ParseList(despesas);

        // Assert
        Assert.Equal(despesas.Count, despesaDtos.Count);
        for (int i = 0; i < despesas.Count; i++)
        {
            Assert.Equal(despesas[i].Id, despesaDtos[i].Id);
            Assert.Equal(despesas[i].Descricao, despesaDtos[i].Descricao);
        }
    }
}
