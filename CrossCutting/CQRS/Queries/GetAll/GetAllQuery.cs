using MediatR;

namespace CrossCutting.CQRS.Queries;

public sealed class GetAllQuery<T> : BaseProperties<T>, IRequest<List<T>> where T : class, new()
{
    public GetAllQuery(T entity) : base(entity) { }
}
