using Business.Abstractions;
using Business.Dtos;
using Business.Dtos.Parser;
using Domain.Entities;
using Domain.Entities.Abstractions;
using MediatR;

namespace Business.Implementations;
public class CategoriaBusinessImpl: BusinessBase<CategoriaDto, Categoria>
{
    private readonly IMediator _mediator;
    private readonly IUnitOfWork<Categoria> _unitOfWork;
    private readonly CategoriaParser _converter;
    
    public CategoriaBusinessImpl(IMediator mediator, IUnitOfWork<Categoria> unitOfWork): base (unitOfWork)
    {
        _mediator = mediator;
        _unitOfWork = unitOfWork;
        _converter = new CategoriaParser();   
    }

    public override CategoriaDto Create(CategoriaDto obj)
    {
        Categoria categoria = _converter.Parse(obj);
        _unitOfWork.Repository.Insert(ref categoria);
        _unitOfWork.CommitAsync();
        return _converter.Parse(categoria);
    }

    public override Task<IList<CategoriaDto>> FindAll(int idUsuario)
    {
        var lstCategoria = _unitOfWork.Repository.GetAll().Result.Where(c => c.UsuarioId == idUsuario).ToList();
        return Task.FromResult<IList<CategoriaDto>>(_converter.ParseList(lstCategoria)) ;
    }

    public override CategoriaDto FindById(int id, int idUsuario)
    {
        var categoria = _converter.Parse(_unitOfWork.Repository.GetById(id).Result);
        if (idUsuario == categoria.IdUsuario)
            return categoria;
        return null;
    }

    public override CategoriaDto Update(CategoriaDto obj)
    {
        Categoria categoria = _converter.Parse(obj);
        _unitOfWork.Repository.Update(ref categoria);
        _unitOfWork.CommitAsync();
        return _converter.Parse(categoria);
    }

    public override bool Delete(CategoriaDto obj)
    {
        try
        {
            Categoria categoria = _converter.Parse(obj);
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