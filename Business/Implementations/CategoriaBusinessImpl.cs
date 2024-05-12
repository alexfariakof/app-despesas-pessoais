using Business.Abstractions;
using Domain.Entities;
using MediatR;
using AutoMapper;
using Business.Abstractions.Generic;
using Repository.Persistency.UnitOfWork.Abstractions;
using Repository.Persistency.Generic;
using Domain.Entities.ValueObjects;


namespace Business.Implementations;
public class CategoriaBusinessImpl<Dto>: BusinessBase<Dto, Categoria>, IBusiness<Dto, Categoria> where Dto : class, new()
{
    private readonly IMediator _mediator;
    private readonly IUnitOfWork<Categoria> _unitOfWork;
    private readonly IRepositorio<Categoria> _repositorio;
    public CategoriaBusinessImpl(IMediator mediator, IMapper mapper, IUnitOfWork<Categoria> unitOfWork, IRepositorio<Categoria> repositorio) : base (mapper, unitOfWork)
    {
        _mediator = mediator;
        _unitOfWork = unitOfWork;
        _repositorio = repositorio;
    }

    public override Dto Create(Dto obj)
    {
        IsValidCategoria(obj);
        Categoria categoria = this.Mapper.Map<Categoria>(obj);
        _repositorio.Insert(ref categoria);
        return this.Mapper.Map<Dto>(categoria);
    }

    public override List<Dto> FindAll(int idUsuario)
    {
        var lstCategoria =  _repositorio.GetAll().Where(c => c.UsuarioId == idUsuario).ToList();
        return this.Mapper.Map<List<Dto>>(lstCategoria);
    }

    public override Dto FindById(int id, int idUsuario)
    {
        var categoria = this.Mapper.Map<Dto>(_repositorio.Get(id));
        return categoria;
    }

    public override Dto Update(Dto obj)
    {
        IsValidCategoria(obj);
        Categoria categoria = this.Mapper.Map<Categoria>(obj);
        _repositorio.Update(ref categoria);
        return this.Mapper.Map<Dto>(categoria);
    }

    public override bool Delete(Dto obj)
    {
        try
        {
            Categoria categoria = this.Mapper.Map<Categoria>(obj);
            _repositorio.Delete(categoria);
            return true;
        }
        catch
        {
            return false;
        }
    }    

    private void IsValidCategoria(Dto obj)
    {
        Categoria categoria = this.Mapper.Map<Categoria>(obj);
        if (categoria.TipoCategoria != TipoCategoria.TipoCategoriaType.Despesa && categoria.TipoCategoria != TipoCategoria.TipoCategoriaType.Receita)
            throw new ArgumentException("Tipo de Categoria Inválida!");
    }
}