using Business.Dtos;
using Business.Dtos.Parser;
using Business.Generic;
using Domain.Entities;
using Repository.Persistency.Generic;

namespace Business.Implementations;
public class CategoriaBusinessImpl : IBusiness<CategoriaVM>
{
    private readonly IRepositorio<Categoria> _repositorio;
    private readonly CategoriaParser _converter;        

    public CategoriaBusinessImpl(IRepositorio<Categoria> repositorio)
    {
        _repositorio = repositorio;
        _converter = new CategoriaParser();
    }

    public CategoriaVM Create(CategoriaVM obj)
    {
        Categoria categoria = _converter.Parse(obj);
        _repositorio.Insert(ref categoria);
        return _converter.Parse(categoria);
    }

    public List<CategoriaVM> FindAll(int idUsaurio)
    {
        var lstCategoria = _repositorio.GetAll().FindAll(c => c.UsuarioId == idUsaurio);
        return _converter.ParseList(lstCategoria);
    }    
    
    public CategoriaVM FindById(int id, int idUsuario)
    {
        var categoria = _converter.Parse(_repositorio.Get(id));
        if (idUsuario == categoria.IdUsuario)
            return categoria;
        return null;
    }

    public CategoriaVM Update(CategoriaVM obj)
    {
        Categoria categoria = _converter.Parse(obj);
        _repositorio.Update(ref categoria);
        return _converter.Parse(categoria);
    }

    public bool Delete(CategoriaVM obj)
    {
        Categoria categoria = _converter.Parse(obj);
        return _repositorio.Delete(categoria);
    }
}