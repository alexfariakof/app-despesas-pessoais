using MediatR;

namespace CrossCutting.CQRS.Commands;

public sealed class UpdateCommand<T> : PropertiesBase<T>, IRequest<T> where T : class, new()
{
    public UpdateCommand(T entity) : base(entity) {  }
}
