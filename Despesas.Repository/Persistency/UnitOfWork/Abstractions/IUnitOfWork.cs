namespace Repository.Persistency.UnitOfWork.Abstractions;
public interface IUnitOfWork<T> where T : class
{
    IRepositoy<T> Repository { get; }
    Task CommitAsync();
}
