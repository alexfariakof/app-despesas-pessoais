using Business.Abstractions;
using Business.Dtos;
using Business.Dtos.Parser;
using Domain.Entities;
using Domain.Entities.Abstractions;

namespace Business.Implementations;
public class CategoriaBusinessImpl: BusinessBase<CategoriaDto, Categoria>
{
    private readonly IUnitOfWork<Categoria> _unitOfWork;
    private readonly CategoriaParser _converter;        

    public CategoriaBusinessImpl(IUnitOfWork<Categoria> unitOfWork): base (unitOfWork)
    {
        _unitOfWork = unitOfWork;
        _converter = new CategoriaParser();
    }

    public override CategoriaDto Create(CategoriaDto obj)
    {
        Categoria categoria = _converter.Parse(obj);
        _unitOfWork.Repository.Insert(ref categoria);
        return _converter.Parse(categoria);
    }

    public override IList<CategoriaDto> FindAll(int idUsuario)
    {
        var lstCategoria = _unitOfWork.Repository.GetAll().Result.Where(c => c.UsuarioId == idUsuario).ToList();
        return _converter.ParseList(lstCategoria);
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
        return _converter.Parse(categoria);
    }

    public override bool Delete(CategoriaDto obj)
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