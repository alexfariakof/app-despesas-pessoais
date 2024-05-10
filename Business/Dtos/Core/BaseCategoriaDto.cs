using Domain.Entities;

namespace Business.Dtos.Core;
public abstract class BaseCategoriaDto : BaseModelDto
{
    public string? Descricao { get; set; }
    public int IdTipoCategoria { get; set; }
    public TipoCategoria TipoCategoria { get { return (TipoCategoria)IdTipoCategoria; } set { IdTipoCategoria = (int)value; } }

}