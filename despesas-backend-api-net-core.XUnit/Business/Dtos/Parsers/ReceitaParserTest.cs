namespace Business.Dtos.Parser;
public class ReceitaParserTest
{
    [Fact]
    public void Should_Parse_ReceitaDto_To_Receita()
    {
        // Arrange
        var receitaParser = new ReceitaParser();
        var receitaDto = ReceitaFaker.Instance.GetNewFakerVM(1, 1);

        // Act
        var receita = receitaParser.Parse(receitaDto);

        // Assert
        Assert.Equal(receitaDto.Id, receita.Id);
        Assert.Equal(receitaDto.Descricao, receita.Descricao);
        Assert.Equal(receitaDto.IdUsuario, receita.UsuarioId);
    }

    [Fact]
    public void Should_Parse_Receita_To_ReceitaDto()
    {
        // Arrange
        var receitaParser = new ReceitaParser();
        var usuario = UsuarioFaker.Instance.GetNewFaker();
        var receita = ReceitaFaker.Instance.GetNewFaker(
            usuario,
            CategoriaFaker.Instance.GetNewFaker(usuario, TipoCategoria.Receita, usuario.Id)
        );

        // Act
        var receitaDto = receitaParser.Parse(receita);

        // Assert
        Assert.Equal(receita.Id, receitaDto.Id);
        Assert.Equal(receita.Descricao, receitaDto.Descricao);
        Assert.Equal(receita.UsuarioId, receitaDto.IdUsuario);
    }

    [Fact]
    public void Should_Parse_List_ReceitaDtos_To_List_Receitas()
    {
        // Arrange
        var receitaParser = new ReceitaParser();
        var usuarioDto = UsuarioFaker.Instance.GetNewFakerVM();
        var receitaDtos = ReceitaFaker.Instance.ReceitasVMs(usuarioDto, usuarioDto.Id);

        // Act
        var receitas = receitaParser.ParseList(receitaDtos);

        // Assert
        Assert.Equal(receitaDtos.Count, receitas.Count);
        for (int i = 0; i < receitaDtos.Count; i++)
        {
            Assert.Equal(receitaDtos[i].Id, receitas[i].Id);
            Assert.Equal(receitaDtos[i].Descricao, receitas[i].Descricao);
            Assert.Equal(receitaDtos[i].IdUsuario, receitas[i].UsuarioId);
        }
    }

    [Fact]
    public void Should_Parse_List_Receitas_To_List_ReceitaDtos()
    {
        // Arrange
        var receitaParser = new ReceitaParser();
        var usuario = UsuarioFaker.Instance.GetNewFaker();
        var receitas = ReceitaFaker.Instance.Receitas(usuario, usuario.Id);

        // Act
        var receitaDtos = receitaParser.ParseList(receitas);

        // Assert
        Assert.Equal(receitas.Count, receitaDtos.Count);
        for (int i = 0; i < receitas.Count; i++)
        {
            Assert.Equal(receitas[i].Id, receitaDtos[i].Id);
            Assert.Equal(receitas[i].Descricao, receitaDtos[i].Descricao);
            Assert.Equal(receitas[i].UsuarioId, receitaDtos[i].IdUsuario);
        }
    }
}
