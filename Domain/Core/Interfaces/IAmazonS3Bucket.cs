using Domain.Entities;

namespace Domain.Core.Interfaces;
public interface IAmazonS3Bucket
{
    public Task<string> WritingAnObjectAsync(ImagemPerfilUsuario perfilFile, byte[]? file);
    public Task<bool> DeleteObjectNonVersionedBucketAsync(ImagemPerfilUsuario perfilFile);
}
