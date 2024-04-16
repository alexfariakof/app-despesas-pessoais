namespace Business.Dtos.Parser;
public class ReceitaParserTest
{
    [Fact]
    public void Should_Parse_ReceitaVM_To_Receita()
    {
        // Arrange
        var receitaParser = new ReceitaParser();
        var receitaVM = ReceitaFaker.Instance.GetNewFakerVM(1, 1);

        // Act
        var receita = receitaParser.Parse(receitaVM);

        // Assert
        Assert.Equal(receitaVM.Id, receita.Id);
        Assert.Equal(receitaVM.Descricao, receita.Descricao);
        Assert.Equal(receitaVM.IdUsuario, receita.UsuarioId);
    }

    [Fact]
    public void Should_Parse_Receita_To_ReceitaVM()
    {
        // Arrange
        var receitaParser = new ReceitaParser();
        var usuario = UsuarioFaker.Instance.GetNewFaker();
        var receita = ReceitaFaker.Instance.GetNewFaker(
            usuario,
            CategoriaFaker.Instance.GetNewFaker(usuario, TipoCategoria.Receita, usuario.Id)
        );

        // Act
        var receitaVM = receitaParser.Parse(receita);

        // Assert
        Assert.Equal(receita.Id, receitaVM.Id);
        Assert.Equal(receita.Descricao, receitaVM.Descricao);
        Assert.Equal(receita.UsuarioId, receitaVM.IdUsuario);
    }

    [Fact]
    public void Should_Parse_List_ReceitaVMs_To_List_Receitas()
    {
        // Arrange
        var receitaParser = new ReceitaParser();
        var usuarioVM = UsuarioFaker.Instance.GetNewFakerVM();
        var receitaVMs = ReceitaFaker.Instance.ReceitasVMs(usuarioVM, usuarioVM.Id);

        // Act
        var receitas = receitaParser.ParseList(receitaVMs);

        // Assert
        Assert.Equal(receitaVMs.Count, receitas.Count);
        for (int i = 0; i < receitaVMs.Count; i++)
        {
            Assert.Equal(receitaVMs[i].Id, receitas[i].Id);
            Assert.Equal(receitaVMs[i].Descricao, receitas[i].Descricao);
            Assert.Equal(receitaVMs[i].IdUsuario, receitas[i].UsuarioId);
        }
    }

    [Fact]
    public void Should_Parse_List_Receitas_To_List_ReceitaVMs()
    {
        // Arrange
        var receitaParser = new ReceitaParser();
        var usuario = UsuarioFaker.Instance.GetNewFaker();
        var receitas = ReceitaFaker.Instance.Receitas(usuario, usuario.Id);

        // Act
        var receitaVMs = receitaParser.ParseList(receitas);

        // Assert
        Assert.Equal(receitas.Count, receitaVMs.Count);
        for (int i = 0; i < receitas.Count; i++)
        {
            Assert.Equal(receitas[i].Id, receitaVMs[i].Id);
            Assert.Equal(receitas[i].Descricao, receitaVMs[i].Descricao);
            Assert.Equal(receitas[i].UsuarioId, receitaVMs[i].IdUsuario);
        }
    }
}
