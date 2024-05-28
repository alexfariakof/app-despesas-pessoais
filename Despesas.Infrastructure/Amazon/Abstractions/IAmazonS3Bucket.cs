using Domain.Entities;

namespace Despesas.Infrastructure.Amazon.Abstractions;
public interface IAmazonS3Bucket
{
    public Task<string> WritingAnObjectAsync(ImagemPerfilUsuario perfilFile, byte[]? file);
    public Task<bool> DeleteObjectNonVersionedBucketAsync(ImagemPerfilUsuario perfilFile);
}
