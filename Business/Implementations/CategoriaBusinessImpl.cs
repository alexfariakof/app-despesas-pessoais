using Business.Abstractions;
using Business.Dtos;
using Business.Dtos.Parser;
using Domain.Entities;
using Domain.Entities.Abstractions;

namespace Business.Implementations;
public class CategoriaBusinessImpl: BusinessBase<CategoriaVM, Categoria>
{
    private readonly IUnitOfWork<Categoria> _unitOfWork;
    private readonly CategoriaParser _converter;        

    public CategoriaBusinessImpl(IUnitOfWork<Categoria> unitOfWork): base (unitOfWork)
    {
        _unitOfWork = unitOfWork;
        _converter = new CategoriaParser();
    }

    public override CategoriaVM Create(CategoriaVM obj)
    {
        Categoria categoria = _converter.Parse(obj);
        _unitOfWork.Repository.Insert(ref categoria);
        return _converter.Parse(categoria);
    }

    public override IList<CategoriaVM> FindAll(int idUsuario)
    {
        var lstCategoria = _unitOfWork.Repository.GetAll().Result.Where(c => c.UsuarioId == idUsuario).ToList();
        return _converter.ParseList(lstCategoria);
    }    
    
    public override CategoriaVM FindById(int id, int idUsuario)
    {
        var categoria = _converter.Parse(_unitOfWork.Repository.GetById(id).Result);
        if (idUsuario == categoria.IdUsuario)
            return categoria;
        return null;
    }

    public override CategoriaVM Update(CategoriaVM obj)
    {
        Categoria categoria = _converter.Parse(obj);
        _unitOfWork.Repository.Update(ref categoria);
        return _converter.Parse(categoria);
    }

    public override bool Delete(CategoriaVM obj)
    {
        try
        {
            Categoria categoria = _converter.Parse(obj);
            _unitOfWork.Repository.Delete(categoria.Id);

            return true;
        }
        catch
        {
            return false;
        }
    }
}