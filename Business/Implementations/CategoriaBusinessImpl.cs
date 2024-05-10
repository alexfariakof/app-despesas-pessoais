using Business.Abstractions;
using Domain.Entities;
using Domain.Entities.Abstractions;
using MediatR;
using AutoMapper;
using Business.Dtos.Core;
using Business.Generic;

namespace Business.Implementations;
public class CategoriaBusinessImpl: BusinessBase<BaseCategoriaDto, Categoria>, IBusiness<BaseCategoriaDto>
{
    private readonly IMediator _mediator;
    private readonly IUnitOfWork<Categoria> _unitOfWork;    
    public CategoriaBusinessImpl(IMediator mediator, IMapper mapper, IUnitOfWork<Categoria> unitOfWork): base (mapper, unitOfWork)
    {
        _mediator = mediator;
        _unitOfWork = unitOfWork;
    }

    public override BaseCategoriaDto Create(BaseCategoriaDto obj)
    {
        Categoria categoria = this.Mapper.Map<Categoria>(obj);
        _unitOfWork.Repository.Insert(ref categoria);
        _unitOfWork.CommitAsync();
        return this.Mapper.Map<BaseCategoriaDto>(categoria);
    }

    public override List<BaseCategoriaDto> FindAll(int idUsuario)
    {
        var lstCategoria = _unitOfWork.Repository.GetAll().Result.Where(c => c.UsuarioId == idUsuario).ToList();
        return this.Mapper.Map<List<BaseCategoriaDto>>(lstCategoria);
    }

    public override BaseCategoriaDto FindById(int id, int idUsuario)
    {
        var categoria = this.Mapper.Map<BaseCategoriaDto>(_unitOfWork.Repository.GetById(id).Result);
        if (idUsuario == categoria.IdUsuario)
            return categoria;
        return null;
    }

    public override BaseCategoriaDto Update(BaseCategoriaDto obj)
    {
        Categoria categoria = this.Mapper.Map<Categoria>(obj);
        _unitOfWork.Repository.Update(ref categoria);
        _unitOfWork.CommitAsync();
        return this.Mapper.Map<BaseCategoriaDto>(categoria);
    }

    public override bool Delete(BaseCategoriaDto obj)
    {
        try
        {
            Categoria categoria = this.Mapper.Map<Categoria>(obj);
            _unitOfWork.Repository.Delete(categoria.Id);
            _unitOfWork.CommitAsync();
            return true;
        }
        catch
        {
            return false;
        }
    }    
}