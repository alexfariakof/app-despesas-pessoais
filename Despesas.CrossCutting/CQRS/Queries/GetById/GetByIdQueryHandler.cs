using AutoMapper;
using Domain.Core;
using MediatR;
using Repository.Persistency.UnitOfWork.Abstractions;

namespace CrossCutting.CQRS.Queries;

public sealed class GetByIdQueryHandler<T> : IRequestHandler<GetByIdQuery<T>, T> where T : BaseModel, new()
{
    private readonly IUnitOfWork<T> _unitOfWork;
    private readonly IMapper _mapper;

    public GetByIdQueryHandler(IUnitOfWork<T> unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<T> Handle(GetByIdQuery<T> request, CancellationToken cancellationToken)
    {
        var entityToFind = _mapper.Map<T>(request);
        return await _unitOfWork.Repository.GetById(entityToFind.Id);
    }
}
