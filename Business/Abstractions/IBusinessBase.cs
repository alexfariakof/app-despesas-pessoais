namespace Business.Abstractions;
public interface IBusinessBase<Dto, Entity> where Dto : class where Entity : class, new()
{
    Dto Create(Dto usuarioDto);

    Dto FindById(int id, int idUsuario);

    List<Dto> FindAll(int idUsuario);

    Dto Update(Dto usuario);

    bool Delete(Dto usuario);
}