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
    public DespesaBusinessImpl(IMapper mapper, IUnitOfWork<Despesa> unitOfWork, IRepositorio<Despesa> repositorio, IRepositorio<Categoria> repoCategoria) : base(mapper, repositorio, unitOfWork)
    {
        _repositorio = repositorio;
        _repoCategoria = repoCategoria;
        _mapper = mapper;
    }

    public override Dto Create(Dto dto)
    {
        Despesa despesa = _mapper.Map<Despesa>(dto);
        IsValidCategoria(despesa);
        _repositorio.Insert(ref despesa);
        return _mapper.Map<Dto>(despesa);
    }

    public override List<Dto> FindAll(Guid idUsuario)
    {
        var despesas = _repositorio.GetAll().FindAll(d => d.UsuarioId == idUsuario);
        var dtos = _mapper.Map<List<Dto>>(despesas);
        return dtos;
    }

    public override Dto FindById(Guid id, Guid idUsuario)
    {
        var despesa = _repositorio.Get(id);
        if (despesa is null) return null;
        despesa.UsuarioId = idUsuario;
        IsValidDespesa(despesa);
        var despesaDto = _mapper.Map<Dto>(despesa);
        return despesaDto;
    }

    public override Dto Update(Dto dto)
    {
        Despesa despesa = _mapper.Map<Despesa>(dto);
        IsValidDespesa(despesa);
        IsValidCategoria(despesa);
        _repositorio.Update(ref despesa);
        return _mapper.Map<Dto>(despesa);
    }

    public override bool Delete(Dto dto)
    {
        Despesa despesa = _mapper.Map<Despesa>(dto);
        IsValidDespesa(despesa);
        return _repositorio.Delete(despesa);
    }

    private void IsValidCategoria(Despesa dto)
    {
        if (_repoCategoria.GetAll().Find(c => c.UsuarioId == dto.UsuarioId && c.TipoCategoria == TipoCategoria.CategoriaType.Despesa && c.Id == dto.CategoriaId) == null)
            throw new ArgumentException("Categoria inválida para este usuário!");
    }

    private void IsValidDespesa(Despesa dto)
    {
        if (_repositorio.Get(dto.Id)?.Usuario?.Id != dto.UsuarioId)
            throw new ArgumentException("Despesa inválida para este usuário!");
    }
}
