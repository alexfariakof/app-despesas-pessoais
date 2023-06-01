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
        
        public  class AmazomKeys
        {
            public string AccessKey { get; set; }
            public string SecretAccessKey { get; set; }
            public string S3ServiceUrl { get; set; }
        }

        public static async Task<string> WritingAnObjectAsync(PerfilUsuarioFileVM perfilFile)
        {
            string arquivoJson = "AMZOM_KEYS.json";
            try
            {
                string json = File.ReadAllText(arquivoJson);


                var amazomKeys = JsonConvert.DeserializeObject <AmazomKeys>(json);
                string bucketName = "bucket-usuario-perfil";
                string fileContentType = perfilFile.ContentType;
                AmazonS3Config config = new AmazonS3Config();
                config.ServiceURL = amazomKeys.S3ServiceUrl;

                client = new AmazonS3Client(amazomKeys.AccessKey, amazomKeys.SecretAccessKey, config);
    
                var putRquest = new PutObjectRequest
                {
                    CannedACL = fileCannedACL,
                    BucketName = bucketName,
                    Key = perfilFile.Name,
                    ContentType = perfilFile.ContentType,
                    InputStream = new System.IO.MemoryStream(perfilFile.Arquivo)
                };
                PutObjectResponse response = await client.PutObjectAsync(putRquest);
                var url = $"https://{bucketName}.s3.amazonaws.com/{perfilFile.Name}";
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
    }
}
