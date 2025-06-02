using Amazon;
using Amazon.S3;
using Amazon.S3.Model;
using Despesas.Infrastructure.Amazon.Abstractions;
using Domain.Entities;
using Microsoft.Extensions.Options;
using Newtonsoft.Json.Linq;

namespace Despesas.Infrastructure.Amazon;
public class AmazonS3Bucket : IAmazonS3Bucket
{
    private static IAmazonS3Bucket? _amazonS3Bucket;
    private AmazonS3Client? _client;
    private readonly S3CannedACL _fileCannedACL = S3CannedACL.PublicReadWrite;
    private readonly RegionEndpoint _bucketRegion = RegionEndpoint.USEast1;
    private readonly string? _accessKey;
    private readonly string? _secretAccessKey;
    private readonly string? _s3ServiceUrl;
    private readonly string? _bucketName;

    private AmazonS3Bucket()
    {

        var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
        var appsettings = environment == "Development" ? "appsettings.development.json" : "appsettings.json";

        var jsonFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, appsettings);

        if (File.Exists(jsonFilePath))
        {
            var jsonContent = File.ReadAllText(jsonFilePath);
            var config = JObject.Parse(jsonContent);
            _accessKey = config["AmazonS3Configurations"]?["accessKey"]?.ToString();
            _secretAccessKey = config["AmazonS3Configurations"]?["secretAccessKey"]?.ToString();
            _s3ServiceUrl = config["AmazonS3Configurations"]?["s3ServiceUrl"]?.ToString();
            _bucketName = config["AmazonS3Configurations"]?["bucketName"]?.ToString();
        }
        else
        {
            throw new ArgumentException("File appsettings.json não encontrado.");
        }
    }

    public AmazonS3Bucket(IOptions<AmazonS3Options> options)
    {
        _accessKey = options.Value.AccessKey;
        _secretAccessKey = options.Value.SecretAccessKey;
        _s3ServiceUrl = options.Value.S3ServiceUrl;
        _bucketName = options.Value.BucketName;
    }

    public static IAmazonS3Bucket Instance
    {
        get
        {
            return _amazonS3Bucket == null ? new AmazonS3Bucket() : _amazonS3Bucket;
        }
    }

    public async Task<string> WritingAnObjectAsync(ImagemPerfilUsuario perfilFile, byte[]? file)
    {
        try
        {
            string? fileContentType = perfilFile.ContentType;

            var config = new AmazonS3Config
            {
                ServiceURL = _s3ServiceUrl, 
                UseHttp = false,            
                ForcePathStyle = true,      
                RegionEndpoint = RegionEndpoint.USEast1
            }; config.ServiceURL = _s3ServiceUrl;

            _client = new AmazonS3Client(_accessKey, _secretAccessKey, config);

            var putRquest = new PutObjectRequest
            {
                CannedACL = _fileCannedACL,
                BucketName = _bucketName,
                Key = perfilFile.Name,
                ContentType = perfilFile.ContentType,
                InputStream = new MemoryStream(file ?? throw new ArgumentException("Erro no arquivo!"))
            };
            PutObjectResponse response = await _client.PutObjectAsync(putRquest);
            var url = $"{_s3ServiceUrl}/{_bucketName}/{perfilFile.Name}";
            return url;

        }
        catch (Exception ex)
        {
            throw new ArgumentException(ex.Message);
            throw new ArgumentException("AmazonS3Bucket_WritingAnObjectAsync_Errro");
        }
    }

    public async Task<bool> DeleteObjectNonVersionedBucketAsync(ImagemPerfilUsuario perfilFile)
    {
        try
        {
            AmazonS3Config config = new AmazonS3Config();
            config.ServiceURL = _s3ServiceUrl;

            _client = new AmazonS3Client(_accessKey, _secretAccessKey, config);

            var deleteObjectRequest = new DeleteObjectRequest
            {
                BucketName = _bucketName,
                Key = perfilFile.Name,
            };

            Console.WriteLine("Deleting an object");
            await _client.DeleteObjectAsync(deleteObjectRequest);
            return true;
        }
        catch
        {
            return false;
        }
    }
}