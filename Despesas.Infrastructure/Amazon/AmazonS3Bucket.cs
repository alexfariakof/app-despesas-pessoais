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
    private IAmazonS3? _client;
    private readonly S3CannedACL _fileCannedACL = S3CannedACL.PublicRead;
    private readonly RegionEndpoint _bucketRegion = RegionEndpoint.SAEast1;    
    private readonly string? _accessKey;
    private readonly string? _secretAccessKey;
    private readonly string? _s3ServiceUrl;
    private readonly string? _bucketName;

    private AmazonS3Bucket()
    {
        var jsonFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "appsettings.json");

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
            AmazonS3Config config = new AmazonS3Config();
            config.ServiceURL = _s3ServiceUrl;

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
            var url = $"https://{_bucketName}.s3.amazonaws.com/{perfilFile.Name}";
            return url;

        }
        catch
        {
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