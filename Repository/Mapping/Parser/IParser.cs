namespace Repository.Mapping.Parser;
public interface IParser<O, D>
{
    D Parse(O origin);
    List<D> ParseList(List<O> origin);
}