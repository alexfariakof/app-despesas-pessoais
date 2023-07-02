using Amazon;
using Amazon.S3;
using Amazon.S3.Model;
using despesas_backend_api_net_core.Domain.VM;
using Newtonsoft.Json;

namespace despesas_backend_api_net_core.Infrastructure.ExtensionMethods
{
    public class AmazonS3Bucket
    {
        private static readonly S3CannedACL fileCannedACL = S3CannedACL.PublicRead;
        private static readonly RegionEndpoint bucketRegion = RegionEndpoint.SAEast1;
        private static IAmazonS3 client;
        private static string AccessKey;
        private static string SecretAccessKey;
        private static string S3ServiceUrl;
        private static string BucketName;

        public AmazonS3Bucket(string accessKey, string secretAccessKey, string s3ServiceUrl, String bucketName)
        {
            AccessKey = accessKey;
            SecretAccessKey = secretAccessKey;
            S3ServiceUrl = s3ServiceUrl;
            BucketName = bucketName;
        }

        public static async Task<string> WritingAnObjectAsync(ImagemPerfilUsuarioVM perfilFile)
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
                    Key = perfilFile.Name,
                    ContentType = perfilFile.ContentType,
                    InputStream = new System.IO.MemoryStream(perfilFile.Arquivo)
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
                throw ex;
            }
        }
        public static async Task<bool> DeleteObjectNonVersionedBucketAsync(ImagemPerfilUsuarioVM perfilFile)
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
            catch (Exception e)
            {
                return false;
            }
        }
    }
}
