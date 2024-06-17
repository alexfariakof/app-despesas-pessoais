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
    public CategoriaBusinessImpl(IMediator mediator, IMapper mapper, IUnitOfWork<Categoria> unitOfWork, IRepositorio<Categoria> repositorio) : base (mapper, repositorio, unitOfWork)
    {
        _mediator = mediator;
        _unitOfWork = unitOfWork;
        _repositorio = repositorio;
    }

    public override Dto Create(Dto dto)
    {
        IsValidTipoCategoria(dto);
        Categoria categoria = this.Mapper.Map<Categoria>(dto);
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
        var categoria = _repositorio?.Find(c => c.Id == id && c.Usuario.Id == idUsuario)?.FirstOrDefault();
        return this.Mapper.Map<Dto>(categoria);
    }

    public override Dto Update(Dto dto)
    {
        IsValidTipoCategoria(dto);
        IsValidCategoria(dto);
        Categoria categoria = this.Mapper.Map<Categoria>(dto);
        _repositorio.Update(ref categoria);
        return this.Mapper.Map<Dto>(categoria);
    }

    public override bool Delete(Dto dto)
    {
        IsValidCategoria(dto);
        try
        {            
            var categoria = this.Mapper.Map<Categoria>(dto);
            _repositorio.Delete(categoria);
            return true;
        }
        catch
        {
            return false;
        }
    }    

    private void IsValidTipoCategoria(Dto dto)
    {
        var categoria = this.Mapper.Map<Categoria>(dto);
        if (categoria.TipoCategoria != TipoCategoria.CategoriaType.Despesa && categoria.TipoCategoria != TipoCategoria.CategoriaType.Receita)
            throw new ArgumentException("Tipo de Categoria Inválida!");
    }

    private void IsValidCategoria(Dto dto)
    {
        var categoria = this.Mapper.Map<Categoria>(dto);
        if (_repositorio.Get(categoria.Id)?.Usuario?.Id != categoria.UsuarioId)
            throw new ArgumentException("Categoria inválida para este usuário!");
    }
}