using MediatR;

namespace CrossCutting.CQRS.Commands.Create;
public sealed class CreateCommand<T>: PropertiesBase<T>, IRequest<T> where T : class, new()
{
    public CreateCommand(T entity) : base(entity)  {  }
}
