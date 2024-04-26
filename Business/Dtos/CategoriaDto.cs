using Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace Business.Dtos;
public class CategoriaDto : BaseModelDto
{
    [Required(ErrorMessage = "O campo Descrição é obrigatório.")]
    public string? Descricao { get; set; }    
    [Required(ErrorMessage = "O campo Tipo de categoria é obrigatório.")]
    public int IdTipoCategoria { get; set; }
    public TipoCategoria TipoCategoria { get { return (TipoCategoria)IdTipoCategoria; } set   {  IdTipoCategoria = (int)value;  } }
}