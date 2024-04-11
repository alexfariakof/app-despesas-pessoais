using Business.Dtos;
using Business.Dtos.Parser;
using Domain.Core;
using Domain.Core.Interfaces;
using Domain.Entities;
using Repository.Persistency.Generic;

namespace Business.Implementations;

public class ImagemPerfilUsuarioBusinessImpl : IImagemPerfilUsuarioBusiness
{
    private readonly IRepositorio<ImagemPerfilUsuario> _repositorio;
    private readonly ImagemPerfilUsuarioParser _converter;
    private readonly IAmazonS3Bucket _amazonS3Bucket;
    public ImagemPerfilUsuarioBusinessImpl(IRepositorio<ImagemPerfilUsuario> repositorio, IAmazonS3Bucket amazonS3Bucket = null)
    {
        _repositorio = repositorio;
        _converter = new ImagemPerfilUsuarioParser();
        _amazonS3Bucket = amazonS3Bucket == null ? AmazonS3Bucket.GetInstance : amazonS3Bucket; 
    }

    public ImagemPerfilVM Create(ImagemPerfilVM obj)
    {
        ImagemPerfilUsuario? perfilFile = _converter.Parse(obj);
        try
        {
            string url = _amazonS3Bucket.WritingAnObjectAsync(perfilFile, obj.Arquivo).GetAwaiter().GetResult();
            obj.Url = url;
            
            _repositorio.Insert(ref perfilFile);
            return _converter.Parse(perfilFile);
        }
        catch
        {
            _amazonS3Bucket.DeleteObjectNonVersionedBucketAsync(perfilFile).GetAwaiter();
        }
        return null;
    }

    public List<ImagemPerfilVM> FindAll(int idUsuario)
    {
        var lstPerfilFile = _repositorio.GetAll();
        return _converter.ParseList(lstPerfilFile);
    }

    public ImagemPerfilVM FindById(int id, int idUsuario)
    {
        var imagemPerfilUsuario = _converter.Parse(_repositorio.Get(id));
        if (imagemPerfilUsuario.IdUsuario != idUsuario)
            return null;

        return imagemPerfilUsuario;
    }

    public UsuarioVM FindByIdUsuario(int idUsuario)
    {
        try
        {
            var usuario = _repositorio?.GetAll()?.Find(u => u.UsuarioId == idUsuario)?.Usuario;
            return new UsuarioParser().Parse(usuario);
        }
        catch 
        { 
            return null;  
        }            
    }

    public ImagemPerfilVM Update(ImagemPerfilVM obj)
    {        
        try
        {
            var validImagemPerfil = _repositorio.GetAll().Find(prop => prop.Usuario.Id.Equals(obj.IdUsuario));
            if (validImagemPerfil == null)
                throw new ArgumentException("Erro ao atualizar iamgem do perfil!");

            _amazonS3Bucket.DeleteObjectNonVersionedBucketAsync(validImagemPerfil).GetAwaiter().GetResult();
            var imagemPerfilUsuario = new ImagemPerfilVM 
            {

                Id = validImagemPerfil.Id,
                Url = _amazonS3Bucket.WritingAnObjectAsync(validImagemPerfil, obj.Arquivo).GetAwaiter().GetResult(),
                Name = obj.Name,
                Type = obj.Type,
                ContentType = obj.ContentType,
                IdUsuario = validImagemPerfil.UsuarioId,
                Arquivo = obj.Arquivo
            };
            var resultImagePerfilUsuario = new ImagemPerfilUsuarioParser().Parse(imagemPerfilUsuario);
            _repositorio.Update(ref resultImagePerfilUsuario);            
            return _converter.Parse(resultImagePerfilUsuario);
        }
        catch
        {
            return null;
        }
    }

    public bool Delete(int idUsuario)
    {
        var imagemPerfilUsuario = _repositorio.GetAll().Find(prop => prop.Usuario.Id.Equals(idUsuario));
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