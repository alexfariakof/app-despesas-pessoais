namespace Domain.Entities.ValueObjects;
public record TipoCategoria
{
    public static implicit operator TipoCategoriaType(TipoCategoria tc) => (TipoCategoriaType)tc.Id;
    public static implicit operator TipoCategoria(TipoCategoriaType tipoCategoria) => new TipoCategoria(tipoCategoria);
    public static implicit operator TipoCategoria(int tipoCategoria) => new TipoCategoria(tipoCategoria);
    public static bool operator ==(TipoCategoria tipoCategoria, TipoCategoriaType tipoCategoriaType) => tipoCategoria?.Id == (int)tipoCategoriaType;
    public static bool operator !=(TipoCategoria tipoCategoria, TipoCategoriaType tipoCategoriaType) => !(tipoCategoria == tipoCategoriaType);

    public enum TipoCategoriaType : int
    {
        Despesa = 1,
        Receita = 2
    }
    public int Id { get; set; }
    public string Name { get; set; }

    public TipoCategoria() { }

    public TipoCategoria(int id)
    {
        Id = id;
        Name = GetTipoCategoriaName((TipoCategoriaType)id);
    }

    private string GetTipoCategoriaName(TipoCategoriaType tipoCategoria)
    {
        if (TipoCategoriaType.Despesa == tipoCategoria)
            return "Despesa";
        else if (TipoCategoriaType.Receita == tipoCategoria)
            return "Receita";

        throw new ArgumentException("Tipo de Categoria inexistente!");
    }
}

