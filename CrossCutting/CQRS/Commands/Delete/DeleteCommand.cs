using MediatR;

namespace CrossCutting.CQRS.Commands;

public sealed class DeleteCommand<T> : BaseProperties<T>, IRequest<T> where T : class, new()
{
    public DeleteCommand(T entity) : base(entity) { }
}
