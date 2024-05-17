namespace Business.Dtos.Core;
public abstract class CategoriaDtoBase : ModelDtoBase
{
    public virtual string? Descricao { get; set; }
    public virtual int IdTipoCategoria { get; set; }
    public virtual TipoCategoriaDto TipoCategoria { get { return (TipoCategoriaDto)IdTipoCategoria; } set { IdTipoCategoria = (int)value; } }
}