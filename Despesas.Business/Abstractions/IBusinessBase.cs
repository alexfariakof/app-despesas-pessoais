namespace Business.Abstractions;
public interface IBusinessBase<Dto, Entity> where Dto : class where Entity : class, new()
{
    Dto Create(Dto usuarioDto);

    Dto FindById(Guid id, Guid idUsuario);
    Dto FindById(Guid id);

    List<Dto> FindAll(Guid idUsuario);

    Dto Update(Dto usuario);

    bool Delete(Dto usuario);
}