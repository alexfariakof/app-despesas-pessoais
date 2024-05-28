using Domain.Core;
using Repository.Persistency.UnitOfWork.Abstractions;

namespace Repository.Persistency.UnitOfWork;
public class UnitOfWork<T>: IUnitOfWork<T> where T : BaseModel
{
    private IRepositoy<T>? _repository;
    private readonly RegisterContext _context;

    public UnitOfWork(RegisterContext context)
    {
        _context = context;
    }

    public IRepositoy<T> Repository
    {
        get
        {
            return _repository = _repository ?? new Repository.UnitOfWork.UnitOfWork<T>(_context);
        }
    }

    public async Task CommitAsync()
    {
        await _context.SaveChangesAsync();
    }
}