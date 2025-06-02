using Despesas.Infrastructure.Amazon;
using Despesas.Infrastructure.Amazon.Abstractions;

namespace Despesas.WebApi.CommonDependenceInject;
public static class AmazonS3BucketDependenceInject
{

    public static void AddAmazonS3BucketConfigurations(this WebApplicationBuilder builder)
    {
        builder.Services.Configure<AmazonS3Options>(builder.Configuration.GetSection("AmazonS3Configurations"));
        builder.Services.AddSingleton<IAmazonS3Bucket, AmazonS3Bucket>();
    }
}