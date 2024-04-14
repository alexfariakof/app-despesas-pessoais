using AutoMapper;
using Domain.Core;
using Domain.Entities.Abstractions;
using MediatR;

namespace CrossCutting.CQRS.Commands;

public sealed class DeleteCommandHandler<T> : IRequestHandler<DeleteCommand<T>, T> where T : BaseModel, new()
{
    private readonly IUnitOfWork<T> _unitOfWork;
    private readonly IMapper _mapper;

    public DeleteCommandHandler(IUnitOfWork<T> unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<T> Handle(DeleteCommand<T> request, CancellationToken cancellationToken)
    {
        var entityToDelete = _mapper.Map<T>(request);

        var existingEntity = await _unitOfWork.Repository.GetById(entityToDelete.Id);
        if (existingEntity is null)
            throw new InvalidOperationException($"{nameof(existingEntity)} not found !");

        _unitOfWork.Repository.Delete(entityToDelete.Id);
        await _unitOfWork.CommitAsync();
        return entityToDelete;
    }
}
