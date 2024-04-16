using MediatR;

namespace CrossCutting.CQRS.Queries;

public sealed class GetByIdQuery<T> : BaseProperties<T>, IRequest<T> where T : class, new()
{
    public GetByIdQuery(T entity) : base(entity) { }
}
