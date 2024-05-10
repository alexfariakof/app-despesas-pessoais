using Domain.Entities;

namespace Business.Dtos.Core;
public abstract class BaseCategoriaDto : BaseModelDto
{
    public virtual string? Descricao { get; set; }
    public virtual int IdTipoCategoria { get; set; }
    public virtual TipoCategoria TipoCategoria { get { return (TipoCategoria)IdTipoCategoria; } set { IdTipoCategoria = (int)value; } }
}