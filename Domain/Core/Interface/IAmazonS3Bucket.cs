using Domain.VM;

namespace Domain.Core.Interface;
public interface IAmazonS3Bucket
{
    public Task<string> WritingAnObjectAsync(ImagemPerfilVM perfilFile);
    public Task<bool> DeleteObjectNonVersionedBucketAsync(ImagemPerfilVM perfilFile);
}
