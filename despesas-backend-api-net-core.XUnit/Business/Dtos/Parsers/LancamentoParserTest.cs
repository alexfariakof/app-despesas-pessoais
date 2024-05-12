using Business.Dtos.v1;
using Domain.Entities.ValueObjects;
using Fakers.v1;

namespace Business.Dtos.Parser;
public class LancamentoParserTest
{
    [Fact]
    public void Should_Parse_LancamentoDto_To_Lancamento()
    {
        // Arrange
        var lancamentoParser = new LancamentoParser();
        var lancamentoDto = new LancamentoDto
        {
            Id = 1,
            UsuarioId = 1,
            IdDespesa = 1,
            IdReceita = 0,
            Valor = 2000,
            Data = DateTime.Now.ToShortDateString(),
            Descricao = "LancamentoDto Teste",
            TipoCategoria = "Despesa",
            Categoria = Mock.Of<Categoria>().Descricao,
        };

        // Act
        var lancamento = lancamentoParser.Parse(lancamentoDto);

        // Assert
        Assert.Equal(lancamentoDto.Id, lancamento.Id);
        Assert.Equal(lancamentoDto.UsuarioId, lancamento.UsuarioId);
        Assert.Equal(lancamentoDto.IdDespesa, lancamento.DespesaId);
        Assert.Equal(lancamentoDto.IdReceita, lancamento.ReceitaId);
        Assert.Equal(lancamentoDto.Valor, lancamento.Valor);
        Assert.Equal(DateTime.Parse(lancamentoDto.Data).ToShortDateString(), lancamento.Data.ToShortDateString());
    }

    [Fact]
    public void Should_Parse_Lancamento_To_LancamentoDto()
    {
        // Arrange
        var lancamentoParser = new LancamentoParser();
        var lancamento = new Lancamento
        {
            Id = 1,
            UsuarioId = 1,
            DespesaId = 1,
            ReceitaId = 0,
            Valor = 2000,
            Data = DateTime.Now,
            Descricao = "Lancamento Teste",
            DataCriacao = DateTime.Now,
            Categoria = Mock.Of<Categoria>()
        };

        // Act
        var lancamentoDto = lancamentoParser.Parse(lancamento);

        // Assert
        // Assert
        Assert.Equal(lancamentoDto.Id, lancamento.Id);
        Assert.Equal(lancamentoDto.UsuarioId, lancamento.UsuarioId);
        Assert.Equal(lancamentoDto.IdDespesa, lancamento.DespesaId);
        Assert.Equal(lancamentoDto.IdReceita, lancamento.ReceitaId);
        Assert.Equal(lancamentoDto.Valor, lancamento.Valor);
        Assert.Equal(DateTime.Parse(lancamentoDto.Data).ToShortDateString(), lancamento.Data.ToShortDateString());
        //Assert.Equal(lancamentoDto.Descricao, lancamento.Descricao);
    }

    [Fact]
    public void Should_Parse_List_LancamentoDto_To_Lancamento()
    {
        // Arrange
        var lancamentoParser = new LancamentoParser();
        var lancamentoDtos = new List<LancamentoDto>
        {
            new LancamentoDto
            {
                Id = 1,
                UsuarioId = 1,
                IdDespesa = 1,
                IdReceita = 0,
                Valor = 2000,
                Data = DateTime.Now.ToLocalTime().ToShortDateString(),
                Descricao = "LancamentoDto Teste",
                TipoCategoria = "Despesa",
                Categoria = Mock.Of<Categoria>().Descricao,
            },
            new LancamentoDto
            {
                Id = 2,
                UsuarioId = 3,
                IdDespesa = 0,
                IdReceita = 1,
                Valor = 500,
                Data = DateTime.Now.ToLocalTime().ToShortDateString(),
                Descricao = "LancamentoDto Teste",
                TipoCategoria = "Receita",
                Categoria = Mock.Of<Categoria>().Descricao,
            },
            new LancamentoDto
            {
                Id = 3,
                UsuarioId = 2,
                IdDespesa = 1,
                IdReceita = 0,
                Valor = 70000,
                Data = DateTime.Now.ToLocalTime().ToShortDateString(),
                Descricao = "LancamentoDto Teste",
                TipoCategoria = "Despesa",
                Categoria = Mock.Of<Categoria>().Descricao,
            }
        };

        // Act
        var lancamentos = lancamentoParser.ParseList(lancamentoDtos);

        // Assert
        Assert.Equal(lancamentoDtos.Count, lancamentos.Count);
        for (int i = 0; i < lancamentoDtos.Count; i++)
        {
            Assert.Equal(lancamentoDtos[i].Id, lancamentos[i].Id);
            //Assert.Equal(lancamentoDtos[i].Descricao, lancamentos[i].Descricao);
            Assert.Equal(lancamentoDtos[i].UsuarioId, lancamentos[i].UsuarioId);
        }
    }

