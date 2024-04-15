using MediatR;

namespace CrossCutting.CQRS.Queries;

public sealed class GetAllQuery<T> : BaseProperties<T>, IRequest<IEnumerable<T>> where T : class, new()
{
    public GetAllQuery(T entity) : base(entity) { }
}
