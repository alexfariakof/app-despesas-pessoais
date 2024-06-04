namespace Business.Abstractions;
public interface IImagemPerfilUsuarioBusiness<Dto, DtoUsuario> where Dto : class where DtoUsuario: class, new()
{
    Dto Create(Dto obj);
    Dto FindById(int id, int idUsuario);
    List<Dto> FindAll(int idUsuario);
    Dto Update(Dto obj);
    bool Delete(int idUsuario);
    DtoUsuario FindByIdUsuario(int idUsuario);
}
