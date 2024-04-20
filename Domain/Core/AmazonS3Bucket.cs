using Amazon;
using Amazon.S3;
using Amazon.S3.Model;
using Domain.Core.Interfaces;
using Domain.Entities;
using Newtonsoft.Json.Linq;

namespace Domain.Core;
public class AmazonS3Bucket : IAmazonS3Bucket
{
    private static IAmazonS3Bucket? Instance;
    private readonly S3CannedACL fileCannedACL = S3CannedACL.PublicRead;
    private readonly RegionEndpoint bucketRegion = RegionEndpoint.SAEast1;
    private IAmazonS3? client;
    private readonly string? AccessKey;
    private readonly string? SecretAccessKey;
    private readonly string? S3ServiceUrl;
    private readonly string? BucketName;

    private AmazonS3Bucket()
    {
        var jsonFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "appsettings.json");

        if (File.Exists(jsonFilePath))
        {
            var jsonContent = File.ReadAllText(jsonFilePath);
            var config = JObject.Parse(jsonContent);
            AccessKey = config["AmazonS3Bucket"]?["accessKey"]?.ToString();
            SecretAccessKey = config["AmazonS3Bucket"]?["secretAccessKey"]?.ToString();
            S3ServiceUrl = config["AmazonS3Bucket"]?["s3ServiceUrl"]?.ToString();
            BucketName = config["AmazonS3Bucket"]?["bucketName"]?.ToString();
        }
    }
    public static IAmazonS3Bucket GetInstance
    {
        get
        {
            return Instance == null ? new AmazonS3Bucket() : Instance;
        }
    }
    public async Task<string> WritingAnObjectAsync(ImagemPerfilUsuario perfilFile, byte[]? file)
    {
        try
        {
            string? fileContentType = perfilFile.ContentType;
            AmazonS3Config config = new AmazonS3Config();
            config.ServiceURL = S3ServiceUrl;

            client = new AmazonS3Client(AccessKey, SecretAccessKey, config);

            var putRquest = new PutObjectRequest
            {
                CannedACL = fileCannedACL,
                BucketName = BucketName,
                Key = perfilFile.Name,
                ContentType = perfilFile.ContentType,
                InputStream = new MemoryStream(file ?? throw new ArgumentException("Erro no arquivo!"))
            };
            PutObjectResponse response = await client.PutObjectAsync(putRquest);
            var url = $"https://{BucketName}.s3.amazonaws.com/{perfilFile.Name}";
            return url;

        }
        catch (Exception ex)
        {
            throw new Exception("AmazonS3Bucket_WritingAnObjectAsync_Errro ", ex);
        }
    }
    public async Task<bool> DeleteObjectNonVersionedBucketAsync(ImagemPerfilUsuario perfilFile)
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