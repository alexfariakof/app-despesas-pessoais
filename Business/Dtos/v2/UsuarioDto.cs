using Business.Dtos.Core;
using Business.HyperMedia;
using Business.HyperMedia.Abstractions;
using Domain.Entities;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Business.Dtos.v2;
public class UsuarioDto : BaseUsuarioDto, ISupportHyperMedia
{
    [Required(ErrorMessage = "O campo Nome é obrigatório.")]
    public override string? Nome { get; set; }

    public override string? SobreNome { get; set; }

    [Required(ErrorMessage = "O campo Telefone é obrigatório.")]
    public override string? Telefone { get; set; }

    [EmailAddress(ErrorMessage = "O campo Email é inválido.")]
    [Required(ErrorMessage = "O campo Email é obrigatório.")]
    public override string? Email { get; set; }

    [JsonIgnore]
    public override PerfilUsuario PerfilUsuario { get; set; }

    public IList<HyperMediaLink> Links { get; set; } = new List<HyperMediaLink>();
}