using MediatR;

namespace CrossCutting.CQRS.Commands;

public sealed class DeleteCommand<T> : PropertiesBase<T>, IRequest<T> where T : class, new()
{
    public DeleteCommand(T entity) : base(entity) { }
}
