using Business.Dtos.Core;
using Domain.Entities.ValueObjects;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace Business.Dtos.v1;
public class UsuarioDto : UsuarioDtoBase
{
    [Required]
    public override string? Nome { get; set; }

    public override string? SobreNome { get; set; }

    [Required]
    public override string? Telefone { get; set; }

    [EmailAddress]
    [Required]
    public override string? Email { get; set; }

    [JsonProperty(PropertyName = "perfilUsuario", NullValueHandling = NullValueHandling.Ignore)]
    public override PerfilUsuario PerfilUsuario { get; set; }

}