using Business.Dtos.Core;
using Domain.Entities;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Business.Dtos.v1;
public class UsuarioDto : BaseUsuarioDto
{
    [Required]
    public override string? Nome { get; set; }

    public override string? SobreNome { get; set; }

    [Required]
    public override string? Telefone { get; set; }

    [EmailAddress]
    [Required]
    public override string? Email { get; set; }

    [JsonIgnore]
    public PerfilUsuario PerfilUsuario { get; set; }

}