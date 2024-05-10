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
    public string? Nome { get; set; }

    public string? SobreNome { get; set; }

    [Required(ErrorMessage = "O campo Telefone é obrigatório.")]
    public string? Telefone { get; set; }

    [EmailAddress(ErrorMessage = "O campo Email é inválido.")]
    [Required(ErrorMessage = "O campo Email é obrigatório.")]
    public string? Email { get; set; }

    [JsonIgnore]
    public PerfilUsuario PerfilUsuario { get; set; }

    public IList<HyperMediaLink> Links { get; set; } = new List<HyperMediaLink>();
}