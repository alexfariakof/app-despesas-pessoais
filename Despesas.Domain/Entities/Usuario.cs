using Domain.Core;
using Domain.Entities.ValueObjects;

namespace Domain.Entities;
public class Usuario : BaseModel
{
    public string Nome { get; set; } = String.Empty;
    public string SobreNome { get; set; } = String.Empty;
    public string Telefone { get; set; } = String.Empty;
    public string Email { get; set; } = String.Empty;
    public virtual StatusUsuario StatusUsuario { get; set; }
    public virtual PerfilUsuario? PerfilUsuario { get; set; }
    public virtual IList<Categoria> Categorias { get; set; } = new List<Categoria>();

    public Usuario CreateUsuario(Usuario newUsuario)
    {
        IsValidUsuario(newUsuario.Nome, newUsuario.Email, newUsuario.Telefone);
        newUsuario.StatusUsuario = StatusUsuario.Ativo;
        newUsuario.PerfilUsuario = new PerfilUsuario(PerfilUsuario.Perfil.User);

        List<Categoria> defaultCategorias = new List<Categoria>();
        defaultCategorias.Add(new Categoria
        {
            Descricao = "Alimentação",
            TipoCategoria = new TipoCategoria(TipoCategoria.CategoriaType.Despesa)
        });
        defaultCategorias.Add(new Categoria
        {
            Descricao = "Casa",
            TipoCategoria = new TipoCategoria(TipoCategoria.CategoriaType.Despesa)
        });
        defaultCategorias.Add(new Categoria
        {
            Descricao = "Serviços",
            TipoCategoria = new TipoCategoria(TipoCategoria.CategoriaType.Despesa)
        });
        defaultCategorias.Add(new Categoria
        {
            Descricao = "Saúde",
            TipoCategoria = new TipoCategoria(TipoCategoria.CategoriaType.Despesa)
        });
        defaultCategorias.Add(new Categoria
        {
            Descricao = "Imposto",
            TipoCategoria = new TipoCategoria(TipoCategoria.CategoriaType.Despesa)
        });
        defaultCategorias.Add(new Categoria
        {
            Descricao = "Transporte",
            TipoCategoria = new TipoCategoria(TipoCategoria.CategoriaType.Despesa)
        });
        defaultCategorias.Add(new Categoria
        {
            Descricao = "Lazer",
            TipoCategoria = new TipoCategoria(TipoCategoria.CategoriaType.Despesa)
        });
        defaultCategorias.Add(new Categoria
        {
            Descricao = "Outros",
            TipoCategoria = new TipoCategoria(TipoCategoria.CategoriaType.Despesa)
        });

        defaultCategorias.Add(new Categoria
        {
            Descricao = "Salário",
            TipoCategoria = new TipoCategoria(TipoCategoria.CategoriaType.Receita)
        });
        defaultCategorias.Add(new Categoria
        {
            Descricao = "Prêmio",
            TipoCategoria = new TipoCategoria(TipoCategoria.CategoriaType.Receita)
        });
        defaultCategorias.Add(new Categoria
        {
            Descricao = "Investimento",
            TipoCategoria = new TipoCategoria(TipoCategoria.CategoriaType.Receita)
        });
        defaultCategorias.Add(new Categoria
        {
            Descricao = "Benefício",
            TipoCategoria = new TipoCategoria(TipoCategoria.CategoriaType.Receita)
        });
        defaultCategorias.Add(new Categoria
        {
            Descricao = "Outros",
            TipoCategoria = new TipoCategoria(TipoCategoria.CategoriaType.Receita)
        });

        newUsuario.Categorias = defaultCategorias;
        return newUsuario;
    }

    private static void IsValidUsuario(string nome, string email, string telefone)
    {
        if (String.IsNullOrEmpty(nome) || String.IsNullOrWhiteSpace(nome))
            throw new ArgumentException("Nome não pode ser em branco ou nulo.");

        if (String.IsNullOrEmpty(email) || String.IsNullOrWhiteSpace(email))
            throw new ArgumentException("Email não pode ser em branco ou nulo.");

        if (String.IsNullOrEmpty(telefone) || String.IsNullOrWhiteSpace(telefone))
            throw new ArgumentException("Telefone não pode ser em branco ou nulo.");

    }
}