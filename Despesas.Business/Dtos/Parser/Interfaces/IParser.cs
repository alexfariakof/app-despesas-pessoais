namespace Business.Dtos.Parser.Interfaces;
public interface IParser<O, D>
{
    D Parse(O origin);
    List<D> ParseList(List<O> origin);
}