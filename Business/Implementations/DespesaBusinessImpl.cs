using AutoMapper;
using Business.Abstractions;
using Business.Dtos.Core;
using Business.Generic;
using Domain.Entities;
using Domain.Entities.Abstractions;
using Repository.Persistency.Generic;

namespace Business.Implementations;
public class DespesaBusinessImpl : BusinessBase<BaseDespesaDto, Despesa>, IBusiness<BaseDespesaDto>
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
    public override  BaseDespesaDto Create(BaseDespesaDto obj)
    {
        IsCategoriaValid(obj);
        Despesa despesa = _mapper.Map<Despesa>(obj);
        _repositorio.Insert(ref despesa);
        return _mapper.Map<BaseDespesaDto>(despesa);
    }

    public override List<BaseDespesaDto> FindAll(int idUsuario)
    {
        var despesas = _repositorio.GetAll().FindAll(d => d.UsuarioId == idUsuario);
        foreach (var despesa in despesas)
            despesa.Categoria = _repoCategoria.Get(despesa.CategoriaId);
        return _mapper.Map<List<BaseDespesaDto>>(despesas);
    }

    public override  BaseDespesaDto FindById(int id, int idUsuario)
    {
        var despesa = _repositorio.Get(id);
        despesa.Categoria = _repoCategoria.Get(despesa.CategoriaId);
        var despesaDto = _mapper.Map<BaseDespesaDto>(despesa);

        if (despesaDto.IdUsuario == idUsuario)
            return despesaDto;
        return null;
    }

    public override  BaseDespesaDto Update(BaseDespesaDto obj)
    {
        IsCategoriaValid(obj);
        Despesa despesa = _mapper.Map<Despesa>(obj);
        _repositorio.Update(ref despesa);
        return _mapper.Map<BaseDespesaDto>(despesa);
    }

    public override  bool Delete(BaseDespesaDto obj)
    {
        Despesa despesa = _mapper.Map<Despesa>(obj);
        return _repositorio.Delete(despesa);
    }
    private void IsCategoriaValid(BaseDespesaDto obj)
    {
        if (_repoCategoria.GetAll().Find(c => c.UsuarioId == obj.IdUsuario && c.TipoCategoria == TipoCategoria.Despesa && c.Id == obj.IdCategoria) == null)
            throw new ArgumentException("Erro Categoria inexistente ou não cadastrada!");
    }
}
