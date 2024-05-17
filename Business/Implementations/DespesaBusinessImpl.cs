using AutoMapper;
using Business.Abstractions;
using Business.Abstractions.Generic;
using Business.Dtos.Core;
using Domain.Entities;
using Domain.Entities.ValueObjects;
using Repository.Persistency.Generic;
using Repository.Persistency.UnitOfWork.Abstractions;

namespace Business.Implementations;
public class DespesaBusinessImpl<Dto> : BusinessBase<Dto, Despesa>, IBusiness<Dto, Despesa> where Dto : DespesaDtoBase, new()
{
    private readonly IRepositorio<Despesa> _repositorio;
    private readonly IRepositorio<Categoria> _repoCategoria;
    private readonly IMapper _mapper;
    public DespesaBusinessImpl(IMapper mapper, IUnitOfWork<Despesa> unitOfWork, IRepositorio<Despesa> repositorio, IRepositorio<Categoria> repoCategoria): base(mapper, unitOfWork)
    {
        _repositorio = repositorio;
        _repoCategoria = repoCategoria;
        _mapper = mapper;
    }

    public override Dto Create(Dto obj)
    {
        Despesa despesa = _mapper.Map<Despesa>(obj);
        IsValidCategoria(despesa);
        _repositorio.Insert(ref despesa);
        return _mapper.Map<Dto>(despesa);
    }

    public override List<Dto> FindAll(int idUsuario)
    {
        var despesas = _repositorio.GetAll().FindAll(d => d.UsuarioId == idUsuario);
        return _mapper.Map<List<Dto>>(despesas);
    }

    public override  Dto FindById(int id, int idUsuario)
    {
        var despesa = _repositorio.Get(id);
        var despesaDto = _mapper.Map<Dto>(despesa);
        return despesaDto; 
    }

    public override  Dto Update(Dto obj)
    {
        Despesa despesa = _mapper.Map<Despesa>(obj);
        IsValidDespesa(despesa);
        IsValidCategoria(despesa);        
        _repositorio.Update(ref despesa);
        return _mapper.Map<Dto>(despesa);
    }

    public override  bool Delete(Dto obj)
    {
        Despesa despesa = _mapper.Map<Despesa>(obj);
        IsValidDespesa(despesa);
        return _repositorio.Delete(despesa);
    }

    private void IsValidCategoria(Despesa obj)
    {
        if (_repoCategoria.GetAll().Find(c => c.UsuarioId == obj.UsuarioId && c.TipoCategoria == TipoCategoria.TipoCategoriaType.Despesa && c.Id == obj.Categoria.Id) == null)
            throw new ArgumentException("Categoria inválida para este usuário!");
    }

    private void IsValidDespesa(Despesa obj)
    {
        if (_repositorio.Get(obj.Id).Usuario.Id != obj.UsuarioId)
            throw new ArgumentException("Despesa inválida para este usuário!");

    }
}
