using __mock__.Repository;
using Repository.Abastractions;

namespace Repository.Abstractions;

public sealed class BaseRepositoryTest : IClassFixture<BaseRepositoryFixture>
{
    public class BaseRepositoryClassTest : BaseRepository<Receita>
    {
        public RegisterContext Context { get; }
        public BaseRepositoryClassTest(RegisterContext context) : base(context)
        {
            Context = context;
        }
    }

    private readonly BaseRepositoryFixture _fixture;

    public BaseRepositoryTest(BaseRepositoryFixture fixture)
    {
        _fixture = fixture;
    }

    [Fact]
    public void Insert_ShouldAddEntity()
    {
        var repository = new BaseRepositoryClassTest(_fixture.Context);
        var receita = MockReceita.Instance.GetReceita();
        receita.Categoria.TipoCategoria = _fixture.Context.TipoCategoria.Single(tp => tp.Id.Equals(2));
        receita.Usuario = MockUsuario.Instance.GetUsuario();
        receita.Usuario.PerfilUsuario = _fixture.Context.PerfilUsuario.Single(pu => pu.Id.Equals(1));

        repository.Insert(ref receita);

        Assert.Contains(_fixture.Context.Receita, r => r.Descricao == receita.Descricao);
    }

    [Fact]
    public void Update_ShouldModifyEntity()
    {
        var repository = new BaseRepositoryClassTest(_fixture.Context);
        var receita = _fixture.Context.Receita.First();
        receita.Categoria.TipoCategoria = _fixture.Context.TipoCategoria.Single(tp => tp.Id.Equals(2));
        receita.Usuario = MockUsuario.Instance.GetUsuario();
        receita.Usuario.PerfilUsuario = _fixture.Context.PerfilUsuario.Single(pu => pu.Id.Equals(1));
        receita.Descricao = "Updated Receita";

        repository.Update(ref receita);

        Assert.Equal("Updated Receita", _fixture.Context.Receita.First(r => r.Id == receita.Id).Descricao);
    }

    [Fact]
    public void Delete_ShouldRemoveEntity()
    {
        var repository = new BaseRepositoryClassTest(_fixture.Context);
        var receita = _fixture.Context.Receita.First();

        bool result = repository.Delete(receita);

        Assert.True(result);
        Assert.DoesNotContain(_fixture.Context.Receita, r => r.Id == receita.Id);
    }

    [Fact]
    public void GetAll_ShouldReturnAllEntities()
    {
        var repository = new BaseRepositoryClassTest(_fixture.Context);

        var receitas = repository.GetAll();

        Assert.Equal(_fixture.Context.Receita.Count(), receitas.Count());
    }

    [Fact]
    public void Get_ShouldReturnEntityById()
    {
        var repository = new BaseRepositoryClassTest(_fixture.Context);
        var receita = _fixture.Context.Receita.First();

        var result = repository.Get(receita.Id);

        Assert.NotNull(result);
        Assert.Equal(receita.Descricao, result.Descricao);
    }

    [Fact]
    public void Find_ShouldReturnEntitiesMatchingExpression()
    {
        var repository = new BaseRepositoryClassTest(_fixture.Context);
        var receita = _fixture.Context.Receita.Last();

        var result = repository.Find(r => r.Descricao.Contains(receita.Descricao));

        Assert.NotEmpty(result);
    }

    [Fact]
    public void ExistsById_ShouldReturnTrueIfEntityExists()
    {
        var repository = new BaseRepositoryClassTest(_fixture.Context);
        var receita = _fixture.Context.Receita.First();

        var exists = repository.Exists(receita.Id);

        Assert.True(exists);
    }

    [Fact]
    public void ExistsByExpression_ShouldReturnTrueIfEntityExists()
    {
        var repository = new BaseRepositoryClassTest(_fixture.Context);
        var receita = _fixture.Context.Receita.First();
        var exists = repository.Exists(r => r.Id.Equals(receita.Id));

        Assert.True(exists);
    }
}
