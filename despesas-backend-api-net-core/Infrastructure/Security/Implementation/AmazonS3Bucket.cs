using Amazon;
using Amazon.S3;
using Amazon.S3.Model;
using despesas_backend_api_net_core.Domain.VM;

namespace despesas_backend_api_net_core.Infrastructure.Security.Implementation
{
    public class AmazonS3Bucket : IAmazonS3Bucket
    {
        private static IAmazonS3Bucket? Instance;
        private readonly S3CannedACL fileCannedACL = S3CannedACL.PublicRead;
        private readonly RegionEndpoint bucketRegion = RegionEndpoint.SAEast1;
        private IAmazonS3 client;
        private string AccessKey;
        private string SecretAccessKey;
        private string S3ServiceUrl;
        private string BucketName;

        private AmazonS3Bucket()
        {

        }
        public static IAmazonS3Bucket GetInstance
        {
            get
            {
                return Instance == null ? new AmazonS3Bucket() : Instance;
            }
        }
        public void SetConfiguration(string accessKey, string secretAccessKey, string s3ServiceUrl, string bucketName)
        {
            AccessKey = accessKey;
            SecretAccessKey = secretAccessKey;
            S3ServiceUrl = s3ServiceUrl;
            BucketName = bucketName;
        }
        public async Task<string> WritingAnObjectAsync(ImagemPerfilUsuarioVM perfilFile)
        {
            try
            {
                string fileContentType = perfilFile.ContentType;
                AmazonS3Config config = new AmazonS3Config();
                config.ServiceURL = S3ServiceUrl;

                client = new AmazonS3Client(AccessKey, SecretAccessKey, config);

                var putRquest = new PutObjectRequest
                {
                    CannedACL = fileCannedACL,
                    BucketName = BucketName,
                    Key = perfilFile.Name,
                    ContentType = perfilFile.ContentType,
                    InputStream = new MemoryStream(perfilFile.Arquivo)
                };
                PutObjectResponse response = await client.PutObjectAsync(putRquest);
                var url = $"https://{BucketName}.s3.amazonaws.com/{perfilFile.Name}";
                return url;

            }
            catch (AmazonS3Exception ex)
            {
                throw new Exception("Error encountered ***. Message:'{0}' when writing an object", ex);
            }
            catch (Exception ex)
            {
                throw new Exception("AmazonS3Bucket_WritingAnObjectAsync_Errro ", ex);
            }
        }
        public async Task<bool> DeleteObjectNonVersionedBucketAsync(ImagemPerfilUsuarioVM perfilFile)
        {
            try
            {
                AmazonS3Config config = new AmazonS3Config();
                config.ServiceURL = S3ServiceUrl;

                client = new AmazonS3Client(AccessKey, SecretAccessKey, config);


                var deleteObjectRequest = new DeleteObjectRequest
                {
                    BucketName = BucketName,
                    Key = perfilFile.Name,
                };

                Console.WriteLine("Deleting an object");
                await client.DeleteObjectAsync(deleteObjectRequest);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}