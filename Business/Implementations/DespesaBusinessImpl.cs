using AutoMapper;
using Business.Abstractions;
using Business.Dtos.Core;
using Business.Generic;
using Domain.Entities;
using Domain.Entities.Abstractions;
using Repository.Persistency.Generic;

namespace Business.Implementations;
public class DespesaBusinessImpl<Dto> : BusinessBase<Dto, Despesa>, IBusiness<Dto, Despesa> where Dto : BaseDespesaDto, new()
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
        IsCategoriaValid(obj);
        Despesa despesa = _mapper.Map<Despesa>(obj);
        _repositorio.Insert(ref despesa);
        return _mapper.Map<Dto>(despesa);
    }

    public override List<Dto> FindAll(int idUsuario)
    {
        var despesas = _repositorio.GetAll().FindAll(d => d.UsuarioId == idUsuario);
        foreach (var despesa in despesas)
            despesa.Categoria = _repoCategoria.Get(despesa.CategoriaId);
        return _mapper.Map<List<Dto>>(despesas);
    }

    public override  Dto FindById(int id, int idUsuario)
    {
        var despesa = _repositorio.Get(id);
        despesa.Categoria = _repoCategoria.Get(despesa.CategoriaId);
        var despesaDto = _mapper.Map<Dto>(despesa);
        return despesaDto; 
    }

    public override  Dto Update(Dto obj)
    {
        IsCategoriaValid(obj);
        Despesa despesa = _mapper.Map<Despesa>(obj);
        _repositorio.Update(ref despesa);
        return _mapper.Map<Dto>(despesa);
    }

    public override  bool Delete(Dto obj)
    {
        Despesa despesa = _mapper.Map<Despesa>(obj);
        return _repositorio.Delete(despesa);
    }
    private void IsCategoriaValid(Dto obj)
    {
        if (_repoCategoria.GetAll().Find(c => c.UsuarioId == obj.IdUsuario && c.TipoCategoria == TipoCategoria.Despesa && c.Id == obj.IdCategoria) == null)
            throw new ArgumentException("Erro Categoria inexistente ou não cadastrada!");
    }
}
