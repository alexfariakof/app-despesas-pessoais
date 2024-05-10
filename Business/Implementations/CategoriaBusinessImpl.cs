using Business.Abstractions;
using Domain.Entities;
using Domain.Entities.Abstractions;
using MediatR;
using AutoMapper;
using Business.Generic;


namespace Business.Implementations;
public class CategoriaBusinessImpl<Dto>: BusinessBase<Dto, Categoria>, IBusiness<Dto, Categoria> where Dto : class, new()
{
    private readonly IMediator _mediator;
    private readonly IUnitOfWork<Categoria> _unitOfWork;    
    public CategoriaBusinessImpl(IMediator mediator, IMapper mapper, IUnitOfWork<Categoria> unitOfWork): base (mapper, unitOfWork)
    {
        _mediator = mediator;
        _unitOfWork = unitOfWork;
    }

    public override Dto Create(Dto obj)
    {
        Categoria categoria = this.Mapper.Map<Categoria>(obj);
        _unitOfWork.Repository.Insert(ref categoria);
        _unitOfWork.CommitAsync();
        return this.Mapper.Map<Dto>(categoria);
    }

    public override List<Dto> FindAll(int idUsuario)
    {
        var lstCategoria = _unitOfWork.Repository.GetAll().Result.Where(c => c.UsuarioId == idUsuario).ToList();
        return this.Mapper.Map<List<Dto>>(lstCategoria);
    }

    public override Dto FindById(int id, int idUsuario)
    {
        var categoria = this.Mapper.Map<Dto>(_unitOfWork.Repository.GetById(id).Result);
        return categoria;
    }

    public override Dto Update(Dto obj)
    {
        Categoria categoria = this.Mapper.Map<Categoria>(obj);
        _unitOfWork.Repository.Update(ref categoria);
        _unitOfWork.CommitAsync();
        return this.Mapper.Map<Dto>(categoria);
    }

    public override bool Delete(Dto obj)
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