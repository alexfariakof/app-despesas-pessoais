namespace Domain.Entities.ValueObjects;
public record TipoCategoria
{
    public static implicit operator CategoriaType(TipoCategoria tc) => (CategoriaType)tc.Id;
    public static implicit operator TipoCategoria(int tipoCategoria) =>  new TipoCategoria((CategoriaType)tipoCategoria);
    public static bool operator ==(TipoCategoria tipoCategoria, CategoriaType tipoCategoriaType) => tipoCategoria?.Id == (int)tipoCategoriaType;
    public static bool operator !=(TipoCategoria tipoCategoria, CategoriaType tipoCategoriaType) => !(tipoCategoria?.Id == (int)tipoCategoriaType);

    public enum CategoriaType
    {
        Invalid = 0,
        Despesa = 1,
        Receita = 2
    }
    public int Id { get; set; }
    public string Name { get; set; }

    public TipoCategoria() {  }

    public TipoCategoria(int id)
    {
        Id = id;
        Name = GetTipoCategoriaName((CategoriaType)id);
    }

    public TipoCategoria(CategoriaType tipoCategoria)
    {
        Id = (int)tipoCategoria;
        Name = GetTipoCategoriaName(tipoCategoria);
    }

    private string GetTipoCategoriaName(CategoriaType tipoCategoria)
    {
        if (CategoriaType.Despesa == tipoCategoria)
            return "Despesa";
        else if (CategoriaType.Receita == tipoCategoria)
            return "Receita";
        else if (CategoriaType.Invalid == tipoCategoria)
            return "Invalid";


        throw new ArgumentException("Tipo de Categoria inexistente!");
    }
}

