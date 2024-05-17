using Domain.Core;
using Domain.Entities.ValueObjects;

namespace Domain.Entities;
public class Usuario : BaseModel
{
    public string? Nome { get; set; }
    public string? SobreNome { get; set; }
    public string? Telefone { get; set; }
    public string? Email { get; set; }
    public virtual StatusUsuario StatusUsuario { get; set; }
    public virtual PerfilUsuario PerfilUsuario { get; set; }
    public virtual IList<Categoria> Categorias { get; set; }  = new List<Categoria>();
    public Usuario CreateUsuario(string nome, string sobreNome, string email, string telefone, StatusUsuario statusUsuario, PerfilUsuario perfilUsuario)
    {
        IsValidUsuario(nome, email, telefone);

        List<Categoria> defaultCategorias = new List<Categoria>();
        defaultCategorias.Add(new Categoria
        {
            Descricao = "Alimentação",
            TipoCategoria = new TipoCategoria(1)
        });
        defaultCategorias.Add(new Categoria
        {
            Descricao = "Casa",
            TipoCategoria = new TipoCategoria(1)
        });
        defaultCategorias.Add(new Categoria
        {
            Descricao = "Serviços",
            TipoCategoria = new TipoCategoria(1)
        });
        defaultCategorias.Add(new Categoria
        {
            Descricao = "Saúde",
            TipoCategoria = new TipoCategoria(1)
        });
        defaultCategorias.Add(new Categoria
        {
            Descricao = "Imposto",
            TipoCategoria = new TipoCategoria(1)
        });
        defaultCategorias.Add(new Categoria
        {
            Descricao = "Transporte",
            TipoCategoria = new TipoCategoria(1)
        });
        defaultCategorias.Add(new Categoria
        {
            Descricao = "Lazer",
            TipoCategoria = new TipoCategoria(1)
        });
        defaultCategorias.Add(new Categoria
        {
            Descricao = "Outros",
            TipoCategoria = new TipoCategoria(1)
        });

        defaultCategorias.Add(new Categoria
        {
            Descricao = "Salário",
            TipoCategoria = new TipoCategoria(2)
        });
        defaultCategorias.Add(new Categoria
        {
            Descricao = "Prêmio",
            TipoCategoria = new TipoCategoria(2)
        });
        defaultCategorias.Add(new Categoria
        {
            Descricao = "Investimento",
            TipoCategoria = new TipoCategoria(2)
        });
        defaultCategorias.Add(new Categoria
        {
            Descricao = "Benefício",
            TipoCategoria = new TipoCategoria(2)
        });
        defaultCategorias.Add(new Categoria
        {
            Descricao = "Outros",
            TipoCategoria = new TipoCategoria(2)
        });

        var newUsuario = new Usuario() 
        { 
            Nome = nome,
            SobreNome = sobreNome,
            Telefone = telefone,
            Email = email,
            StatusUsuario = statusUsuario,
            PerfilUsuario = perfilUsuario,            
            Categorias = defaultCategorias            
        };

        
        return newUsuario;
    }

    private void IsValidUsuario(string nome, string email, string telefone)
    {
        if (String.IsNullOrEmpty(nome) || String.IsNullOrWhiteSpace(nome))
            throw new ArgumentException("Nome não pode ser em branco ou nulo.");

        if (String.IsNullOrEmpty(email) || String.IsNullOrWhiteSpace(email))
            throw new ArgumentException("Email não pode ser em branco ou nulo.");

        if (String.IsNullOrEmpty(telefone) || String.IsNullOrWhiteSpace(telefone))
            throw new ArgumentException("Telefone não pode ser em branco ou nulo.");

    }
}