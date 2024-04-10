using Domain.Core;
using Domain.Core.Interface;
using Domain.Entities;
using Domain.VM;
using Repository.Mapping;
using Repository.Persistency.Generic;

namespace Business.Implementations;
public class ImagemPerfilUsuarioBusinessImpl : IImagemPerfilUsuarioBusiness
{
    private readonly IRepositorio<ImagemPerfilUsuario> _repositorio;
    private readonly ImagemPerfilUsuarioMap _converter;
    private readonly IAmazonS3Bucket _amazonS3Bucket;
    public ImagemPerfilUsuarioBusinessImpl(IRepositorio<ImagemPerfilUsuario> repositorio, IAmazonS3Bucket amazonS3Bucket = null)
    {
        _repositorio = repositorio;
        _converter = new ImagemPerfilUsuarioMap();
        _amazonS3Bucket = amazonS3Bucket == null ? AmazonS3Bucket.GetInstance : amazonS3Bucket; 
    }
    public ImagemPerfilVM Create(ImagemPerfilVM obj)
    {
        try
        {
            string url = _amazonS3Bucket.WritingAnObjectAsync(obj).GetAwaiter().GetResult();
            obj.Url = url;
            ImagemPerfilUsuario perfilFile = _converter.Parse(obj);
            _repositorio.Insert(ref perfilFile);
            return _converter.Parse(perfilFile);
        }
        catch
        {
            _amazonS3Bucket.DeleteObjectNonVersionedBucketAsync(obj).GetAwaiter();
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
            var usuario = _repositorio.GetAll().Find(u => u.UsuarioId == idUsuario).Usuario;
            return new UsuarioMap().Parse(usuario);
        }
        catch
        {
            return null;
        }            
    }
    public ImagemPerfilVM Update(ImagemPerfilVM obj)
    {
        var validImagemPerfil = FindAll(obj.IdUsuario).Find(prop => prop.IdUsuario.Equals(obj.IdUsuario));
        try
        {                
            if (validImagemPerfil == null)
                throw new NullReferenceException("ImagemPerfilUsuarioVM");

            _amazonS3Bucket.DeleteObjectNonVersionedBucketAsync(validImagemPerfil).GetAwaiter().GetResult();
            var imagemPerfilUsuario = new ImagemPerfilVM 
            {

                Id = validImagemPerfil.Id,
                Url = _amazonS3Bucket.WritingAnObjectAsync(obj).GetAwaiter().GetResult(),
                Name = obj.Name,
                Type = obj.Type,
                ContentType = obj.ContentType,
                IdUsuario = validImagemPerfil.IdUsuario,
                Arquivo = obj.Arquivo
            };
            var resultImagePerfilUsuario = new ImagemPerfilUsuarioMap().Parse(imagemPerfilUsuario);
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
        var imagemPerfilUsuario = FindAll(idUsuario).Find(prop  => prop.IdUsuario.Equals(idUsuario));
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