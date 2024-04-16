namespace Business.Dtos.Parser;
public class DespesaParserTest
{
    [Fact]
    public void Should_Parse_DespesaVM_To_Despesa()
    {
        // Arrange
        var despesaParser = new DespesaParser();
        var despesaVM = DespesaFaker.Instance.GetNewFakerVM(1, 1);

        // Act
        var despesa = despesaParser.Parse(despesaVM);

        // Assert
        Assert.Equal(despesaVM.Id, despesa.Id);
        Assert.Equal(despesaVM.Descricao, despesa.Descricao);
        Assert.Equal(despesaVM.IdUsuario, despesa.UsuarioId);
    }

    [Fact]
    public void Should_Parse_Despesa_To_DespesaVM()
    {
        // Arrange
        var despesaParser = new DespesaParser();
        var usuario = UsuarioFaker.Instance.GetNewFaker();
        var despesa = DespesaFaker.Instance.GetNewFaker(
            usuario,
            CategoriaFaker.Instance.GetNewFaker(usuario, TipoCategoria.Despesa, usuario.Id)
        );
        // Act
        var despesaVM = despesaParser.Parse(despesa);

        // Assert
        Assert.Equal(despesa.Id, despesaVM.Id);
        Assert.Equal(despesa.Descricao, despesaVM.Descricao);
        Assert.Equal(despesa.UsuarioId, despesaVM.IdUsuario);
    }

    [Fact]
    public void Should_Parse_List_DespesaVM_To_List_Despesa()
    {
        // Arrange
        var despesaParser = new DespesaParser();
        var usuario = UsuarioFaker.Instance.GetNewFaker();
        var despesaVMs = DespesaFaker.Instance.Despesas(usuario, usuario.Id);

        // Act
        var despesas = despesaParser.ParseList(despesaVMs);

        // Assert
        Assert.Equal(despesaVMs.Count, despesas.Count);
        for (int i = 0; i < despesaVMs.Count; i++)
        {
            Assert.Equal(despesaVMs[i].Id, despesas[i].Id);
            Assert.Equal(despesaVMs[i].Descricao, despesas[i].Descricao);
            Assert.Equal(despesaVMs[i].UsuarioId, despesas[i].IdUsuario);
        }
    }

    [Fact]
    public void Should_Parse_List_Despesa_To_List_DespesaVM()
    {
        // Arrange
        var despesaParser = new DespesaParser();
        var usuarioVM = UsuarioFaker.Instance.GetNewFakerVM();
        var despesas = DespesaFaker.Instance.DespesasVMs(usuarioVM, usuarioVM.Id);

        // Act
        var despesaVMs = despesaParser.ParseList(despesas);

        // Assert
        Assert.Equal(despesas.Count, despesaVMs.Count);
        for (int i = 0; i < despesas.Count; i++)
        {
            Assert.Equal(despesas[i].Id, despesaVMs[i].Id);
            Assert.Equal(despesas[i].Descricao, despesaVMs[i].Descricao);
            Assert.Equal(despesas[i].IdUsuario, despesaVMs[i].UsuarioId);
        }
    }
}
