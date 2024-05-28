using Domain.Core;
using Domain.Entities.ValueObjects;
using System.Text.Json.Serialization;

namespace Domain.Entities;
public class Categoria : BaseModel
{        
    public string? Descricao { get; set; }
    public int UsuarioId { get; set; }        
    public virtual Usuario? Usuario { get; set; }    
    public virtual TipoCategoria TipoCategoria { get; set; }    
    public Categoria() { }

    public Categoria(string? descricao, int usuarioId, Usuario? usuario, TipoCategoria tipoCategoria)
    {
        Descricao = descricao;
        UsuarioId = usuarioId;
        Usuario = usuario;
        TipoCategoria = tipoCategoria;
    }

    [JsonConstructor]
    public Categoria(int id, string? descricao, int usuarioId, Usuario? usuario, TipoCategoria tipoCategoria)
    {
        Id = id;
        Descricao = descricao;
        UsuarioId = usuarioId;
        Usuario = usuario;
        TipoCategoria = tipoCategoria;
    }
}