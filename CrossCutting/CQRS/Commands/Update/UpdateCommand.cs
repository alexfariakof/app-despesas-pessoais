using MediatR;

namespace CrossCutting.CQRS.Commands;

public sealed class UpdateCommand<T> : BaseProperties<T>, IRequest<T> where T : class, new()
{
    public UpdateCommand(T entity) : base(entity) {  }
}
