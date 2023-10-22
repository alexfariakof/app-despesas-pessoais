using despesas_backend_api_net_core.Domain.VM;

namespace despesas_backend_api_net_core.Infrastructure.Security
{
    public interface IAmazonS3Bucket
    {
        public Task<string> WritingAnObjectAsync(ImagemPerfilUsuarioVM perfilFile);
        public Task<bool> DeleteObjectNonVersionedBucketAsync(ImagemPerfilUsuarioVM perfilFile);
    }
}
