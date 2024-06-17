namespace Despesas.Infrastructure.Amazon;
public  class AmazonS3Options
{
    public string? AccessKey {  get; set; }
    public string? SecretAccessKey { get; set; }
    public string? S3ServiceUrl { get; set; }
    public string? BucketName { get; set; }

}
