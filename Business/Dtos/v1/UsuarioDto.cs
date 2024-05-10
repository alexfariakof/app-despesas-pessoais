using Business.Dtos.Core;
using Domain.Entities;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Business.Dtos.v1;
public class UsuarioDto : BaseUsuarioDto
{
    [Required]
    public string? Nome { get; set; }

    public string? SobreNome { get; set; }

    [Required]
    public string? Telefone { get; set; }

    [EmailAddress]
    [Required]
    public string? Email { get; set; }

    [JsonIgnore]
    public PerfilUsuario PerfilUsuario { get; set; }

}