    [Fact]
    public void Should_Parse_Parse_List_Lancamento_To_lancamentoDto()
    {
        // Arrange
        var lancamentoParser = new LancamentoParser();
        var lancamentos = new List<Lancamento>
        {
            new Lancamento
            {
                Id = 1,
                UsuarioId = 1,
                DespesaId = 1,
                ReceitaId = 0,
                Valor = 2000,
                Data = DateTime.Now,
                Descricao = "Lancamento Teste 1",
                DataCriacao = DateTime.Now,
                Categoria = Mock.Of<Categoria>()
            },
            new Lancamento
            {
                Id = 3,
                UsuarioId = 3,
                DespesaId = 0,
                ReceitaId = 1,
                Valor = 20,
                Data = DateTime.Now,
                Descricao = "Lancamento Teste 2",
                DataCriacao = DateTime.Now,
                Categoria = Mock.Of<Categoria>()
            },
            new Lancamento
            {
                Id = 3,
                UsuarioId = 2,
                DespesaId = 1,
                ReceitaId = 0,
                Valor = 0,
                Data = DateTime.Now,
                Descricao = "Lancamento Teste 3",
                DataCriacao = DateTime.Now,
                Categoria = Mock.Of<Categoria>()
            }
        };

        // Act
        var lancamentoDtos = lancamentoParser.ParseList(lancamentos);

        // Assert
        Assert.Equal(lancamentos.Count, lancamentoDtos.Count);
        for (int i = 0; i < lancamentos.Count; i++)
        {
            Assert.Equal(lancamentos[i].Id, lancamentoDtos[i].Id);
            //Assert.Equal(lancamentos[i].Descricao, lancamentoDtos[i].Descricao);
            Assert.Equal(lancamentos[i].UsuarioId, lancamentoDtos[i].UsuarioId);
        }
    }

    [Fact]
    public void Should_Parse_Despesa_To_Lancamento()
    {
        // Arrange
        var map = new LancamentoParser();
        var usuario = UsuarioFaker.Instance.GetNewFaker();
        var origin = DespesaFaker.Instance.GetNewFaker(
            usuario,
            CategoriaFaker.Instance.GetNewFaker(usuario, TipoCategoria.TipoCategoriaType.Despesa, usuario.Id)
        );

        // Act
        var result = map.Parse(origin);

        // Assert
        Assert.Equal(origin.Valor, result.Valor);
        Assert.Equal(origin.Descricao, result.Descricao);
        Assert.Equal(origin.UsuarioId, result.UsuarioId);
        Assert.Equal(origin.Usuario, result.Usuario);
        Assert.Equal(origin.Id, result.DespesaId);
        Assert.Equal(origin, result.Despesa);
        Assert.Equal(0, result.ReceitaId);
        Assert.NotNull(result.Receita);
        Assert.IsType<Receita>(result.Receita);
        Assert.Equal(origin.CategoriaId, result.CategoriaId);
        Assert.Equal(origin.Categoria, result.Categoria);
    }

    [Fact]
    public void Should_Parse_Receita_To_Lancamento()
    {
        // Arrange
        var map = new LancamentoParser();
        var usuario = UsuarioFaker.Instance.GetNewFaker();
        var origin = ReceitaFaker.Instance.GetNewFaker(
            usuario,
            CategoriaFaker.Instance.GetNewFaker(usuario, TipoCategoria.TipoCategoriaType.Receita, usuario.Id)
        );

        // Act
        var result = map.Parse(origin);

        // Assert
        Assert.Equal(origin.Valor, result.Valor);
        Assert.Equal(origin.Descricao, result.Descricao);
        Assert.Equal(origin.UsuarioId, result.UsuarioId);
        Assert.Equal(origin.Usuario, result.Usuario);
        Assert.Equal(0, result.DespesaId);
        Assert.NotNull(result.Despesa);
        Assert.IsType<Despesa>(result.Despesa);
        Assert.Equal(origin.Id, result.ReceitaId);
        Assert.Equal(origin, result.Receita);
        Assert.Equal(origin.CategoriaId, result.CategoriaId);
        Assert.Equal(origin.Categoria, result.Categoria);
    }

    [Fact]
    public void Should_Parse_List_Despesas_To_List_Lancamentos()
    {
        // Arrange
        var map = new LancamentoParser();
        var usuarario = UsuarioFaker.Instance.GetNewFaker();
        var origin = DespesaFaker.Instance.Despesas(usuarario, usuarario.Id);

        // Act
        var result = map.ParseList(origin);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(origin.Count, result.Count);
        for (int i = 0; i < result.Count; i++)
        {
            Assert.Equal(origin[i].Valor, result[i].Valor);
            Assert.Equal(origin[i].Descricao, result[i].Descricao);
            Assert.Equal(origin[i].UsuarioId, result[i].UsuarioId);
            Assert.Equal(origin[i].Usuario, result[i].Usuario);
            Assert.Equal(origin[i].Id, result[i].DespesaId);
            Assert.Equal(origin[i], result[i].Despesa);
            Assert.Equal(0, result[i].ReceitaId);
            Assert.NotNull(result[i].Receita);
            Assert.IsType<Receita>(result[i].Receita);
            Assert.Equal(origin[i].CategoriaId, result[i].CategoriaId);
            Assert.Equal(origin[i].Categoria, result[i].Categoria);
        }
    }

    [Fact]
    public void Should_Parse_List_Receitas_To_List_Lancamentos()
    {
        // Arrange
        var map = new LancamentoParser();
        var usuarario = UsuarioFaker.Instance.GetNewFaker();
        var origin = ReceitaFaker.Instance.Receitas(usuarario, usuarario.Id);

        // Act
        var result = map.ParseList(origin);

        Assert.NotNull(result);
        Assert.Equal(origin.Count, result.Count);
        for (int i = 0; i < result.Count; i++)
        {
            Assert.Equal(origin[i].Valor, result[i].Valor);
            Assert.Equal(origin[i].Descricao, result[i].Descricao);
            Assert.Equal(origin[i].UsuarioId, result[i].UsuarioId);
            Assert.Equal(origin[i].Usuario, result[i].Usuario);
            Assert.Equal(0, result[i].DespesaId);
            Assert.NotNull(result[i].Despesa);
            Assert.IsType<Despesa>(result[i].Despesa);
            Assert.Equal(origin[i].Id, result[i].ReceitaId);
            Assert.Equal(origin[i], result[i].Receita);
            Assert.Equal(origin[i].CategoriaId, result[i].CategoriaId);
            Assert.Equal(origin[i].Categoria, result[i].Categoria);
        }
    }
}
