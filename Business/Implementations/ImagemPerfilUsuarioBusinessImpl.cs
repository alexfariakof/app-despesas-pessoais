using AutoMapper;
using Business.Abstractions;
using Business.Dtos.Core;
using Domain.Core;
using Domain.Core.Interfaces;
using Domain.Entities;
using Repository.Persistency.Generic;

namespace Business.Implementations;
public class ImagemPerfilUsuarioBusinessImpl<Dto, DtoUsuario> : IImagemPerfilUsuarioBusiness<Dto, DtoUsuario> where Dto : BaseImagemPerfilDto, new() where DtoUsuario : BaseUsuarioDto, new()
{
    private readonly IMapper _mapper;
    private readonly IRepositorio<ImagemPerfilUsuario> _repositorio;
    private readonly IRepositorio<Usuario> _repositorioUsuario;    
    private readonly IAmazonS3Bucket _amazonS3Bucket;
    public ImagemPerfilUsuarioBusinessImpl(IMapper mapper, IRepositorio<ImagemPerfilUsuario> repositorio, IRepositorio<Usuario> repositorioUsuario,  IAmazonS3Bucket amazonS3Bucket = null)
    {
        _mapper = mapper;
        _repositorio = repositorio;
        _repositorioUsuario = repositorioUsuario;        
        _amazonS3Bucket = amazonS3Bucket == null ? AmazonS3Bucket.GetInstance : amazonS3Bucket; 
    }

    public Dto Create(Dto obj)
    {
        ImagemPerfilUsuario? perfilFile = _mapper.Map<ImagemPerfilUsuario>(obj);
        try
        {
            perfilFile.Url = _amazonS3Bucket.WritingAnObjectAsync(perfilFile, obj.Arquivo).GetAwaiter().GetResult();
            perfilFile.Usuario = _repositorioUsuario.Get(perfilFile.UsuarioId);
            _repositorio.Insert(ref perfilFile);
            return _mapper.Map<Dto>(perfilFile);
        }
        catch
        {
            _amazonS3Bucket.DeleteObjectNonVersionedBucketAsync(perfilFile).GetAwaiter();
        }
        return null;
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

    public Dto Update(Dto obj)
    {        
        try
        {
            var validImagemPerfil = _repositorio.GetAll().Find(prop => prop.UsuarioId.Equals(obj.UsuarioId));
            if (validImagemPerfil == null)
                throw new ArgumentException("Erro ao atualizar iamgem do perfil!");

            _amazonS3Bucket.DeleteObjectNonVersionedBucketAsync(validImagemPerfil).GetAwaiter().GetResult();
            validImagemPerfil.Url = _amazonS3Bucket.WritingAnObjectAsync(validImagemPerfil, obj.Arquivo).GetAwaiter().GetResult();
            _repositorio.Update(ref validImagemPerfil);            
            return _mapper.Map<Dto>(validImagemPerfil);
        }
        catch
        {
            return null;
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