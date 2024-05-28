using AutoMapper;
using Business.Abstractions;
using Business.Dtos.Core;
using Despesas.Infrastructure.Amazon.Abstractions;
using Domain.Entities;
using Repository.Persistency.Generic;

namespace Business.Implementations;
public class ImagemPerfilUsuarioBusinessImpl<Dto, DtoUsuario> : IImagemPerfilUsuarioBusiness<Dto, DtoUsuario> where Dto : ImagemPerfilDtoBase, new() where DtoUsuario : UsuarioDtoBase, new()
{
    private readonly IMapper _mapper;
    private readonly IRepositorio<ImagemPerfilUsuario> _repositorio;
    private readonly IRepositorio<Usuario> _repositorioUsuario;    
    private readonly IAmazonS3Bucket _amazonS3Bucket;
    public ImagemPerfilUsuarioBusinessImpl(IMapper mapper, IRepositorio<ImagemPerfilUsuario> repositorio, IRepositorio<Usuario> repositorioUsuario,  IAmazonS3Bucket amazonS3Bucket)
    {
        _mapper = mapper;
        _repositorio = repositorio;
        _repositorioUsuario = repositorioUsuario;        
        _amazonS3Bucket = amazonS3Bucket;
    }

    public Dto Create(Dto dto)
    {
        ImagemPerfilUsuario? perfilFile = _mapper.Map<ImagemPerfilUsuario>(dto);
        try
        {
            perfilFile.Url = _amazonS3Bucket.WritingAnObjectAsync(perfilFile, dto.Arquivo).GetAwaiter().GetResult();
            perfilFile.Usuario = _repositorioUsuario.Get(perfilFile.UsuarioId) ?? throw new();
            _repositorio.Insert(ref perfilFile);
            return _mapper.Map<Dto>(perfilFile);
        }
        catch
        {
            _amazonS3Bucket.DeleteObjectNonVersionedBucketAsync(perfilFile).GetAwaiter();
            throw new ArgumentException("Erro ao criar imagem de perfil.");
        }
    }

    public List<Dto> FindAll(int idUsuario)
    {
        var lstPerfilFile = _repositorio.GetAll();
        return _mapper.Map<List<Dto>>(lstPerfilFile);
    }

    public Dto FindById(int id, int idUsuario)
    {
        var imagemPerfilUsuario = _mapper.Map<Dto>(_repositorio.Get(id));
        if (imagemPerfilUsuario.UsuarioId != idUsuario)
            return null;

        return imagemPerfilUsuario;
    }

    public DtoUsuario FindByIdUsuario(int idUsuario)
    {
        try
        {
            var usuario = _repositorio?.GetAll()?.Find(u => u.UsuarioId == idUsuario)?.Usuario;
            return _mapper.Map<DtoUsuario>(usuario);
        }
        catch 
        { 
            return null;  
        }            
    }

    public Dto Update(Dto dto)
    {        
        try
        {
            var validImagemPerfil = _repositorio.GetAll().Find(prop => prop.UsuarioId.Equals(dto.UsuarioId));
            if (validImagemPerfil == null)
                throw new();

            _amazonS3Bucket.DeleteObjectNonVersionedBucketAsync(validImagemPerfil).GetAwaiter().GetResult();
            validImagemPerfil.Url = _amazonS3Bucket.WritingAnObjectAsync(validImagemPerfil, dto.Arquivo).GetAwaiter().GetResult();
            _repositorio.Update(ref validImagemPerfil);            
            return _mapper.Map<Dto>(validImagemPerfil);
        }
        catch
        {
            throw new ArgumentException("Erro ao atualizar iamgem do perfil!");
        }
    }

    public bool Delete(int idUsuario)
    {
        var imagemPerfilUsuario = _repositorio.GetAll().Find(prop => prop.UsuarioId.Equals(idUsuario));
        if (imagemPerfilUsuario != null)
        {
            var result = _amazonS3Bucket.DeleteObjectNonVersionedBucketAsync(imagemPerfilUsuario).GetAwaiter().GetResult();
            if (result)
            {
               return _repositorio.Delete(new ImagemPerfilUsuario { Id = imagemPerfilUsuario.Id });
            }
        }
        return false;
    }
}