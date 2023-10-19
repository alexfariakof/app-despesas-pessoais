using despesas_backend_api_net_core.Infrastructure.Security.Implementation;
using despesas_backend_api_net_core.Infrastructure.Security;
using Microsoft.Extensions.Configuration;
using Xunit.Extensions.Ordering;
using System.Reflection;
using MySqlX.XDevAPI.Common;
using Moq;
using System;

namespace Test.XUnit.Infrastructure.Security.Implementation
{
    [Order(20)]
    public class AmazonS3BucketTest
    {
        private IAmazonS3Bucket _amazonS3Bucket;
        private readonly string _accessKey = null;
        private readonly string _secretAccessKey  = null;
        private readonly string _s3ServiceUrl = null;
        private readonly string _bucketName = null;
        private readonly string _regionEndpoint = null;

        public AmazonS3BucketTest()
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(AppContext.BaseDirectory)
                .AddJsonFile("appsettings.json")
                .Build();

            _amazonS3Bucket =  AmazonS3Bucket.GetInstance;
            _accessKey = configuration.GetSection("AmazonS3Bucket:accessKey").Value;
            _secretAccessKey = configuration.GetSection("AmazonS3Bucket:secretAccessKey").Value;
            _s3ServiceUrl = configuration.GetSection("AmazonS3Bucket:s3ServiceUrl").Value;
            _bucketName = configuration.GetSection("AmazonS3Bucket:bucketName").Value;
            _regionEndpoint = configuration.GetSection("AmazonS3Bucket:regionEndpoint").Value;
            _amazonS3Bucket.SetConfiguration(_accessKey, _secretAccessKey, _s3ServiceUrl, _bucketName);
        }


        [Fact, Order(1)]
        public void Should_Returns_Instance_Of_IAmazonS3Bucket()
        {

            // Assert
            Assert.NotNull(_amazonS3Bucket);
            Assert.IsAssignableFrom<IAmazonS3Bucket>(_amazonS3Bucket);
        }

        [Fact, Order(2)]
        public void Should_SetConfiguration()
        {
            // Arrange
            var amazonS3BucketType = typeof(AmazonS3Bucket);
            var accessKeyProperty = amazonS3BucketType.GetField("AccessKey", BindingFlags.NonPublic | BindingFlags.Instance);
            var secretAccessKeyField = amazonS3BucketType.GetField("SecretAccessKey", BindingFlags.NonPublic | BindingFlags.Instance);
            var s3ServiceUrlField = amazonS3BucketType.GetField("S3ServiceUrl", BindingFlags.NonPublic | BindingFlags.Instance);
            var bucketNameField = amazonS3BucketType.GetField("BucketName", BindingFlags.NonPublic | BindingFlags.Instance);

            // Act
            var accessKey = (string)accessKeyProperty.GetValue(_amazonS3Bucket);
            var secretAccessKey = (string)secretAccessKeyField.GetValue(_amazonS3Bucket);
            var s3ServiceUrl = (string)s3ServiceUrlField.GetValue(_amazonS3Bucket);
            var bucketName = (string)bucketNameField.GetValue(_amazonS3Bucket);

            // Assert
            Assert.Equal(_accessKey, accessKey);
            Assert.Equal(_secretAccessKey, secretAccessKey);
            Assert.Equal(_s3ServiceUrl, s3ServiceUrl);
            Assert.Equal(_bucketName, bucketName);
        }

        [Fact, Order(3)]
        public async Task WritingAnObjectAsync_Should_Return_Url()
        {
            // Arrange
            var perfilFile = new ImagemPerfilUsuarioVM
            {
                Name = "test-image.jpg",
                ContentType = "image/jpeg",
              //  Arquivo = new byte[] { 0x01, 0x02, 0x03 } // Sample image data
            };
            var mockAmazonS3Bucket = new Mock<IAmazonS3Bucket>(MockBehavior.Strict);
            mockAmazonS3Bucket.Setup(s => s.WritingAnObjectAsync(perfilFile)).ReturnsAsync($"https://{_bucketName}.s3.amazonaws.com/{perfilFile.Name}");

            // Act
            var url = await mockAmazonS3Bucket.Object.WritingAnObjectAsync(perfilFile);

            // Assert
            Assert.NotNull(url);
            Assert.StartsWith($"https://{_bucketName}.s3.amazonaws.com/", url);
        }

        [Fact, Order(4)]
        public async Task WritingAnObjectAsync_Should_Throws_Exception()
        {
            // Arrange
            var perfilFile = new ImagemPerfilUsuarioVM
            {
                Name = "test-image.jpg",
                ContentType = "image/jpeg",
                //Arquivo = new byte[] { 0x01, 0x02, 0x03 } // Sample image data
            };
            // Act & Assert
            var exception = await Assert.ThrowsAsync<Exception>(async () => await _amazonS3Bucket.WritingAnObjectAsync(perfilFile));

            Assert.NotNull(exception);
            Assert.IsType<Exception>(exception);
            Assert.Equal("AmazonS3Bucket_WritingAnObjectAsync_Errro ", exception.Message);

        }

        [Fact, Order(5)]
        public async Task DeleteObjectNonVersionedBucketAsync_Should_Return_True()
        {
            // Arrange
            var perfilFile = new ImagemPerfilUsuarioVM
            {
                Name = "test-image.jpg"
            };
            var mockAmazonS3Bucket = new Mock<IAmazonS3Bucket>(MockBehavior.Strict);

            mockAmazonS3Bucket.Setup(s => s.DeleteObjectNonVersionedBucketAsync(perfilFile)).ReturnsAsync(true);

            // Act
            var result = await mockAmazonS3Bucket.Object.DeleteObjectNonVersionedBucketAsync(perfilFile);

            // Assert
            Assert.True(result);
        }

        [Fact, Order(6)]
        public async Task DeleteObjectNonVersionedBucketAsync_Should_Throws_Erro_And_Return_False()
        {
            // Arrange
            var perfilFile = new ImagemPerfilUsuarioVM
            {
                Name = "non-existing-file.jpg"
            };

            // Act
            var result = await _amazonS3Bucket.DeleteObjectNonVersionedBucketAsync(perfilFile);

            // Assert
            Assert.False(result);
        }

    }
}
