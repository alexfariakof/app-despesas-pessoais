using Domain.Core;

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
        List<Categoria> defaultCategorias = new List<Categoria>();
        defaultCategorias.Add(new Categoria
        {
            Descricao = "Alimentação",
            TipoCategoria = TipoCategoria.Despesa
        });
        defaultCategorias.Add(new Categoria
        {
            Descricao = "Casa",
            TipoCategoria = TipoCategoria.Despesa
        });
        defaultCategorias.Add(new Categoria
        {
            Descricao = "Serviços",
            TipoCategoria = TipoCategoria.Despesa
        });
        defaultCategorias.Add(new Categoria
        {
            Descricao = "Saúde",
            TipoCategoria = TipoCategoria.Despesa
        });
        defaultCategorias.Add(new Categoria
        {
            Descricao = "Imposto",
            TipoCategoria = TipoCategoria.Despesa
        });
        defaultCategorias.Add(new Categoria
        {
            Descricao = "Transporte",
            TipoCategoria = TipoCategoria.Despesa
        });
        defaultCategorias.Add(new Categoria
        {
            Descricao = "Lazer",
            TipoCategoria = TipoCategoria.Despesa
        });
        defaultCategorias.Add(new Categoria
        {
            Descricao = "Outros",
            TipoCategoria = TipoCategoria.Despesa
        });

        defaultCategorias.Add(new Categoria
        {
            Descricao = "Salário",
            TipoCategoria = TipoCategoria.Receita
        });
        defaultCategorias.Add(new Categoria
        {
            Descricao = "Prêmio",
            TipoCategoria = TipoCategoria.Receita
        });
        defaultCategorias.Add(new Categoria
        {
            Descricao = "Investimento",
            TipoCategoria = TipoCategoria.Receita
        });
        defaultCategorias.Add(new Categoria
        {
            Descricao = "Benefício",
            TipoCategoria = TipoCategoria.Receita
        });
        defaultCategorias.Add(new Categoria
        {
            Descricao = "Outros",
            TipoCategoria = TipoCategoria.Receita
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
}