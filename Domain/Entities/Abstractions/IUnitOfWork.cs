namespace Domain.Entities.Abstractions;
public interface IUnitOfWork<T> where T : class
{
    IRepositoy<T> Repository { get; }
    Task CommitAsync();
}
